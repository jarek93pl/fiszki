function LoadTextTypeAnswer(data) {
    return data;
}
function SetRow(control, data) {
    control.children('#PeriodTime').text(data.PeriodTime);
    control.children('#IsLimitTime').text(data.IsLimitTime);
    control.children('#LimitTimeInSek').text(data.LimitTimeInSek);
    control.children('#Id').text(data.Id);
    control.children('#TypeAnswer').val(data.TypeAnswer);
    control.children('#TypeAnswerText').text(LoadTextTypeAnswer(data.TypeAnswer));
    control.attr('id', data.Id);
}
function GetRow(control) {
    return new Bag(
        control.children('#PeriodTime').text(),
        control.children('#IsLimitTime').text(),
        control.children('#LimitTimeInSek').text(),
        control.children('#Id').text(),
        control.children('#TypeAnswer').val());
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
                TypeAnswerInt: CurrentRow.TypeAnswerInt,
                IsLimitTime: CurrentRow.IsLimitTime,
                LimitTimeInSek: CurrentRow.LimitTimeInSek
            }
        );
    });
    return returnedArrer;
}