$(document).ready(function () {

    // ==============================
    // 🌟 1. Load Statistics
    // ==============================
    function loadStats() {
        $.get('/Equipment/GetTotalCountEquipment', function (res) {
            if (res.totalCount !== undefined) {
                $('#totalEquipmentCount').text(res.totalCount);
            }
        }).fail(function () {
            $('#totalEquipmentCount').text('0');
        });

        $.get('/Equipment/GetTotalCountExercise', function (res) {
            if (res.totalCount !== undefined) {
                $('#totalExerciseCount').text(res.totalCount);
            }
        }).fail(function () {
            $('#totalExerciseCount').text('0');
        });
    }

    // ==============================
    // 🌟 2. Load Equipment
    // ==============================
    let currentSearch = "";

    function loadEquipment(searchTerm = "") {
        // Show loading spinner
        $('#equipmentContainer').html(`
            <div class="col-12">
                <div class="loading-spinner text-center py-5">
                    <div class="spinner-border text-primary" role="status">
                        <span class="visually-hidden">Loading...</span>
                    </div>
                </div>
            </div>
        `);

        $.ajax({
            url: '/Equipment/GetAll',
            method: 'GET',
            data: { search: searchTerm },
            success: function (res) {
                $('#equipmentContainer').empty();

                if (res.data && res.data.length > 0) {
                    res.data.forEach(item => {
                        let imgSrc = item.ImagePath
                            ? (item.ImagePath.startsWith('/Images/') ? item.ImagePath : '/Images/' + item.ImagePath) + '?t=' + new Date().getTime()
                            : '/Images/default.jpg';

                        $('#equipmentContainer').append(`
                            <div class="col-lg-3 col-md-4 col-sm-6">
                                <div class="equipment-card">
                                    <div class="equipment-image-wrapper">
                                        <img src="${imgSrc}" 
                                             alt="${item.Name}"
                                             class="equipment-image">
                                    </div>
                                    <h5 class="equipment-name">${item.Name}</h5>
                                    <div class="equipment-actions">
                                        <button class="btn-action btn-action-edit editEquipmentBtn" 
                                                data-id="${item.Id}">
                                            <i class="bi bi-pencil me-1"></i> Edit
                                        </button>
                                        <button class="btn-action btn-action-delete deleteEquipmentBtn" 
                                                data-id="${item.Id}"
                                                title="Delete">
                                            <i class="bi bi-trash"></i>
                                        </button>
                                    </div>
                                </div>
                            </div>
                        `);
                    });
                } else {
                    $('#equipmentContainer').html(`
                        <div class="col-12">
                            <div class="empty-state">
                                <div class="empty-state-icon">
                                    <i class="bi bi-inbox"></i>
                                </div>
                                <h3 class="empty-state-title">No Equipment Found</h3>
                                <p class="empty-state-text">
                                    ${searchTerm ? 'Try adjusting your search criteria' : 'Start by adding your first equipment'}
                                </p>
                            </div>
                        </div>
                    `);
                }
            },
            error: function () {
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: 'Failed to load equipment data.',
                    confirmButtonColor: '#6366f1'
                });
                $('#equipmentContainer').html(`
                    <div class="col-12">
                        <div class="empty-state">
                            <div class="empty-state-icon text-danger">
                                <i class="bi bi-exclamation-triangle"></i>
                            </div>
                            <h3 class="empty-state-title">Error Loading Data</h3>
                            <p class="empty-state-text">Please try refreshing the page</p>
                        </div>
                    </div>
                `);
            }
        });
    }

    // ==============================
    // 🌟 3. Add New Equipment
    // ==============================
    $('#openAddModal').click(function () {
        $('#equipmentForm')[0].reset();
        $('#addImagePreviewWrapper').hide();
        $('#addEquipmentModal').modal('show');
    });

    $('#saveEquipmentBtn').click(function () {
        const name = $('#equipmentName').val().trim();

        if (!name) {
            Swal.fire({
                icon: 'warning',
                title: 'Missing Information',
                text: 'Please enter equipment name',
                confirmButtonColor: '#6366f1'
            });
            return;
        }

        let formData = new FormData();
        formData.append("Name", name);

        if ($('#equipmentImage')[0].files[0]) {
            formData.append("EquipmentImageFile", $('#equipmentImage')[0].files[0]);
        }

        $.ajax({
            url: '/Equipment/AddEquipment',
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            beforeSend: function () {
                Swal.fire({
                    title: 'Saving...',
                    text: 'Please wait',
                    allowOutsideClick: false,
                    didOpen: () => {
                        Swal.showLoading();
                    }
                });
            },
            success: function (res) {
                Swal.close();
                if (res.success) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Success!',
                        text: res.message,
                        confirmButtonColor: '#6366f1',
                        timer: 2000
                    });
                    $('#addEquipmentModal').modal('hide');
                    $('#equipmentForm')[0].reset();
                    $('#addImagePreviewWrapper').hide();
                    loadEquipment(currentSearch);
                    loadStats();
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: res.message,
                        confirmButtonColor: '#6366f1'
                    });
                }
            },
            error: function () {
                Swal.fire({
                    icon: 'error',
                    title: 'Server Error',
                    text: 'Failed to add equipment. Please try again.',
                    confirmButtonColor: '#6366f1'
                });
            }
        });
    });

    // ==============================
    // 🌟 4. Open Edit Modal
    // ==============================
    $(document).on('click', '.editEquipmentBtn', function () {
        let id = $(this).data('id');

        Swal.fire({
            title: 'Loading...',
            text: 'Please wait',
            allowOutsideClick: false,
            didOpen: () => {
                Swal.showLoading();
            }
        });

        $.get(`/Equipment/GetById/${id}`, function (res) {
            Swal.close();
            if (res.success) {
                $('#editEquipmentName').val(res.data.Name);

                let imgSrc = res.data.ImagePath
                    ? (res.data.ImagePath.startsWith('/Images/') ? res.data.ImagePath : '/Images/' + res.data.ImagePath) + '?t=' + new Date().getTime()
                    : '/Images/default.jpg';

                $('#currentEquipmentImage').attr('src', imgSrc);
                $('#editImagePreviewWrapper').hide();
                $('#editEquipmentImage').val('');
                $('#editSaveBtn').data('id', res.data.Id);
                $('#editEquipmentModal').modal('show');
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: res.message,
                    confirmButtonColor: '#6366f1'
                });
            }
        }).fail(function () {
            Swal.close();
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Failed to load equipment data',
                confirmButtonColor: '#6366f1'
            });
        });
    });

    // ==============================
    // 🌟 5. Save Edit
    // ==============================
    $('#editSaveBtn').click(function () {
        const id = $(this).data('id');
        const name = $('#editEquipmentName').val().trim();

        if (!name) {
            Swal.fire({
                icon: 'warning',
                title: 'Missing Information',
                text: 'Please enter equipment name',
                confirmButtonColor: '#6366f1'
            });
            return;
        }

        let formData = new FormData();
        formData.append("Id", id);
        formData.append("Name", name);

        if ($('#editEquipmentImage')[0].files[0]) {
            formData.append("EquipmentImageFile", $('#editEquipmentImage')[0].files[0]);
        }

        $.ajax({
            url: '/Equipment/Update',
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            beforeSend: function () {
                Swal.fire({
                    title: 'Updating...',
                    text: 'Please wait',
                    allowOutsideClick: false,
                    didOpen: () => {
                        Swal.showLoading();
                    }
                });
            },
            success: function (res) {
                Swal.close();
                if (res.success) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Updated!',
                        text: res.message,
                        confirmButtonColor: '#6366f1',
                        timer: 2000
                    });
                    $('#editEquipmentModal').modal('hide');
                    loadEquipment(currentSearch);
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: res.message,
                        confirmButtonColor: '#6366f1'
                    });
                }
            },
            error: function () {
                Swal.fire({
                    icon: 'error',
                    title: 'Server Error',
                    text: 'Failed to update equipment. Please try again.',
                    confirmButtonColor: '#6366f1'
                });
            }
        });
    });

    // ==============================
    // 🌟 6. Delete Equipment
    // ==============================
    $(document).on('click', '.deleteEquipmentBtn', function () {
        let id = $(this).data('id');

        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#ef4444',
            cancelButtonColor: '#6b7280',
            confirmButtonText: 'Yes, delete it!',
            cancelButtonText: 'Cancel'
        }).then(result => {
            if (result.isConfirmed) {
                $.ajax({
                    url: `/Equipment/Delete/${id}`,
                    type: 'DELETE',
                    beforeSend: function () {
                        Swal.fire({
                            title: 'Deleting...',
                            text: 'Please wait',
                            allowOutsideClick: false,
                            didOpen: () => {
                                Swal.showLoading();
                            }
                        });
                    },
                    success: function (res) {
                        Swal.close();
                        if (res.success) {
                            Swal.fire({
                                icon: 'success',
                                title: 'Deleted!',
                                text: res.message,
                                confirmButtonColor: '#6366f1',
                                timer: 2000
                            });
                            loadEquipment(currentSearch);
                            loadStats();
                        } else {
                            Swal.fire({
                                icon: 'error',
                                title: 'Error',
                                text: res.message,
                                confirmButtonColor: '#6366f1'
                            });
                        }
                    },
                    error: function () {
                        Swal.fire({
                            icon: 'error',
                            title: 'Server Error',
                            text: 'Failed to delete equipment. Please try again.',
                            confirmButtonColor: '#6366f1'
                        });
                    }
                });
            }
        });
    });

    // ==============================
    // 🌟 7. Search Functionality
    // ==============================
    $('#searchEquipment').on('input', function () {
        let term = $(this).val().trim();
        currentSearch = term;

        // Debounce search
        clearTimeout(window.searchTimeout);
        window.searchTimeout = setTimeout(function () {
            loadEquipment(term);
        }, 300);
    });

    // ==============================
    // 🌟 8. Modal Cleanup
    // ==============================
    $('#addEquipmentModal').on('hidden.bs.modal', function () {
        $('#equipmentForm')[0].reset();
        $('#addImagePreviewWrapper').hide();
    });

    $('#editEquipmentModal').on('hidden.bs.modal', function () {
        $('#editEquipmentForm')[0].reset();
        $('#editImagePreviewWrapper').hide();
    });

    // ==============================
    // 🌟 9. Initialize Page
    // ==============================
    loadEquipment();
    loadStats();
});