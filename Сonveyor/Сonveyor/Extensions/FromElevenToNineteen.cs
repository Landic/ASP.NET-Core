using Сonveyor.Middleware;

namespace Сonveyor.Extensions
{
    public static class FromElevenToNineteen
    {
        public static IApplicationBuilder UseFromElevenToNineteen(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FromElevenToNineteenMiddleware>();
        }
    }
}
