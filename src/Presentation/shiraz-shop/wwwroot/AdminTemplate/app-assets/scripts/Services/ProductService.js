var ProductService = function () {
    var Delete = function (requestDto, ajaxSuccess, ajaxFailed) {
        $.ajax({
            contentType: 'application/x-www-form-urlencoded',
            dataType: 'json',
            type: "DELETE",
            url: "Delete",
            data: requestDto,
            success: ajaxSuccess,
            error: ajaxFailed
        });
    };

    return {
        Delete: Delete,
    };
}();