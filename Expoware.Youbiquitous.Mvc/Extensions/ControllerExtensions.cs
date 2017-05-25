///////////////////////////////////////////////////////////////////
//
// Youbiquitous.MVC v1.0
// Author: Dino Esposito
//

using Expoware.Youbiquitous.Mvc.Results;
using System.IO;
using System.Web.Mvc;

namespace Expoware.Youbiquitous.Mvc.Extensions
{
    public static class ControllerExtensions
    {
        /// <summary>
        /// Computes the HTML out of a Razor template and returns it as a string
        /// </summary>
        /// <param name="controller">Root controller object</param>
        /// <param name="partialPath">Razor file name (ie, pv_EmailOfAssignment)</param>
        /// <param name="model">View model to pass</param>
        /// <returns>string</returns>
        public static string RenderPartialViewToString(this Controller controller, string partialPath, object model)
        {
            if (string.IsNullOrEmpty(partialPath))
                partialPath = controller.ControllerContext.RouteData.GetRequiredString("action");

            // Set the view model
            controller.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, partialPath);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);

                viewResult.View.Render(viewContext, sw);
                return sw.GetStringBuilder().ToString();
            }
        }

        /*
        * The basic idea of JSONP is retrieving JSON data via script tags. Script tags work around
        * same-origin policy limitations. So you point to a remote cross-domain JSON source and pass
        * an extra parameter (defined by the endpoint) so that the site return a call to your function 
        * which receives JSON data. Simply pointing the SCRIPT tag to the JSON URL would work but 
        * it would get you just data, and when evaluated within the browser, it has no externally detectable effect.
        */
        public static JsonpResult Jsonp(this Controller controller, object data, JsonRequestBehavior behavior = JsonRequestBehavior.AllowGet)
        {
            return new JsonpResult
            {
                Data = data,
                JsonRequestBehavior = behavior
            };
        }
    }
}