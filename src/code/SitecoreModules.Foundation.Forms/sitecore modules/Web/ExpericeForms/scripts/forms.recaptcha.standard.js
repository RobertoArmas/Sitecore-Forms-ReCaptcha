(function ($) {
    if (window.reCaptchaEnterprise != undefined) {
        document.addEventListener("DOMContentLoaded", (event) => {
            window.reCaptchaEnterprise.inputs.forEach((params) => {
                var form = params.input.parentElement.parentElement;
                $(form).find('input[type="submit"]').on("click",
                    (e) => {
                        var targetItem = e.currentTarget;
                        var recaptchaTimestamp = targetItem.dataset.recaptchaTimestamp;
                        if (!recaptchaTimestamp || Date.now() - recaptchaTimestamp > 60000) {
                            grecaptcha.execute(window.reCaptchaEnterprise.publicKey, { action: params.action }).then((token) => {
                                targetItem.dataset.recaptchaTimestamp = Date.now();
                                params.input.value = token;
                                targetItem.click();
                            });
                        } else {
                            return true;
                        }
                        return false;
                    });
            });
        });
    }
})(jQuery);