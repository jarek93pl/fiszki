$('AdmitAnswear').hide();
var CountTry = 0;

function DontShowChar() {
    return $('.CharResponse').filter(function () {
        return $(this).text() === "";
    });
}

function ShowAnswearHangman() {
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
    CommonAdmitAnswear();
}
CheckAnswear = function () {
    if (DontShowChar().length === 0)
        return true;
    if (CountTry >= 6) {
        return false;
    }
    return null;
};

ShowAnswear = function (IsCorrect) {
    ShowAnswearHangman(IsCorrect);
    $('#UserTryText').prop('disabled', true);
    $('#CheckUserTryText').prop('disabled', true);
    $('.buttonChar').prop('disabled', true);
};

AdmitedAnswear = function () {

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
    if (hideCharBefore.length === DontShowChar().length) {
        Missed();
    }
    else {
        CommonAdmitAnswear();
    }

});
$('#CheckUserTryText').click(function () {
    var userResponse = $('#UserTryText').val();
    if (userResponse === $('#CorrectAnswearText').text()) {

        ShowAnswearHangman();
        CommonAdmitAnswear();
    }
    else {
        $('#UserTryText').val('');
        $('#MissedList').append('<li>' + userResponse+'</li>');
        Missed();
    }
});