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

        public string Name { get; set; }
        public static string CookieName => "_YBQ_Culture_";

        /// <summary>
        /// ASP.NET MVC preliminary step in any controller method
        /// </summary>
        /// <param name="filterContext">Execution context of the filter</param>
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

        /// <summary>
        /// Saves the specified language to the culture cookie
        /// </summary>
        /// <param name="response">HTTP response</param>
        /// <param name="language">Code for the language</param>
        /// <param name="expireDays">Expires in given number of days</param>
        public static void SavePreferredCulture(HttpResponseBase response, 
            string language, int expireDays = 1)
        {
            var cookie = new HttpCookie(CookieName) { Expires = DateTime.UtcNow.AddDays(expireDays) };
            cookie.Values[CookieLangEntry] = language;
            response.SetCookie(cookie);
        }

        /// <summary>
        /// Read the current language in the culture cookie
        /// </summary>
        /// <param name="httpRequestBase">HTTP request</param>
        /// <returns>String</returns>
        public static string GetSavedCultureOrDefault(HttpRequestBase httpRequestBase)
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

        /// <summary>
        /// Registers the Culture attribute globally
        /// </summary>
        public static void Register()
        {
            GlobalFilters.Filters.Add(new CultureAttribute());
        }

        #region PRIVATE
        private static void SetCultureOnThread(string language)
        {
            var cultureInfo = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }
        #endregion
    }
}