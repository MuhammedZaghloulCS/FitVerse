$(document).ready(function () {

    // ==============================
    // 🌟 1. Load Anatomy Data
    // ==============================
    let currentSearch = "";

    function loadAnatomy(searchTerm = "") {
        // Show loading spinner
        $('#anatomyContainer').html(`
            <div class="col-12">
                <div class="loading-spinner text-center py-5">
                    <div class="spinner-border text-primary" role="status">
                        <span class="visually-hidden">Loading...</span>
                    </div>
                </div>
            </div>
        `);

        $.ajax({
            url: '/Anatomy/GetAll',
            method: 'GET',
            data: { search: searchTerm },
            success: function (res) {
                $('#anatomyContainer').empty();

                if (res.data && res.data.length > 0) {
                    res.data.forEach(item => {
                        let imgSrc = item.ImagePath
                            ? (item.ImagePath.startsWith('/Images/') ? item.ImagePath : '/Images/' + item.ImagePath) + '?t=' + new Date().getTime()
                            : '/Images/default.jpg';

                        $('#anatomyContainer').append(`
                            <div class="col-lg-3 col-md-4 col-sm-6">
                                <div class="anatomy-card">
                                    <div class="anatomy-image-wrapper">
                                        <img src="${imgSrc}" 
                                             alt="${item.Name}"
                                             class="anatomy-image">
                                    </div>
                                    <h5 class="anatomy-name">${item.Name}</h5>
                                    <div class="anatomy-actions">
                                        <button class="btn-action btn-action-edit editAnatomyBtn" 
                                                data-id="${item.Id}">
                                            <i class="bi bi-pencil me-1"></i> Edit
                                        </button>
                                        <button class="btn-action btn-action-delete deleteAnatomyBtn" 
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
                    $('#anatomyContainer').html(`
                        <div class="col-12">
                            <div class="empty-state">
                                <div class="empty-state-icon">
                                    <i class="bi bi-inbox"></i>
                                </div>
                                <h3 class="empty-state-title">No Body Parts Found</h3>
                                <p class="empty-state-text">
                                    ${searchTerm ? 'Try adjusting your search criteria' : 'Start by adding your first body part'}
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
                    text: 'Failed to load anatomy data.',
                    confirmButtonColor: '#6366f1'
                });
                $('#anatomyContainer').html(`
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
    // 🌟 2. Add New Anatomy
    // ==============================
    $('#saveAnatomyBtn').click(function () {
        const name = $('#Name').val().trim();

        if (!name) {
            Swal.fire({
                icon: 'warning',
                title: 'Missing Information',
                text: 'Please enter body part name',
                confirmButtonColor: '#6366f1'
            });
            return;
        }

        let formData = new FormData();
        formData.append("Name", name);

        if ($('#Image')[0].files[0]) {
            formData.append("ImageFile", $('#Image')[0].files[0]);
        }

        $.ajax({
            url: '/Anatomy/AddAnatomy',
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
                    $('#addAnatomyModal').modal('hide');
                    $('#anatomyForm')[0].reset();
                    $('#addImagePreviewWrapper').hide();
                    loadAnatomy(currentSearch);

                    // Reload page to update stats
                    setTimeout(() => {
                        location.reload();
                    }, 2000);
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
                    text: 'Failed to add body part. Please try again.',
                    confirmButtonColor: '#6366f1'
                });
            }
        });
    });

    // ==============================
    // 🌟 3. Open Edit Modal
    // ==============================
    $(document).on('click', '.editAnatomyBtn', function () {
        let id = $(this).data('id');

        Swal.fire({
            title: 'Loading...',
            text: 'Please wait',
            allowOutsideClick: false,
            didOpen: () => {
                Swal.showLoading();
            }
        });

        $.get(`/Anatomy/GetById/${id}`, function (res) {
            Swal.close();
            if (res.success) {
                $('#editAnatomyName').val(res.data.Name);

                let imgSrc = res.data.ImagePath
                    ? (res.data.ImagePath.startsWith('/Images/') ? res.data.ImagePath : '/Images/' + res.data.ImagePath) + '?t=' + new Date().getTime()
                    : '/Images/default.jpg';

                $('#currentAnatomyImage').attr('src', imgSrc);
                $('#editImagePreviewWrapper').hide();
                $('#editAnatomyImage').val('');
                $('#editSaveBtn').data('id', res.data.Id);
                $('#editAnatomyModal').modal('show');
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
                text: 'Failed to load anatomy data',
                confirmButtonColor: '#6366f1'
            });
        });
    });

    // ==============================
    // 🌟 4. Save Edit
    // ==============================
    $('#editSaveBtn').click(function () {
        const id = $(this).data('id');
        const name = $('#editAnatomyName').val().trim();

        if (!name) {
            Swal.fire({
                icon: 'warning',
                title: 'Missing Information',
                text: 'Please enter body part name',
                confirmButtonColor: '#6366f1'
            });
            return;
        }

        let formData = new FormData();
        formData.append("Id", id);
        formData.append("Name", name);

        if ($('#editAnatomyImage')[0].files[0]) {
            formData.append("ImageFile", $('#editAnatomyImage')[0].files[0]);
        }

        $.ajax({
            url: '/Anatomy/Update',
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
                    $('#editAnatomyModal').modal('hide');
                    loadAnatomy(currentSearch);
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
                    text: 'Failed to update body part. Please try again.',
                    confirmButtonColor: '#6366f1'
                });
            }
        });
    });

    // ==============================
    // 🌟 5. Delete Anatomy
    // ==============================
    $(document).on('click', '.deleteAnatomyBtn', function () {
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
                    url: `/Anatomy/Delete/${id}`,
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
                            loadAnatomy(currentSearch);

                            // Reload page to update stats
                            setTimeout(() => {
                                location.reload();
                            }, 2000);
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
                            text: 'Failed to delete body part. Please try again.',
                            confirmButtonColor: '#6366f1'
                        });
                    }
                });
            }
        });
    });

    // ==============================
    // 🌟 6. Search Functionality
    // ==============================
    $('#searchAnatomy').on('input', function () {
        let term = $(this).val().trim();
        currentSearch = term;

        // Debounce search
        clearTimeout(window.searchTimeout);
        window.searchTimeout = setTimeout(function () {
            loadAnatomy(term);
        }, 300);
    });

    // ==============================
    // 🌟 7. Modal Cleanup
    // ==============================
    $('#addAnatomyModal').on('hidden.bs.modal', function () {
        $('#anatomyForm')[0].reset();
        $('#addImagePreviewWrapper').hide();
    });

    $('#editAnatomyModal').on('hidden.bs.modal', function () {
        $('#editAnatomyForm')[0].reset();
        $('#editImagePreviewWrapper').hide();
    });

    // ==============================
    // 🌟 8. Initialize Page
    // ==============================
    loadAnatomy();
});