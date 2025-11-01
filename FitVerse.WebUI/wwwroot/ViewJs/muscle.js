// ==========================
// Global Variables
// ==========================
let currentPage = 1;
let pageSize = 6;
let currentSearch = '';

// ==========================
// Document Ready
// ==========================
$(document).ready(function () {
    loadMusclePaged();
    loadAnatomyGroups(); // للـ Add Modal
    loadStats(); 
    // Search handler
    $('#searchMuscle').on('input', function () {
        currentSearch = $(this).val().trim();
        currentPage = 1;
        loadMusclePaged();
    });
    //filter handler
    $('#anatomyFilter').on('change', function () {
        currentPage = 1;
        loadMusclePaged();
    });
});

// ==========================
// Load Muscles (Paged)
// ==========================
function loadMusclePaged() {
    let anatomyFilter = $('#anatomyFilter').val();

    $.ajax({
        url: '/Muscle/GetPaged',
        method: 'GET',
        data: {
            page: currentPage,
            pageSize,
            search: currentSearch,
            anatomyId: anatomyFilter
        },
        success: function (response) {
            const tbody = $('#Data');
            tbody.empty();

            const data = response.Data || [];

            if (!Array.isArray(data)) {
                swal("Error", "Invalid data format returned from server.", "error");
                console.error("Response:", response);
                return;
            }

            data.forEach(item => {
                tbody.append(`
                    <tr>
                        <td>${item.Name}</td>
                        <td>${item.AnatomyName || 'Unknown'}</td>
                        <td>${item.ExerciseCount || 0} exercises</td>
                        <td>
                            <button class="btn btn-sm btn-outline-primary me-2"
                                    data-id="${item.Id}"
                                    data-name="${encodeURIComponent(item.Name)}"
                                    data-description="${encodeURIComponent(item.Description || '')}"
                                    data-anatomy="${item.AnatomyId}"
                                    onclick="openEditFromBtn(this)">
                                <i class="bi bi-pencil"></i> Edit
                            </button>
                            <button class="btn btn-sm btn-outline-danger"
                                    onclick="deleteMuscle(${item.Id})">
                                <i class="bi bi-trash"></i> Delete
                            </button>
                        </td>
                    </tr>
                `);
            });

            renderPagination(response.CurrentPage || 1, response.TotalPages || 1);
        },
        error: function () {
            swal("Error", "Failed to load muscles!", "error");
        }
    });
}


// ==========================
// Pagination
// ==========================
function renderPagination(currentPage, totalPages) {
    const pagination = $('.pagination');
    pagination.empty();

    const prevDisabled = currentPage === 1 ? 'disabled' : '';
    const nextDisabled = currentPage === totalPages ? 'disabled' : '';

    pagination.append(`<li class="page-item ${prevDisabled}"><a class="page-link" href="#" onclick="changePage(${currentPage - 1})">Previous</a></li>`);

    for (let i = 1; i <= totalPages; i++) {
        const active = i === currentPage ? 'active' : '';
        pagination.append(`<li class="page-item ${active}"><a class="page-link" href="#" onclick="changePage(${i})">${i}</a></li>`);
    }

    pagination.append(`<li class="page-item ${nextDisabled}"><a class="page-link" href="#" onclick="changePage(${currentPage + 1})">Next</a></li>`);
}

function changePage(page) {
    if (page < 1) return;
    currentPage = page;
    loadMusclePaged();
}

// ==========================
// Load Anatomy Groups
// ==========================
function loadAnatomyGroups(selectId = '#AnatomyGroup', selectedId = null) {
    $.ajax({
        url: '/Muscle/GetAnatomyGroups',
        method: 'GET',
        success: function (response) {
            // تحقق أن Data موجودة ومصفوفة
            if (!response || !response.Data || !Array.isArray(response.Data)) {
                console.error("Invalid data format returned from server.", response);
                return;
            }

            const select = $(selectId);
            select.empty();
            select.append('<option value="">Select body part...</option>');

            response.Data.forEach(item => {
                const selected = item.Id === selectedId ? 'selected' : '';
                select.append(`<option value="${item.Id}" ${selected}>${item.Name}</option>`);
            });
        },
        error: function () {
            $(selectId).html('<option value="">Failed to load</option>');
        }
    });
}

// ==========================
// Open Edit Modal
// ==========================
function openEditFromBtn(btn) {
    const id = $(btn).data('id');
    const name = decodeURIComponent($(btn).data('name'));
    const description = decodeURIComponent($(btn).data('description') || '');
    const anatomyId = $(btn).data('anatomy');

    $('#editMuscleId').val(id);
    $('#editName').val(name);
    $('#editDescription').val(description);

    loadAnatomyGroups('#editAnatomyGroup', anatomyId);
    $('#editMuscleModal').modal('show');
}

// ==========================
// CRUD Operations
// ==========================
function addMuscle() {
    const name = $('#Name').val().trim();
    const description = $('#Description').val().trim();
    const anatomyId = $('#AnatomyGroup').val();

    if (!name || !anatomyId) {
        swal("Error", "Please enter a name and select a body part.", "error");
        return;
    }

    $.post('/Muscle/Create', { Name: name, Description: description, AnatomyId: anatomyId }, function (response) {
        console.log("Response:", response);

        const success = response.success ?? response.Success;
        const message = response.message ?? response.Message;

        if (success) {
            swal("Added!", message || "Muscle added successfully!", "success");
            $('#addMuscleModal').modal('hide');
            $('#addMuscleForm')[0].reset();
            loadMusclePaged();
            loadStats();
        } else {
            swal("Error", message || "Something went wrong!", "error");
        }
    });

}


function updateMuscle() {
    const id = $('#editMuscleId').val();
    const name = $('#editName').val();
    const description = $('#editDescription').val();
    const anatomyId = $('#editAnatomyGroup').val();

    if (!name || !anatomyId) {
        swal("Error", "Name and Anatomy Group are required!", "error");
        return;
    }

    $.post('/Muscle/Update',
        { Id: id, Name: name, Description: description, AnatomyId: anatomyId },
        function (response) {
            if (response.Success) {
                swal("Updated!", response.Message || "Updated successfully!", "success");
                $('#editMuscleModal').modal('hide');
                loadMusclePaged();
                loadStats();
            } else {
                swal("Error", response.Message || "Something went wrong!", "error");
            }
        }
    );

}

function deleteMuscle(id) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover this muscle!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then(willDelete => {
        if (willDelete) {
            $.post('/Muscle/Delete', { id }, function (response) {
                if (response.success) {
                    swal("Deleted!", response.message, "success");
                    loadMusclePaged();
                    loadStats();
                } else {
                    swal("Error", response.message, "error");
                }
            });
        }
    });
}
function loadStats() {
    $.ajax({
        url: '/Muscle/GetStats',
        method: 'GET',
        success: function (response) {
            $('.stat-card .stat-value').eq(0).text(response.TotalMuscles);
            $('.stat-card .stat-value').eq(1).text(response.TotalAnatomyGroups);
            $('.stat-card .stat-value').eq(2).text(response.TotalExercises);
        },
        error: function () {
            console.error("Failed to load stats");
        }
    });
}

