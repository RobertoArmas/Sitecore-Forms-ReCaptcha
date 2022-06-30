using Newtonsoft.Json;

namespace SitecoreModules.Foundation.Forms.Models.ReCaptcha
{
    public class TokenProperties
    {

        [JsonProperty("valid")]
        public bool Valid { get; set; }
        [JsonProperty("hostname")]
        public string Hostname { get; set; }
        [JsonProperty("action")]
        public string Action { get; set; }
    }
}
