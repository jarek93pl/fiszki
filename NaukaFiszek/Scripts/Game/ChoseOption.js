function ResponseFilter(IsCorrect) {
    return $('.ResponseDiv').filter(function (index) {
        return ($(this).children('.AnswerCheckbox').is(":checked") == $(this).children('.CorrectMark').val()) == IsCorrect;

    });
}
function AnswerDisabled() {

    $('.AnswerCheckbox').prop('disabled', true);
   
}
CheckAnswer = function () {
    return ResponseFilter(false).length == 0;
};

ShowAnswer = function (IsCorrect) {
    ResponseFilter(false).addClass('WrongResponse');
    ResponseFilter(true).addClass('CorrectResponse');
    AnswerDisabled();
};

AdmitedAnswer = function () {
    AnswerDisabled();
};