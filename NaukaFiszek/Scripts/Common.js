
function loadPage(e) {
    loadPageUsingUrl(e, this.href);
}
function loadPageUsingUrl(e, adres) {

    e.preventDefault();
    $('#content').remove();
    $('#container').html("<div id='content'></div>");
    $('#content').load(adres, function () {
        CheckRedairect($('#content').text());
    });
}
function Post(url, data, completed) {
    var request = new XMLHttpRequest();
    request.open('POST', url, false);
    request.setRequestHeader("Content-Type", "application/json");

    request.onload = function () {
        if (request.status === 200 && CheckRedairect(request.response)) {
            if (request.response === "")
            {
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