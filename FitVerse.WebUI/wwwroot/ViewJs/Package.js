let currentPage = 1;
let pageSize = 5;
let currentSearch = "";
let searchTimeout;

$(document).ready(function () {
    loadPackagePaged();

    // Search with debounce
    $('#searchPackage').on('input', function () {
        clearTimeout(searchTimeout);
        searchTimeout = setTimeout(() => {
            currentSearch = $(this).val().trim();
            currentPage = 1;
            loadPackagePaged();
        }, 400);
    });

    // Clear inputs on modal close
    $('#addPackageModal').on('hidden.bs.modal', function () {
        clearInputs();
    });

    // Handle Add/Edit form submit
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
            swal("Error", "Please fill all fields correctly.", "error");
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
                    swal(submitterId === "editBtn" ? "✅ Updated" : "✅ Success", res.message, "success", { timer: 1500 });
                    clearInputs();
                    loadPackagePaged();
                    $('#addPackageModal').modal('hide');
                } else {
                    swal("❌ Error", res.message, "error");
                }
            },
            error: function () {
                swal("⚠️ Warning", "An error occurred.", "error");
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
            response.data.forEach(item => {
                $('#Data').append(`
                    <tr>
                        <td>${item.Name}</td>
                        <td>${item.Price} EGP</td>
                        <td>${item.Sessions}</td>
                        <td>${item.Description}</td>
                        <td class="actions">
                            <button type="button" onclick="getById(${item.Id})" class="btn-icon" title="Edit">
                                <i class="fas fa-edit"></i>
                            </button>
                            <button type="button" onclick="Delete(${item.Id})" class="btn-icon text-danger" title="Delete">
                                <i class="fas fa-trash-alt"></i>
                            </button>
                        </td>
                    </tr>
                `);
            });
            renderPagination(response.currentPage, response.totalPages);
            $('.modal-backdrop').remove();
            $('body').removeClass('modal-open');
        },
        error: function () {
            swal("Error", "Failed to load packages. Please try again.", "error");
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

            toggleButtons(true); // إظهار زر التعديل وإخفاء زر الإضافة

            var modal = new bootstrap.Modal(document.getElementById('addPackageModal'));
            modal.show();
        } else {
            swal("Error", response.message, "error");
        }
    }).fail(() => swal("Error", "Failed to fetch package data.", "error"));
}

// ================== DELETE PACKAGE ==================
function Delete(id) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, this package cannot be recovered!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    }).then(willDelete => {
        if (willDelete) {
            $.post(`/Package/Delete?id=${id}`, response => {
                if (response.success) {
                    swal("Deleted!", response.message, "success", { timer: 1500 });
                    loadPackagePaged();
                } else {
                    swal("Error", response.message, "error");
                }
            }).fail(() => swal("Error", "An error occurred while deleting the package.", "error"));
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
    $('#Id').val('0');
    $('#name').val('');
    $('#price').val('');
    $('#sessions').val('');
    $('#description').val('');
    $('#isActive').prop('checked', true);
    toggleButtons(false); // إخفاء زر التعديل وإظهار زر الإضافة
    $('.modal-backdrop').remove();
    $('body').removeClass('modal-open');
}

// ================== TOGGLE BUTTONS ==================
function toggleButtons(isEditMode) {
    if (isEditMode) {
        $('#editBtn').removeClass('d-none'); // إظهار زر التعديل
        $('#addBtn').addClass('d-none');     // إخفاء زر الإضافة
    } else {
        $('#editBtn').addClass('d-none');    // إخفاء زر التعديل
        $('#addBtn').removeClass('d-none');  // إظهار زر الإضافة
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
