var ShopService = function () {
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

    var ChangeStatus = function (requestDto, ajaxSuccess, ajaxFailed) {
        $.ajax({
            contentType: 'application/x-www-form-urlencoded',
            dataType: 'json',
            type: "PATCH",
            url: "ShopSatusChange",
            data: requestDto,
            success: ajaxSuccess,
            error: ajaxFailed
        });
    };

    var Edit = function (requestDto, ajaxSuccess, ajaxFailed) {
        $.ajax({
            contentType: 'application/x-www-form-urlencoded',
            dataType: 'json',
            type: "PUT",
            url: "Edit",
            data: requestDto,
            success: ajaxSuccess,
            error: ajaxFailed
        });
    };

    var AssignManager = function (requestDto, ajaxSuccess, ajaxFailed) {
        $.ajax({
            contentType: 'application/x-www-form-urlencoded',
            dataType: 'json',
            type: "POST",
            url: baseApplicationPath + 'admin/shopmanagers/add',
            data: requestDto,
            success: ajaxSuccess,
            error: ajaxFailed
        });
    };

    return {
        Delete: Delete,
        ChangeStatus: ChangeStatus,
        Edit: Edit,
        AssignManager: AssignManager
    }
}();