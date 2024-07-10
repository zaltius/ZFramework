namespace ZFramework.Common.Exceptions
{
    /// <summary>
    /// The exception that is thrown when there is an attempt to update an old version of an already modified record.
    /// </summary>
    public class ConcurrentUpdateException : InvalidOperationException, IException
    {
        /// <summary>
        /// Builds an instance of this class.
        /// </summary>
        /// <param name="message"> A <see cref="String"/> that describes the error. The content of message is intended
        /// to be understood by humans. The caller of this constructor is required to ensure
        /// that this string has been localized for the current system culture.</param>
        public ConcurrentUpdateException(string message) : base(message)
        {
            HResult = 30;
        }

        /// <summary>
        /// Builds an instance of this class with the provided inner exception.
        /// </summary>
        /// <param name="message"> A <see cref="String"/> that describes the error. The content of message is intended
        /// to be understood by humans. The caller of this constructor is required to ensure
        /// that this string has been localized for the current system culture.</param>
        /// <param name="innerException"> The exception that is the cause of the current exception. If the innerException
        /// parameter is not null, the current exception is raised in a catch block that handles the inner exception.</param>
        public ConcurrentUpdateException(string message, Exception innerException)
            : base(message, innerException)
        {
            HResult = 30;
        }

        /// <summary>
        /// Builds an instance of this class.
        /// </summary>
        public ConcurrentUpdateException()
        {
            HResult = 30;
        }
    }
}