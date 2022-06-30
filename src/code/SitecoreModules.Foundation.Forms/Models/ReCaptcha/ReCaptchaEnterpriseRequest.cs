using Newtonsoft.Json;

namespace SitecoreModules.Foundation.Forms.Models.ReCaptcha
{
    public class ReCaptchaEnterpriseRequest
    {
        [JsonProperty("event")]
        public ReCaptchaEvent Event { get; set; }

        public class ReCaptchaEvent
        {
            public string token { get; set; }
            public string siteKey { get; set; }
            public string expectedAction { get; set; }
        }
    }
}
