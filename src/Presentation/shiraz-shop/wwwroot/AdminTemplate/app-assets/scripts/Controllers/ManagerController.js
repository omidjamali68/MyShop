var ManagerController = function (managerService) {
    var Delete = function (managerId) {
        swal.fire({
            title: 'حذف مدیر',
            text: "کاربر گرامی از حذف مدیر مطمئن هستید؟",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            cancelButtonColor: '#7cacbe',
            confirmButtonText: 'بله ، مدیر حذف شود',
            cancelButtonText: 'خیر'
        }).then((result) => {
            if (result.value) {

                var requestDto = {
                    'managerId': managerId,
                };

                managerService.Delete(requestDto, AjaxSuccess, AjaxFailed);
            }
        })
    };

    var ShowEditModal = function (id, firstName, lastName, age) {
        $('#Edit_FirstName').val(firstName)
        $('#Edit_Id').val(id)
        $('#Edit_LastName').val(lastName)
        $('#Edit_Age').val(age)

        $('#EditForm').modal('show');
    };

    var Edit = function () {

        var id = $("#Edit_Id").val();
        var firstName = $("#Edit_FirstName").val();
        var lastName = $("#Edit_LastName").val();
        var age = $("#Edit_Age").val();

        var requestDto = {
            'Id': id,
            'FirstName': firstName,
            'LastName': lastName,
            'Age': age
        };

        managerService.Edit(requestDto, AjaxSuccess, AjaxFailed);

    };

    var AjaxSuccess = function (data) {
        if (data.isSuccess == true) {
            swal.fire(
                'موفق!',
                data.message[0],
                'success'
            ).then(function (isConfirm) {
                location.reload();
            });
        }
        else {
            swal.fire(
                'هشدار!',
                data.message[0],
                'warning'
            );
        }
    };

    var AjaxFailed = function (request, status, error) {
        alert(request.responseText);
    };

    return {
        Delete: Delete,
        ShowEditModal: ShowEditModal,
        Edit: Edit
    }
}(ManagerService);