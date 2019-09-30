
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
function Post(url, data, completed) {
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

function CheckRedairect(text) {
    if (text === 'WylogowanieXDRwylogowanieXDR') {
        window.location.href = '';
        return false;
    }
    return true;
}

function SendFile(control, type, responseFunc) {

    var formData = new FormData();

    var file = control.files[0];
    formData.append("FileUpload", file);


    $.ajax({
        type: 'post',
        url: '../Comon/SaveFile',
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
function GetGuid() {
    return ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, c =>
        (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
    );
}


