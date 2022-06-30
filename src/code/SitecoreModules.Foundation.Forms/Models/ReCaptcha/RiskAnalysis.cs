using Newtonsoft.Json;

namespace SitecoreModules.Foundation.Forms.Models.ReCaptcha
{
    public class RiskAnalysis
    {
        [JsonProperty("score")]
        public float Score { get; set; }
    }
}
