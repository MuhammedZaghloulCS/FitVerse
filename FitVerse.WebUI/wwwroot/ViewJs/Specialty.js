$(document).ready(function () {
    loadSpecialty();
    loadStats();
});

/* ================================
   Load Specialties
================================ */
function loadSpecialty() {
    // Show loading spinner
    $('#specialtiesContainer').html(`
        <div class="col-12">
            <div class="loading-spinner text-center py-5">
                <div class="spinner-border text-primary" role="status">
                    <span class="visually-hidden">Loading...</span>
                </div>
            </div>
        </div>
    `);

    $.ajax({
        url: '/Specialty/GetAll',
        method: 'GET',
        success: function (response) {
            $('#specialtiesContainer').empty();

            if (response.data && response.data.length > 0) {
                response.data.forEach(function (item) {
                    const imageUrl = item.Image || '/images/default-specialty.png';
                    const imageSrc = imageUrl + '?t=' + new Date().getTime(); // Cache busting

                    $('#specialtiesContainer').append(`
                        <div class="col-lg-3 col-md-4 col-sm-6">
                            <div class="specialty-card">
                                <div class="specialty-image-wrapper">
                                    <img src="${imageSrc}" 
                                         alt="${item.Name}" 
                                         class="specialty-image">
                                </div>
                                <h5 class="specialty-name">${item.Name}</h5>
                                <p class="specialty-description">${item.Description || 'No description available'}</p>
                                <div class="specialty-badge">
                                    <i class="bi bi-person-badge me-2"></i>
                                    ${item.CoachesCount || 0} Coaches
                                </div>
                                <div class="specialty-actions">
                                    <button class="btn-action btn-action-edit"
                                            onclick="editSpecialty(${item.Id}, '${escapeHtml(item.Name)}', '${escapeHtml(item.Description || '')}', '${imageUrl}')">
                                        <i class="bi bi-pencil me-1"></i> Edit
                                    </button>
                                    <button class="btn-action btn-action-delete"
                                            onclick="deleteSpecialty(${item.Id})"
                                            title="Delete">
                                        <i class="bi bi-trash"></i>
                                    </button>
                                </div>
                            </div>
                        </div>
                    `);
                });
            } else {
                $('#specialtiesContainer').html(`
                    <div class="col-12">
                        <div class="empty-state">
                            <div class="empty-state-icon">
                                <i class="bi bi-inbox"></i>
                            </div>
                            <h3 class="empty-state-title">No Specialties Found</h3>
                            <p class="empty-state-text">Start by adding your first specialty</p>
                        </div>
                    </div>
                `);
            }
        },
        error: function () {
            $('#specialtiesContainer').html(`
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
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Failed to load specialties.',
                confirmButtonColor: '#6366f1'
            });
        }
    });
}

/* ================================
   Helper Function to Escape HTML
================================ */
function escapeHtml(text) {
    if (!text) return '';
    const map = {
        '&': '&amp;',
        '<': '&lt;',
        '>': '&gt;',
        '"': '&quot;',
        "'": '&#039;'
    };
    return text.replace(/[&<>"']/g, m => map[m]);
}

/* ================================
   Add Specialty
================================ */
function addSpecialty() {
    const name = $('#specialtyName').val().trim();
    const description = $('#specialtyDescription').val().trim();
    const imageFile = $('#specialtyImage')[0].files[0];

    if (!name || !description) {
        Swal.fire({
            icon: 'warning',
            title: 'Missing Information',
            text: 'Please fill in all required fields.',
            confirmButtonColor: '#6366f1'
        });
        return;
    }

    if (!imageFile) {
        Swal.fire({
            icon: 'warning',
            title: 'Missing Image',
            text: 'Please select an image for the specialty.',
            confirmButtonColor: '#6366f1'
        });
        return;
    }

    const formData = new FormData();
    formData.append("Name", name);
    formData.append("Description", description);
    formData.append("Image", imageFile);

    Swal.fire({
        title: 'Saving...',
        text: 'Please wait',
        allowOutsideClick: false,
        didOpen: () => {
            Swal.showLoading();
        }
    });

    $.ajax({
        url: '/Specialty/Create',
        method: 'POST',
        processData: false,
        contentType: false,
        data: formData,
        success: function (response) {
            Swal.close();
            Swal.fire({
                icon: 'success',
                title: 'Success!',
                text: response.message || 'Specialty added successfully.',
                confirmButtonColor: '#6366f1',
                timer: 2000
            });
            $('#addSpecialtyModal').modal('hide');
            $('#addSpecialtyForm')[0].reset();
            $('#addImagePreviewWrapper').hide();
            loadSpecialty();
            loadStats();
        },
        error: function (xhr) {
            Swal.close();
            console.error(xhr.responseText);
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Something went wrong. Please try again.',
                confirmButtonColor: '#6366f1'
            });
        }
    });
}

/* ================================
   Edit Specialty (open modal)
================================ */
function editSpecialty(id, name, description, imagePath) {
    $('#specialtyId').val(id);
    $('#editSpecialtyName').val(name);
    $('#editSpecialtyDescription').val(description);

    if (imagePath && imagePath !== '/images/default-specialty.png') {
        const imageSrc = imagePath + '?t=' + new Date().getTime();
        $('#currentImagePreview').attr('src', imageSrc).show();
    } else {
        $('#currentImagePreview').hide();
    }

    $('#editSpecialtyImage').val('');
    $('#editImagePreviewWrapper').hide();
    $('#editSpecialtyModal').modal('show');
}

/* ================================
   Update Specialty
================================ */
function updateSpecialty() {
    const id = $('#specialtyId').val();
    const name = $('#editSpecialtyName').val().trim();
    const description = $('#editSpecialtyDescription').val().trim();
    const imageFile = $('#editSpecialtyImage')[0].files[0];

    if (!name) {
        Swal.fire({
            icon: 'warning',
            title: 'Missing Information',
            text: 'Name is required!',
            confirmButtonColor: '#6366f1'
        });
        return;
    }

    if (!description) {
        Swal.fire({
            icon: 'warning',
            title: 'Missing Information',
            text: 'Description is required!',
            confirmButtonColor: '#6366f1'
        });
        return;
    }

    const formData = new FormData();
    formData.append("Id", id);
    formData.append("Name", name);
    formData.append("Description", description);

    if (imageFile) {
        formData.append("Image", imageFile);
    }

    Swal.fire({
        title: 'Updating...',
        text: 'Please wait',
        allowOutsideClick: false,
        didOpen: () => {
            Swal.showLoading();
        }
    });

    $.ajax({
        url: '/Specialty/Update',
        method: 'PUT',
        processData: false,
        contentType: false,
        data: formData,
        success: function (response) {
            Swal.close();
            Swal.fire({
                icon: 'success',
                title: 'Updated!',
                text: response.message || 'Specialty updated successfully.',
                confirmButtonColor: '#6366f1',
                timer: 2000
            });
            $('#editSpecialtyModal').modal('hide');
            $('#editSpecialtyForm')[0].reset();
            $('#editImagePreviewWrapper').hide();
            loadSpecialty();
            loadStats();
        },
        error: function (xhr) {
            Swal.close();
            console.error(xhr.responseText);
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Server error occurred while updating specialty.',
                confirmButtonColor: '#6366f1'
            });
        }
    });
}

/* ================================
   Delete Specialty
================================ */
function deleteSpecialty(id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#ef4444',
        cancelButtonColor: '#6b7280',
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: 'Cancel'
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire({
                title: 'Deleting...',
                text: 'Please wait',
                allowOutsideClick: false,
                didOpen: () => {
                    Swal.showLoading();
                }
            });

            $.ajax({
                url: '/Specialty/Delete?id=' + id,
                method: 'DELETE',
                success: function (response) {
                    Swal.close();
                    const message = response.message || "Deleted successfully.";
                    Swal.fire({
                        icon: 'success',
                        title: 'Deleted!',
                        text: message,
                        confirmButtonColor: '#6366f1',
                        timer: 2000
                    });
                    loadSpecialty();
                    loadStats();
                },
                error: function () {
                    Swal.close();
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: 'Failed to delete specialty.',
                        confirmButtonColor: '#6366f1'
                    });
                }
            });
        }
    });
}

/* ================================
   Load Stats
================================ */
function loadStats() {
    $.ajax({
        url: '/Specialty/GetStats',
        method: 'GET',
        success: function (response) {
            $('#totalSpecialties').text(response.totalSpecialties || 0);
            $('#totalCoaches').text(response.totalCoaches || 0);
        },
        error: function () {
            console.error("Failed to load stats");
            $('#totalSpecialties').text('0');
            $('#totalCoaches').text('0');
        }
    });
}

/* ================================
   Modal Cleanup
================================ */
$('#addSpecialtyModal').on('hidden.bs.modal', function () {
    $('#addSpecialtyForm')[0].reset();
    $('#addImagePreviewWrapper').hide();
});

$('#editSpecialtyModal').on('hidden.bs.modal', function () {
    $('#editSpecialtyForm')[0].reset();
    $('#editImagePreviewWrapper').hide();
    $('#currentImagePreview').hide();
});