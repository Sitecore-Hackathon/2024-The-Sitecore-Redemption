using Sitecore.Data;
using SitecoreRedemption.Foundation.AIServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitecoreRedemption.Foundation.AIServices.Services
{
    public interface IContentOptimizerService
    {
        Task<OpenAiResponse> OptimizeContentAsync(string prompt, string settingId);
    }
}
