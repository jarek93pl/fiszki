
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
    var request = new XMLHttpRequest();
    request.open('POST', url, false);
    request.setRequestHeader("Content-Type", "application/json");

    request.onload = function () {
        if (request.status === 200 && CheckRedairect(request.response)) {
            if (request.response === "") {
                completed(undefined);
            }
            else {
                completed(JSON.parse(request.response));
            }
        }
    };
    request.send(JSON.stringify(data));

}

function CheckRedairect(text) {
    if (text === 'WylogowanieXDRwylogowanieXDR') {
        window.location.href = '';
        return false;
    }
    return true;
}
function SendFile(control, type) {

    var reader = new FileReader();
    reader.onload = function () {

        var arrayBuffer = this.result,
            array = new Uint8Array(arrayBuffer),
            binaryString = String.fromCharCode.apply(null, array);
        Post('Comon/SaveFile', { DataFileValue: btoa(binaryString), TypeValue: type, Extension: control.value.split('.').pop() }, function () {

        });
        console.log(binaryString);
    };
    reader.readAsArrayBuffer(control.files[0]);
}



