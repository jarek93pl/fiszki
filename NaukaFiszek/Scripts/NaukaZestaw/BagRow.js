function LoadTextTypeAnswear(data) {
    return data;
}
function SetRow(control, data) {
    control.children('#PeriodTime').text(data.PeriodTime);
    control.children('#IsLimitTime').text(data.IsLimitTime);
    control.children('#LimitTimeInSek').text(data.LimitTimeInSek);
    control.children('#Id').text(data.Id);
    control.children('#TypeAnswear').val(data.TypeAnswear);
    control.children('#TypeAnswearText').text(LoadTextTypeAnswear(data.TypeAnswear));
    control.attr('id', data.Id);
}
function GetRow(control) {
    return new Bag(
        control.children('#PeriodTime').text(),
        control.children('#IsLimitTime').text(),
        control.children('#LimitTimeInSek').text(),
        control.children('#Id').text(),
        control.children('#TypeAnswear').val());
}

function SetLastRow(data) {
    SetRow($('#BagTable tr').last(), data);
}
function SetRowById(data) {
    SetRow($('#BagTable #' + data.Id), data);
}
$('#BagTable').on('click', '#EditBag', function (e) {
    e.preventDefault();
    SetBagEditor(GetRow($(this).parents('tr')));
    ShowBagEditor();
});

$('#BagTable').on('click', '#RemoveBag', function (e) {
    e.preventDefault();
    $(this).parents('tr').remove();
});
function LoadTeachBag() {

    var returnedArrer = [];
    $('#BagTable tr.dataRow').each(function () {
        var CurrentRow = GetRow($(this));
        returnedArrer.push(
            {
                PeriodTime: CurrentRow.PeriodTime,
                TypeAnswearInt: CurrentRow.TypeAnswearInt,
                IsLimitTime: CurrentRow.IsLimitTime,
                LimitTimeInSek: CurrentRow.LimitTimeInSek
            }
        );
    });
    return returnedArrer;
}