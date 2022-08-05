using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using SitecoreModules.Foundation.Forms.Services;

namespace SitecoreModules.Foundation.Forms
{
    public class ServiceRegistration : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            var isStandard =
                Sitecore.Configuration.Settings.GetBoolSetting("Foundation.Forms.ReCaptchaEnterprise.IsStandard",
                    false);
            if (isStandard)
            {
                serviceCollection.AddTransient<IReCaptchaService, ReCaptchaStandardService>();
            }
            else
            {
                serviceCollection.AddTransient<IReCaptchaService, ReCaptchaEnterpriseService>();

            }
        }
    }
}
