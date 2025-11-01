
$(document).ready(function () {
    loadSpecialty();
    loadStats();
});
function loadSpecialty() {
    $.ajax({
        url: '/Specialty/GetAll',
        method: 'GET',
        success: function (response) {
            $('#specialtiesContainer').empty();

            response.data.forEach(function (item) {

                // ✅ استخدم الصورة فقط بدون ألوان أو تمييز
                const imageHtml = item.Image
                    ? `<img src="${item.Image}" alt="${item.Name}" 
                            class="img-fluid rounded-circle mb-3" 
                            style="width:80px; height:80px; object-fit:cover;">`
                    : `<img src="/images/default-specialty.png" alt="default" 
                            class="img-fluid rounded-circle mb-3" 
                            style="width:80px; height:80px; object-fit:cover;">`;

                $('#specialtiesContainer').append(`
                    <div class="col-lg-3 col-md-6">
                        <div class="card-custom">
                            <div class="card-body-custom text-center">
                                ${imageHtml}
                                <h5 class="fw-bold mb-2">${item.Name}</h5>
                                <p class="text-muted small mb-3">${item.Description || ''}</p>
                                <div class="mb-3">
                                    <span class="badge-custom badge-primary">${item.CoachesCount} Coaches</span>
                                </div>
                                <div class="d-flex gap-2">
                                    <button class="btn btn-outline-primary btn-sm flex-grow-1"
                                        onclick="editSpecialty(${item.Id}, '${item.Name}', '${item.Description}', '${item.ImagePath || ''}')"
                                        <i class="bi bi-pencil"></i> Edit
                                    </button>
                                    <button class="btn btn-danger btn-sm" onclick="deleteSpecialty(${item.Id})">
                                        <i class="bi bi-trash"></i>
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                `);
            });
        },
        error: function () {
            console.error("❌ Failed to load specialties.");
        }
    });
}


function addSpecialty() {
    const name = $('#specialtyName').val().trim();
    const description = $('#specialtyDescription').val().trim();
    const imageFile = $('#specialtyImage')[0].files[0];

    if (!name || !description || !imageFile) {
        swal("Error", "Please fill all fields before submitting.", "error");
        return;
    }

    const formData = new FormData();
    formData.append("Name", name);
    formData.append("Description", description);
    formData.append("Image", imageFile);

    $.ajax({
        url: '/Specialty/Create',
        method: 'POST',
        processData: false,
        contentType: false,
        data: formData,
        success: function (response) {
            swal("Success!", response.message || "Specialty added successfully.", "success");
            $('#addSpecialtyModal').modal('hide');
            $('#specialtyName').val('');
            $('#specialtyDescription').val('');
            $('#specialtyImage').val('');
            loadSpecialty();
        },
        error: function (xhr) {
            console.error(xhr.responseText);
            swal("Error", "Something went wrong. Please try again.", "error");
        }
    });
}


function editSpecialty(id, name, description, imagePath) {
    $('#specialtyId').val(id);
    $('#editSpecialtyName').val(name);
    $('#editSpecialtyDescription').val(description);

    if (imagePath) {
        $('#currentImagePreview').attr('src', imagePath).show();
    } else {
        $('#currentImagePreview').hide();
    }

    $('#editSpecialtyImage').val('');

    $('#editSpecialtyModal').modal('show');
}

function updateSpecialty() {
    const id = $('#specialtyId').val();
    const name = $('#editSpecialtyName').val().trim();
    const description = $('#editSpecialtyDescription').val().trim();
    const imageFile = $('#editSpecialtyImage')[0].files[0];

    if (!name) {
        swal("Error", "Name is required!", "error");
        return;
    }

    const formData = new FormData();
    formData.append("Id", id);
    formData.append("Name", name);
    formData.append("Description", description);

    if (imageFile) {
        formData.append("Image", imageFile);
    }

    $.ajax({
        url: '/Specialty/Update',
        method: 'PUT',
        processData: false,
        contentType: false,
        data: formData,
        success: function (response) {
            swal("Updated!", response.message || "Specialty updated successfully.", "success");
            $('#editSpecialtyModal').modal('hide');
            $('#editSpecialtyImage').val('');
            loadSpecialty();
        },
        error: function (xhr) {
            console.error(xhr.responseText);
            swal("Error", "Server error occurred while updating specialty.", "error");
        }
    });
}


function deleteSpecialty(id) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you cannot recover this!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                url: '/Specialty/Delete?id=' + id,
                method: 'DELETE',
                success: function (response) {
                    const message = response.message || "Deleted successfully.";
                    swal("Deleted!", message, "success");
                    loadSpecialty();
                },
                error: function () {
                    swal("Error", "Failed to delete specialty.", "error");
                }
            });
        }
    });
}


function loadStats() {
    $.ajax({
        url: '/Specialty/GetStats',
        method: 'GET',
        success: function (response) {
            $('.stat-card .stat-value').eq(0).text(response.totalSpecialties);
            $('.stat-card .stat-value').eq(1).text(response.totalCoaches);
        },
        error: function () {
            console.error("Failed to load stats");
        }
    });
}