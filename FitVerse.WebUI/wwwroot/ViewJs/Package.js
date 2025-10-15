let currentPage = 1;
let pageSize = 5;
let currentSearch = "";

$(document).ready(function () {
    loadPackagePaged();

    $('#searchPackage').on('input', function () {
        currentSearch = $(this).val().trim();
        currentPage = 1;
        loadPackagePaged();
    });
});

// ================== LOAD ALL ==================
function loadPackagePaged() {
    $.ajax({
        url: '/Package/GetPaged',
        method: 'GET',
        data: {
            page: currentPage,
            pageSize: pageSize,
            search: currentSearch
        },
        success: function (response) {
            $('#Data').empty();
            response.data.forEach(function (item) {
                $('#Data').append(`
                    <tr>
                        <td>#${item.id}</td>
                        <td>${item.name}</td>
                        <td>${item.price} EGP</td>
                        <td>${item.sessions}</td>
                        <td class="actions">
                            <button type="button" onclick="getById(${item.id})" class="btn-icon" title="Edit">
                                <i class="fas fa-edit"></i>
                            </button>
                            <button type="button" onclick="Delete(${item.id})" class="btn-icon text-danger" title="Delete">
                                <i class="fas fa-trash-alt"></i>
                            </button>
                        </td>
                    </tr>
                `);
            });

            renderPagination(response.currentPage, response.totalPages);
        },
        error: function () {
            swal("Error", "Failed to load packages. Please try again.", "error");
        }
    });
}

// ================== ADD PACKAGE ==================
function addPackage() {
    var name = $('#name').val().trim();
    var price = $('#price').val().trim();
    var sessions = $('#sessions').val().trim();

    if (name === '') {
        swal("Error", "Please enter the package name.", "error");
        return;
    }
    if (price === '' || isNaN(price) || price <= 0) {
        swal("Error", "Please enter a valid price.", "error");
        return;
    }
    if (sessions === '' || isNaN(sessions) || sessions <= 0) {
        swal("Error", "Please enter a valid number of sessions.", "error");
        return;
    }

    $.ajax({
        url: '/Package/Create',
        method: 'POST',
        data: {
            name: name,
            price: price,
            sessions: sessions
        },
        success: function (response) {
            if (response.success) {
                swal("Success", `${response.message}`, "success");
                loadPackagePaged();
                clearInputs();
            } else {
                swal("Error", `${response.message}`, "error");
            }
        },
        error: function () {
            swal("Error", "An error occurred while adding the package.", "error");
        }
    });
}

// ================== GET PACKAGE BY ID ==================
function getById(id) {
    $.ajax({
        url: '/Package/GetById?id=' + id,
        method: 'GET',
        success: function (response) {
            if (response.success) {
                $('#Id').val(response.data.id);
                $('#name').val(response.data.name);
                $('#price').val(response.data.price);
                $('#sessions').val(response.data.sessions);

                $('#addBtn').hide();
                $('#editBtn').show();
            } else {
                swal("Error", `${response.message}`, "error");
            }
        },
        error: function () {
            swal("Error", "Failed to fetch package data.", "error");
        }
    });
}

// ================== UPDATE PACKAGE ==================
function updatePackage() {
    var id = $('#Id').val();
    var name = $('#name').val().trim();
    var price = $('#price').val().trim();
    var sessions = $('#sessions').val().trim();

    if (name === '') {
        swal("Error", "Please enter the package name.", "error");
        return;
    }
    if (price === '' || isNaN(price) || price <= 0) {
        swal("Error", "Please enter a valid price.", "error");
        return;
    }
    if (sessions === '' || isNaN(sessions) || sessions <= 0) {
        swal("Error", "Please enter a valid number of sessions.", "error");
        return;
    }

    $.ajax({
        url: '/Package/Update',
        method: 'POST',
        data: {
            id: id,
            name: name,
            price: price,
            sessions: sessions
        },
        success: function (response) {
            if (response.success) {
                swal("Success", `${response.message}`, "success");
                $('#addBtn').show();
                $('#editBtn').hide();
                clearInputs();
                loadPackagePaged();
            } else {
                swal("Error", `${response.message}`, "error");
            }
        },
        error: function () {
            swal("Error", "An error occurred while updating the package.", "error");
        }
    });
}

// ================== DELETE PACKAGE ==================
function Delete(id) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, this package cannot be recovered!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    url: '/Package/Delete?id=' + id,
                    method: 'POST',
                    success: function (response) {
                        if (response.success) {
                            swal("Deleted!", `${response.message}`, "success");
                            loadPackagePaged();
                        } else {
                            swal("Error", `${response.message}`, "error");
                        }
                    },
                    error: function () {
                        swal("Error", "An error occurred while deleting the package.", "error");
                    }
                });
            }
        });
}

// ================== PAGINATION ==================
function renderPagination(currentPage, totalPages) {
    const pagination = $('.pagination');
    pagination.empty();

    const prevDisabled = currentPage === 1 ? 'disabled' : '';
    const nextDisabled = currentPage === totalPages ? 'disabled' : '';

    pagination.append(`<button class="btn-icon" ${prevDisabled} onclick="changePage(${currentPage - 1})"><i class="fas fa-chevron-left"></i></button>`);

    for (let i = 1; i <= totalPages; i++) {
        const active = i === currentPage ? 'active' : '';
        pagination.append(`<button class="btn-icon ${active}" onclick="changePage(${i})">${i}</button>`);
    }

    pagination.append(`<button class="btn-icon" ${nextDisabled} onclick="changePage(${currentPage + 1})"><i class="fas fa-chevron-right"></i></button>`);
}

function changePage(page) {
    if (page < 1) return;
    currentPage = page;
    loadPackagePaged();
}

// ================== CLEAR INPUTS ==================
function clearInputs() {
    $('#Id').val('');
    $('#name').val('');
    $('#price').val('');
    $('#sessions').val('');
}
