var allData;
var filteredData = [];
var currentPage = 1;
var rowsPerPage = 5;

$(function () {
    GetAllUsers();
    setupSearch();
    setupRoleFilter();
    setupActivationFilter();
    setupResetButton();
    setupAddUserModalReset();
    EditUser();
    DeleteUser();
    ExportData();
});

// ============================
// Fetch All Users
// ============================
function GetAllUsers() {
    $.ajax({
        method: 'get',
        url: '/Admin/Users/GetAllUsers',
        success: function (data) {
            allData = data.data;
            filteredData = allData;
            currentPage = 1;
            renderUsersPaginated(filteredData, currentPage);
            renderPagination(filteredData);
        },
        error: function (xhr, status, err) {
            console.log("Error fetching users: " + err);
        }
    });
}

// ============================
// Search by name or email
// ============================
function setupSearch() {
    $('#search-by').on('input', function () {
        filterAll();
    });
}

// ============================
// Filter by role
// ============================
function setupRoleFilter() {
    $('#selected-role').on('change', function () {
        filterAll();
    });
}

// ============================
// Filter by activation status
// ============================
function setupActivationFilter() {
    $('#activation').on('change', function () {
        filterAll();
    });
}

// ============================
// Combine all filters together
// ============================
function filterAll() {
    let role = $('#selected-role').val();
    let status = $('#activation').val();
    let searchVal = $('#search-by').val().toLowerCase();

    filteredData = allData;

    // Search filter
    if (searchVal) {
        filteredData = filteredData.filter(u =>
            u.FullName.toLowerCase().includes(searchVal) ||
            u.Email.toLowerCase().includes(searchVal)
        );
    }

    // Role filter
    if (role === '1') filteredData = filteredData.filter(u => u.Role.toLowerCase() === 'coach');
    if (role === '2') filteredData = filteredData.filter(u => u.Role.toLowerCase() === 'client');
    if (role === '3') filteredData = filteredData.filter(u => u.Role.toLowerCase() === 'admin');

    // Status filter
    if (status === '1') filteredData = filteredData.filter(u => u.Status.toLowerCase() === 'active');
    if (status === '2') filteredData = filteredData.filter(u => u.Status.toLowerCase() === 'inactive');

    currentPage = 1;
    renderUsersPaginated(filteredData, currentPage);
    renderPagination(filteredData);
}

// ============================
// Reset filters
// ============================
function setupResetButton() {
    $('#reset').on('click', function () {
        $('#search-by').val('');
        $('#selected-role').val('0');
        $('#activation').val('0');
        filteredData = allData;
        currentPage = 1;
        renderUsersPaginated(allData, currentPage);
        renderPagination(allData);
    });
}

// ============================
// Render users for current page
// ============================
function renderUsersPaginated(users, page) {
    $('#all-users').empty();
 
    if (users.length === 0) {
        $('#all-users').append(`
            <tr>
                <td colspan="7" class="text-center text-muted py-4">
                    No users found
                </td>
            </tr>
        `);
        $('#no-of-users').text(0);
        $('#no-of-users2').text(0);
        return;
    }

    let start = (page - 1) * rowsPerPage;
    let end = start + rowsPerPage;
    let paginatedUsers = users.slice(start, end);
    $('#p-first-row-number').text(`${start+1}`)
    $('#p-last-row-number').text(`${end}`)
    paginatedUsers.forEach(user => {
        $('#all-users').append(`
            <tr>
                <td><input type="checkbox" class="form-check-input"></td>
                <td>
                    <div class="d-flex align-items-center gap-3">
                        <img src="https://ui-avatars.com/api/?name=${user.FullName}&background=6366f1&color=fff" 
                             alt="User" style="width: 40px; height: 40px; border-radius: 50%;">
                        <div>
                            <div class="fw-semibold">${user.FullName}</div>
                            <small class="text-muted">Phone: ${user.PhoneNumber}</small>
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

                                        <button id="edit" class="btn btn-sm btn-outline-custom" title="Edit">
                                            <i class="bi bi-pencil"></i>
                                        </button>
                                        <div class="dropdown">
                                        <button id="delete" class="btn btn-sm btn-danger" " >
                                              <i class="bi bi-trash"></i>
                                        </button>
                                        
                                    </div>

                                    </div>
                                </td>
            </tr>
        `);
    });

    $('#no-of-users').text(users.length);
    $('#no-of-users2').text(users.length);
}

// ============================
// Render pagination controls (Fixed scroll issue)
// ============================
function renderPagination(users) {
    const totalPages = Math.ceil(users.length / rowsPerPage);
    const paginationEl = $('.pagination');
    paginationEl.empty();

    if (totalPages <= 1) return;

    paginationEl.append(`
        <li class="page-item ${currentPage === 1 ? 'disabled' : ''}">
            <a class="page-link" href="#" data-page="${currentPage - 1}">Previous</a>
        </li>
    `);

    for (let i = 1; i <= totalPages; i++) {
        paginationEl.append(`
            <li class="page-item ${currentPage === i ? 'active' : ''}">
                <a class="page-link" href="#" data-page="${i}">${i}</a>
            </li>
        `);
    }

    paginationEl.append(`
        <li class="page-item ${currentPage === totalPages ? 'disabled' : ''}">
            <a class="page-link" href="#" data-page="${currentPage + 1}">Next</a>
        </li>
    `);

    // Attach click events dynamically
    $('.page-link').off('click').on('click', function (e) {
        e.preventDefault(); // <-- prevents scrolling to top
        const page = parseInt($(this).data('page'));
        goToPage(page);
    });
}

// ============================
// Pagination handler
// ============================
function goToPage(page) {
    const data = filteredData.length ? filteredData : allData;
    const totalPages = Math.ceil(data.length / rowsPerPage);

    if (page < 1) page = 1;
    if (page > totalPages) page = totalPages;
  

    currentPage = page;
    renderUsersPaginated(data, currentPage);
    renderPagination(data);
}

    // Reset when Cancel button is clicked
function setupAddUserModalReset(name) {
    if (name) {
        $(name).on('click', 'button[type="reset"]', function (e) {
            e.preventDefault(); // Prevent Bootstrap from interfering
            const form = $(this).closest('form')[0]; // Get the form element
            form.reset(); // Reset all inputs
            $(form).find('.text-danger').text(''); // Clear validation messages if any
        });
    }
    else {
        $('#addUserModal').on('click', 'button[type="reset"]', function (e) {
            e.preventDefault(); // Prevent Bootstrap from interfering
            const form = $(this).closest('form')[0]; // Get the form element
            form.reset(); // Reset all inputs
            $(form).find('.text-danger').text(''); // Clear validation messages if any
        });
    }

}
function EditUser() {
    $('#all-users').on('click', 'button#edit', function () {
        const row = $(this).closest('tr');
        const index = row.index();
        const userIndex = (currentPage - 1) * rowsPerPage + index;
        const user = filteredData.length ? filteredData[userIndex] : allData[userIndex];
        console.log(user.UserName);
        //$('#UpdateUserModal').modal('show');
        window.location.href = `/Admin/Users/Profile/${user.UserName}`;
        // Split full name into first and last
        const names = user.FullName.split(' ');
        $('#UpdateId').val(user.Id);
        $('#UpdateFirstName').val(names[0] || '');
        $('#UpdateLastName').val(names.slice(1).join(' ') || '');
        $('#UpdateEmail').val(user.Email);
        $('#UpdatePhoneNumber').val(user.PhoneNumber);
        $('#UpdateRole').val(user.Role);
        $('#UpdateStatus').val(user.Status);
        setupAddUserModalReset('#UpdateUserModal')
    });
}

function DeleteUser() {
    $('#all-users').on('click', 'button#delete', function () {
        const row = $(this).closest('tr');
        const index = row.index();
        const userIndex = (currentPage - 1) * rowsPerPage + index;
        const user = filteredData.length ? filteredData[userIndex] : allData[userIndex];

        Swal.fire({
            title: `Are you sure?`,
            text: `Do you want to delete ${user.FullName}? ${user.Role === 'Coach' ? 'All related data (plans, feedbacks, etc.) will be deleted.' : 'All related data will be deleted.'}`,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            cancelButtonColor: '#3085d6',
            confirmButtonText: 'Yes, delete it!',
            cancelButtonText: 'Cancel',
            showLoaderOnConfirm: true,
            preConfirm: () => {
                return $.ajax({
                    url: `/Admin/Users/DeleteUser`,
                    method: 'POST',
                    data: { id: user.Id },
                    dataType: 'json'
                }).then(response => {
                    if (!response.success) {
                        throw new Error(response.message);
                    }
                    return response;
                }).catch(error => {
                    Swal.showValidationMessage(
                        `Request failed: ${error.message || error.responseJSON?.message || 'Unknown error'}`
                    );
                });
            },
            allowOutsideClick: () => !Swal.isLoading()
        }).then((result) => {
            if (result.isConfirmed && result.value) {
                Swal.fire({
                    icon: 'success',
                    title: 'Deleted!',
                    text: result.value.message || `${user.FullName} has been deleted successfully.`,
                    timer: 2000,
                    showConfirmButton: false
                });
                
                // Update data and refresh UI
                filteredData = filteredData.filter(u => u.Id !== user.Id);
                allData = allData.filter(u => u.Id !== user.Id);
                
                // Adjust current page if needed
                const totalPages = Math.ceil(filteredData.length / rowsPerPage);
                if (currentPage > totalPages && totalPages > 0) {
                    currentPage = totalPages;
                }
                
                renderUsersPaginated(filteredData, currentPage);
                renderPagination(filteredData);
            }
        });
    });
}


function ExportData() {
    $('#exportExcelBtn').on('click', function () {
        const dataToExport = filteredData.length ? filteredData : allData;

        if (!dataToExport || dataToExport.length === 0) {
            alert("No data to export!");
            return;
        }

        const exportData = dataToExport.map(u => ({
            "ID": u.Id,
            "Name": u.FullName,
            "Email": u.Email,
            "Role": u.Role,
            "Status": u.Status,
            "Joined Date": new Date(u.JoinedDate).toLocaleDateString('en-US')
        }));

        const ws = XLSX.utils.json_to_sheet(exportData);
        const wb = XLSX.utils.book_new();
        XLSX.utils.book_append_sheet(wb, ws, "Users");

        XLSX.writeFile(wb, `Users-${new Date().toISOString().slice(0, 10)}.xlsx`);
    });
}

