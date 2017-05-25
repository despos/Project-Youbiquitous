///////////////////////////////////////////////////////////////////
//
// Youbiquitous.MVC v1.0
// Author: Dino Esposito
//

using System;
using System.Web.Mvc;

namespace Expoware.Youbiquitous.Mvc.Filters
{
    public class AddHeaderAttribute : ActionFilterAttribute
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext == null)
                return;

            if (!string.IsNullOrEmpty(Name) && !String.IsNullOrEmpty(Value))
                filterContext.RequestContext.HttpContext.Response.AddHeader(Name, Value);
            return;
        }
    }
}