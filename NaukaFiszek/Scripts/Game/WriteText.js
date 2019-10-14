function AdmitedAnswearWrite() {
    $('#AnswearText').prop('disabled', true);
}

if ($('#TypeAnswear').val() == 1) {

    CheckAnswear = function () {
        return $('#CorrectAnswearText').text() === $('#AnswearText').val();
    };

    ShowAnswear = function (IsCorrect) {
        ShowAnswearText(IsCorrect);
    };

    AdmitedAnswear = AdmitedAnswearWrite;
}
if ($('#TypeAnswear').val() == 3) {

    ShowAnswear = function (IsCorrect) {
        ShowAnswearText(IsCorrect);
    };

    AdmitedAnswear = AdmitedAnswearWrite;
}
