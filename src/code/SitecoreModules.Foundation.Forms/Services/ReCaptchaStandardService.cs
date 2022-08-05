using SitecoreModules.Foundation.Forms.Helpers;
using SitecoreModules.Foundation.Forms.Models.ReCaptcha;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SitecoreModules.Foundation.Forms.Services
{
    public class ReCaptchaStandardService : IReCaptchaService
    {
        private readonly string _apiUrl =
            Sitecore.Configuration.Settings.GetSetting("Foundation.Forms.ReCaptchaEnterprise.Api.SiteVerify");

        private readonly string _apiSecretV2 =
            Sitecore.Configuration.Settings.GetSetting("Foundation.Forms.ReCaptchaEnterprise.ApiSecretV2");
        private readonly string _apiSecretV3 =
            Sitecore.Configuration.Settings.GetSetting("Foundation.Forms.ReCaptchaEnterprise.ApiSecretV3");

        private readonly double _scoreToEval =
            Sitecore.Configuration.Settings.GetDoubleSetting("Foundation.Forms.ReCaptchaEnterprise.Score", 0.5);

        public async Task<bool> Verify(string recaptchaToken, bool isV3)
        {
            var secret = isV3 ? _apiSecretV3 : _apiSecretV2;
            string url = $"{_apiUrl}?secret={secret}&response={recaptchaToken}";
            using (var httpClient = new HttpClient())
            {
                try
                {
                    string responseString = await httpClient.GetStringAsync(url);
                    var reCaptchaResponse =  Newtonsoft.Json.JsonConvert.DeserializeObject<ReCaptchaStandardResponse>(responseString);
                    if (reCaptchaResponse != null && reCaptchaResponse.success)
                    {
                        if (isV3)
                        {
                            return reCaptchaResponse.score >= _scoreToEval;
                        }
                        else
                        {
                            return reCaptchaResponse.success;
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                return false;

            }
        }

        public bool VerifySync(string recaptchaToken, bool isV3 = true)
        {
            return AsyncHelpers.RunSync(() => Verify(recaptchaToken, isV3));
        }
    }
}
