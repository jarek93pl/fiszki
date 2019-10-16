$('#AdmitAnswer').hide();
var CountTry = 0;

function DontShowChar() {
    return $('.CharResponse').filter(function () {
        return $(this).text() === "";
    });
}

function ShowAnswerHangman() {
    $('.CharResponse').each(function () {
        ShowChar(this);
    });
}
function ShowChar(showingChar) {
    $(showingChar).text(showingChar.dataset['hidechar']);
}
function Missed() {
    CountTry++;
    $("#hangMan").css("background-position", (-75 * CountTry) + "px 0");
    CommonAdmitAnswer();
}
CheckAnswer = function () {
    if (DontShowChar().length === 0)
        return true;
    if (CountTry >= 6) {
        return false;
    }
    return null;
};

ShowAnswer = function (IsCorrect) {
    ShowAnswerHangman(IsCorrect);
    $('#UserTryText').prop('disabled', true);
    $('#CheckUserTryText').prop('disabled', true);
    $('.buttonChar').prop('disabled', true);
};

AdmitedAnswer = function () {

};

$('.buttonChar').click(function () {
    $(this).hide();
    var hideCharBefore = DontShowChar();
    var CountHideCharBefore = hideCharBefore.length;
    var SelectedChar = $(this).text();
    hideCharBefore.filter(function () {
        return this.dataset['hidechar'] === SelectedChar;
    }).each(function () {
        ShowChar(this);
    });
    if (CountHideCharBefore === DontShowChar().length) {
        Missed();
    }
    else {
        CommonAdmitAnswer();
    }

});
$('#CheckUserTryText').click(function () {
    var userResponse = $('#UserTryText').val();
    if (userResponse === $('#CorrectAnswerText').text()) {

        ShowAnswerHangman();
        CommonAdmitAnswer();
    }
    else {
        $('#UserTryText').val('');
        $('#MissedList').append('<li>' + userResponse+'</li>');
        Missed();
    }
});