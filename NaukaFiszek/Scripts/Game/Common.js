var AdmitedAnswer = null;//na potrzeby gry wielosobowej 
var CheckAnswer = null;
var ShowAnswer = null;
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
            CommonAdmitAnswer();
        }
    }, 1000);
}
function CommonShowAnswer(result) {
    if (ShowAnswer !== null) {
        ShowAnswer(result);
    }
    $('#NextFiche').show();
}
function CommonAdmitAnswer() {
    if (AdmitedAnswer !== null) {
        AdmitedAnswer();
    }
    if (CheckAnswer !== null || TimeEnded) {
        var IsCorrectAnswer = CheckAnswer();

        if (TimeEnded) {
            IsCorrectAnswer = false;
        }
        if (IsCorrectAnswer === null) {
            return;
        }
       
        CommonShowAnswer(IsCorrectAnswer);
        SendAnswer(IsCorrectAnswer);
    }
    else {
        CommonShowAnswer(null);
        $('#UserChoseDiv').show();
    }
}
function ShowAnswerText(IsCorrect) {
    var className;
    if (IsCorrect !== null) {

        if (IsCorrect) {
            className = 'CorrectResponse';
        }
        else {
            className = 'WrongResponse';
        }
    } 
    $('#CorrectAnswerLabel').show();
    $('#CorrectAnswerText').show();
    $('#CorrectAnswerText').addClass(className);
}
function SendAnswer(IsCorrect) {

    PostAction('SendAnswer', { idTeachSet: $('#IdTeachSet').val(), IdFiche: $('#Fiche_Id').val(), IsCorrect: IsCorrect }, function () {

    });
}
$('#AdmitAnswer').click(function () {
    $(this).hide();
    CommonAdmitAnswer();
});

$('#Know').click(function () {
    SendAnswer(true);
});
$('#NotKnow').click(function () {
    SendAnswer(false);

});
$('#NextFiche').click(loadPage);
