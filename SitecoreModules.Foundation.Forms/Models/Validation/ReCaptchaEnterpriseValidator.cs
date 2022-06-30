using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.ExperienceForms.Mvc.Models.Validation;
using SitecoreModules.Foundation.Forms.Models.Fields;
using SitecoreModules.Foundation.Forms.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SitecoreModules.Foundation.Forms.Models.Validation
{
    public class ReCaptchaEnterpriseValidator : ValidationElement<string>
    {
        private readonly IReCaptchaService reCaptchaService = ServiceLocator.ServiceProvider.GetService<IReCaptchaService>();

        public ReCaptchaEnterpriseValidator(ValidationDataModel validationItem)
            : base(validationItem)
        {
        }

        protected string Value { get; set; }
        protected bool IsV3 { get; set; }


        public override void Initialize(object validationModel)
        {
            base.Initialize(validationModel);
            if (!(validationModel is ReCaptchaEnterpiseViewModel stringInputViewModel))
                return;
            this.Value = stringInputViewModel.CaptchaValue;
            this.IsV3 = stringInputViewModel.RecaptchaModeInvisible;

        }

        public override ValidationResult Validate(object value)
        {
            var isValid = reCaptchaService.VerifySync((string)value, IsV3);
            RecaptchaContext.RecaptchaValidated = isValid;
            if (isValid)
                return ValidationResult.Success;
            return new ValidationResult(this.FormatMessage());
        }

        public override IEnumerable<ModelClientValidationRule> ClientValidationRules { get; }
    }
}
