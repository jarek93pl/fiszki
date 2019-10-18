
function HideEditor() {
    $('#NewBagLink').show();
    $('#BagEditor').hide();
}
function LoadDataFromBagEditor() {
    return new Bag(
        $('#BagEditor #PeriodTime').val(),
        $('#BagEditor #IsLimitTime').is(":checked"),
        $('#BagEditor #LimitTimeInSek').val(),
        $('#BagEditor #Id').val(),
        $('#BagEditor #TypeAnswer').val()
    );
}
function SetBagEditor(data) {
    $('#BagEditor #PeriodTime').val(data.PeriodTime);
    $('#BagEditor #IsLimitTime').prop('checked', data.IsLimitTime === 'true');
    $('#BagEditor #LimitTimeInSek').val(data.LimitTimeInSek);
    $('#BagEditor #Id').val(data.Id);
    $('#BagEditor #TypeAnswer').val(data.TypeAnswer);
    SetVisiblityLimitTimeInSekContainer();
}

function CreateNew(e) {
    $.get(TeachBagAdress('BagRow'), function (data) {
        $('#BagTable').append(data);
        var editorData = LoadDataFromBagEditor();
        editorData.Id = GetGuid();
        SetLastRow(editorData);
    });
}
$('#BagApprover').click(function (e) {
    if (ValidatingBagApprover()) {
        var load = LoadDataFromBagEditor();
        e.preventDefault();
        HideEditor();
        if (load.Id === '') {
            CreateNew();
        }
        else {
            SetRowById(load);
        }
    }
});

$('#BagEditorHider').click(function (e) {
    e.preventDefault();
    HideEditor();
});
$('#BagEditor #IsLimitTime').change(SetVisiblityLimitTimeInSekContainer);

function SetVisiblityLimitTimeInSekContainer() {
    var editorData = LoadDataFromBagEditor();
    if (editorData.IsLimitTime) {
        $('#BagEditor #LimitTimeInSekContainer').show();
    }
    else {
        $('#BagEditor #LimitTimeInSekContainer').hide();
    }
}
function ValidatingBagApprover() {
    return ValidatingControl("#LimitTimeInSek", ValidatingLimitTimeInSek, "Wartość limitu czsu musi być dodatnią liczbą") &
        ValidatingControl("#BagEditor #PeriodTime", ValidatingPeriodTime, "Wartość okresu czasu nie odopowiada formatowi hh;mm;ss");

}
function ValidatingLimitTimeInSek() {
    if ($('#BagEditor #IsLimitTime').is(":checked")) {
        var intValue = parseInt($('#BagEditor  #LimitTimeInSek').val());
        return !isNaN(intValue) && intValue > 0;
    }
    else {
        return true;
    }
}
function ValidatingPeriodTime() {
    return /^\d{1,2}:\d{1,2}:\d{1,2}$/.test($('#BagEditor #PeriodTime').val());
}