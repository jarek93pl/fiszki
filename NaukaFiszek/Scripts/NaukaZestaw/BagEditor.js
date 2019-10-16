
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
    var load = LoadDataFromBagEditor();
    e.preventDefault();
    HideEditor();
    if (load.Id === '') {
        CreateNew();
    }
    else {
        SetRowById(load);
    }
});

$('#BagEditorHider').click(function (e) {
    e.preventDefault();
    HideEditor();
});
$('#BagEditor #IsLimitTime').change(function () {
    var editorData = LoadDataFromBagEditor();
    if (editorData.IsLimitTime) {
        $('#BagEditor #LimitTimeInSekContainer').show();
    }
    else {
        $('#BagEditor #LimitTimeInSekContainer').hide();
    }
});
