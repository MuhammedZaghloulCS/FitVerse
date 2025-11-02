let currentPage = 1;
let pageSize = 5;
let currentSearch = "";
let searchTimeout;

$(document).ready(function () {
    loadPackagePaged();

    // 🔍 Search with debounce
    $('#searchPackage').on('input', function () {
        clearTimeout(searchTimeout);
        searchTimeout = setTimeout(() => {
            currentSearch = $(this).val().trim();
            currentPage = 1;
            loadPackagePaged();
        }, 400);
    });

    // 🧹 Clear inputs when modal closes
    $('#addPackageModal').on('hidden.bs.modal', function () {
        clearInputs();
    });

    // 💾 Handle Add/Edit form submit
    $("#PackageForm").submit(function (e) {
        e.preventDefault();
        let formData = new FormData(this);

        // Checkbox handling
        formData.set("IsActive", $('#isActive').is(':checked') ? "true" : "false");

        // Determine Add or Edit
        let submitterId = e.originalEvent?.submitter?.id;
        let url = submitterId === "editBtn" ? '/Package/Update' : '/Package/Create';

        // Simple validation
        const name = formData.get("Name").trim();
        const price = parseFloat(formData.get("Price"));
        const sessions = parseInt(formData.get("Sessions"));
        if (!name || isNaN(price) || price <= 0 || isNaN(sessions) || sessions <= 0) {
            Swal.fire("Error", "Please fill all fields correctly.", "error");
            return;
        }

        $.ajax({
            url: url,
            method: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (res) {
                if (res.success) {
                    Swal.fire({
                        icon: "success",
                        title: submitterId === "editBtn" ? "Updated!" : "Success!",
                        text: res.message,
                        timer: 1500,
                        showConfirmButton: false
                    });
                    clearInputs();
                    loadPackagePaged();
                    $('#addPackageModal').modal('hide');
                } else {
                    Swal.fire("Error", res.message, "error");
                }
            },
            error: function () {
                Swal.fire("Error", "An unexpected error occurred.", "error");
            }
        });
    });
});


// ================== LOAD PACKAGES ==================
function loadPackagePaged() {
    $.ajax({
        url: '/Package/GetPaged',
        method: 'GET',
        data: { page: currentPage, pageSize: pageSize, search: currentSearch },
        success: function (response) {
            $('#Data').empty();

            if (response.data.length === 0) {
                $('#Data').append(`<tr><td colspan="5" class="text-center text-muted py-3">No packages found.</td></tr>`);
                renderPagination(1, 1);
                return;
            }

            response.data.forEach(item => {
                $('#Data').append(`
                    <tr>
                        <td>${item.Name}</td>
                        <td>${item.Price} EGP</td>
                        <td>${item.Sessions}</td>
                        <td>${item.Description || ''}</td>
<td class="actions text-center">
                            <button type="button" onclick="getById(${item.Id})" class="btn btn-sm btn-outline-primary me-2" title="Edit">
                                <i class="bi bi-pencil"></i>
                            </button>
                            <button type="button" onclick="Delete(${item.Id})" class="btn btn-sm btn-outline-danger" title="Delete">
                                <i class="bi bi-trash"></i>
                            </button>
                        </td>
                    </tr>
                `);
            });

            renderPagination(response.currentPage, response.totalPages);
        },
        error: function () {
            Swal.fire("Error", "Failed to load packages.", "error");
        }
    });
}


// ================== GET PACKAGE BY ID ==================
function getById(id) {
    $.get(`/Package/GetById?id=${id}`, response => {
        if (response.success) {
            $('#id').val(response.data.Id);
            $('#name').val(response.data.Name);
            $('#price').val(response.data.Price);
            $('#sessions').val(response.data.Sessions);
            $('#description').val(response.data.Description);
            $('#isActive').prop('checked', response.data.IsActive);

            toggleButtons(true);

            var modal = new bootstrap.Modal(document.getElementById('addPackageModal'));
            modal.show();
        } else {
            Swal.fire("Error", response.message, "error");
        }
    }).fail(() => Swal.fire("Error", "Failed to fetch package data.", "error"));
}


// ================== DELETE PACKAGE ==================
function Delete(id) {
    Swal.fire({
        title: "Are you sure?",
        text: "Once deleted, this package cannot be recovered!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#d33",
        cancelButtonColor: "#6c757d",
        confirmButtonText: "Yes, delete it!"
    }).then(result => {
        if (result.isConfirmed) {
            $.post(`/Package/Delete?id=${id}`, response => {
                if (response.success) {
                    Swal.fire({
                        icon: "success",
                        title: "Deleted!",
                        text: response.message,
                        timer: 1500,
                        showConfirmButton: false
                    });
                    loadPackagePaged();
                } else {
                    Swal.fire("Error", response.message, "error");
                }
            }).fail(() => Swal.fire("Error", "Error while deleting package.", "error"));
        }
    });
}


// ================== PAGINATION ==================
function renderPagination(currentPage, totalPages) {
    const pagination = $('.pagination');
    pagination.empty();
    const prevDisabled = currentPage === 1 ? 'disabled' : '';
    const nextDisabled = currentPage === totalPages ? 'disabled' : '';
    pagination.append(`<button class="btn btn-sm btn-primary me-2" ${prevDisabled} onclick="changePage(${currentPage - 1})"><i class="fas fa-chevron-left"></i>Prev</button>`);
    for (let i = 1; i <= totalPages; i++) {
        const active = i === currentPage ? 'active' : '';
        pagination.append(`<button class="btn btn-sm btn-outline-primary me-2" ${active}" onclick="changePage(${i})">${i}</button>`);
    }
    pagination.append(`<button class="btn btn-sm btn-primary me-2" ${nextDisabled} onclick="changePage(${currentPage + 1})"><i class="fas fa-chevron-right"></i>Next</button>`);
}

function changePage(page) {
    if (page < 1) return;
    currentPage = page;
    loadPackagePaged();
}


// ================== CLEAR INPUTS ==================
function clearInputs() {
    $('#id').val('0');
    $('#name').val('');
    $('#price').val('');
    $('#sessions').val('');
    $('#description').val('');
    $('#isActive').prop('checked', true);
    toggleButtons(false);
}


// ================== TOGGLE BUTTONS ==================
function toggleButtons(isEditMode) {
    if (isEditMode) {
        $('#editBtn').removeClass('d-none');
        $('#addBtn').addClass('d-none');
    } else {
        $('#editBtn').addClass('d-none');
        $('#addBtn').removeClass('d-none');
    }
}


// ================== REFRESH ==================
function refreshPackages() {
    $('#searchPackage').val('');
    currentSearch = '';
    currentPage = 1;
    clearInputs();
    loadPackagePaged();
}
