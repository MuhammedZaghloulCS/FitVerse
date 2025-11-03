let currentPage = 1;
let pageSize = 5;
let currentSearch = "";

$(document).ready(function () {
    loadAnatomy();
});
function loadAnatomy() {
    $.ajax({
        url: '/Anatomy/GetAll',
        method: 'GET',
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
                    </tr>`);
            });
        },
    })
}
function addAnatomy() {
    var name = $('#name').val();
    if (name === '') {

        swal("Error", "Name is required!", "error");
        return;
    }
    $.ajax({
        url: '/Anatomy/Create',
        method: 'POST',
        data: { name: name },
        success: function (response) {
            if (response.success) {

                swal("Good job!", `${response.message}`, "success");

                loadAnatomy();
                $('#Name').val('');
            } else {
                swal("Error", `${response.message}`, "error");

            }
        },
    })
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

