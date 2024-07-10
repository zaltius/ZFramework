namespace ZFramework.Common.Authentication
{
    /// <summary>
    /// Implementation of <see cref="ICurrentRequest"/>
    /// </summary>
    public class CurrentRequest : CurrentRequest<ICurrentUser>, ICurrentRequest
    {
        /// <summary>
        /// Builds an instance of this class.
        /// </summary>
        public CurrentRequest()
        {
            CurrentUser = new CurrentUser();
        }
    }

    /// <summary>
    /// Implementation of <see cref="ICurrentRequest"/>
    /// </summary>
    public class CurrentRequest<TCurrentUser> : ICurrentRequest<TCurrentUser>
        where TCurrentUser : ICurrentUser
    {
        ///
        /// <inheritdoc/>
        ///
        public TCurrentUser CurrentUser { get; set; }

        ///
        /// <inheritdoc/>
        ///
        public bool IsEnabled { get; set; }

        ///
        /// <inheritdoc/>
        ///
        public string RemoteIpAddress { get; set; }

        ///
        /// <inheritdoc/>
        ///
        public string RequestId { get; set; }
    }
}
