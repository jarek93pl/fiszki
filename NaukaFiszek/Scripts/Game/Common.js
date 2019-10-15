var AdmitedAnswear = null;//na potrzeby gry wielosobowej 
var CheckAnswear = null;
var ShowAnswear = null;
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
    if (CheckAnswear !== null) {
        var IsCorrectAnswear = CheckAnswear();
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
