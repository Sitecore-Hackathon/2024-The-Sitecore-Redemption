using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using SitecoreRedemption.Foundation.AIServices.Services;
using System;

namespace SitecoreRedemption.Foundation.AIServices.DI
{
    public class RegisterContainer : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddHttpClient<IContentOptimizerService, ContentOptimizerService>();
            serviceCollection.AddTransient<IContentOptimizerService, ContentOptimizerService>();         
        }
    }
}
