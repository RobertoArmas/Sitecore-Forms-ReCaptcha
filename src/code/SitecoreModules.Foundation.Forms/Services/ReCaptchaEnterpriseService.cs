using Newtonsoft.Json;
using Sitecore.Diagnostics;
using SitecoreModules.Foundation.Forms.Helpers;
using SitecoreModules.Foundation.Forms.Models.ReCaptcha;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SitecoreModules.Foundation.Forms.Services
{
    public class ReCaptchaEnterpriseService : IReCaptchaService
    {
        private readonly HttpClient _client = new HttpClient();

        private readonly string _apiUrl =
            Sitecore.Configuration.Settings.GetSetting("Foundation.Forms.ReCaptchaEnterprise.Api.SiteVerify");

        private readonly string _apiKey =
            Sitecore.Configuration.Settings.GetSetting("Foundation.Forms.ReCaptchaEnterprise.ApiKey");

        private readonly string _siteKeyV3 =
            Sitecore.Configuration.Settings.GetSetting("Foundation.Forms.ReCaptchaEnterprise.PublicKeyV3", "");
        private readonly string _siteKeyV2 =
            Sitecore.Configuration.Settings.GetSetting("Foundation.Forms.ReCaptchaEnterprise.PublicKeyV2", "");

        private readonly double _scoreToEval =
            Sitecore.Configuration.Settings.GetDoubleSetting("Foundation.Forms.ReCaptchaEnterprise.Score", 0.5);

        public async Task<bool> Verify(string response, bool isV3)
        {
            try
            {
                var apiUrl = string.Format(_apiUrl, _apiKey);
                var requestObj = new ReCaptchaEnterpriseRequest();
                requestObj.Event = new ReCaptchaEnterpriseRequest.ReCaptchaEvent();
                requestObj.Event.token = response;
                requestObj.Event.siteKey = isV3 ? _siteKeyV3 : _siteKeyV2;
                string output = JsonConvert.SerializeObject(requestObj);
                var content = new StringContent(output, Encoding.UTF8, "application/json");
                var responseMessage = await _client.PostAsync(apiUrl, content);
                var jsonString = await responseMessage.Content.ReadAsStringAsync();
                var reCaptchaResponse = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<ReCaptchaEnterpriseResponse>(jsonString));
                Log.Info($"ReCaptcha Response: {jsonString}", this);
                if (reCaptchaResponse.tokenProperties != null && reCaptchaResponse.tokenProperties.Valid && reCaptchaResponse.RiskAnalysis != null)
                {
                    return reCaptchaResponse.RiskAnalysis.Score >= _scoreToEval;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex, this);
            }

            return false;
        }

        public bool VerifySync(string response, bool isV3 = true)
        {
            return AsyncHelpers.RunSync(() => Verify(response, isV3));
        }
    }
}
