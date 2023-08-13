var ManagerService = function () {
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

    var GetManager = function (requestDto, onSuccess, onFailure) {
        $.ajax({
            contentType: 'application/x-www-form-urlencoded',
            dataType: 'json',
            type: "GET",
            url: baseApplicationPath + 'admin/managers/getbyid',
            data: requestDto,
            success: onSuccess,
            error: onFailure
        });
    };

    return {
        Delete: Delete,
        Edit: Edit,
        GetManager: GetManager
    }
}();