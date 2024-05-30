namespace AdminCreateUserDemo.Middleware
{
    public static class ForceChangePasswordMiddlewareExtensions
    {
        public static IApplicationBuilder UseForceChangePassword(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ForceChangePasswordMiddleware>();
        }
    }
}
