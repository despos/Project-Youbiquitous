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
    public class MultipleActionResult : ActionResult
    {
        public const string ChunkSeparator = "---|||---";

        public IList<ActionResult> ActionResults { get; }

        public MultipleActionResult(params ActionResult[] results)
        {
            if (ActionResults == null)
                ActionResults = new List<ActionResult>();
            foreach (var r in results)
                ActionResults.Add(r);
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var total = ActionResults.Count;
            for (var index = 0; index < total; index++)
            {
                var pv = ActionResults[index];

                // By design, the MODEL is a shared location that refers to the single
                // rendering request. We're trying to render multiple blocks in the context of the 
                // same request. As a result, each time we call PartialView the MODEL is overridden.
                // No big deal if all partial views get the same model; but if it's different
                // we must store each model in a ViewData entry using anything like the view name 
                // as the key. And set back the "right" model here before proceeding.
                var result = pv as ViewResult;
                if (result != null)
                {
                    var viewResult = result;
                    viewResult.ViewData.Model = viewResult.ViewData[viewResult.ViewName];
                }

                // Render the view
                pv.ExecuteResult(context);
                if (index < total - 1)
                    context.HttpContext.Response.Output.Write(ChunkSeparator);
            }
        }
    }
}