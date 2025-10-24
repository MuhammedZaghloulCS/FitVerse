let currentPage = 1;
let pageSize = 5;
let currentSearch = "";

$(document).ready(function () {
    loadAnatomy();
    $('#saveAnatomyBtn').click(function () {
        addAnatomy();
    });
    //loadMuscleCount();
    //loadAnatomyCount();
});
$(document).ready(function () {
    loadAnatomy();
});

// ✅ تحميل كل الـ Anatomy
function loadAnatomy() {
    $.ajax({
        url: '/Anatomy/GetAll',
        method: 'GET',
        success: function (response) {
            let container = $('#anatomyContainer');
            container.empty();

            if (!response.data || response.data.length === 0) {
                container.html('<div class="text-center text-muted">No anatomy found.</div>');
                return;
            }

            response.data.forEach(function (item) {
                container.append(`
                    <div class="col-lg-3 col-md-4 col-sm-6">
                        <div class="p-3 border rounded shadow-sm">
                            <div class="d-flex justify-content-between align-items-start mb-2">
                                <h6 class="fw-bold mb-0">${item.name}</h6>

                                <div class="dropdown">
                                    <button class="btn btn-sm btn-outline-custom" data-bs-toggle="dropdown">
                                        <i class="bi bi-three-dots-vertical"></i>
                                    </button>
                                    <ul class="dropdown-menu">
                                        <li><a class="dropdown-item" href="#" onclick="editAnatomy(${item.id}, '${item.name}')">
                                            <i class="bi bi-pencil me-2"></i>Edit</a></li>
                                        <li><a class="dropdown-item" href="#" onclick="viewMuscles(${item.id}, '${item.name}')">
                                            <i class="bi bi-eye me-2"></i>View Muscles</a></li>
                                        <li><hr class="dropdown-divider"></li>
                                        <li><a class="dropdown-item text-danger" href="#" onclick="deleteAnatomy(${item.id})">
                                            <i class="bi bi-trash me-2"></i>Delete</a></li>
                                    </ul>
                                </div>
                            </div>
                            <p class="text-muted small mb-2">No description available</p>
                            <div class="d-flex gap-2">
                                <span class="badge bg-primary">${item.muscleCount || 0} Muscles</span>
                                <span class="badge bg-success">${item.exerciseCount || 0} Exercises</span>
                            </div>
                        </div>
                    </div>
                `);
            });
        },
        error: function () {
            swal("Error", "Failed to load anatomy!", "error");
        }
    });
}
function viewMuscles(anatomyId) {
    $.ajax({
        url: '/Anatomy/GetMusclesByAnatomyId', // الأكشن اللي بيرجع العضلات
        method: 'GET',
        data: { anatomyId: anatomyId },
        success: function (response) {
            let container = $('#musclesList');
            container.empty(); // نفرّغ اللي جوه المودال قبل نعرض الجديد

            if (response.data && response.data.length > 0) {
                // لو في عضلات.. اعرضها في كروت
                response.data.forEach(function (muscle) {
                    container.append(`
                        <div class="col-md-6">
                            <div class="p-3 border rounded shadow-sm">
                                <div class="d-flex justify-content-between align-items-center mb-1">
                                    <h6 class="fw-bold mb-0">${muscle.name}</h6>

                                    </div>
                                </div>
                            </div>
                        </div>
                    `);
                });
            } else {
                // لو مفيش عضلات
                container.html('<div class="text-center text-muted">No muscles found for this anatomy.</div>');
            }

            // افتح المودال بعد تحميل البيانات
            $('#viewMusclesModal').modal('show');
        },
        error: function () {
            swal("Error", "Failed to load muscles!", "error");
        }
    });
}


// ✅ حذف Anatomy
function deleteAnatomy(id) {
    swal({
        title: "Are you sure?",
        text: "This anatomy will be deleted permanently!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                url: '/Anatomy/Delete?id=' + id,
                method: 'POST',
                success: function (response) {
                    if (response.success) {
                        swal("Deleted!", response.message, "success");
                        loadAnatomy();
                    } else {
                        swal("Error", response.message, "error");
                    }
                },
                error: function () {
                    swal("Error", "Failed to delete anatomy!", "error");
                }
            });
        }
    });
}

// ✅ تعديل Anatomy
function editAnatomy(id, name) {
    swal({
        title: "Edit Anatomy",
        content: {
            element: "input",
            attributes: {
                value: name,
                placeholder: "Enter new name",
            },
        },
        buttons: true,
    }).then((newName) => {
        if (newName && newName.trim() !== "") {
            $.ajax({
                url: '/Anatomy/Update',
                method: 'POST',
                data: { Id: id, Name: newName },
                success: function (response) {
                    if (response.success) {
                        swal("Updated!", response.message, "success");
                        loadAnatomy();
                    } else {
                        swal("Error", response.message, "error");
                    }
                },
                error: function () {
                    swal("Error", "Failed to update anatomy!", "error");
                }
            });
        }
    });
}

// ✅ عرض العضلات المرتبطة بـ Anatomy





function addAnatomy() {
    var name = $('#Name').val().trim();

    if (name === '') {
        swal("Error", "Name is required!", "error");
        return;
    }

    $.ajax({
        url: '/Anatomy/Create',
        method: 'POST',
        data: { Name: name }, // لازم نفس اسم البارام في AddAnatomyVM
        success: function (response) {
            if (response.success) {
                swal({
                    title: "✅ Added Successfully!",
                    text: response.message,
                    icon: "success",
                }).then(() => {
                    $('#addAnatomyModal').modal('hide'); // إخفاء المودال
                    $('#Name').val(''); // تفريغ الحقل
                    loadAnatomy(); // تحديث الجدول أو القائمة
                });
            } else {
                swal("Error", response.message, "error");
            }
        },
        error: function () {
            swal("Error", "Failed to add anatomy!", "error");
        }
    });
}

function getById(id) {
    $.ajax({
        url: '/Anatomy/GetById?id=' + id,
        method: 'GET',
        success: function (response) {
            if (response.success) {
                $('#Id').val(response.data.id);
                $('#name').val(response.data.name);
                $('#addBtn').hide();
                $('#editBtn').show();
            } else {
                Swal.fire({
                    title: "Error",
                    text: `${response.message}`,
                    icon: "error"
                });
            }
        },
    });
}

function updateAnatomy() {
    var id = $('#Id').val();
    var name = $('#name').val();
    if (name === '') {
        swal("Error", "Name is required!", "error");

        return;
    }
    $.ajax({
        url: '/Anatomy/Update',
        method: 'POST',
        data: { id: id, name: name },
        success: function (response) {
            if (response.success) {
                swal("Good job!", `${response.message}`, "success");

                $('#addBtn').show();
                $('#editBtn').hide();
                loadAnatomy();
                $('#Id').val('');
                $('#name').val('');
            } else {
                swal("Error", `${response.message}`, "error");

            }
        },
    })
}

function Delete(id) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover this !",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    url: '/Anatomy/Delete?id=' + id,
                    method: 'POST',
                    success: function (response) {
                        if (response.success) {
                            swal("Good job!", `${response.message}`, "success");
                            loadAnatomy();
                        } else {
                            swal("Error", `${response.message}`, "error");
                        }
                    },
                })

            } else {
                swal("Your imaginary file is safe!");
            }
        });
}





$(document).ready(function () {
    loadAnatomyPaged();

    $('#searchAnatomy').on('input', function () {
        currentSearch = $(this).val().trim();
        currentPage = 1; 
        loadAnatomyPaged();
    });
});

function loadAnatomyPaged() {
    $.ajax({
        url: '/Anatomy/GetPaged',
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
    loadAnatomyPaged();
}
//function loadMuscleCount() {
//    $.ajax({
//        url: '/Anatomy/GetMuscleCount',
//        method: 'GET',
//        success: function (response) {
//            if (response.count !== undefined) {
//                $('.stat-value').text(response.count);
//            } else {
//                $('.stat-value').text('0');
//            }
//        },
//        error: function () {
//            $('.stat-value').text('Error');
//        }


//    });
//}
//function loadAnatomyCount() {
//    $.ajax({
//        url: '/Anatomy/GetAllCount',
//        method: 'GET',
//        success: function (response) {
//            $('#anatomyCount').text(response.count ?? 0);
//        },
//        error: function () {
//            $('#anatomyCount').text('Error');
//        }
//    });
//}