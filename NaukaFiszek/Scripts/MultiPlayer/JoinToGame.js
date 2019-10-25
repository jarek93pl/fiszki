$('#LogiInToGame').click(function (e) {
    e.preventDefault();
    PostAction('/MultiPlayer/JoinToGame', { GuidGame: $('#GuidGame').val() }, function (data) {
        if (data.GuidGame === null) {
            ValidatingControl('#GuidGame', function () { return false; },
                "Gra o podanym Guidze nie istnieje lub już się rozpoczeła, lub jesteś już zalagowany do innej gry spróbuj się wylogować i spróbować jeszcze raz");
        }
        else {
            loadPageUsingUrl(e, '/MultiPlayer/WaitingForPlayer');
        }
    });

});