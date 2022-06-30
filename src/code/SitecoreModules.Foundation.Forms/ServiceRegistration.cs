using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using SitecoreModules.Foundation.Forms.Services;

namespace SitecoreModules.Foundation.Forms
{
    public class ServiceRegistration : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IReCaptchaService, ReCaptchaEnterpriseService>();
        }
    }
}
