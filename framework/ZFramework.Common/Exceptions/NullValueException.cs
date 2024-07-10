namespace ZFramework.Common.Exceptions
{
    /// <summary>
    /// The exception that is thrown when there is an attempt to dereference a null object reference.
    /// </summary>
    public class NullValueException : NullReferenceException, IException
    {
        /// <summary>
        /// Builds an instance of this class.
        /// </summary>
        /// <param name="message"> A <see cref="string"/> that describes the error. The content of message is intended
        /// to be understood by humans. The caller of this constructor is required to ensure
        /// that this string has been localized for the current system culture.</param>
        public NullValueException(string message) : base(message)
        {
            HResult = 50;
        }

        /// <summary>
        /// Builds an instance of this class with the provided inner exception.
        /// </summary>
        /// <param name="message"> A <see cref="string"/> that describes the error. The content of message is intended
        /// to be understood by humans. The caller of this constructor is required to ensure
        /// that this string has been localized for the current system culture.</param>
        /// <param name="innerException"> The exception that is the cause of the current exception. If the innerException
        /// parameter is not null, the current exception is raised in a catch block that handles the inner exception.</param>
        public NullValueException(string message, Exception innerException)
            : base(message, innerException)
        {
            HResult = 50;
        }

        /// <summary>
        /// Builds an instance of this class.
        /// </summary>
        public NullValueException()
        {
            HResult = 50;
        }
    }
}