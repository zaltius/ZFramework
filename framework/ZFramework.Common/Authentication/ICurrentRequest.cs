namespace ZFramework.Common.Authentication
{
    /// <summary>
    /// Represents the current HTTP request.
    /// </summary>
    public interface ICurrentRequest : ICurrentRequest<ICurrentUser>
    {
    }

    /// <summary>
    /// Represents the current HTTP request.
    /// </summary>
    public interface ICurrentRequest<TCurrentUser>
        where TCurrentUser : ICurrentUser
    {
        /// <summary>
        /// Represents the current logged user, if any.
        /// </summary>
        public TCurrentUser CurrentUser { get; set; }

        /// <summary>
        /// Indicates wether there is an ongoing HTTP request.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Remote IP address the request comes from.
        /// </summary>
        public string RemoteIpAddress { get; set; }

        /// <summary>
        /// Current request's id.
        /// </summary>
        public string RequestId { get; set; }
    }
}
