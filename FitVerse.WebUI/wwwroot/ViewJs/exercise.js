$(document).ready(function () {
    loadExercises();
    loadMuscles();
    loadEquipments();

    // Handle form submit (Add / Update)
    $('#exerciseForm').on('submit', function (e) {
        e.preventDefault();
        let id = $('#exerciseId').val();
        if (id) updateExercise();
        else addExercise();

        e.target.reset();
    });

    // Reset button
    $('#resetBtn').click(function () {
        clearForm();
    });

    // Search feature
    $('#searchExercise').on('input', function () {
        let value = $(this).val().toLowerCase();
        $('#exerciseTable tbody tr').filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
        });
    });
});



function loadMuscles() {
    $.ajax({
        
        url: 'Muscle/GetAll',
        method: 'GET',
        success: function (response) {
            let dropdown = $('#muscleId');
            dropdown.empty().append('<option value="" disabled selected>Select muscle</option>');
            response.data.forEach(function (m) {
                dropdown.append(`<option value="${m.Id}">${m.Name}</option>`);
            });
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
            response.data.forEach(function (eq) {
                dropdown.append(`<option value="${eq.Id}">${eq.Name}</option>`);
            });
        },
        error: function () {
            swal("Error", "Failed to load equipment!", "error");
        }
    });
}

function loadExercises() {
    $.ajax({
        url: '/Exercise/GetAll',
        method: 'GET',
        success: function (response) {
            let table = $('#exerciseTable tbody');
            table.empty();
            response.data.forEach(function (item) {
                console.log(item.MuscleName)

                table.append(`
                    <tr>
                        <td>#${item.Id}</td>
                        <td>${item.Name}</td>
                        <td>${item.MuscleName}</td>
                        <td>${item.EquipmentName}</td>
                        <td><a href="${item.VideoLink}" target="_blank">View</a></td>
                        <td>${item.Description}</td>
                        <td class="actions">
                            <button type="button" onclick="getExerciseById(${item.Id})" class="btn-icon" title="Edit">
                                <i class="fas fa-edit"></i>
                            </button>
                            <button type="button" onclick="deleteExercise(${item.Id})" class="btn-icon text-danger" title="Delete">
                                <i class="fas fa-trash-alt"></i>
                            </button>
                        </td>
                    </tr>
                `);
            });
        },
        
        error: function (xhr, status, error) {
            // Try to extract detailed message
            let errMsg =
                xhr.responseJSON?.message ||
                xhr.responseText ||
                error ||
                "An unknown error occurred.";

            swal("Error", "Failed to load exercises!\n" + errMsg, "error");
        }
    });
}
 
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
                swal("Added!", response.message, "success");
                clearForm();
                loadExercises();
            } else {
                swal("Error", response.message, "error");
            }
        },
        error: function (err) {
            swal("Error", "Failed to add exercise!" , "error");
        }
    });
}

function getExerciseById(id) {
    $.ajax({
        url: '/Exercise/GetById?id=' + id,
        method: 'GET',
        success: function (response) {
            if (response.success) {
                let data = response.data;
                $('#exerciseId').val(data.id);
                $('#exerciseName').val(data.name);
                $('#muscleId').val(data.muscleId);
                $('#equipmentId').val(data.equipmentId);
                $('#videoLink').val(data.videoLink);
                $('#description').val(data.description);
                $('#saveBtn').html('<i class="fas fa-save"></i> Update Exercise');
            } else {
                swal("Error", response.message, "error");
            }
        },
        error: function () {
            swal("Error", "Failed to fetch exercise!", "error");
        }
    });
}

function updateExercise() {
    let exercise = {
        Id: $('#exerciseId').val(),
        Name: $('#exerciseName').val(),
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
                loadExercises();
                $('#saveBtn').html('<i class="fas fa-save"></i> Save Exercise');
            } else {
                swal("Error", response.message, "error");
            }
        },
        error: function () {
            swal("Error", "Failed to update exercise!", "error");
        }
    });
}

function deleteExercise(id) {
    swal({
        title: "Are you sure?",
        text: "This exercise will be permanently deleted!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                url: '/Exercise/Delete?id=' + id,
                method: 'POST',
                success: function (response) {
                    if (response.success) {
                        swal("Deleted!", response.message, "success");
                        loadExercises();
                    } else {
                        swal("Error", response.message, "error");
                    }
                },
                error: function () {
                    swal("Error", "Failed to delete exercise!", "error");
                }
            });
        }
    });
}

function clearForm() {
    $('#exerciseId').val('');
    $('#exerciseName').val('');
    $('#muscleId').val('');
    $('#equipmentId').val('');
    $('#videoLink').val('');
    $('#description').val('');
}
