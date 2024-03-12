using ManageFinances.Helpers;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
    
namespace ManageFinances.Middlewares
{
    public class JwtMiddleware : IMiddleware
    {
        private readonly JwtSecurityTokenHandlerWrapper _jwtSecurityTokenHandler;

        public JwtMiddleware(JwtSecurityTokenHandlerWrapper jwtSecurityTokenHandler)
        {
            _jwtSecurityTokenHandler = jwtSecurityTokenHandler;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!token.IsNullOrEmpty()) {
                try
                {
                    var claimsPrincipal = _jwtSecurityTokenHandler.ValidateJwtToken(token);

                    var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                    context.Items["UserId"] = userId;

                    if (context.Request.Path.ToString().StartsWith("/auth/sign"))
                    {
                        context.Response.Redirect("/");
                    }
                }
                catch (Exception exception)
                {
                    if(exception is SecurityTokenExpiredException)
                    {
                        context.Response.Cookies.Delete("access_token");
                    }
                    // If the token is invalid, throw an exception
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                }
            }
            // Continue processing the request
            await next(context);
        }
    }
}
