$(function () {
    $("#coachForm").submit(function (e) {
        e.preventDefault();
        let formData = new FormData(this);
        // checkbox دائمًا
        formData.set("IsActive", $('#IsActive').is(':checked') ? "true" : "false");
        // معرفة أي زر ضغط المستخدم
        let submitterId = e.originalEvent?.submitter?.id;
        let url = submitterId === "UpdateCoach" ? '/Coach/UpdateCoach' : '/Coach/AddCoach';
        $.ajax({
            url: url,
            method: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (res) {
                if (res.success) {
                    swal(submitterId === "UpdateCoach" ? "✅ Updated" : "✅ Success", res.message, "success");
                    $("#coachForm")[0].reset();
                    LoadCoaches();

                    // إظهار/إخفاء الأزرار
                    if (submitterId === "UpdateCoach") {
                        $('#UpdateCoach').hide();
                        $('#SaveCoach').show();
                    }
                } else {
                    swal("❌ Error", res.message, "error");
                }
            },
            error: function () {
                swal("⚠️ Warning", "An error occurred.", "error");
            }
        });
    });
});



$(document).ready(function () {
    LoadCoaches();
});

function LoadCoaches() {
    $.ajax({
        url: '/Coach/GetAllCoaches',
        method: 'GET',
        success: function (res) {
            $('#UpdateCoach').hide();
            $('#SaveCoach').show();
            if (res.success) {
                let coaches = res.data;
                let coachTableBody = $("#Data");
                coachTableBody.empty();

                coaches.forEach(coach => {
                    let status = coach.IsActive ? "✅ Active" : "❌ Inactive";
                    let row = `
                        <tr>
                            <td>${coach.Name}</td>
                            <td>${coach.Name}</td>
                            <td>${coach.Title}</td>
                            <td>${coach.About}</td>
                            <td><img src="${coach.ImagePath}" alt="${coach.Name}" width="80"/></td>
                            <td>${status}</td>

                            <td class="actions">
                                <button type="button" onclick="getById('${coach.Id}')" class="btn-icon" title="Edit">
                                    <i class="fas fa-edit"></i>
                                </button>
                                <button type="button" onclick="DeleteCoach('${coach.Id}')" class="btn-icon text-danger" title="Delete">
                                    <i class="fas fa-trash-alt"></i>
                                </button>
                            </td>
                        </tr>`;
                    coachTableBody.append(row);
                });
            } else {
                swal("❌ Error", res.message, "error");
            }
        },
        error: function () {
            swal("⚠️ Warning", "An error occurred while fetching coaches.", "error");
        }
    });
}

function getById(id) {
    $.ajax({
        url: '/Coach/GetCoachById?id=' + id,
        method: 'GET',
        success: function (response) {
            if (response.success) {
                let coach = response.data;
                console.log("IsActive value:", response.data.IsActive, typeof coach.IsActive);
                $('#coachId').val(coach.Id);
                $('#coachName').val(coach.Name);
                $('#coachTitle').val(coach.Title);
                $('#coachAbout').val(coach.About);
                $('#IsActive').prop('checked', coach.IsActive);
                $('#UpdateCoach').show();
                $('#SaveCoach').hide();
            } else {
                swal("❌ Error", response.message, "error");
            }
        },
        error: function () {
            swal("⚠️ Warning", "Could not load coach details.", "error");
        }
    });
}

function DeleteCoach(id) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover this coach!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                url: '/Coach/DeleteCoach?id=' + id,
                method: 'DELETE',
                success: function (res) {
                    if (res.success) {
                        swal("✅ Success", res.message, "success");
                        LoadCoaches();
                    } else {
                        swal("❌ Error", res.message, "error");
                    }
                },
                error: function () {
                    swal("⚠️ Warning", "An error occurred while deleting the coach.", "error");
                }
            });
        }
    });

}
let currentPage = 1;
let pageSize = 10;
let currentSearch = "";

function loadEquipments(page = 1, search = "") {
    currentPage = page;
    currentSearch = search;

    $.ajax({
        url: '/Coach/GetPaged',
        method: 'GET',
        data: { page: page, pageSize: pageSize, search: search },
        success: function (res) {
            let tbody = $('#Data');
            tbody.empty();

            if (!res.data || res.data.length === 0) {
                tbody.append('<tr><td colspan="6">No results found.</td></tr>');
            } else {
                res.data.forEach(item => {
                    let status = item.IsActive ? "✅ Active" : "❌ Inactive";
                    let row = `
                        <tr>
                            <td>${item.Name}</td>
                            <td>${item.Title}</td>
                            <td>${item.About}</td>
                            <td><img src="${item.ImagePath}" alt="${item.Name}" width="80"/></td>
                            <td>${status}</td>
                            <td class="actions">
                                <button type="button" onclick="getById('${item.Id}')" class="btn-icon" title="Edit">
                                    <i class="fas fa-edit"></i>
                                </button>
                                <button type="button" onclick="DeleteCoach('${item.Id}')" class="btn-icon text-danger" title="Delete">
                                    <i class="fas fa-trash-alt"></i>
                                </button>
                            </td>
                        </tr>`;
                    tbody.append(row);
                });
            }

            renderPagination(res.totalPages);
        },
        error: function () {
            alert('Error loading equipments.');
        }
    });
}

function renderPagination(totalPages) {
    let pagination = $('#pagination');
    pagination.empty();

    for (let i = 1; i <= totalPages; i++) {
        let active = (i === currentPage) ? 'active' : '';
        pagination.append(`
            <li class="page-item ${active}">
                <a class="page-link" href="#" onclick="loadEquipments(${i}, '${currentSearch}')">${i}</a>
            </li>
        `);
    }
}

$('#searchInput').on('keyup', function () {
    let searchTerm = $(this).val().trim();
    loadEquipments(1, searchTerm); 
});

$(document).ready(function () {
    loadEquipments();
});
