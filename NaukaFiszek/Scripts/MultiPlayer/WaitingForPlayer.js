MutiPlayerAvtivate();
$('#StartGame').click(function () {
    PostAction('MultiPlayer/WaitingForPlayer',
        {
            GuidGame: $('#GuidGame').val()
        }
        , function () {

    });
});