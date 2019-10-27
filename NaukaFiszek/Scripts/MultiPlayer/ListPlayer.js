var EndIndex = 0;
if ($('#GameIsStated').val() == 'False') {
    var sourcePlayer = new EventSource('MultiPlayer/RefreshListPlayer');
}
sourcePlayer.onmessage = LoadList;

function LoadList(e) {
    if ($('#GameIsStated').val() == 'True') {
        sourcePlayer.close();
    }
    if (e.data === "break") {
        sourcePlayer.close();
        $('#IsDisactive').show();
        return;
    }
    returnedData = JSON.parse(e.data);
    $('.RowPlayerDetails').remove();
    returnedData.forEach(function (row) {
        $('#PlayerResult').append('<tr class="RowPlayerDetails"><td class="Name">' + row.Name + '</td><td class="Point">' + row.Point + '</td></tr>');
    });
}