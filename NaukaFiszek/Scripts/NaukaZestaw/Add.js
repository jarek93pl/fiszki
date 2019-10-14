$('#NewBagLink').click(function (e) {
    e.preventDefault();
    SetBagEditor(new Bag('1:00:00', '', '', '', '0'));
    ShowBagEditor();
});

$('#SendTeachSet').click(function (e) {

    var iddSetFiche = $('#IdSetFiche').val();
    if (iddSetFiche === '0') {
        iddSetFiche = $('#FicheSetList option:selected').attr('id');
    }
    PostAction(TeachBagAdress('Add'),
        {
            FirstTypeAnswearInt: $('#FirstTypeAnswear').val(),
            IdSetFiche: iddSetFiche,
            Name: $('#Name').val(),
            teachBags: LoadTeachBag()
        },
        function () {

        }
    );
    e.preventDefault();
});