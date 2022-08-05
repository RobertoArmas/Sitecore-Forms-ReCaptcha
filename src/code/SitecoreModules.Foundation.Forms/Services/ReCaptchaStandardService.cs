using SitecoreModules.Foundation.Forms.Helpers;
using SitecoreModules.Foundation.Forms.Models.ReCaptcha;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SitecoreModules.Foundation.Forms.Services
{
    class ReCaptchaStandardService : IReCaptchaService
    {
        private readonly string _apiUrl =
            Sitecore.Configuration.Settings.GetSetting("Foundation.Forms.ReCaptchaEnterprise.Api.SiteVerify");

        private readonly string _siteKeyV3 =
            Sitecore.Configuration.Settings.GetSetting("Foundation.Forms.ReCaptchaEnterprise.PublicKeyV3", "");
        private readonly string _siteKeyV2 =
            Sitecore.Configuration.Settings.GetSetting("Foundation.Forms.ReCaptchaEnterprise.PublicKeyV2", "");

        private readonly double _scoreToEval =
            Sitecore.Configuration.Settings.GetDoubleSetting("Foundation.Forms.ReCaptchaEnterprise.Score", 0.5);

        public async Task<bool> Verify(string recaptchaToken, bool isV3)
        {
            var siteKey = isV3 ? _siteKeyV3 : _siteKeyV2;
            string url = $"{_apiUrl}?secret={siteKey}&response={recaptchaToken}";
            using (var httpClient = new HttpClient())
            {
                try
                {
                    string responseString = await httpClient.GetStringAsync(url);
                    var reCaptchaResponse =  Newtonsoft.Json.JsonConvert.DeserializeObject<ReCaptchaStandardResponse>(responseString);
                    if (reCaptchaResponse != null && reCaptchaResponse.success)
                    {
                        return reCaptchaResponse.score >= _scoreToEval;
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
