﻿var prefixadress = ''; //'/Fiszka/'; 
function FicheResponse(name, ContentType, IdFile, id) {
    this.Name = name;
    this.ContentType = ContentType;
    this.IdFile = IdFile;
    this.id = id;
    this.ShowAlert = function () {
        alert('Name: ' + this.name + " ContentType:" + this.ContentType + " IdFile: " + this.IdFile + " id: " + id);

    };
    return this;
}
function LoadNameContentType() {

}
function PrefixToIdResponse(idRow) {
    return '#' + idRow + ' .';
}
function LoadFromIdResponse(idRow) {
    var prefix = PrefixToIdResponse(idRow);
    return FicheResponse($(prefix + 'NameFicheResponseRow').text(),
        $(prefix + 'TypeFichRow').text(),
        $(prefix + 'IdFileResponseRow').val(),
        idRow);

}
function SaveFromIdResponse(idRow, data) {
    var prefix = PrefixToIdResponse(idRow);
    $(prefix + 'TypeFichRow').text(data.ContentType);
    $(prefix + 'IdFileResponseRow').val(data.IdFile);
    $(prefix + 'NameFicheResponseRow').text(data.Name);

}
function LoadEditor() {
    var data = FicheResponse(
        $('#FicheNameEditor').val(),
        $('#TypePromptResponseEditor').val(),
        $('#IdFileResponseEditor').val(),
        $('.ClassFicheEditor').val());
    return data;
}
function SetEditor(data) {

    $('#FicheNameEditor').val(data.Name),
        $('#TypePromptResponseEditor').val(data.ContentType),
        $('#IdFileResponseEditor').val(data.IdFile),
        $('.ClassFicheEditor').val(data.id);
}

function LoadResponses() {

    var returnedArrer = [];
    $('.responseTableRow').each(function () {

        var CurrentRow = LoadFromIdResponse($(this).attr('id'));
        returnedArrer.push(
            {
                ContentTypeToDispley: CurrentRow.ContentType,
                Id: ChangeZero(CurrentRow.id),
                IdFile: CurrentRow.IdFile,
                Name: CurrentRow.Name
            }
        );
    }
    );
    return returnedArrer;
}
function ChangeZero(data) {
    if (isNaN(data)) {
        return 0;
    }
    else {
        return data;
    }
}
$('#TypePrompt').change(function () {

    if ($(this).val() === '0') {
        $('#AddFileDiv').hide();
    }
    else {
        $('#AddFileDiv').show();
    }
});

$('#ContainerficheResponsesEdit').on('change', '#TypePromptResponseEditor', function () {//4 to tekst

    if ($(this).val() === '0') {
        $('#PrompContainerResponse').hide();
    }
    else {
        $('#PrompContainerResponse').show();
    }
});

$('#AddFileDiv').on('change', '#PromptFile', function () {

    SendFile(document.getElementById('PromptFile'), 1, function (data) {
        $('#IdPromptFile').val(data.Id);
    });
});

$('#AddFileDivResponse').on('change', '#PromptFileResponse', function () {

    SendFile(document.getElementById('PromptFileResponse'), 2, function (data) {
        $('#IdFileResponseEditor').val(data.Id);
    });
});

$('#CreateResponse').click(function (e) {
    e.preventDefault();
    SetEditor(FicheResponse('', '', '', ''));
    $('#ficheResponsesEdit').show();
    $('#ResponseEditorAdd').show();
    $('#ResponseEditorEdit').hide();
    $('#CreateResponse').hide();

});
$('#Responses').on('click', '.EditResponse',
    function (e) {
        e.preventDefault();
        var idText = $(this).parents('.responseTableRow').attr('id');
        var rowData = LoadFromIdResponse(idText);
        SetEditor(rowData);
        $('#ResponseEditorAdd').hide();
        $('#ResponseEditorEdit').show();
        $('#ficheResponsesEdit').show();
        $('#CreateResponse').hide();
    });
$('#Responses').on('click', '.DeleteResponse',
    function (e) {
        e.preventDefault();
        var idText = $(this).parents('.responseTableRow').attr('id');
        if (!isNaN(idText) && parseInt(idText) > 0) {
            Post(prefixadress + 'DeleteResponse', { id: idText }, function () {
                $('#' + idText).remove();
            });
        }
        else {
            $('#' + idText).remove();
        }
    });

$('#ResponseEditorAdd').click(function (e) {
    e.preventDefault();

    $.get(prefixadress + 'ResponseRow', function (data) {
        $('#ResponsesTable').append(data);
        var dataEditor = LoadEditor();

        SaveFromIdResponse('IsNewFicheResponseRow', dataEditor);
        $('#IsNewFicheResponseRow').attr('id', GetGuid());
        $('#ficheResponsesEdit').hide();
        $('#CreateResponse').show();
    });

});
$('#ResponseEditorHide').click(function (e) {
    e.preventDefault();
    $('#ficheResponsesEdit').hide();
    $('#CreateResponse').show();
});
$('#ResponseEditorEdit').click(function (e) {
    e.preventDefault();

    var dataEditor = LoadEditor();
    SaveFromIdResponse(dataEditor.id, dataEditor);
    $('#ficheResponsesEdit').hide();
    $('#CreateResponse').show();
});
$('#SendFiche').click(function (e) {
    e.preventDefault();
    Post(prefixadress + 'Dodaj',
        {
            Prompt: $('#Prompt').val(),
            Response: $('#Response').val(),
            IntTypePrompt: $('#TypePrompt').val(),
            Id: $('#Id').val(),
            IdPromptFile: $('#IdPromptFile').val(),
            IdFicheSet: $('#IdFicheSet').val(),
            FicheResponses: LoadResponses()
        }
        , function () {

        });
});
$('.EditSet').click(function (e) {
    e.preventDefault();
    loadPageUsingUrl(e, $(this).attr('href'));
});