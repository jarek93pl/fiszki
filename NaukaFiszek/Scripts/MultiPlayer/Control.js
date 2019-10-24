var sourceComannd = null;
function LoadControler() {
    sourceComannd = new EventSource('MultiPlayer/GetCommand');
    sourceComannd.onmessage=function (e) {
        if (e.data === "break") {
            SourceUnload();
            return;
        }
        var table = e.data.split(':');
        switch (table[0]) {
            case "ShowResponse":
                if (table[1] === $('#Fiche_Id').val()) {
                    //CommonShowAnswer(null);
                }
                break;
            case "LoadNextFiche":
                loadPageUsingUrl(null, 'MultiPlayer/GetFiche');
                break;
            case "ShowList":
                loadPageUsingUrl(null, 'MultiPlayer/WaitingForPlayer');
                break;
            default:
        }
    };
}

function MutiPlayerAvtivate() {
    if (sourceComannd === null) {
        LoadControler();
    }
}
function MutiPlayerDeavtivate() {

    PostAction('MultiPlayer/Unregister', {}, function () {
    });
    SourceUnload();
}
function SourceUnload() {
    if (sourceComannd !== null) {
        sourceComannd.close();
    }
    sourceComannd = null;
}