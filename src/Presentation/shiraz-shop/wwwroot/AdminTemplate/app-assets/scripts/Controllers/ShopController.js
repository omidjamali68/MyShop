var ShopController = function (shopService, managerService) {
    var SelectExistManager = function () {        
        var value = $("#exist-manager-id").val();
        
        var requestDto = {
            'managerId': value,
        };

        managerService.GetManager(requestDto, getManagerComplete, AjaxFailed);
    };

    var getManagerComplete = function (data) {
        console.log(data);
        $('#AssignManager_firstName').val(data.firstName);
        $('#AssignManager_lastName').val(data.lastName);
        $('#AssignManager_age').val(data.age);
        $('#AssignManager_mobile').val(data.mobileNumber);        
    };

    var Delete = function (shopId) {
        swal.fire({
            title: 'حذف فروشگاه',
            text: "کاربر گرامی از حذف فروشگاه مطمئن هستید؟",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            cancelButtonColor: '#7cacbe',
            confirmButtonText: 'بله ، فروشگاه حذف شود',
            cancelButtonText: 'خیر'
        }).then((result) => {
            if (result.value) {

                var requestDto = {
                    'ShopId': shopId,
                };

                shopService.Delete(requestDto, AjaxSuccess, AjaxFailed);
            }
        })
    };

    var ChangeStatus = function (shopId) {
        swal.fire({
            title: 'تغییر وضعیت فروشگاه',
            text: "کاربر گرامی از تغییر وضعیت فروشگاه مطمئن هستید؟",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            cancelButtonColor: '#7cacbe',
            confirmButtonText: 'بله ، تغییر وضعیت انجام شود',
            cancelButtonText: 'خیر'
        }).then((result) => {            
            if (result.value) {

                var requestDto = {
                    'ShopId': shopId,
                };

                shopService.ChangeStatus(requestDto, AjaxSuccess, AjaxFailed);
            }
        })
    };

    var ShowEditModal = function (shopId, name, address) {
        $('#Edit_Name').val(name)
        $('#Edit_ShopId').val(shopId)
        $('#Edit_Address').val(address)

        $('#EditShop').modal('show');
    };

    var Edit = function () {

        var shopId = $("#Edit_ShopId").val();
        var name = $("#Edit_Name").val();
        var address = $("#Edit_Address").val();

        var requestDto = {
            'ShopId': shopId,
            'Name': name,
            'Address': address,
        };

        shopService.Edit(requestDto, AjaxSuccess, AjaxFailed);

    };

    var ShowAssignManagerModal = function (shopId, name) {
        $('#AssignManager_ShopId').val(shopId)
        $('#AssignManager_Name').val(name)

        $('#AssignManager').modal('show');

    };

    var AssignManager = function () {

        var shopId = $("#AssignManager_ShopId").val();
        var firstName = $("#AssignManager_firstName").val();
        var lastName = $("#AssignManager_lastName").val();
        var age = $("#AssignManager_age").val();
        var mobile = $("#AssignManager_mobile").val();

        var requestDto = {
            'ShopId': shopId,
            'firstName': firstName,
            'lastName': lastName,
            'age': age,
            'mobileNumber': mobile
        };

        shopService.AssignManager(requestDto, AjaxSuccess, AjaxFailed);

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
        ChangeStatus: ChangeStatus,
        Edit: Edit,
        ShowEditModal: ShowEditModal,
        ShowAssignManagerModal: ShowAssignManagerModal,
        AssignManager: AssignManager,
        SelectExistManager: SelectExistManager
    }
}(ShopService, ManagerService);