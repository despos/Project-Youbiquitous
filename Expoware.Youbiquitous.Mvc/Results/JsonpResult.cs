///////////////////////////////////////////////////////////////////
//
// Youbiquitous.MVC v1.0
// Author: Dino Esposito
//

using System;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Expoware.Youbiquitous.Mvc.Results
{
    public class JsonpResult : JsonResult
    {
        // The callback name here is the parameter name to be added to the URL to specify the
        // name of the JavaScript function padding the JSON response. This name is arbitrary and
        // is part of your site’s SDK.
        private const string JsonpFunctionName = "callback";

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            if ((JsonRequestBehavior == JsonRequestBehavior.DenyGet) &&
                string.Equals(context.HttpContext.Request.HttpMethod, "GET"))
                throw new InvalidOperationException();

            var response = context.HttpContext.Response;
            if (!String.IsNullOrEmpty(ContentType))
                response.ContentType = ContentType;
            else
                response.ContentType = "application/json";
            if (ContentEncoding != null)
                response.ContentEncoding = this.ContentEncoding;

            if (Data != null)
            {
                var serializer = new JavaScriptSerializer();
                var buffer = String.Format("{0}({1})", JsonpFunctionName, serializer.Serialize(Data));
                response.Write(buffer);
            }
        }
    }

}