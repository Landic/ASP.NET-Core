using Сonveyor.Middleware;

namespace Сonveyor.Extensions
{
    public static class FromOneToTen
    {
        public static IApplicationBuilder UseFromOneToTen(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FromOneToTenMiddleware>();
        }
    }
}
