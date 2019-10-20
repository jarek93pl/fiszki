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
            CommonShowAnswer(false);
            $('#AdmitAnswer').hide();
        }
    }, 1000);
}
function CommonShowAnswer(result) {
    if (ShowAnswer !== null) {
        ShowAnswer(result);
    }
    $('#NextFiche').show();
    $('#UserChoseDiv').hide();
    if (result === true) {
        $('#PromptGameDiv').css('background-color', 'green');
    }
    if (result === false) {
        $('#PromptGameDiv').css('background-color', 'red');
    }
}
function CommonAdmitAnswer() {
    if (AdmitedAnswer !== null) {
        AdmitedAnswer();
    }
    if (CheckAnswer !== null || TimeEnded) {
        var IsCorrectAnswer;

        if (TimeEnded) {
            IsCorrectAnswer = false;
        }
        else {
            IsCorrectAnswer = CheckAnswer();
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
    $('#CorrectAnswerDiv').show();
    $('#CorrectAnswerText').addClass(className);
}
function SendAnswer(IsCorrect) {
    if (TimeToEnd>0) {
        clearInterval(TimerToEnd);
        $('#TimeText').hide();
    }
    PostAction('Gra/SendAnswer', { idTeachSet: $('#IdTeachSet').val(), IdFiche: $('#Fiche_Id').val(), IsCorrect: IsCorrect }, function () {

    });
}
$('#AdmitAnswer').click(function () {
    $(this).hide();
    CommonAdmitAnswer();
});

$('#Know').click(function () {
    SendAnswer(true);
    $('#UserChoseDiv').hide();
});
$('#NotKnow').click(function () {
    SendAnswer(false);
    $('#UserChoseDiv').hide();

});
$('#NextFiche').click(loadPage);
