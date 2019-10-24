var EndIndex = 0;
var sourcePlayer = new EventSource('MultiPlayer/RefreshListPlayer');
sourcePlayer.onmessage = function (e) {
    if (e.data === "break") {
        sourcePlayer.close();
        $('#IsDisactive').show();
        return;
    }
    var returnedData = JSON.parse(e.data);
    returnedData.ChangeLogs.forEach(function (row) {
        if (EndIndex >= row.EndIndex) {
            EndIndex++;
            if (row.ActionName === "Register") {
                $('#PlayerResult').append('<tr class="RowPlayerDetails"><td class="Name">' + row.Login + '</td><td class="Point">' + row.Point + '</td></tr>');
            }
            if (row.ActionName === "Leave") {
                $('td:contains(' + row.Login + ')').parents('tr').remove();
            }
        }
    });
};