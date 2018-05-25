///////////////////////////////////////////////////////////////////
//
// Youbiquitous.MVC v1.0
// Author: Dino Esposito
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Expoware.Youbiquitous.Mvc.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RequireReferrerAttribute : ActionMethodSelectorAttribute
    {
        public RequireReferrerAttribute(params string[] trustedServers)
        {
            TrustedServers = trustedServers;
        }

        /// <summary>
        /// Array of servers acceptable as referrers
        /// </summary>
        public string[] TrustedServers { get; }

        /// <summary>
        /// Determines if the action method is valid for the request
        /// </summary>
        /// <param name="controllerContext">Controller context</param>
        /// <param name="methodInfo">Descriptor of the method being called</param>
        /// <returns></returns>
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            var referrer = controllerContext.HttpContext.Request.UrlReferrer;
            if (referrer == null)
                return false;
            var list = new List<string>(TrustedServers);
            var url = referrer.AbsoluteUri.ToLower();
            return list.Any(ts => url.StartsWith(ts.ToLower()));
        }      
    }
}