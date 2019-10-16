function AdmitedAnswerWrite() {
    $('#AnswerText').prop('disabled', true);
}

if ($('#TypeAnswer').val() == 1) {

    CheckAnswer = function () {
        return $('#CorrectAnswerText').text() === $('#AnswerText').val();
    };

    ShowAnswer = function (IsCorrect) {
        ShowAnswerText(IsCorrect);
    };

    AdmitedAnswer = AdmitedAnswerWrite;
}
if ($('#TypeAnswer').val() == 3) {

    ShowAnswer = function (IsCorrect) {
        ShowAnswerText(IsCorrect);
    };

    AdmitedAnswer = AdmitedAnswerWrite;
}
