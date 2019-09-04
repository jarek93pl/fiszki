function loadPage(e) {

    e.preventDefault();
    $('#content').remove();
    $('#container').html("<div id='content'></div>");
    $('#content').load(this.href);
}
function Post(url, data, completed) {
    var request = new XMLHttpRequest();
    request.open('POST', url, false);
    request.setRequestHeader("Content-Type", "application/json");

    request.onload = function () {
        if (request.status === 200) {
            completed(JSON.parse(request.response));
        }
    };
    request.send(JSON.stringify(data));


}