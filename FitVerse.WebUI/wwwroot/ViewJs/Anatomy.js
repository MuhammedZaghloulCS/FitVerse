

$(document).ready(function () {

 

    function loadAnatomy(searchTerm = "") {
        $.ajax({
            url: '/Anatomy/GetAll',
            method: 'GET',
            data: { search: searchTerm },
            success: function (res) {
                $('#anatomyContainer').empty();

                if (res.data && res.data.length > 0) {
                    res.data.forEach(item => {
                        let imgSrc = item.imagePath
                            ? (item.imagePath.startsWith('/Images/') ? item.imagePath : '/Images/' + item.imagePath) + '?t=' + new Date().getTime()
                            : '/Images/default.jpg';

                        //let imgSrc = item.imagePath ? item.imagePath : '/Images/default.jpg';
                        $('#anatomyContainer').append(`
                        <div class="col-lg-3 col-md-4 col-sm-6">
                            <div class="card-custom text-center p-3 shadow-sm">
                                <img src="${imgSrc}" 
                                     class="rounded mb-3" 
                                     style="height:100px;object-fit:cover;">
                                <h5 class="fw-bold mb-2">${item.name}</h5>
                                
                                <div class="d-flex gap-2">
                                    <button class="btn btn-outline-custom btn-sm flex-grow-1 editAnatomyBtn" data-id="${item.id}">
                                        <i class="bi bi-pencil"></i> Edit
                                    </button>
                                    <button class="btn btn-danger-custom btn-sm deleteAnatomyBtn" data-id="${item.id}">
                                        <i class="bi bi-trash"></i>
                                    </button>
                                </div>
                            </div>
                        </div>
                    `);
                    });
                } else {
                    $('#anatomyContainer').html('<div class="text-center text-muted py-5">No anatomy found.</div>');
                }
            },
            error: function () {
                Swal.fire('Error', 'Failed to load anatomy data.', 'error');
            }
        });
    }


    // ==============================
    // 🌟 3. إضافة معدة جديدة
    // ==============================
    $('#saveAnatomyBtn').click(function () {
        console.log("✅ Add button clicked");
        let formData = new FormData();
        formData.append("Name", $('#Name').val());
        if ($('#Image')[0].files[0]) {
            formData.append("ImageFile", $('#Image')[0].files[0]);
        }

        $.ajax({
            url: '/Anatomy/AddAnatomy',
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            beforeSend: () => Swal.showLoading(),
            success: function (res) {
                Swal.close();
                if (res.success) {
                    Swal.fire('Success', res.message, 'success');
                    $('#addAnatomyModal').modal('hide');
                    $('#anatomyForm')[0].reset();
                    $('#addImagePreview').hide();
                    loadEquipment();
                    loadStats();
                } else {
                    Swal.fire('Error', res.message, 'error');
                }
            },
            error: () => Swal.fire('Error', 'Server error while adding anatomy.', 'error')
        });
    });

    // ==============================
    // 🌟 4. فتح مودال التعديل
    // ==============================
    $(document).on('click', '.editAnatomyBtn', function () {
        let id = $(this).data('id');
        $.get(`/Anatomy/GetById/${id}`, function (res) {
            if (res.success) {
                $('#editAnatomyName').val(res.data.name);
                let newSrc = res.data.imagePath + '?t=' + new Date().getTime();
                $('#currentAnatomyImage').attr('src', newSrc);

                $('#editSaveBtn').data('id', res.data.id);
                $('#editAnatomyModal').modal('show');
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
        formData.append("Name", $('#editAnatomyName').val());

        if ($('#editAnatomyImage')[0].files[0]) {
            formData.append("ImageFile", $('#editAnatomyImage')[0].files[0]);
        }

        $.ajax({
            url: '/Anatomy/Update',
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            beforeSend: () => Swal.showLoading(),
            success: function (res) {
                Swal.close();
                if (res.success) {
                    Swal.fire('Updated', res.message, 'success');
                    $('#editAnatomyModal').modal('hide');

                    // ✅ إعادة تحميل الكاردات مع منع الكاش
                    loadAnatomy();

                    // ✅ تحديث الصورة الحالية في المودال لو محتاج
                    let imgSrc = res.data.imagePath
                        ? (res.data.imagePath.startsWith('/Images/') ? res.data.imagePath : '/Images/' + res.data.imagePath) + '?t=' + new Date().getTime()
                        : '/Images/default.jpg';
                    $('#currentAnatomyImage').attr('src', imgSrc);

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
    $(document).on('click', '.deleteAnatomyBtn', function () {
        let id = $(this).data('id');

        Swal.fire({
            title: 'Are you sure?',
            text: "You are about to delete this anatomy!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Yes, delete it!'
        }).then(result => {
            if (result.isConfirmed) {
                $.ajax({
                    url: `/Anatomy/Delete/${id}`,
                    type: 'DELETE',
                    success: function (res) {
                        if (res.success) {
                            Swal.fire('Deleted!', res.message, 'success');
                            loadAnatomy();
                          
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
        loadAnatomy();
    });

    $('.form-control-custom').on('keyup', function (e) {
        if (e.key === 'Enter') {
            currentSearch = $(this).val();
            loadAnatomy();
        }
    });
    $(document).ready(function () {
        // ✅ زرار فتح مودال الإضافة
        $('#openAddModal').click(function () {
            
            $('#addAnatomyModal').modal('show');
        });
    });
    $('#searchAnatomy').on('input', function () {
        let term = $(this).val().trim();
        loadAnatomy(term);
    });
    loadAnatomy();


});








