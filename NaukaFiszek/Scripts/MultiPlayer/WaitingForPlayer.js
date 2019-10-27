MutiPlayerAvtivate();
$('#StartGame').click(function () {
    PostAction('MultiPlayer/WaitingForPlayer',
        {
            GuidGame: $('#GuidGame').val()
        }
        , function (data) {
            if (!data.IsSuccess) {
                ValidatingControl("#StartGame", function () { return false; }, "gry nie udało się stworzyć najprawdopodobniej w tym zestawie nie ma odpowiedniej fiszki");
                $('#StartGame').hide();
                $('#HederStart').text("Ten zestaw nie ma fiszek do tej gry wielosobowej");
            }

    });
});