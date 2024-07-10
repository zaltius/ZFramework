namespace ZFramework.Common.Exceptions
{
    /// <summary>
    /// The exception that is thrown when there is an attempt to dereference a null entity reference.
    /// </summary>
    public class NullEntityException : NullReferenceException, IException
    {
        /// <summary>
        /// Builds an instance of this class.
        /// </summary>
        /// <param name="message"> A <see cref="string"/> that describes the error. The content of message is intended
        /// to be understood by humans. The caller of this constructor is required to ensure
        /// that this string has been localized for the current system culture.</param>
        public NullEntityException(string message) : base(message)
        {
            HResult = 60;
        }

        /// <summary>
        /// Builds an instance of this class with the provided inner exception.
        /// </summary>
        /// <param name="message"> A <see cref="string"/> that describes the error. The content of message is intended
        /// to be understood by humans. The caller of this constructor is required to ensure
        /// that this string has been localized for the current system culture.</param>
        /// <param name="innerException"> The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public NullEntityException(string message, Exception innerException)
            : base(message, innerException)
        {
            HResult = 60;
        }

        /// <summary>
        /// Builds an instance of this class.
        /// </summary>
        public NullEntityException()
        {
            HResult = 60;
        }
    }
}