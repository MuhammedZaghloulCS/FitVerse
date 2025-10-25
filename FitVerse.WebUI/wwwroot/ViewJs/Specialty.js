
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

                let color = "#4a4a4a"; 

                const name = item.Name.toLowerCase();

                if (name.includes("nutrition") || name.includes("weight")) color = "#4CAF50"; 
                else if (name.includes("cardio") || name.includes("hiit")) color = "#E91E63"; 
                else if (name.includes("strength")) color = "#2196F3"; 
                else if (name.includes("yoga") || name.includes("flexibility")) color = "#FF9800"; 
                else if (name.includes("crossfit")) color = "#9C27B0"; 
                else if (name.includes("boxing") || name.includes("mma")) color = "#f44336";
                else if (name.includes("bodybuilding")) color = "#FF5722"; 
                else if (name.includes("running") || name.includes("endurance")) color = "#009688";

                const iconHtml = item.Icon
                    ? `<i class="${item.Icon}" style="font-size: 2rem; color: ${color};"></i>`
                    : `<span style="font-size: 2rem;">💪</span>`;

                $('#specialtiesContainer').append(`
                     <div class="col-lg-3 col-md-6">

                        <div class="card-custom">
                            <div class="card-body-custom text-center">
                                <div class="mb-3">${iconHtml}</div>
                                <h5 class="fw-bold mb-2">${item.Name}</h5>
                                <p class="text-muted small mb-3">${item.Description || ''}</p>
                                <div class="mb-3">
                                    <span class="badge-custom badge-primary">${item.CoachesCount} Coaches</span>
                                </div>
                                <div class="d-flex gap-2">
                                  <button class="btn btn-outline-primary btn-sm flex-grow-1" onclick="editSpecialty(${item.Id})" data-bs-toggle="modal" data-bs-target="#editSpecialtyModal">
                                          <i class="bi bi-pencil"></i> Edit </button>
                                  <button class="btn btn-danger btn-sm" onclick="deleteSpecialty(${item.Id})">
                                          <i class="bi bi-trash"></i>  </button>
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


$.ajax({
    url: '/Specialty/Create',
    method: 'POST',
    contentType: 'application/json',
    data: JSON.stringify({
        name: $('#specialtyName').val(),
        description: $('#specialtyDescription').val(),
        icon: $('#specialtyIcon').val()
    }),
    success: function (response) {
     
            swal("Success",  "Specialty added successfully!", "success");
            loadSpecialty();
       
    },
    error: function () {
        swal("Error", "Failed to add specialty.", "error");
    }
});



function addSpecialty() {
    const name = $('#specialtyName').val().trim();
    const description = $('#specialtyDescription').val().trim();
    const icon = $('#specialtyIcon').val().trim();

    if (!name || !description || !icon) {
        swal("Error", "Please fill all fields before submitting.", "error");
        return;
    }

    $.ajax({
        url: '/Specialty/Create', 
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ Name: name, Description: description, Icon: icon }),
        success: function (response) {
            if (response.success || response.Success) {
                swal("Success!", response.message || "Specialty added successfully.", "success");
                $('#addSpecialtyModal').modal('hide');
                loadSpecialty();
            } else {
                swal("Error", response.message || "Failed to add specialty.", "error");
            }
        },
        error: function (xhr) {
            console.error(xhr.responseText);
            swal("Error", "Invalid data. Please fill all fields.", "error");
        }
    });
}


function updateSpecialty() {
    const id = $('#specialtyId').val();
    const name = $('#editSpecialtyName').val().trim();
    const description = $('#editSpecialtyDescription').val().trim();
    const icon = $('#editSpecialtyIcon').val().trim();

    if (!name) {
        swal("Error", "Name is required!", "error");
        return;
    }

    $.ajax({
        url: '/Specialty/Update',
        method: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify({ Id: id, Name: name, Description: description, Icon: icon }),
        success: function (response) {

                swal("Updated!", response.message, "success");
                $('#editSpecialtyModal').modal('hide');
                loadSpecialty();
          
            
        },
        error: function () {
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