using AdminCreateUserDemo.Data;
using Microsoft.AspNetCore.Identity;

namespace AdminCreateUserDemo.Middleware
{
    public sealed class ForceChangePasswordMiddleware
    {
        private readonly RequestDelegate _next;

        public ForceChangePasswordMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserManager<ApplicationUser> userManager)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var user = await userManager.GetUserAsync(context.User);
                if (user is not null && user.PasswordHash == user.SecurityStamp)
                {
                    if (!context.Request.Path.Value.Contains("/Account/ChangePassword")) 
                    {
                        context.Response.Redirect("/Account/ChangePassword");
                        return;
                    }
                }
            }

            await _next(context);
        }
    }
}
