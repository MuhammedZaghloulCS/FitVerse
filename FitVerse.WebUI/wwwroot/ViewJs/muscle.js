$(document).ready(function () {
    loadAnatomyGroups();
    loadMuscles();
    $('#editBtn').hide();
});

function loadMuscles() {
    $.ajax({
        url: '/Muscle/GetAll',
        method: 'GET',
        success: function (response) {
            $('#Data').empty();
            response.data.forEach(function (item) {
                $('#Data').append(`
                    <tr>
                        <td>#${item.Id}</td>
                        <td>${item.Name}</td>
                        <td>${item.AnatomyName}</td>
                        <td class="actions">
                            <button type="button" onclick="getById(${item.Id})" class="btn-icon" title="Edit">
                                <i class="fas fa-edit"></i>
                            </button>
                            <button type="button" onclick="Delete(${item.Id})" class="btn-icon text-danger" title="Delete">
                                <i class="fas fa-trash-alt"></i>
                            </button>
                        </td>
                    </tr>`);
            });
        },
        error: function () {
            swal("Error", "Failed to load muscles!", "error");
        }
    });
}

function loadAnatomyGroups() {
    $.ajax({
        url: '/Muscle/GetAnatomyGroups',
        method: 'GET',
        success: function (response) {
            var dropdown = $('#AnatomyGroup');
            dropdown.empty();
            dropdown.append('<option value="">Select Anatomy Group</option>');
            response.data.forEach(function (item) {
                dropdown.append(`<option value="${item.id}">${item.name}</option>`);
            });
        },
        error: function () {
            swal("Error", "Failed to load anatomy groups!", "error");
        }
    });
}

function addMuscle() {
    let name = $("#Name").val();
    let anatomyName = $("#AnatomyGroup option:selected").text(); 

    $.ajax({
        url: '/Muscle/Create',
        method: 'POST',
        data: {
            Name: name,
            AnatomyName: anatomyName
        },
        success: function (response) {
                    if (response.success) {
                        swal("Added!", response.message, "success");
                     
                    } else {
                        swal("Error", response.message, "error");
                    }
                },
    });
}


function getById(id) {
    $.ajax({
        url: '/Muscle/GetById?id=' + id,
        method: 'GET',
        success: function (response) {
            if (response.success) {
                $('#Id').val(response.data.id);
                $('#Name').val(response.data.name);
                $('#AnatomyGroup').val(response.data.anatomyId); 
                $('#addBtn').hide();
                $('#editBtn').show()
                  
            } else {
                swal("Error", response.message, "error");
            }
        },
        error: function () {
            swal("Error", "Failed to fetch muscle data!", "error");
        }
    });
}

function updateMuscle() {
    var id = $('#Id').val();
    var name = $('#Name').val();
    var anatomyGroup = $('#AnatomyGroup').val();

    if (name === '' || !anatomyGroup) {
        swal("Error", "Name and Anatomy Group are required!", "error");
        return;
    }

    $.ajax({
        url: '/Muscle/Update',
        method: 'POST',
        data: {
            Id: id,
            Name: name,
            AnatomyId: anatomyGroup  
        },
        success: function (response) {
            console.log(response);
            if (response.success) {
                swal("Updated!", response.message, "success");
                $('#addBtn').show();
                $('#editBtn').hide();
                clearForm();
                loadMuscles();
            } else {
                swal("Error", response.message, "error");
            }
        },
        error: function (xhr) {
            console.log(xhr.responseText);
            swal("Error", "Something went wrong!", "error");
        }
    });
}


function Delete(id) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover this muscle!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                url: '/Muscle/Delete?id=' + id,
                method: 'POST',
                success: function (response) {
                    if (response.success) {
                        swal("Deleted!", response.message, "success");
                        loadMuscles();
                    } else {
                        swal("Error", response.message, "error");
                    }
                },
                error: function () {
                    swal("Error", "Failed to delete muscle!", "error");
                }
            });
        } else {
            swal("Action canceled!");
        }
    });
}
$(document).ready(function () {
    loadMusclePaged();

    $('#searchMuscle').on('input', function () {
        currentSearch = $(this).val().trim();
        currentPage = 1;
        loadMusclePaged();
    });
});

function loadMusclePaged() {
    $.ajax({
        url: '/Muscle/GetPaged',
        method: 'GET',
        data: {
            page: currentPage,
            pageSize: pageSize,
            search: currentSearch
        },
        success: function (response) {
            $('#Data').empty();
            response.data.forEach(function (item) {
                $('#Data').append(`
                    <tr>
                        <td>#${item.id}</td>
                        <td>${item.name}</td>
                        <td class="actions">
                            <button type="button" onclick="getById(${item.id})" class="btn-icon" title="Edit">
                                <i class="fas fa-edit"></i>
                            </button>
                            <button type="button" onclick="Delete(${item.id})" class="btn-icon text-danger" title="Delete">
                                <i class="fas fa-trash-alt"></i>
                            </button>
                        </td>
                    </tr>
                `);
            });

            renderPagination(response.currentPage, response.totalPages);
        }
    });
}

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
    loadMusclePaged();
 
}
function clearForm() {
    $('#Id').val('');
    $('#Name').val('');
    $('#AnatomyGroup').val('');
}
