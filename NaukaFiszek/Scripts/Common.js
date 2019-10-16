
function loadPage(e) {
    loadPageUsingUrl(e, this.href);
}
function loadPageUsingUrl(e, adres) {
    loadPageUsingBase(e, adres, 'content');
}

function loadPageUsingBase(e, adres, nameHtml) {

    e.preventDefault();
    $('#' + nameHtml).remove();
    $('#container').html("<div id='content'></div>");
    $('#' + nameHtml).load(adres, function () {
        CheckRedairect($('#' + nameHtml).text());
    });
}
function PostAction(url, data, completed) {
    $.ajax({
        url: url,
        type: "POST",
        cache: false,
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data === "") {
                completed(undefined);
            }
            else {
                completed(data);
            }
        },
        error: function (jqXhr, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });

}
function SendFileAction(control, type, responseFunc) {

    var formData = new FormData();

    var file = control.files[0];
    formData.append("FileUpload", file);


    $.ajax({
        type: 'post',
        url: '/Comon/SaveFile',
        data: formData,
        dataType: 'json',
        contentType: false,
        processData: false,
        success: responseFunc,
        error: function (error) {
            alert("errror");
        }
    });
    /*
    var reader = new FileReader();
    reader.onload = function () {

        var arrayBuffer = this.result;
        binaryString = String.fromCharCode.apply(null, new Uint8Array(arrayBuffer));
        Post('../Comon/SaveFile', { DataFileValue: btoa(binaryString), TypeValue: type, Extension: control.value.split('.').pop() }, responseFunc);
    };
    reader.readAsArrayBuffer(control.files[0]);
    */
}
function CheckRedairect(text) {
    if (text === 'WylogowanieXDRwylogowanieXDR') {
        window.location.href = '';
        return false;
    }
    return true;
}

function GetGuid() {
    return ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, c =>
        (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
    );
}
var TimeShowingErrorMs = 5000;
function ValidatingControl(selector, functionToValidating, ErrorMessage) {

    RemoveErrorElementBySelector(selector);
    if (!functionToValidating()) {
        $('#ListError').append('<li style="color:red" class="ErrorRowCommon"> <input type="hidden" value="' + selector + '" />' + ErrorMessage + '</li>');
        $(selector).addClass('ErrorElement');
        CheckVisibilityErrorAlert();

        setTimeout(function () {
            RemoveErrorElementBySelector(selector);
            CheckVisibilityErrorAlert();
        }, TimeShowingErrorMs);
        return false;
    }
    else {
        RemoveErrorElementBySelector(selector);
        $(selector).removeClass('ErrorElement');
        CheckVisibilityErrorAlert();
        return true;
    }
}
function RemoveErrorElementBySelector(selector) {

    $('[value="' + selector + '"]').parent('.ErrorRowCommon').remove();
}
function ValidatingControlMinLenght2(selector, ErrorMessage) {
    return ValidatingControl(selector, function () {
        return $(selector).val().length > 2;
    }, ErrorMessage);
}
function ValidatingControlMinLenght2ById(id) {
    ValidatingControlMinLenght2('#' + id, "Pole " + $('[for=' + id + ']').text() + " musi mieć przynajmniej 2 znaki");
}
function CheckVisibilityErrorAlert() {
    var $listErrorDiv = $('#ErrorAlert');
    if ($('#ListError').children().length === 0) {
        $listErrorDiv.hide();
    }
    else {
        $listErrorDiv.show();
    }
}