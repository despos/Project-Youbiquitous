///////////////////////////////////////////////////////////////////
//
// Youbiquitous.MVC v1.0
// Author: Dino Esposito
//

using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Expoware.Youbiquitous.Mvc.Results
{

    public class MultipleViewResult : JsonResult
    {
        public const string ChunkSeparator = "---|||---";

        public IList<PartialViewResult> PartialViewResults { get; private set; }

        public MultipleViewResult(params PartialViewResult[] views)
        {
            if (PartialViewResults == null)
                PartialViewResults = new List<PartialViewResult>();
            foreach (var v in views)
                PartialViewResults.Add(v);
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            var total = PartialViewResults.Count;
            for (var index = 0; index < total; index++)
            {
                var pv = PartialViewResults[index];

                // By design, the MODEL is a shared location that refers to the single
                // rendering request. We're trying to render multiple blocks in the context of the 
                // same request. As a result, each time we call PartialView the MODEL is overridden.
                // No big deal if all partial views get the same model; but if it's different
                // we must store each model in a ViewData entry using anything like the view name 
                // as the key. And set back the "right" model here before proceeding.
                pv.ViewData.Model = pv.ViewData[pv.ViewName];

                // Render the view
                pv.ExecuteResult(context);
                if (index < total - 1)
                    context.HttpContext.Response.Output.Write(ChunkSeparator);
            }
        }

    }
}