///////////////////////////////////////////////////////////////////
//
// Youbiquitous.MVC v1.0
// Author: Dino Esposito
//

using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Expoware.Youbiquitous.Mvc.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CultureAttribute : ActionFilterAttribute
    {
        private const String CookieLangEntry = "language";

        public String Name { get; set; }
        public static String CookieName => "_Culture";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var culture = Name;
            if (String.IsNullOrEmpty(culture))
                culture = GetSavedCultureOrDefault(filterContext.RequestContext.HttpContext.Request);

            // Set culture on current thread
            SetCultureOnThread(culture);

            // Proceed as usual
            base.OnActionExecuting(filterContext);
        }

        public static void SavePreferredCulture(HttpResponseBase response, String language,
            Int32 expireDays = 1)
        {
            var cookie = new HttpCookie(CookieName) { Expires = DateTime.UtcNow.AddDays(expireDays) };
            cookie.Values[CookieLangEntry] = language;
            response.SetCookie(cookie);
        }

        public static String GetSavedCultureOrDefault(HttpRequestBase httpRequestBase)
        {
            var culture = Thread.CurrentThread.CurrentCulture.Name;
            var cookie = httpRequestBase.Cookies[CookieName];
            if (cookie != null)
            {
                var cultureInCookie = cookie.Values[CookieLangEntry];
                culture = cultureInCookie ?? culture;
            }
            return culture;
        }

        private static void SetCultureOnThread(String language)
        {
            var cultureInfo = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }
    }
}