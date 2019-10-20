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

    $('.ResponseDiv').filter(function (index) { return $(this).children('.CorrectMark').val() === '1'; }).addClass('CorrectResponse');
    $('.ResponseDiv').filter(function (index) { return $(this).children('.CorrectMark').val() === '0' && $(this).children('.AnswerCheckbox').is(":checked") === true; }).addClass('WrongResponse');

    AnswerDisabled();
};

AdmitedAnswer = function () {
    AnswerDisabled();
};