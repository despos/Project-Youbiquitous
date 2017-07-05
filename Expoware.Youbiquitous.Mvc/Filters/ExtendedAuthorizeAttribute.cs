///////////////////////////////////////////////////////////////////
//
// Youbiquitous YBQ : app starter 
// Copyright (c) Youbiquitous srls 2017
//
// Author: Dino Esposito (http://youbiquitous.net)
//

using System;
using System.Web.Mvc;

namespace Expoware.Youbiquitous.Mvc.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ExtendedAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            CheckIfUserIsAuthenticated(filterContext);
        }

        private static void CheckIfUserIsAuthenticated(AuthorizationContext filterContext)
        {
            // If Result is NULL we're OK: the user is authenticated and authorized
            if (filterContext.Result == null)
                return;

            // Is this an AJAX request?
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                // For an AJAX request, just shut down
                // This might give you troubles if you use OLD Ajax methods in MVC
                // such as Ajax.BeginForm
                filterContext.HttpContext.Response.StatusCode = 401;
                filterContext.HttpContext.Response.End();
            }

            // If here, you're getting a HTTP 401 (unauthorized) code
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                throw new Exception("Authenticated but not authorized");
            }
        }
    }

}