var prefixadressMulti = '';// '/MultiPlayer/';
$('#TypeAnswer').val(0);
$('#CreateGame').click(function (e) {

    e.preventDefault();
    if (ValidatingCreateGame()) {

        var limitTime = 0;
        if ($('#IsLimitTime').is(":checked")) {
            limitTime = parseInt($('#LimitTimeInSek').val());

        }
            PostAction(AdressMultiPlayer('CreateGame'),
                {
                    TypeAnswerInt: $('#TypeAnswer').val(),
                    IdSetFiche: $('#FicheSetList option:selected').attr('id'),
                    LimitTimeInSek: limitTime
                },
                function () {
                    AdressMultiPlayer('WaitingForPlayer');
                }
            );
    }
});
$('#IsLimitTime').change(function () {
    if ($('#IsLimitTime').is(":checked")) {
        $('#LimitTimeInSekContainer').show();
    }
    else {
        $('#LimitTimeInSekContainer').hide();
        $('#LimitTimeInSek').val('0');
    }
});
function ValidatingCreateGame() {
    return ValidatingControl('#LimitTimeInSek', ValidatingLimitTimeInSek, "Wartość limitu czsu musi być dodatnią liczbą") &
        ValidatingControl('#TypeAnswer', ValidatingTypeAnswer,"Musisz wybrać typ odpowiedzi");
}
function AdressMultiPlayer(data) {
    return prefixadressMulti + data;
}

function ValidatingTypeAnswer() {
    if ($('#TypeAnswer').val() === '0') {
        return false;
    }
    return true;
}
function ValidatingLimitTimeInSek() {
    if ($('#IsLimitTime').is(":checked")) {
        var intValue = parseInt($('#LimitTimeInSek').val());
        return !isNaN(intValue) && intValue > 0;
    }
    else {
        return true;
    }
}