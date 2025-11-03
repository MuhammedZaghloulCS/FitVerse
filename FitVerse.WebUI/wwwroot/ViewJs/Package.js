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
    // Check if enhanced version exists and use it, otherwise use original
    if (typeof window.loadPackagePagedEnhanced === 'function') {
        window.loadPackagePagedEnhanced();
        return;
    }
    
    const statusFilter = $('#statusFilter').val() || '';
    
    $.ajax({
        url: '/Package/GetPaged',
        method: 'GET',
        data: { 
            page: currentPage, 
            pageSize: pageSize, 
            search: currentSearch,
            status: statusFilter 
        },
        success: function (response) {
            $('#Data').empty();

            if (response.data.length === 0) {
                $('#Data').append(`
                    <tr>
                        <td colspan="5" class="text-center py-5">
                            <div class="empty-state">
                                <i class="bi bi-box-seam"></i>
                                <h6>No packages found</h6>
                                <p class="mb-0">Try adjusting your search criteria or create a new package.</p>
                            </div>
                        </td>
                    </tr>
                `);
                renderPagination(1, 1);
                if ($('#paginationInfo').length) {
                    $('#paginationInfo').text('Showing 0 of 0 packages');
                }
                return;
            }

            response.data.forEach(item => {
                const statusBadge = item.IsActive 
                    ? '<span class="package-status active">Active</span>'
                    : '<span class="package-status inactive">Inactive</span>';
                
                // Check if enhanced layout exists
                if ($('.package-details').length > 0 || $('.action-buttons').length > 0) {
                    // Enhanced layout
                    $('#Data').append(`
                        <tr>
                            <td class="package-details">
                                <div class="package-name">${item.Name}</div>
                                <p class="package-description">${item.Description || 'No description available'}</p>
                            </td>
                            <td class="text-center">
                                <span class="package-price">${item.Price} EGP</span>
                            </td>
                            <td class="text-center">
                                <span class="package-sessions">${item.Sessions}</span>
                            </td>
                            <td class="text-center">${statusBadge}</td>
                            <td class="text-center">
                                <div class="action-buttons">
                                    <button type="button" onclick="getById(${item.Id})" 
                                            class="btn btn-sm btn-outline-primary" title="Edit Package">
                                        <i class="bi bi-pencil"></i>
                                    </button>
                                    <button type="button" onclick="Delete(${item.Id})" 
                                            class="btn btn-sm btn-outline-danger" title="Delete Package">
                                        <i class="bi bi-trash"></i>
                                    </button>
                                </div>
                            </td>
                        </tr>
                    `);
                } else {
                    // Original layout
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
                }
            });

            renderPagination(response.currentPage, response.totalPages);
            
            // Update pagination info if element exists
            if ($('#paginationInfo').length) {
                const start = (response.currentPage - 1) * pageSize + 1;
                const end = Math.min(response.currentPage * pageSize, response.totalCount || response.data.length);
                const total = response.totalCount || response.data.length;
                $('#paginationInfo').text(`Showing ${start}-${end} of ${total} packages`);
            }
            
            // Update stats if function exists
            if (typeof updateStats === 'function') {
                updateStats(response.data);
            }
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
    const pagination = $('#pagination');
    pagination.empty();
    
    if (totalPages <= 1) return;
    
    // Previous button
    const prevDisabled = currentPage === 1 ? 'disabled' : '';
    pagination.append(`
        <li class="page-item ${prevDisabled}">
            <a class="page-link" href="#" onclick="changePage(${currentPage - 1}); return false;">
                <i class="bi bi-chevron-left"></i> Previous
            </a>
        </li>
    `);
    
    // Page numbers with smart pagination
    let startPage = Math.max(1, currentPage - 2);
    let endPage = Math.min(totalPages, currentPage + 2);
    
    // Adjust if we're near the beginning or end
    if (currentPage <= 3) {
        endPage = Math.min(5, totalPages);
    }
    if (currentPage > totalPages - 3) {
        startPage = Math.max(1, totalPages - 4);
    }
    
    // First page and ellipsis
    if (startPage > 1) {
        pagination.append(`
            <li class="page-item">
                <a class="page-link" href="#" onclick="changePage(1); return false;">1</a>
            </li>
        `);
        if (startPage > 2) {
            pagination.append(`<li class="page-item disabled"><span class="page-link">...</span></li>`);
        }
    }
    
    // Page numbers
    for (let i = startPage; i <= endPage; i++) {
        const active = i === currentPage ? 'active' : '';
        pagination.append(`
            <li class="page-item ${active}">
                <a class="page-link" href="#" onclick="changePage(${i}); return false;">${i}</a>
            </li>
        `);
    }
    
    // Last page and ellipsis
    if (endPage < totalPages) {
        if (endPage < totalPages - 1) {
            pagination.append(`<li class="page-item disabled"><span class="page-link">...</span></li>`);
        }
        pagination.append(`
            <li class="page-item">
                <a class="page-link" href="#" onclick="changePage(${totalPages}); return false;">${totalPages}</a>
            </li>
        `);
    }
    
    // Next button
    const nextDisabled = currentPage === totalPages ? 'disabled' : '';
    pagination.append(`
        <li class="page-item ${nextDisabled}">
            <a class="page-link" href="#" onclick="changePage(${currentPage + 1}); return false;">
                Next <i class="bi bi-chevron-right"></i>
            </a>
        </li>
    `);
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
