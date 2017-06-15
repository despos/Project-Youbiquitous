///////////////////////////////////////////////////////////////////
//
// Youbiquitous.MVC v1.0
// Author: Dino Esposito
//

using System;
using System.Web;

namespace Expoware.Youbiquitous.Mvc.Extensions
{
    public static class RequestExtensions
    {
        /// <summary>
        /// Checks whether the current request is coming via AJAX
        /// </summary>
        /// <param name="request">ASP.NET Request object</param>
        /// <returns>True or False</returns>
        public static bool IsAjaxRequest(this HttpRequestBase request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            if (request["X-Requested-With"] == "XMLHttpRequest")
                return true;
            if (request.Headers != null)
                return request.Headers["X-Requested-With"] == "XMLHttpRequest";
            return false;
        }
    }
}