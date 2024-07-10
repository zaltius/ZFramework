using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ZFramework.Common.Authentication;
using ZFramework.Infrastructure.EntityFrameworkCore.Auditing;
using ZFramework.Infrastructure.EntityFrameworkCore.Filtering;
using System.Reflection;

namespace ZFramework.Infrastructure.EntityFrameworkCore
{
    /// <summary>
    /// Context that provides useful functionality such as automatic filters and auditing property check and set.
    /// </summary>
    public abstract class EfCoreDbContext : DbContext
    {
        /// <summary>
        /// Defines a mediator to publish domain events.
        /// </summary>
        protected readonly IMediator? Mediator;

        /// <summary>
        /// Defines the current request.
        /// </summary>
        protected readonly ICurrentRequest? CurrentRequest;

        private static readonly MethodInfo ConfigureGlobalFiltersMethodInfo
           = typeof(EfCoreDbContext).GetMethod(nameof(ConfigureGlobalFilters), BindingFlags.Instance | BindingFlags.NonPublic);

        /// <summary>
        /// Defines if entities which implement <see cref=" Domain.Entities.ISoftDelete"/> should not be included in queries if they are deleted.
        /// </summary>
        protected virtual bool IsSoftDeleteFilterEnabled { get; set; } = true;

        /// <summary>
        /// Creates an instance of this class.
        /// </summary>
        protected EfCoreDbContext()
        {
        }

        /// <summary>
        /// Constructor to configure the context. Should be always called.
        /// </summary>
        /// <param name="options">The configuration for this context</param>
        protected EfCoreDbContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// Constructor to configure the context. Should be always called.
        /// </summary>
        /// <param name="options">The configuration for this context</param>
        /// <param name="mediator">Mediator to publish domain events.</param>
        protected EfCoreDbContext(DbContextOptions options, IMediator mediator) : base(options)
        {
            Mediator = mediator;
        }

        /// <summary>
        /// Constructor to configure the context. Should be always called.
        /// </summary>
        /// <param name="options">The configuration for this context</param>
        /// <param name="mediator">Mediator to publish domain events.</param>
        /// <param name="currentRequest">Contract used to trace current request.</param>
        protected EfCoreDbContext(DbContextOptions options, IMediator mediator, ICurrentRequest currentRequest) : base(options)
        {
            Mediator = mediator;
            CurrentRequest = currentRequest;
        }

        /// <summary>
        /// Constructor to configure the context. Should be always called.
        /// </summary>
        /// <param name="options">The configuration for this context</param>
        /// <param name="currentRequest">Contract used to trace current request.</param>
        protected EfCoreDbContext(DbContextOptions options, ICurrentRequest currentRequest) : base(options)
        {
            CurrentRequest = currentRequest;
        }

        /// <summary>
        /// Cheks and sets auditing properties for all audit-enabled properties with pending changes in the context.
        ///
        /// Saves all changes made in this context to the database.
        /// This method will automatically call Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges
        /// to discover any changes to entity instances before saving to the underlying database.
        /// This can be disabled via Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        public override int SaveChanges()
        {
            PerformAuditOperations();

            // TODO: Have a new property in IDomainEvent to determine if an event should execute before or after SaveChanges().

            // Events dispatched before save changes mustnt' call to UnitOfWork.Commit().
            // All changes will committed as an atomic operation with the next line base.SaveChanges().
            //Mediator.DispatchDomainEvents(this);

            var result = base.SaveChanges();

            // Events dispatched after save changes must call to UnitOfWork.Commit() to enseure all the changes are saved.
            Mediator.DispatchDomainEvents(this);

            return result;
        }

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// This method will automatically call Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges
        /// to discover any changes to entity instances before saving to the underlying database.
        /// This can be disabled via Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled.
        /// Multiple active operations on the same context instance are not supported. Use
        /// 'await' to ensure that any asynchronous operations have completed before calling
        /// another method on this context.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">Indicates whether Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AcceptAllChanges
        /// is called after the changes have been sent successfully to the database.</param>
        /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            PerformAuditOperations();

            await Mediator.DispatchDomainEventsAsync(this);

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken); ;
        }

        /// <summary>
        /// Configures the global filters for all entities and applies configuration from all <see cref="IEntityTypeConfiguration{TEntity}"/>
        /// instances that are defined in the calling assembly.
        ///
        /// Override this method to further configure the model that was discovered by convention
        /// from the entity types exposed in <see cref="DbSet{TEntity}"/> properties
        /// on your derived context. The resulting model may be cached and re-used for subsequent
        /// instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and
        /// other extensions) typically define extension methods on this object that allow
        /// you to configure aspects of the model that are specific to a given database.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetCallingAssembly());

            base.OnModelCreating(modelBuilder);

            SetGlobalFilters(modelBuilder);
        }

        #region Auditing

        /// <summary>
        /// Performs all audit-related opperations to all entities with pendig changes.
        /// </summary>
        protected void PerformAuditOperations()
        {
            SetAuditProperties();
        }

        /// <summary>
        /// Checks and sets the auditing properties por every audit-enabled entity with pending changes.
        /// </summary>
        protected void SetAuditProperties()
        {
            EfCoreAuditPropertiesHelper.ApplyChangesForAuditProperties(ChangeTracker.Entries().ToList(), CurrentRequest?.CurrentUser);
        }

        #endregion Auditing

        #region Filtering

        /// <summary>
        /// Configures the global filters.
        /// </summary>
        /// <typeparam name="TEntity">Represents an entity to configure the filters to.</typeparam>
        /// <param name="modelBuilder">Model builder ot the context.</param>
        /// <param name="entityType">Represents the entity in the context's Microsoft.EntityFrameworkCore.Metadata.IMutableModel.</param>
        protected void ConfigureGlobalFilters<TEntity>(ModelBuilder modelBuilder, IMutableEntityType entityType)
            where TEntity : class
        {
            if (entityType.BaseType != null || !GlobalFiltersHelper.ShouldFilterEntity<TEntity>())
            {
                return;
            }

            var filterExpression = GlobalFiltersHelper.CreateFilterExpression<TEntity>(IsSoftDeleteFilterEnabled);

            if (filterExpression != null)
            {
                modelBuilder.Entity<TEntity>().HasQueryFilter(filterExpression);
            }
        }

        private void SetGlobalFilters(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                ConfigureGlobalFiltersMethodInfo
                    .MakeGenericMethod(entityType.ClrType)
                    .Invoke(this, new object[] { modelBuilder, entityType });
            }
        }

        #endregion Filtering
    }
}