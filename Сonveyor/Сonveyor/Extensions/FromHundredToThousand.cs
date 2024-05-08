using Сonveyor.Middleware;

namespace Сonveyor.Extensions
{
    public static class FromHundredToThousand
    {
        public static IApplicationBuilder UseFromHundredToThousand(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FromHundredToThousandMiddleware>();
        }
    }
}
