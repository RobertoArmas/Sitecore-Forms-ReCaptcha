$.validator.setDefaults({ ignore: ":hidden:not(.fxt-captcha-enterprise)" });
/**
 * Google Recaptcha
 */
var reCaptchaArray = reCaptchaArray || [];
$.validator.unobtrusive.adapters.add("recaptcha", function (options) {
    options.rules["recaptcha"] = true;
    if (options.message) {
        options.messages["recaptcha"] = options.message;
    }
});

$.validator.addMethod("recaptcha", function (value, element, exclude) {
    return true;
});
recaptchasRendered = false;
var loadReCaptchas = function () {
    for (var i = 0; i < reCaptchaArray.length; i++) {
        var reCaptcha = reCaptchaArray[i];
        if (reCaptcha.IsRendered === undefined) {
            reCaptcha.IsRendered = true;
            reCaptcha();
        }
    }
};

(function ($) {

    document.addEventListener("DOMContentLoaded", (event) => {
        if (window.reCaptchaEnterprise != undefined) {
            window.reCaptchaEnterprise.inputs.forEach((params) => {
                var form = params.input.closest("form");
                var submitBtn = $(form).find('input[type="submit"]').length != 0
                    ? $(form).find('input[type="submit"]')
                    : $(form).find('button[type="submit"]');
                submitBtn.on("click",
                    (e) => {
                        var targetItem = e.currentTarget;
                        var recaptchaTimestamp = targetItem.dataset.recaptchaTimestamp;
                        if (!recaptchaTimestamp || Date.now() - recaptchaTimestamp > 60000) {
                            grecaptcha.execute(window.reCaptchaEnterprise.publicKey, { action: params.action }).then((token) => {
                                targetItem.dataset.recaptchaTimestamp = Date.now();
                                params.input.value = "s"+token;
                                targetItem.click();
                            });
                        } else {
                            return true;
                        }
                        return false;
                    });
            });
        }
    });

})(jQuery);