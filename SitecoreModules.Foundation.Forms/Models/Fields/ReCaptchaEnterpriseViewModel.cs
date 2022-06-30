using Newtonsoft.Json;
using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.ExperienceForms.Models;
using Sitecore.ExperienceForms.Mvc.Models.Fields;
using Sitecore.ExperienceForms.Mvc.Models.Validation;
using Sitecore.ExperienceForms.ValueProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SitecoreModules.Foundation.Forms.Models.Fields
{
    [Serializable]
    public class ReCaptchaEnterpiseViewModel
         : FieldViewModel, IValueField, IValidatableField
    {
        [NonSerialized]
        private List<IValidationElement> _validations;
        public List<ValidationDataModel> ValidationDataModels { get; } = new List<ValidationDataModel>();
        //[ReCaptchaValidationV3(ErrorMessage = "captcha.required")]
        [DynamicValidation]
        public string CaptchaValue { get; set; }

        public string CaptchaPublicKeyV2 => Settings.GetSetting("Foundation.Forms.ReCaptchaEnterprise.PublicKeyV2");
        public string CaptchaPublicKeyV3 => Settings.GetSetting("Foundation.Forms.ReCaptchaEnterprise.PublicKeyV3");

        public void InitializeValue(FieldValueProviderContext context)
        {
        }

        protected override void UpdateItemFields(Item item)
        {
            Assert.ArgumentNotNull((object)item, nameof(item));
            base.UpdateItemFields(item);
            item.Fields["Validations"]?.SetValue(StringUtil.ArrayToString((Array)this.ValidationDataModels.Select<ValidationDataModel, string>((Func<ValidationDataModel, string>)(v => v.ItemId)).ToArray<string>(), '|'), false);
            item.Fields["Is Invisible Mode"]?.SetValue(this.RecaptchaModeInvisible ? "1" : string.Empty, false);
        }

        protected override void InitItemProperties(Item item)
        {
            Assert.ArgumentNotNull((object)item, nameof(item));
            base.InitItemProperties(item);
            this.RecaptchaModeInvisible = MainUtil.GetBool(item.Fields["Is Invisible Mode"]?.Value, false);
            this.InitializeValidations(item);
        }

        protected virtual void InitializeValidations(Item item)
        {
            Assert.ArgumentNotNull((object)item, nameof(item));
            Field field = item.Fields["Validations"];
            string[] values;
            if (field == null)
                values = (string[])null;
            else
                values = field.Value.Split('|');
            if (values == null)
                return;
            foreach (string path in values)
            {
                Item validationItem = item.Database.GetItem(path, item.Language);
                if (validationItem != null)
                    this.ValidationDataModels.Add(new ValidationDataModel(validationItem));
            }
        }

        public bool RecaptchaModeInvisible { get; set; }

        public bool Required { get; set; }

        public bool IsTrackingEnabled { get; set; }

        public bool AllowSave { get; set; }

        public string GetStringValue()
        {
            return CaptchaValue;
        }

        [JsonIgnore]
        public IEnumerable<ModelClientValidationRule> ClientValidationRules { get; }
        [JsonIgnore]
        public IEnumerable<IValidationElement> Validations
        {
            get
            {
                if (this._validations == null)
                {
                    this._validations = new List<IValidationElement>();
                    this.ValidationDataModels.ForEach((Action<ValidationDataModel>)(validationDataModel =>
                    {
                        IValidationElement validationElement = validationDataModel.CreateValidationElement();
                        validationElement.Initialize((object)this);
                        this._validations.Add(validationElement);
                    }));
                }
                return (IEnumerable<IValidationElement>)this._validations;

            }
        }
    }
}
