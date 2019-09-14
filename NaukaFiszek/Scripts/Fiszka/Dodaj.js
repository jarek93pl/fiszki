$('#TypePrompt').change(function () {
    if ($('#dropDownId').val() === 'Text') {
        $('Responses').hide();
    }
    else {
        $('Responses').show();
    }
});

$('*').on('change', '#PromptFile' ,function () {
    
    SendFile(document.getElementById('PromptFile'),1);
});