using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ZFramework.Common.Exceptions;
using ZFramework.Common.Http.Responses;
using System.Text;

namespace ZFramework.Web.Middlewares
{
    /// <summary>
    /// Middleware to intercept web application pipeline to serve standarized responses for every request.
    /// </summary>
    public class StandardHttpResponseMiddleware
    {
        /// <summary>
        /// Specifies the settings on the <see cref="JsonSerializer"/> object used to serialize responses.
        /// </summary>
        protected readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        /// <summary>
        /// Represents a type used to perform logging.
        /// </summary>
        protected readonly ILogger<StandardHttpResponseMiddleware> Logger;

        /// <summary>
        /// A function that can process an HTTP request.
        /// </summary>
        protected readonly RequestDelegate Next;

        /// <summary>
        /// Creates an instance of this class.
        /// </summary>
        /// <param name="next">A function that can process an HTTP request.</param>
        /// <param name="logger">Represents a type used to perform logging in this service.</param>
        public StandardHttpResponseMiddleware(RequestDelegate next, ILogger<StandardHttpResponseMiddleware> logger)
        {
            Logger = logger;
            Next = next;
        }

        /// <summary>
        /// Processes the next request in the pipeline and wraps the final result.
        /// </summary>
        /// <param name="httpContext">Encapsulates all HTTP-specific information about an individual HTTP request.</param>
        /// <returns></returns>
        public virtual async Task InvokeAsync(HttpContext httpContext)
        {
            Stream originBody;

            try
            {
                await Next(httpContext);
            }
            catch (Exception ex)
            {
                originBody = ReplaceBody(httpContext.Response);

                httpContext.Response.Body.Seek(0, SeekOrigin.Begin);

                var content = WrapFailedResult(httpContext, ex);

                await HttpResponseWritingExtensions.WriteAsync(httpContext.Response, content, Encoding.UTF8);

                await ReturnBody(httpContext.Response, originBody);
            }
        }

        /// <summary>
        /// Wraps the failed context response result in a standarized wrapper.
        /// </summary>
        /// <param name="httpContext">Body of the context response.</param>
        /// <param name="exception">Catched exception that produced the errors.</param>
        /// <returns></returns>
        protected virtual string WrapFailedResult(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";

            var result = new HttpErrorResponse()
            {
                Code = exception.HResult,
                Errors = new List<string>() { exception.Message }
            };

            if (exception is IFailedValidationException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                result.Title = "One or more validation errors occurred.";
            }
            else if (exception is IException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status424FailedDependency;
                result.Title = "A handled exception occurred.";
            }
            else
            {
                result.Title = "An unhandled exception occurred.";
            }

            foreach (var item in exception.Data.Values)
            {
                result.Errors.Add(item.ToString());
            }

            Logger.LogError(exception, result.Title);

            return JsonConvert.SerializeObject(result, _jsonSerializerSettings);
        }

        private Stream ReplaceBody(HttpResponse response)
        {
            var originBody = response.Body;

            response.Body = new MemoryStream();

            return originBody;
        }

        private async Task ReturnBody(HttpResponse response, Stream originBody)
        {
            response.Body.Seek(0, SeekOrigin.Begin);

            await response.Body.CopyToAsync(originBody);

            response.Body = originBody;
        }
    }
}