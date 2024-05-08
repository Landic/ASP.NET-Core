using Сonveyor.Middleware;

namespace Сonveyor.Extensions
{
    public static class FromThousandToHundredThousand
    {
        public static IApplicationBuilder UseFromThousandToHundredThousand(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FromThousandToHundredThousandMiddleware>();
        }
    }
}
