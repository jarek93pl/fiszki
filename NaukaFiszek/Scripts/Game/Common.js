var AdmitedAnswear = null;//na potrzeby gry wielosobowej 
var CheckAnswear = null;
var ShowAnswear = null;
var TimeEnded = false;
var TimeToEnd=0;
if ($('#LimitTimeSek').val() !== '0') {
    TimeToEnd = parseInt($('#LimitTimeSek').val());
  var TimerToEnd=  setInterval(function () {
        TimeToEnd--;
        $('#TimeText').text("pozostało " + TimeToEnd + " sek");
        if (TimeToEnd<0) {
            TimeEnded = true;
            $('#TimeText').text("Czas się skończył");
            clearInterval(TimerToEnd);
            CommonAdmitAnswear();
        }
    }, 1000);
}
function CommonShowAnswear(result) {
    if (ShowAnswear !== null) {
        ShowAnswear(result);
    }
    $('#NextFiche').show();
}
function CommonAdmitAnswear() {
    if (AdmitedAnswear !== null) {
        AdmitedAnswear();
    }
    if (CheckAnswear !== null || TimeEnded) {
        var IsCorrectAnswear = CheckAnswear();

        if (TimeEnded) {
            IsCorrectAnswear = false;
        }
        if (IsCorrectAnswear === null) {
            return;
        }
       
        CommonShowAnswear(IsCorrectAnswear);
        SendAnswear(IsCorrectAnswear);
    }
    else {
        CommonShowAnswear(null);
        $('#UserChoseDiv').show();
    }
}
function ShowAnswearText(IsCorrect) {
    var className;
    if (IsCorrect !== null) {

        if (IsCorrect) {
            className = 'CorrectResponse';
        }
        else {
            className = 'WrongResponse';
        }
    } 
    $('#CorrectAnswearLabel').show();
    $('#CorrectAnswearText').show();
    $('#CorrectAnswearText').addClass(className);
}
function SendAnswear(IsCorrect) {

    PostAction('SendAnswear', { idTeachSet: $('#IdTeachSet').val(), IdFiche: $('#Fiche_Id').val(), IsCorrect: IsCorrect }, function () {

    });
}
$('#AdmitAnswear').click(function () {
    $(this).hide();
    CommonAdmitAnswear();
});

$('#Know').click(function () {
    SendAnswear(true);
});
$('#NotKnow').click(function () {
    SendAnswear(false);

});
$('#NextFiche').click(loadPage);
