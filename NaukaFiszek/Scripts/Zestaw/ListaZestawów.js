$('#addSet').click(function (e) {
    loadPageUsingUrl(e, $(this).attr('href'));
});

$('.Remuver').click(function (e) {
    e.preventDefault();
    var id = $(this).attr('id');
    PostAction(this.href, { "id": id }, function () {
        $('.SetElement#' + id).remove();
    });
});

$('.EditSet').click(function (e) {
    e.preventDefault();
    var id = $(this).attr('id');
    loadPageUsingUrl(e, $(this).attr('href') + '/' + id);
    });