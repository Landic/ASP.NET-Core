using Microsoft.AspNetCore.Mvc.Filters;
using MusicPortal.BLL.Services;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace MusicPortal.Infrastructure
{
    public class CultureAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context) { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string? cultureName = null;
            var cultureCookie = context.HttpContext.Request.Cookies["lang"];
            if (cultureCookie != null)
            {
                cultureName = cultureCookie;
            }
            else
            {
                cultureName = "ru";
            }

            List<string> cultures = context.HttpContext.RequestServices.GetRequiredService<ILangService>().languageList().Select(t => t.ShortName).ToList()!;
            if (!cultures.Contains(cultureName))
            {
                cultureName = "ru";
            }

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName);
        }
    }
}
