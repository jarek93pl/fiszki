$('.DeleteButton').click(function (e) {
    e.preventDefault();
    var row = $(this).parents('tr');
    PostAction(TeachBagAdress( 'Delete'), { id: row.attr('id') }, function () {
        row.remove();
    });
});
$('.AddNewButton').click(loadPage);
$('.LearnButton').click(loadPage);