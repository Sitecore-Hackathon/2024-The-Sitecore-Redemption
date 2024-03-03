using Sitecore.Mvc.Controllers;
using SitecoreRedemption.Foundation.AIServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SitecoreRedemption.Feature.AIContentOptimizer.Controllers
{
    public class AIContentOptimizerController : SitecoreController
    {
        private readonly ContentOptimizerService _contentOptimizerService;

        public AIContentOptimizerController(ContentOptimizerService contentOptimizerService)
        {
            _contentOptimizerService = contentOptimizerService;
        }

        //public async Task<JsonResult> GetOptimizationSuggestions(string itemId)
        //{
        //    var item = Sitecore.Context.Database.GetItem(new Sitecore.Data.ID(itemId));
        //    if (item == null) return Json(new { success = false, message = "Item not found." });

        //    var prompt = item.Fields["YourTextField"].Value;
        //    var optimizationResult = await _contentOptimizerService.OptimizeContentAsync(prompt);

        //    return Json(new
        //    {
        //        success = true,
        //        choices = optimizationResult.Choices
        //    }, JsonRequestBehavior.AllowGet);
        //}
    }
}
