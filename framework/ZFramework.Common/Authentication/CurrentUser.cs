namespace ZFramework.Common.Authentication
{
    /// <summary>
    /// Implementation of <see cref="ICurrentUser"/>
    /// </summary>
    public class CurrentUser : ICurrentUser
    {
        ///
        /// <inheritdoc/>
        ///
        public string Id { get; set; }
    }
}
