using System;
using System.Collections.Generic;

namespace SitecoreModules.Foundation.Forms.Models.ReCaptcha
{
    public class ReCaptchaStandardResponse
    {
        public DateTime challenge_ts { get; set; }
        public float score { get; set; }
        public List<string> ErrorCodes { get; set; }
        public bool success { get; set; }
        public string hostname { get; set; }
    }
}
