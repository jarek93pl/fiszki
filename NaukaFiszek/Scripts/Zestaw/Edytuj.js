$('a').click(function (e) {
    loadPageUsingUrl(e, $(this).attr('href'));
});