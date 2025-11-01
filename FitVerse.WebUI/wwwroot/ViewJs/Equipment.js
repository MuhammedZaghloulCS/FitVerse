$(document).ready(function () {

    // ==============================
    // 🌟 1. تحميل الإحصائيات
    // ==============================
    function loadStats() {
        $.get('/Equipment/GetTotalCountEquipment', function (res) {
            if (res.totalCount !== undefined)
                $('#totalEquipmentCount').text(res.totalCount);
        });

        $.get('/Equipment/GetTotalCountExercise', function (res) {
            if (res.totalCount !== undefined)
                $('#totalExerciseCount').text(res.totalCount);
        });
    }

    // ==============================
    // 🌟 2. تحميل المعدات (بدون pagination)
    // ==============================
    let currentSearch = "";

    function loadEquipment(searchTerm = "") {
        $.ajax({
            url: '/Equipment/GetAll',
            method: 'GET',
            data: { search: searchTerm  },
            success: function (res) {
                $('#equipmentContainer').empty();

                if (res.data && res.data.length > 0) {
                    res.data.forEach(item => {
                        let imgSrc = item.imagePath
                            ? (item.imagePath.startsWith('/Images/') ? item.imagePath : '/Images/' + item.imagePath) + '?t=' + new Date().getTime()
                            : '/Images/default.jpg';

                        //let imgSrc = item.imagePath ? item.imagePath : '/Images/default.jpg';
                        $('#equipmentContainer').append(`
                        <div class="col-lg-3 col-md-4 col-sm-6">
                            <div class="card-custom text-center p-3 shadow-sm">
                                <img src="${imgSrc}" 
                                     class="rounded mb-3" 
                                     style="height:100px;object-fit:cover;">
                                <h5 class="fw-bold mb-2">${item.name}</h5>
                                
                                <div class="d-flex gap-2">
                                    <button class="btn btn-outline-custom btn-sm flex-grow-1 editEquipmentBtn" data-id="${item.id}">
                                        <i class="bi bi-pencil"></i> Edit
                                    </button>
                                    <button class="btn btn-danger-custom btn-sm deleteEquipmentBtn" data-id="${item.id}">
                                        <i class="bi bi-trash"></i>
                                    </button>
                                </div>
                            </div>
                        </div>
                    `);
                    });
                } else {
                    $('#equipmentContainer').html('<div class="text-center text-muted py-5">No equipment found.</div>');
                }
            },
            error: function () {
                Swal.fire('Error', 'Failed to load equipment data.', 'error');
            }
        });
    }


    // ==============================
    // 🌟 3. إضافة معدة جديدة
    // ==============================
    $('#saveEquipmentBtn').click(function () {
        console.log("✅ Add button clicked");
        let formData = new FormData();
        formData.append("Name", $('#equipmentName').val());
        if ($('#equipmentImage')[0].files[0]) {
            formData.append("EquipmentImageFile", $('#equipmentImage')[0].files[0]);
        }

        $.ajax({
            url: '/Equipment/AddEquipment',
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            beforeSend: () => Swal.showLoading(),
            success: function (res) {
                Swal.close();
                if (res.success) {
                    Swal.fire('Success', res.message, 'success');
                    $('#addEquipmentModal').modal('hide');
                    $('#equipmentForm')[0].reset();
                    $('#addImagePreview').hide();
                    loadEquipment();
                    loadStats();
                } else {
                    Swal.fire('Error', res.message, 'error');
                }
            },
            error: () => Swal.fire('Error', 'Server error while adding equipment.', 'error')
        });
    });

    // ==============================
    // 🌟 4. فتح مودال التعديل
    // ==============================
    $(document).on('click', '.editEquipmentBtn', function () {
        let id = $(this).data('id');
        $.get(`/Equipment/GetById/${id}`, function (res) {
            if (res.success) {
                $('#editEquipmentName').val(res.data.name);
                let newSrc = res.data.imagePath + '?t=' + new Date().getTime();
                $('#currentEquipmentImage').attr('src', newSrc);

                $('#editSaveBtn').data('id', res.data.id);
                $('#editEquipmentModal').modal('show');
            } else {
                Swal.fire('Error', res.message, 'error');
            }
        });
    });

    // ==============================
    // 🌟 5. حفظ التعديل
    // ==============================
    $('#editSaveBtn').click(function () {
        let id = $(this).data('id');
        let formData = new FormData();
        formData.append("Id", id);
        formData.append("Name", $('#editEquipmentName').val());

        if ($('#editEquipmentImage')[0].files[0]) {
            formData.append("EquipmentImageFile", $('#editEquipmentImage')[0].files[0]);
        }

        $.ajax({
            url: '/Equipment/Update',
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            beforeSend: () => Swal.showLoading(),
            success: function (res) {
                Swal.close();
                if (res.success) {
                    Swal.fire('Updated', res.message, 'success');
                    $('#editEquipmentModal').modal('hide');

                    // ✅ إعادة تحميل الكاردات مع منع الكاش
                    loadEquipment();

                    // ✅ تحديث الصورة الحالية في المودال لو محتاج
                    let imgSrc = res.data.imagePath
                        ? (res.data.imagePath.startsWith('/Images/') ? res.data.imagePath : '/Images/' + res.data.imagePath) + '?t=' + new Date().getTime()
                        : '/Images/default.jpg';
                    $('#currentEquipmentImage').attr('src', imgSrc);

                } else {
                    Swal.fire('Error', res.message, 'error');
                }
            },
            error: () => Swal.fire('Error', 'Server error while updating.', 'error')
        });
    });


    // ==============================
    // 🌟 6. حذف معدة
    // ==============================
    $(document).on('click', '.deleteEquipmentBtn', function () {
        let id = $(this).data('id');

        Swal.fire({
            title: 'Are you sure?',
            text: "You are about to delete this equipment!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Yes, delete it!'
        }).then(result => {
            if (result.isConfirmed) {
                $.ajax({
                    url: `/Equipment/Delete/${id}`,
                    type: 'DELETE',
                    success: function (res) {
                        if (res.success) {
                            Swal.fire('Deleted!', res.message, 'success');
                            loadEquipment();
                            loadStats();
                        } else {
                            Swal.fire('Error', res.message, 'error');
                        }
                    },
                    error: () => Swal.fire('Error', 'Server error while deleting.', 'error')
                });
            }
        });
    });

    // ==============================
    // 🌟 7. البحث
    // ==============================
    $('.btn-outline-custom').click(function () {
        currentSearch = $('.form-control-custom').val();
        loadEquipment();
    });

    $('.form-control-custom').on('keyup', function (e) {
        if (e.key === 'Enter') {
            currentSearch = $(this).val();
            loadEquipment();
        }
    });
    $(document).ready(function () {
        // ✅ زرار فتح مودال الإضافة
        $('#openAddModal').click(function () {
            console.log("✅ فتح مودال الإضافة");
            $('#addEquipmentModal').modal('show');
        });
    });
    $('#searchEquipment').on('input', function () {
        let term = $(this).val().trim();
        loadEquipment(term);
    });

    // ==============================
    // 🌟 8. عند فتح الصفحة
    // ==============================
    loadEquipment();
    loadStats();

});
