
// loads the previous page
function GoBack() {
    window.history.back();
}

// sets the focus on the passed button to submit the current from
function SubmitOnEnter(btnSubmit) {
    if (window.event && window.event.keyCode == 13) {
        btnSubmit.focus();
    }
}

//Resets the fields of the passed user control
function ResetFields() {
    var InputFields = document.forms[0].getElementsByTagName("input");
    var textAreas = document.forms[0].getElementsByTagName("textarea");
    var Lists = document.forms[0].getElementsByTagName("select");
    var radiosFlag = 0;

    // reset the input fields
    for (var i = 0; i < InputFields.length; i++) {
        var inputFieldType = InputFields[i].getAttribute("type");

        // reset the field if it is a child for the current user control and if it is from the types (text, radio, checkbox)
        if (inputFieldType == "text" | inputFieldType == "password") {
            InputFields[i].value = "";
        }
        else if (inputFieldType == "checkbox") {
            InputFields[i].checked = false;
        }
        else if (inputFieldType == "radio") {
            if (radiosFlag == 0) {
                InputFields[i].checked = true;
                radiosFlag = 1;
            }
            else {
                InputFields[i].checked = false;
            }
        }
    }

    //reset text areas
    for (var i = 0; i < textAreas.length; i++) {
        textAreas[i].value = "";
    }

    // reset the list selection to the first item
    for (var i = 0; i < Lists.length; i++) {
        Lists[i].selectedIndex = 0;
    }
}

function maxLength(field, maxChars) {
    if (field.value.length >= maxChars) {
        event.returnValue = false;
        alert("more than " + maxChars + " chars");
        return false;
    }
}

function maxLengthPaste(field, maxChars) {
    event.returnValue = false;
    if ((field.value.length + window.clipboardData.getData("Text").length) > maxChars) {
        alert("more than " + maxChars + " chars");
        return false;
    }
    event.returnValue = true;
}
// the code will look like this in aspx file
// <textarea rows="5" cols="6" onKeyPress='return maxLength(this,"30");' onpaste='return maxLengthPaste(this,"30");'></textarea>

function ConfirmDelete() {
    return confirm(PreConfirmDelete);
}

function ConfirmApproval() {
    return confirm(PreConfirmApprove);
}

function ConfirmRejection() {
    return confirm(PreConfirmReject);
}

function ConfirmActivate() {
    return confirm(PreConfirmActivate);
}

function ConfirmDeactivate() {
    return confirm(PreConfirmDeactivate);
}

function ConfirmBlocking() {
    return confirm(PreConfirmBlocking);
}

function ConfirmSettingCurrent() {
    return confirm(PreConfirmSettingCurrent);
}

function ClearWaterMark(inputID, textColor, oldText, validator) {

    var field = document.getElementById(inputID);
    var valid = document.getElementById(validator);

    if (field.value == oldText) {
        field.value = "";
        field.style.fontStyle = "normal";
        field.style.color = textColor;
        ValidatorEnable(valid, false);

    }
}

function ShowWaterMark(inputID, textColor, markText, validator) {

    var field = document.getElementById(inputID);
    var valid = document.getElementById(validator);

    if (field.value == "") {
        field.value = markText;
        field.style.fontStyle = "italic";
        field.style.color = textColor;
        ValidatorEnable(valid, true);
    }
}

function validEmail($email, $skipDNS) {
    $isValid = true;
    $atIndex = strrpos($email, "@");
    if (is_bool($atIndex) && !$atIndex) {
        $isValid = false;
    }
    else {
        $domain = substr($email, $atIndex + 1);
        $local = substr($email, 0, $atIndex);
        $localLen = strlen($local);
        $domainLen = strlen($domain);
        if ($localLen < 1 || $localLen > 64) {
            // local part length exceeded
            $isValid = false;
        }
        else if ($domainLen < 1 || $domainLen > 255) {
            // domain part length exceeded
            $isValid = false;
        }
        else if ($local[0] == '.' || $local[$localLen - 1] == '.') {
            // local part starts or ends with '.'
            $isValid = false;
        }
        else if (preg_match('/\\.\\./', $local)) {
            // local part has two consecutive dots
            $isValid = false;
        }
        else if (!preg_match('/^[A-Za-z0-9\\-\\.]+$/', $domain)) {
            // character not valid in domain part
            $isValid = false;
        }
        else if (preg_match('/\\.\\./', $domain)) {
            // domain part has two consecutive dots
            $isValid = false;
        }
        else if (!preg_match('/^(\\\\.|[A-Za-z0-9!#%&`_=\\/$\'*+?^{}|~.-])+$/', str_replace("\\\\", "", $local))) {
            // character not valid in local part unless 
            // local part is quoted
            if (!preg_match('/^"(\\\\"|[^"])+"$/', str_replace("\\\\", "", $local))) {
                $isValid = false;
            }
        }

        if (!$skipDNS) {
            if ($isValid && !(checkdnsrr($domain, "MX") || checkdnsrr($domain, "A"))) {
                // domain not found in DNS
                $isValid = false;
            }
        }
    }
    return $isValid;
}
