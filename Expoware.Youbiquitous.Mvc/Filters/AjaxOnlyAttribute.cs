///////////////////////////////////////////////////////////////////
//
// Youbiquitous.MVC v1.0
// Author: Dino Esposito
//

using System.Reflection;
using System.Web.Mvc;

namespace Expoware.Youbiquitous.Mvc.Filters
{
    public class AjaxOnlyAttribute : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            return controllerContext.HttpContext.Request.IsAjaxRequest();
        }
    }
}