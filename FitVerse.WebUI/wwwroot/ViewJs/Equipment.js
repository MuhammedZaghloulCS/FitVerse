$(document).ready(function () {
    loadEquipment();
});
function loadEquipment() {
    $.ajax({
        url: '/Equipment/GetAll',
        method: 'GET',
        success: function (response) {

            $('#Data').empty();
            response.data.forEach(function (item) {
                $('#Data').append(`
                <tr>
                        <td>#${item.id}</td>
                        <td>${item.name}</td>
                        <td class="actions">
                            <button type="button" onclick="getById(${item.id})" class="btn-icon" title="Edit">
                                <i class="fas fa-edit"></i>
                            </button>
                            <button type="button" onclick="Delete(${item.id})" class="btn-icon text-danger" title="Delete">
                                <i class="fas fa-trash-alt"></i>
                            </button>
                        </td>
                    </tr>`);
            });
        },
    })
}
function addEquipment() {
    var name = $('#name').val();
    if (name === '') {

        swal("Error", "Name is required!", "error");
        return;
    }
    $.ajax({
        url: '/Equipment/Create',
        method: 'POST',
        data: { name: name },
        success: function (response) {
            if (response.success) {

                swal("Good job!", `${response.message}`, "success");

                loadEquipment();
                $('#Name').val('');
            } else {
                swal("Error", `${response.message}`, "error");

            }
        },
    })
}

function getById(id) {
    $.ajax({
        url: '/Equipment/GetById?id=' + id,
        method: 'GET',
        success: function (response) {
            if (response.success) {
                $('#Id').val(response.data.id);
                $('#name').val(response.data.name);
                $('#addBtn').hide();
                $('#editBtn').show();
            } else {
                Swal.fire({
                    title: "Error",
                    text: `${response.message}`,
                    icon: "error"
                });
            }
        },
    });
}

function updateEquipment() {
    var id = $('#Id').val();
    var name = $('#name').val();
    if (name === '') {
        swal("Error", "Name is required!", "error");

        return;
    }
    $.ajax({
        url: '/Equipment/Update',
        method: 'POST',
        data: { id: id, name: name },
        success: function (response) {
            if (response.success) {
                swal("Good job!", `${response.message}`, "success");

                $('#addBtn').show();
                $('#editBtn').hide();
                loadEquipment();
                $('#Id').val('');
                $('#name').val('');
            } else {
                swal("Error", `${response.message}`, "error");

            }
        },
    })
}

function Delete(id) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover this !",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    url: '/Equipment/Delete?id=' + id,
                    method: 'POST',
                    success: function (response) {
                        if (response.success) {
                            swal("Good job!", `${response.message}`, "success");
                            loadEquipment();
                        } else {
                            swal("Error", `${response.message}`, "error");
                        }
                    },
                })

            } else {
                swal("Your imaginary file is safe!");
            }
        });
}