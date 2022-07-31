# Sitecore Forms ReCaptcha
This repository contains the code base for a Sitecore Google ReCaptcha Enteprise Version using Sitecore Forms

You will find more info in the following link: [Blog Entry](https://rmcdigital.com/sitecore/sitecore-forms-recaptcha-enterprise-support/)


## Nuget Installation

You can easily install Sitecore Forms ReCaptcha by adding a Nuget Package in your `Website` Project.

```powershell
PM> Install-Package SitecoreModules.Foundation.Forms
```

## Configuration

### Generate Api Keys and Duplicate Config File

It is required to Generate

| Setting | Value |
|---|---|
| Foundation.Forms.ReCaptchaEnterprise.Api.Url| https://www.google.com/recaptcha/enterprise.js?render={0}|
| Foundation.Forms.ReCaptchaEnterprise.Api.SiteVerify|https://recaptchaenterprise.googleapis.com/v1/projects/[PROJECT-ID]/assessments?key={0}|
|Foundation.Forms.ReCaptchaEnterprise.PublicKeyV3|[Public – V3]|
|Foundation.Forms.ReCaptchaEnterprise.PublicKeyV2|[Public – V2]|
|Foundation.Forms.ReCaptchaEnterprise.ApiKey|[Api Key] – This is used for Server Side Validation|
|Foundation.Forms.ReCaptchaEnterprise.Score|Default to 0.5.|

### Add JS to load into Sitecore Forms

You need to ensure that `forms.recaptcha.js` is added on standard values to load the js functionality that is required to work.

Open Form's standard values `/sitecore/templates/System/Forms/Form/__Standard Values` and add `forms.recaptcha.js` to the end of Scripts field.

## Usage:

1. Create a Sitecore Form
2. Drag and Drop "ReCaptcha Enterpise" form control..
3. Configure Invisible / Checkbox
4. Save your Form 