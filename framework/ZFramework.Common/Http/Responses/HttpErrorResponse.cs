namespace ZFramework.Common.Http.Responses
{
    /// <summary>
    /// Standard response for failed HTTP/S calls.
    /// </summary>
    public class HttpErrorResponse
    {
        /// <summary>
        /// Exception error code.
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Collection of errors produced during the execution.
        /// </summary>
        public IList<string> Errors { get; set; }

        /// <summary>
        /// Description of the errors produced.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Creates an instance of this class.
        /// </summary>
        public HttpErrorResponse()
        {
            Errors = new List<string>();
        }
    }
}