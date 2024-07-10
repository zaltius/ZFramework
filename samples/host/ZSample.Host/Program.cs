using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ZFramework.Web.Extensions;
using ZSample.Infrastructure;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddSerilog()
    .AddDomainDependencies(new List<Assembly>() { builder.Configuration.GetDomainAssembly() })
    .AddRepositoriesByConvention(new List<Assembly>() { builder.Configuration.GetInfrastructureAssembly() })
    .AddTransactionalDbContext<ApplicationDbContext>(o => o.UsePostgreSqlPersistence(
        builder.Configuration.GetConnectionString("ApplicationDbContext:PostgreSqlConnectionString")))
    .AddApplicationDependencies(new List<Assembly>() { builder.Configuration.GetApplicationAssembly() })
    .AddCors();

builder.Services.AddControllers(opt => opt.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>())
    .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithEnvironmentName(builder.Configuration);

//Configure Middelwares.
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging() || app.Environment.IsProduction() || app.Environment.EnvironmentName == "LOCAL")
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
        {
            c.EnableFilter();
            c.SwaggerEndpoint("/swagger/v1/swagger.json", $"Sample API (v{Assembly.GetEntryAssembly().GetName().Version})");
            c.DocExpansion(DocExpansion.None);
        });
}

app.UseCorsAllowAny();

app.UseStandardHttpResponses();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();