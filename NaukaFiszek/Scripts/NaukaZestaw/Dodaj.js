$('#NewBagLink').click(function (e) {
    e.preventDefault();
    SetBagEditor(new Bag('1:00:00', '', '', '','0'));
    ShowBagEditor();
});

$('#SendTeachSet').click(function (e) {
    Post(TeachBagAdress('Dodaj'),
        {
            IdSetFiche: $('#IdSetFiche').val(),
            Name: $('#Name').val(),
            teachBags: LoadTeachBag()
        },
        function () {

        }
    );
    e.preventDefault();
});