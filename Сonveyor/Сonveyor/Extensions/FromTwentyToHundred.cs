using Сonveyor.Middleware;

namespace Сonveyor.Extensions
{
    public static class FromTwentyToHundred
    {
        public static IApplicationBuilder UseFromTwentyToHundred(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FromTwentyToHundredMiddleware>();
        }
    }
}
