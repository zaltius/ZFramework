namespace ZFramework.Common.Authentication
{

    /// <summary>
    /// Represents the current logged user.
    /// </summary>
    public interface ICurrentUser
    {
        /// <summary>
        /// Represents the user's id.
        /// </summary>
        public string Id { get; set; }
    }
}
