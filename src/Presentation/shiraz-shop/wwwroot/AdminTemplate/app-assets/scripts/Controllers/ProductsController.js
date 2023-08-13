var ProductsController = function (productService) {
    var Delete = function (productId) {
        swal.fire({
            title: 'حذف محصول',
            text: "کاربر گرامی از حذف محصول مطمئن هستید؟",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            cancelButtonColor: '#7cacbe',
            confirmButtonText: 'بله ، محصول حذف شود',
            cancelButtonText: 'خیر'
        }).then((result) => {
            if (result.value) {

                var requestDto = {
                    'productId': productId,
                };

                productService.Delete(requestDto, AjaxSuccess, AjaxFailed);
            }
        });
    };    

    var AjaxSuccess = function (data) {
        if (data.isSuccess == true) {
            swal.fire(
                'موفق!',
                'عملیات با موفقیت انجام شد',
                'success'
            ).then(function (isConfirm) {
                location.reload();
            });
        }
        else {
            swal.fire(
                'هشدار!',
                data.error.message,
                'warning'
            );
        }
    };

    var AjaxFailed = function (request, status, error) {
        alert(request.responseText);
    };

    return {
        Delete: Delete,
    };

}(ProductService);