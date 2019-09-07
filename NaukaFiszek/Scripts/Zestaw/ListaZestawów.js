$('#addSet').click(function (e) {
    loadPageUsingUrl(e, $(this).attr('href'));
});
$('.Remuver').click(function (e) {
    e.preventDefault();
    var id = $(this).attr('id');
    Post(this.href, { "id": id }, function () {
        $('.SetElement#' + id).remove();
    });
});