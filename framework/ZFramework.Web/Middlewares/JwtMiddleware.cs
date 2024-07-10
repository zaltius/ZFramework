using Microsoft.AspNetCore.Http;
using ZFramework.Common.Authentication;
using System.IdentityModel.Tokens.Jwt;

namespace ZUserPreferences.API.Middelwares
{
    /// <summary>
    /// Middleware to intercept authentication-related info.
    /// </summary>
    public class JwtMiddleware : JwtMiddleware<ICurrentUser, ICurrentRequest>
    {
        /// <summary>
        /// Creates an instance of this class.
        /// </summary>
        /// <param name="next">A function that can process an HTTP request.</param>
        public JwtMiddleware(RequestDelegate next)
            : base(next)
        {
        }

        protected override ICurrentUser CreateCurrentUser()
        {
            return new CurrentUser();
        }
    }

    public abstract class JwtMiddleware<TCurrentUser, TCurrentRequest>
        where TCurrentUser : ICurrentUser
        where TCurrentRequest : ICurrentRequest<TCurrentUser>

    {
        /// <summary>
        /// A function that can process an HTTP request.
        /// </summary>
        protected readonly RequestDelegate Next;

        /// <summary>
        /// Creates an instance of this class.
        /// </summary>
        /// <param name="next">A function that can process an HTTP request.</param>
        protected JwtMiddleware(RequestDelegate next)
        {
            Next = next;
        }

        /// <summary>
        /// Gets logged user info, if any and processes the next request in the pipeline.
        /// </summary>
        /// <param name="httpContext">Encapsulates all HTTP-specific information about an individual HTTP request.</param>
        /// <param name="currentRequest"></param>
        public virtual async Task Invoke(HttpContext httpContext, TCurrentRequest currentRequest)
        {
            currentRequest.CurrentUser ??= CreateCurrentUser();
            currentRequest.IsEnabled = true;
            currentRequest.RemoteIpAddress = httpContext.Connection.RemoteIpAddress.ToString();
            currentRequest.RequestId = httpContext.TraceIdentifier;

            JwtSecurityTokenHandler handler = new();
            var jwt = httpContext?.Request?.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            try
            {
                JwtSecurityToken token = handler.ReadJwtToken(jwt);
                currentRequest.CurrentUser.Id = token.Subject;
            }
            catch (Exception)
            {
            }

            await Next(httpContext);
        }

        protected abstract TCurrentUser CreateCurrentUser();
    }

}
