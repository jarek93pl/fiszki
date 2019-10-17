$('a:not(.RemoveFiche)').click(function (e) {
    loadPageUsingUrl(e, $(this).attr('href'));
});
$('.RemoveFiche').click(function (e) {
    e.preventDefault();
    var $control = $(this);
    PostAction("Fiszka/Delete", { id: $control.attr('id') }, function () {
        $control.parents('tr').remove();
    });
});
