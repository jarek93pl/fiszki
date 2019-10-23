function LoadControler() {

}

var IsAvtivate = false;
function MutiPlayerAvtivate() {
    if (!IsAvtivate) {
        IsAvtivate = true;

        LoadControler();
    }
}
function MutiPlayerDeavtivate() {

    PostAction('MultiPlayer/Unregister', {}, function () {

    });
}