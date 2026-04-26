(function ($) {
  const $form = $("form.contact-form");

  const siteKey = $form.data("recaptcha-site-key");

  let _tokenReady = false;

  function began(xhr) {
    if (_tokenReady) {
      _tokenReady = false;
      return;
    }

    xhr.abort();

    // Hide previous alerts
    showError(false);
    showSuccess(false);

    grecaptcha.ready(function () {
      grecaptcha.execute(siteKey, { action: 'contact_us' })
        .then(function (token) {
          setToken(token);
          _tokenReady = true;
          $form[0].dispatchEvent(new Event('submit', { bubbles: true }));
        });
    });
  }

  function completed () {
    disableBtn(false);
    setToken('');
  }

  function succeed(data) {
    if (data.isSuccess) {
      $form[0].reset();
    }
    showError(!data.isSuccess);
    showSuccess(data.isSuccess);
  }

  function failed (data) {
    showError(true);
    showSuccess(false);
  }

  window.began = began;
  window.completed = completed;
  window.succeed = succeed;
  window.failed = failed;

  function disableBtn(disable) {
    $form.find("input[type='submit']").prop("disabled", disable);
  }

  function showError(show) {
    $form.find(".contact-form-error").toggleClass('d-none', !show);
  }

  function showSuccess(show) {
    $form.find(".contact-form-success").toggleClass('d-none', !show);
  }

  function setToken(token) {
    $form.find('input[name$="ReCaptchaToken"]').val(token);
  }

}(jQuery));