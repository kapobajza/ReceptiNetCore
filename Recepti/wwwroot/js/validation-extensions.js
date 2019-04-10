$.validator.addMethod('password-validation', function (value, element, params) {
    var lengthRequirement = value.length >= 8 && value.length <= 25;
    var hasUpperCaseLetter = false;
    var hasLowerCaseLetter = false;
    var hasDecimalDigit = false;
    var hasSpecialChar = false;

    if (/[a-z]/.test(value)) {
        hasLowerCaseLetter = true;
    }

    if (/[A-Z]/.test(value)) {
        hasUpperCaseLetter = true;
    }

    if (/\d/.test(value)) {
        hasDecimalDigit = true;
    }

    if (/[-_#$]/.test(value)) {
        hasSpecialChar = true;
    }

    var isValid = lengthRequirement
        && hasUpperCaseLetter
        && hasLowerCaseLetter
        && hasDecimalDigit
        && hasSpecialChar;

    return isValid;
});

$.validator.unobtrusive.adapters.add('password-validation', [], function (options) {
    options.rules['password-validation'] = {};
    options.messages['password-validation'] = options.message;
});