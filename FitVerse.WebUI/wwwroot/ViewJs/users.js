var allData;
var currentPage = 1;
var rowsPerPage = 5;

$(function () {
    GetAllUsers();
    SearchByNameOrEmail();
    searchByRole();
    searchByActivation(); // <-- call this
    reset();
});

function GetAllUsers() {
    $.ajax({
        method: 'get',
        url: '/Users/GetAllUsers',
        success: function (data) {
            allData = data.data;
            currentPage = 1;
            renderUsersPaginated(allData, currentPage);
            renderPagination(allData);
        },
        error: function (xhr, status, err) {
            console.log("err: " + err);
        }
    });
}

function SearchByNameOrEmail() {
    $('#search-by').on('input', function () {
        var value = $('#search-by').val();
        let filtered = allData;
        if (value) {
            filtered = allData.filter(u =>
                u.UserName.toLowerCase().includes(value.toLowerCase()) ||
                u.Email.toLowerCase().includes(value.toLowerCase())
            );
        }
        currentPage = 1;
        renderUsersPaginated(filtered, currentPage);
        renderPagination(filtered);
    });
}

function searchByRole() {
    $('#selected-role').on('change', function () {
        filterAll();
    });
}

function searchByActivation() {
    $('#activation').on('change', function () {
        filterAll();
    });
}

// Combine filters
function filterAll() {
    let role = $('#selected-role').val();
    let status = $('#activation').val();
    let searchVal = $('#search-by').val().toLowerCase();

    let filtered = allData;

    // Filter by search
    if (searchVal) {
        filtered = filtered.filter(u =>
            u.UserName.toLowerCase().includes(searchVal) ||
            u.Email.toLowerCase().includes(searchVal)
        );
    }

    // Filter by role
    if (role === '1') filtered = filtered.filter(u => u.Role.toLowerCase() === 'coach');
    if (role === '2') filtered = filtered.filter(u => u.Role.toLowerCase() === 'client');

    // Filter by status
    if (status === '1') filtered = filtered.filter(u => u.Status.toLowerCase() === 'active');
    if (status === '2') filtered = filtered.filter(u => u.Status.toLowerCase() === 'inactive');

    currentPage = 1;
    renderUsersPaginated(filtered, currentPage);
    renderPagination(filtered);
}

function reset() {
    $('#search-by').val('');
    $('#selected-role').val('0');
    $('#activation').val('0');
    $('#reset').on('click', function () {
        currentPage = 1;
        renderUsersPaginated(allData, currentPage);
        renderPagination(allData);
    });
}

function renderUsersPaginated(users, page) {
    $('#all-users').empty();
    let start = (page - 1) * rowsPerPage;
    let end = start + rowsPerPage;
    let paginatedUsers = users.slice(start, end);

    paginatedUsers.forEach(user => {
        $('#all-users').append(`
            <tr>
                <td><input type="checkbox" class="form-check-input"></td>
                <td>
                    <div class="d-flex align-items-center gap-3">
                        <img src="https://ui-avatars.com/api/?name=${user.UserName}&background=6366f1&color=fff" alt="User" style="width: 40px; height: 40px; border-radius: 50%;">
                        <div>
                            <div class="fw-semibold">${user.UserName}</div>
                            <small class="text-muted">ID: #${user.Id}</small>
                        </div>
                    </div>
                </td>
                <td><span class="badge-custom badge-primary">${user.Role}</span></td>
                <td>${user.Email}</td>
                <td>${new Date(user.JoinedDate).toLocaleDateString('en-US', { day: 'numeric', month: 'short', year: 'numeric' })}</td>
                <td>
                  <span class="badge-custom ${user.Status.toLowerCase() === 'active' ? 'badge-success' : 'badge-danger'}">
                    ${user.Status}
                  </span>
                </td>
                                                <td>
                                    <div class="d-flex gap-2">
                                        <button class="btn btn-sm btn-outline-custom" title="View Details">
                                            <i class="bi bi-eye"></i>
                                        </button>
                                        <button class="btn btn-sm btn-outline-custom" title="Edit">
                                            <i class="bi bi-pencil"></i>
                                        </button>
                                        <div class="dropdown">
                                            <button class="btn btn-sm btn-outline-custom" data-bs-toggle="dropdown">
                                                <i class="bi bi-three-dots-vertical"></i>
                                            </button>
                                            <ul class="dropdown-menu">
                                                <li><a class="dropdown-item" href="#"><i class="bi bi-check-circle me-2"></i>Activate</a></li>
                                                <li><a class="dropdown-item" href="#"><i class="bi bi-x-circle me-2"></i>Deactivate</a></li>
                                                <li><a class="dropdown-item" href="#"><i class="bi bi-arrow-repeat me-2"></i>Change Role</a></li>
                                                <li><hr class="dropdown-divider"></li>
                                                <li><a class="dropdown-item text-danger" href="#"><i class="bi bi-trash me-2"></i>Delete</a></li>
                                            </ul>
                                        </div>
                                    </div>
                                </td>
            </tr>
        `);
    });

    $('#no-of-users').text(users.length);
    $('#no-of-users2').text(users.length);
}

function renderPagination(users) {
    const totalPages = Math.ceil(users.length / rowsPerPage);
    const paginationEl = $('.pagination');
    paginationEl.empty();

    if (totalPages <= 1) return;

    paginationEl.append(`
        <li class="page-item ${currentPage === 1 ? 'disabled' : ''}">
            <a class="page-link" href="#" onclick="goToPage(${currentPage - 1})">Previous</a>
        </li>
    `);

    for (let i = 1; i <= totalPages; i++) {
        paginationEl.append(`
            <li class="page-item ${currentPage === i ? 'active' : ''}">
                <a class="page-link" href="#" onclick="goToPage(${i})">${i}</a>
            </li>
        `);
    }

    paginationEl.append(`
        <li class="page-item ${currentPage === totalPages ? 'disabled' : ''}">
            <a class="page-link" href="#" onclick="goToPage(${currentPage + 1})">Next</a>
        </li>
    `);
}

function goToPage(page) {
    const totalPages = Math.ceil(allData.length / rowsPerPage);
    if (page < 1) page = 1;
    if (page > totalPages) page = totalPages;
    currentPage = page;
    filterAll(); // keep filters applied when changing page
}
