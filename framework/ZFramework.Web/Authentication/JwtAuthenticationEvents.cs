using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace ZFramework.Web.Authentication
{
    public class JwtAuthenticationEvents : JwtBearerEvents
    {
        /// <summary>
        /// Invoked if exceptions are thrown during request processing. The exceptions will be re-thrown after this event unless suppressed.
        /// Adds a response header indicating the token expired if that's the reason of failed authentication.
        /// </summary>
        /// <param name="context">A <see cref="AuthenticationFailedContext"/> when authentication has failed.</param>
        public override Task AuthenticationFailed(AuthenticationFailedContext context)
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Add("Token-Expired", "true");
            }

            return base.AuthenticationFailed(context);
        }
    }

}
