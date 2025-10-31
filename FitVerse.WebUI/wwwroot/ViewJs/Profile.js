$(function () {
   
    ChangePersonalInfo();
    ChangeRole();
    ChangePasswordByAdmin();
    ChangePasswordByUser();
})

function ChangePersonalInfo() {
    $('#personalForm').on('submit', function (e) {
        e.preventDefault(); // prevent full form submission

        $.ajax({
            url: '/Admin/Users/ChangePasswordByAdmin',
            method: 'POST',
            data: {
                'UserInfo.UserName': $('#hiddenUserName').val(),
                'ChangePasswordByAdmin.Password': $('#ChangePasswordByAdmin_Password').val(),
                'ChangePasswordByAdmin.ConfirmPassword': $('#ChangePasswordByAdmin_ConfirmPassword').val()
            },
            success: function (res) {
                if (res.success || res.Success) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Success',
                        text: res.message || res.Message,
                        timer: 2000,
                        showConfirmButton: false
                    });
                    $('#AdminFunctions')[0].reset();
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Failed',
                        text: res.message || res.Message
                    });
                }
            },
            error: function () {
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: 'Something went wrong while changing the password.'
                });
            }
        });

    });

}

function ChangeRole() {

    $('#select-role').on('change', function () {
        var oldRole= $(this).val();
        Swal.fire({
            title: 'Change Role',
            text: 'Are you sure you want to change the role?',
            icon: 'warning',
            showConfirmButton: true,
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, change it!'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    method: 'POST',
                    url: '/Admin/Users/ChangeUserRole',
                    data: {
                        UserName: $('#hiddenUserName').val(),
                        Role: $('#select-role').val()
                    },
                    success: function (response) {
                        if (response.Succeeded) {
                            Swal.fire({
                                title: "Role Changed",
                                text: response.message,
                                icon: "success"
                            });
                            
                            location.reload();

                        } else {
                            Swal.fire({
                                title: "Error",
                                text: response.message || "Failed to change role.",
                                icon: "error"
                            });

                            // Revert to old role on failure
                            $('#select-role').val(oldRole);
                        }
                    },
                    error: function () {
                        Swal.fire("Failed, Try Again");
                    }
                });
            }
        });
    });
}

function ChangePasswordByAdmin() {
    $('#AdminFunctions').on('submit', function (e) {
        e.preventDefault();

        
        $.ajax({
            url: '/Admin/Users/ChangePasswordByAdmin',
            method: 'POST',
            data: {
                'UserInfo.UserName': $('#hiddenUserName').val(),
                'ChangePasswordByAdmin.Password': $('#ChangePasswordByAdmin_Password').val(),
                'ChangePasswordByAdmin.ConfirmPassword': $('#ChangePasswordByAdmin_ConfirmPassword').val()
            }, 
            success: function (res) {
                if (res.success || res.Success) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Success',
                        text: res.message || res.Message,
                        timer: 2000,
                        showConfirmButton: false
                    });
                    $('#AdminFunctions')[0].reset();
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Failed',
                        text: res.message || res.Message
                    });
                }
            },
            error: function () {
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: 'Something went wrong while changing the password.'
                });
            }
        });
    });
}

function ChangePasswordByUser() {
    $('#passwordForm').on('submit', function (e) {
        e.preventDefault();



        $.ajax({
            url: '/Admin/Users/ChangePasswordByUser',
            method: 'POST',
            data: {
                'UserInfo.UserName': $('#hiddenUserName').val(),
                'ChangePasswordByUser.OldPassword': $('#ChangePasswordByUser_OldPassword').val(),
                'ChangePasswordByUser.Password': $('#ChangePasswordByUser_Password').val(),
                'ChangePasswordByUser.ConfirmPassword': $('#ChangePasswordByUser_ConfirmPassword').val()
            },
            success: function (res) {
                if (res.success || res.Success) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Success',
                        text: res.message || res.Message,
                        timer: 2000,
                        showConfirmButton: false
                    });
                    $('#passwordForm')[0].reset();
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Failed',
                        text: res.message || res.Message
                    });
                }
            },
            error: function () {
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: 'Something went wrong while changing the password.'
                });
            }
        });
    });
}



