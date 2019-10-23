﻿
var source = new EventSource('MultiPlayer/RefreshListPlayer');
source.onmessage = function (e) {
    if (e.data === "break") {
        source.close();
        $('#IsDisactive').show();
        return;
    }
    var returnedData = JSON.parse(e.data);
    returnedData.ChangeLogs.forEach(function (row) {
        if (row.ActionName === "Register") {
            $('#PlayerResult').append('<tr class="RowPlayerDetails"><td class="Name">' + row.Login + '</td><td class="Point">' + row.Point + '</td></tr>');
        }
        if (row.ActionName === "Leave") {
            $('td:contains(' + row.Login + ')').parents('tr').remove();
        }
    });
};