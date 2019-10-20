$('#NewBagLink').click(function (e) {
    e.preventDefault();
    SetBagEditor(new Bag('1:00:00', '', '', '', '0'));
    ShowBagEditor();
});

$('#SendTeachSet').click(function (e) {

    if (ValidatingSendTeachSet()) {

        var iddSetFiche = $('#IdSetFiche').val();
        if (iddSetFiche === '0') {
            iddSetFiche = $('#FicheSetList option:selected').attr('id');
        }

        var limitTime = 0;
        if ($('#IsLimitTimeSet').is(":checked")) {
            limitTime = parseInt($('#LimitTimeInSekSet').val());
        }
        PostAction(TeachBagAdress('Add'),
            {
                FirstTypeAnswerInt: $('#FirstTypeAnswer').val(),
                IdSetFiche: iddSetFiche,
                Name: $('#Name').val(),
                LimitTimeInSekSet: limitTime,
                teachBags: LoadTeachBag()
            },
            function () {
                loadPageUsingUrl(e, "NaukaZestaw/List");
            }
        );
        e.preventDefault();
    }
});

$('#IsLimitTimeSet').change(function () {
    if ($('#IsLimitTimeSet').is(":checked")) {
        $('#LimitTimeInSekSetContainer').show();
    }
    else {
        $('#LimitTimeInSekSetContainer').hide();
        $('#LimitTimeInSekSet').val('0');
    }
});
function ValidatingSendTeachSet() {
    return ValidatingControlMinLenght2ById('Name')
        & ValidatingControl("#LimitTimeInSekSet", ValidatingLimitTimeInSekSet, "Wartość limitu czsu musi być dodatnią liczbą");
}
function ValidatingLimitTimeInSekSet() {
    if ($('#IsLimitTimeSet').is(":checked")) {
        var intValue = parseInt($('#LimitTimeInSekSet').val());
        return !isNaN(intValue) && intValue > 0;
    }
    else {
        return true;
    }
}
