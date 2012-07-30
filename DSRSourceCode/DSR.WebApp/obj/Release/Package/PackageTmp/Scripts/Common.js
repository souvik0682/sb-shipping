function js_waterMark_Focus(objname, waterMarkText) {
    obj = document.getElementById(objname);
    if (obj.value == waterMarkText) {
        obj.value = "";
    }
}
function js_waterMark_Blur(objname, waterMarkText) {
    obj = document.getElementById(objname);
    if (obj.value == "") {
        obj.value = waterMarkText;
    }
}
function doClick(e, btn) {
    var key;

    if (window.event)
        key = window.event.keyCode;     // for IE
    else
        key = e.which;                  // for firefox

    if (key == 13) {
        document.getElementById(btn).focus();
        document.getElementById(btn).click();
        event.keyCode = 0;
    }
}

function IsNumeric(ctrl) {
    var value = document.getElementById(ctrl.id).value;
    var validChars = "0123456789";
    var strChar;
    var result = true;

    if (value.length == 0) return false;

    for (i = 0; i < value.length && result; i++) {
        strChar = value.charAt(i);

        if (validChars.indexOf(strChar) == -1) {
            result = false;
        }
    }

    if (!result) {
        alert('Please enter a valid numeric value');
        document.getElementById(ctrl.id).value = "";
        ctrl.focus();
        return false;
    }
    else {
        return true;
    }
}

function UnderConstruction() {
    alert('This page is under construction!!');
    return false;
}

function ValidateDecimal(txt) {
    var exp = new RegExp('^[0-9]+(\.[0-9]{1,2})?$');

    if (txt.value.match(exp)) {
        return true;
    }
    else {
        return false;
    }
}

function SetMaxLength(obj, maxLen) {
    ConvertToUpperCase();
    return (obj.value.length < maxLen);
}

function getTopPosition(inputObj) {

    var returnValue = inputObj.offsetTop + inputObj.offsetHeight;
    while ((inputObj = inputObj.offsetParent) != null) returnValue += inputObj.offsetTop;
    return returnValue;
}

function getLeftPosition(inputObj) {
    var returnValue = inputObj.offsetLeft;
    while ((inputObj = inputObj.offsetParent) != null) returnValue += inputObj.offsetLeft;
    return returnValue;
}

function RedirectAfterCancelClick(pagename, message) {
    var result = confirm(message);

    if (result) {
        window.location.href = pagename;
        return false;
    }
    else {
        return false;
    }
}

function ConvertToUpperCase(e) {
    var key;

    if (window.event)
        key = window.event.keyCode;     // for IE
    else
        key = e.which;                  // for firefox


    if ((key > 0x60) && (key < 0x7B)) {
        window.event.keyCode = key - 0x20;
    }
    else {
        e.which = key - 0x20;
    }
}

/***** For Ajax *****/
function pageLoad() {
    var manager = Sys.WebForms.PageRequestManager.getInstance();
    manager.add_initializeRequest(InitializeRequest);
    manager.add_endRequest(endRequest);
    manager.add_beginRequest(OnBeginRequest);
}
function ToggleAsynDiv(visString) {
    var adiv = $get('dvAsync');

    if (adiv != null)
        adiv.style.display = visString;

}
function ClearErrorState() {
    $get('dvAsyncMessage').innerHTML = '';
    ToggleAsynDiv('none');
}

function OnBeginRequest(sender, args) {
    ToggleAsynDiv('none');
}

function endRequest(sender, args) {
    if (args.get_error() != undefined) {
        var errorMessage;
        if (args.get_response().get_statusCode() == '200') {
            errorMessage = args.get_error().message;
        }
        else {
            // Error occurred somewhere other than the server page.
            errorMessage = '* An unspecified error has occurred.';
        }
        args.set_errorHandled(true);
        ToggleAsynDiv('');

        if ($get('ctl00_container_UpdateProgress1') != null)
            $get('ctl00_container_UpdateProgress1').style.display = 'none';

        $get('dvAsyncMessage').innerHTML = "* An error has occured. Please contact system administrator.";
    }
}

function InitializeRequest(sender, args) {
    var manager = Sys.WebForms.PageRequestManager.getInstance();

    if (manager.get_isInAsyncPostBack())
        args.set_cancel(true);

    var postBackElement;
    postBackElement = args.get_postBackElement();

    if ($get('ctl00_container_UpdateProgress1') != null)
        $get('ctl00_container_UpdateProgress1').style.display = '';
}


/***** For Ajax *****/
function hidePrintPanel(dvId, returntype) {
    document.getElementById(dvId).style.display = 'none';
    return returntype;
}
function showPrintPanel(imgId, dvId) {
    var imgPrint = document.getElementById(imgId);
    var dvPrint = document.getElementById(dvId);

    dvPrint.style.left = (getLeftPosition(imgPrint) - 118) + 'px';
    dvPrint.style.display = '';
}