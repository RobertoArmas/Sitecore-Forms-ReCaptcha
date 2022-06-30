using System.Web;

namespace SitecoreModules.Foundation.Forms.Models.Fields
{
    public class RecaptchaContext
    {
        private readonly string _recaptchaValidatedKey = "RecaptchaValidated";

        public static bool RecaptchaValidated
        {
            get => HttpContext.Current.Items.Contains((object)nameof(_recaptchaValidatedKey)) && (bool)HttpContext.Current.Items[(object)nameof(_recaptchaValidatedKey)];
            set => HttpContext.Current.Items[(object)nameof(_recaptchaValidatedKey)] = (object)value;
        }
    }
}
