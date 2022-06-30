using Newtonsoft.Json;

namespace SitecoreModules.Foundation.Forms.Models.ReCaptcha
{
    public class ReCaptchaEnterpriseResponse
    {
        [JsonProperty("riskAnalysis")]
        public RiskAnalysis RiskAnalysis { get; set; }
        [JsonProperty("tokenProperties")]
        public TokenProperties tokenProperties { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
