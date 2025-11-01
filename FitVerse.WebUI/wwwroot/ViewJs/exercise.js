let currentPage = 1;
let pageSize = 6;
let currentSearch = "";

$(document).ready(function () {
    loadMuscles();
    loadEquipments();
    loadExercisesPaged();

    // ✅ live search
    $('#searchInput').on('input', function () {
        currentSearch = $(this).val().trim();
        currentPage = 1;
        loadExercisesPaged();
    });

    // ✅ form submit
    $('#exerciseForm').on('submit', function (e) {
        e.preventDefault();
        let id = $('#exerciseId').val();
        if (id) updateExercise();
        else addExercise();

        e.target.reset();
    });

    // ✅ reset button
    $('#resetBtn').click(clearForm);
});

// Search feature
$('#searchExercise').on('input', function () {
    let value = $(this).val().toLowerCase();
    $('#exerciseTable tbody tr').filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
    });
});

// ======================= LOAD DATA =========================
function loadMuscles() {
    $.ajax({
        url: '/Muscle/GetAll',
        method: 'GET',
        success: function (response) {
            let dropdown = $('#muscleId');
            dropdown.empty().append('<option value="" disabled selected>Select muscle</option>');
            
            console.log('Muscles API response:', response); // Debug log
            
            let muscles = [];
            
            // Handle different response formats
            if (response && response.data && Array.isArray(response.data)) {
                muscles = response.data;
            } else if (response && Array.isArray(response)) {
                muscles = response;
            } else if (response && response.result && Array.isArray(response.result)) {
                muscles = response.result;
            } else if (response && response.items && Array.isArray(response.items)) {
                muscles = response.items;
            } else if (response && typeof response === 'object') {
                // If it's an object, try to find array properties
                const possibleArrays = Object.values(response).filter(val => Array.isArray(val));
                if (possibleArrays.length > 0) {
                    muscles = possibleArrays[0];
                } else {
                    console.error('No array found in response object:', response);
                    return;
                }
            } else {
                console.error('Invalid response format for muscles:', response);
                return;
            }
            
            if (muscles.length > 0) {
                muscles.forEach(m => {
                    // Handle different property name formats
                    const id = m.Id || m.id || m.ID;
                    const name = m.Name || m.name || m.title || m.Title;
                    if (id && name) {
                        dropdown.append(`<option value="${id}">${name}</option>`);
                    }
                });
            }
        },
        error: function () {
            swal("Error", "Failed to load muscles!", "error");
        }
    });
}

function loadEquipments() {
    $.ajax({
        url: '/Equipment/GetAll',
        method: 'GET',
        success: function (response) {
            let dropdown = $('#equipmentId');
            dropdown.empty().append('<option value="" disabled selected>Select equipment</option>');
            
            console.log('Equipment API response:', response); // Debug log
            
            let equipment = [];
            
            // Handle different response formats
            if (response && response.data && Array.isArray(response.data)) {
                equipment = response.data;
            } else if (response && Array.isArray(response)) {
                equipment = response;
            } else if (response && response.result && Array.isArray(response.result)) {
                equipment = response.result;
            } else if (response && response.items && Array.isArray(response.items)) {
                equipment = response.items;
            } else if (response && typeof response === 'object') {
                // If it's an object, try to find array properties
                const possibleArrays = Object.values(response).filter(val => Array.isArray(val));
                if (possibleArrays.length > 0) {
                    equipment = possibleArrays[0];
                } else {
                    console.error('No array found in response object:', response);
                    return;
                }
            } else {
                console.error('Invalid response format for equipment:', response);
                return;
            }
            
            if (equipment.length > 0) {
                equipment.forEach(eq => {
                    // Handle different property name formats
                    const id = eq.Id || eq.id || eq.ID;
                    const name = eq.Name || eq.name || eq.title || eq.Title;
                    if (id && name) {
                        dropdown.append(`<option value="${id}">${name}</option>`);
                    }
                });
            }
        },
        error: function () {
            swal("Error", "Failed to load equipment!", "error");
        }
    });
}


// ======================= EXERCISES (MAIN) =========================
function loadExercisesPaged() {
    $.ajax({
        url: '/Exercise/GetPaged',
        method: 'GET',
        data: { page: currentPage, pageSize: pageSize, search: currentSearch },
        success: function (response) {
            let container = $('#exerciseContainer');
            container.empty();

            // Check if response has data property and it's an array
            let exercises = [];
            if (response && response.data && Array.isArray(response.data)) {
                exercises = response.data;
            } else if (response && Array.isArray(response)) {
                exercises = response;
            } else {
                console.error('Invalid response format for exercises:', response);
                container.html('<p class="text-center text-muted mt-3">Error loading exercises.</p>');
                $('.pagination').empty();
                return;
            }

            if (exercises.length === 0) {
                container.html('<p class="text-center text-muted mt-3">No exercises found.</p>');
                $('.pagination').empty();
                return;
            }

            exercises.forEach(item => {
                let imageUrl = item.ImageUrl && item.ImageUrl.trim() !== "" ? item.ImageUrl : '/images/default-exercise.jpg';
                let muscleName = item.MuscleName || "Unknown";
                let equipmentName = item.EquipmentName || "None";
                let description = item.Description || "No description available.";
                let videoLink = item.VideoLink && item.VideoLink.trim() !== "" ? item.VideoLink : null;

                container.append(`
                    <div class="col-lg-4 col-md-6 mb-4">
                        <div class="workout-card">
                            <img src="${imageUrl}" alt="${item.Name}" class="workout-card-image">
                            <div class="workout-card-body">
                                <h5 class="workout-card-title mb-2">${item.Name}</h5>

                                <div class="workout-card-meta mb-3">
                                    <span class="badge-custom badge-primary">${muscleName}</span>
                                    <span class="badge-custom badge-secondary">${equipmentName}</span>
                                </div>

                                <p class="text-muted small mb-3">${description}</p>

                                <div class="mt-3 d-flex gap-2">
                                    <button class="btn btn-sm btn-outline-primary flex-fill" onclick="getExerciseById(${item.Id})">
                                        <i class="bi bi-pencil me-1"></i> Edit
                            </button>
                                    <button class="btn btn-sm btn-outline-danger flex-fill" onclick="deleteExercise(${item.Id})">
                                        <i class="bi bi-trash me-1"></i> Delete
                            </button>
                                </div>

                                <div class="mt-3">
                                    ${videoLink
                        ? `<a href="${videoLink}" target="_blank" class="btn btn-outline-custom btn-sm w-100">
                                <i class="bi bi-play-circle me-2"></i> View Video
                            </a>`
                        : `<button class="btn btn-outline-custom btn-sm w-100" disabled>
                                <i class="bi bi-play-circle me-2"></i> No Video
                            </button>`}
                                </div>
                            </div>
                        </div>
                    </div>
                `);
            });

            renderPagination(response.currentPage, response.totalPages);
        },
        error: function (xhr) {
            console.error("Failed to load exercises.", xhr.responseText);
        }
    });
}



// ======================= PAGINATION =========================
function renderPagination(currentPage, totalPages) {
    const pagination = $('.pagination');
    pagination.empty();

    if (totalPages <= 1) return;

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
    loadExercisesPaged();
}


// ======================= CRUD =========================
function addExercise() {
    let exercise = {
        Name: $('#Name').val(),
        MuscleId: $('#muscleId').val(),
        EquipmentId: $('#equipmentId').val(),
        VideoLink: $('#videoLink').val(),
        Description: $('#description').val()
    };

    $.ajax({
        url: '/Exercise/Create',
        method: 'POST',
        data: exercise,
        success: function (response) {
            if (response.success) {
                swal({
                    title: "✅ Added Successfully!",
                    icon: "success",
                }).then(() => {
                    $('#addExerciseModal').modal('hide');
                    $('#exerciseForm')[0].reset();
                    loadExercisesPaged(); // يعيد تحميل التمارين
                });
            } else {
                swal("Error", response.message, "error");
            }
        },
        error: function () {
            swal("Error", "Failed to add exercise!", "error");
        }
    });
}

function getExerciseById(Id) {
    $.ajax({
        url: '/Exercise/GetById?id=' + Id,
        method: 'GET',
        success: function (response) {
            if (response.success) {
                let data = response.data;
                $('#exerciseId').val(data.Id);
                $('#Name').val(data.Name);
                $('#muscleId').val(data.MuscleId);
                $('#equipmentId').val(data.EquipmentId);
                $('#videoLink').val(data.VideoLink);
                $('#description').val(data.Description);
                $('#saveBtn').html('<i class="fas fa-save"></i> Update Exercise');
                $('#addExerciseModal').modal('show');
            } else swal("Error", response.message, "error");
        },
        error: function () {
            swal("Error", "Failed to fetch exercise!", "error");
        }
    });
}


function updateExercise() {
    let exercise = {
        Id: $('#exerciseId').val(),
        Name: $('#Name').val(),
        MuscleId: $('#muscleId').val(),
        EquipmentId: $('#equipmentId').val(),
        VideoLink: $('#videoLink').val(),
        Description: $('#description').val()
    };

    $.ajax({
        url: '/Exercise/Update',
        method: 'POST',
        data: exercise,
        success: function (response) {
            if (response.success) {
                swal("Updated!", response.message, "success");
                clearForm();
                loadExercisesPaged();
                $('#saveBtn').html('<i class="fas fa-save"></i> Save Exercise');
            } else swal("Error", response.message, "error");
        },
        error: function () {
            swal("Error", "Failed to update exercise!", "error");
        }
    });
}


function deleteExercise(Id) {
    swal({
        title: "Are you sure?",
        text: "This exercise will be permanently deleted!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                url: '/Exercise/Delete?id=' + Id,
                method: 'POST',
                success: function (response) {
                    if (response.success) {
                        swal("Deleted!", response.message, "success");
                        loadExercisesPaged();
                    } else swal("Error", response.message, "error");
                },
                error: function () {
                    swal("Error", "Failed to delete exercise!", "error");
                }
            });
        }
        else {
            html = '<div class="text-center text-muted">No clients found.</div>';
        }
    });
}
        


//$('#allClientsContainer').html(html);
//        },
//error: function () {
//    $('#allClientsContainer').html('<div class="text-center text-danger">Failed to load clients.</div>');
//}
//    });

//$('#viewAllClientsModal').modal('show');
//});




function clearForm() {
    $('#exerciseId').val('');
    $('#Name').val('');
    $('#muscleId').val('');
    $('#equipmentId').val('');
    $('#videoLink').val('');
    $('#description').val('');
}

$('#viewAllClientsBtn').on('click', function () {
    $('#allClientsContainer').html('<div class="text-center text-muted">Loading...</div>');

    $.ajax({
        url: '/Coach/GetAllClients',
        method: 'GET',
        success: function (response) {
            let clients = response.data;
            let html = '';

            if (clients.length > 0) {
                clients.forEach(client => {
                    html += `
                        <div class="d-flex align-items-center justify-content-between mb-3 pb-3 border-bottom">
                            <div class="d-flex align-items-center gap-3">
                                <div>
                                    <div class="fw-semibold">${client.Name}</div>
                                    <div class="text-muted small">Last payment: ${client.LastPaymentAgo}</div>
                                </div>
                            </div>
                            <div>
                                <span class="badge-custom ${client.IsActive ? 'badge-success' : 'badge-warning'}">
                                    ${client.IsActive ? 'Active' : 'Inactive'}
                                </span>
                            </div>
                        </div>
                    `;
                });
            } else {
                html = '<div class="text-center text-muted">No clients found.</div>';
            }

            $('#allClientsContainer').html(html);
        },
        error: function () {
            $('#allClientsContainer').html('<div class="text-center text-danger">Failed to load clients.</div>');
        }
    });

    $('#viewAllClientsModal').modal('show');
});


