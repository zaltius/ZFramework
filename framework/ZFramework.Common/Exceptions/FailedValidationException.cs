namespace ZFramework.Common.Exceptions
{
    /// <summary>
    /// The exception that is thrown when there is one or more validation errors.
    /// </summary>
    /// </summary>
    public class FailedValidationException : Exception, IFailedValidationException
    {
        /// <summary>
        /// Builds an instance of this class.
        /// </summary>
        /// <param name="message"> A <see cref="String"/> that describes the error. The content of message is intended
        /// to be understood by humans. The caller of this constructor is required to ensure
        /// that this string has been localized for the current system culture.</param>
        public FailedValidationException(string message) : base(message)
        {
            HResult = 40;
        }

        /// <summary>
        /// Builds an instance of this class.
        /// </summary>
        /// <param name="message"> A <see cref="String"/> that describes the error. The content of message is intended
        /// to be understood by humans. The caller of this constructor is required to ensure
        /// that this string has been localized for the current system culture.</param>
        /// <param name="innerException"> The exception that is the cause of the current exception. If the innerException
        /// parameter is not null, the current exception is raised in a catch block that handles the inner exception.</param>
        public FailedValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
            HResult = 40;
        }

        /// <summary>
        /// Builds an instance of this class.
        /// </summary>
        public FailedValidationException()
        {
            HResult = 40;
        }
    }
}