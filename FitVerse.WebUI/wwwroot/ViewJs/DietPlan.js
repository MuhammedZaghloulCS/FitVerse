$(document).ready(function () {

    // 🟢 تحميل جميع خطط الدايت عند تحميل الصفحة
    loadDietPlans();
   

    //// 🟢 دالة تحميل كل الخطط
    //function loadDietPlans() {
    //    $.ajax({
    //        url: '/DietPlan/GetAll',
    //        method: 'GET',
    //        success: function (response) {
    //            $('#dietPlansContainer').empty();

    //            response.data.forEach(function (plan) {
    //                $('#dietPlansContainer').append(`
    //                    <div class="col-lg-4 col-md-6 mb-4">
    //                        <div class="card-custom h-100 shadow-sm">
    //                            <div class="card-body-custom">
    //                                <div class="d-flex justify-content-between align-items-start mb-3">
    //                                    <div>
    //                                        <h5 class="fw-bold mb-1">${plan.name}</h5>
    //                                        <small class="text-muted">${plan.totalCal} calories/day</small>
    //                                    </div>
    //                                    <div class="dropdown">
    //                                        <button class="btn btn-sm btn-outline-custom" data-bs-toggle="dropdown">
    //                                            <i class="bi bi-three-dots-vertical"></i>
    //                                        </button>
    //                                        <ul class="dropdown-menu">
    //                                            <li><a class="dropdown-item view-plan" data-id="${plan.id}" href="#"><i class="bi bi-eye me-2"></i>View Details</a></li>
    //                                            <li><a class="dropdown-item edit-plan" data-id="${plan.id}" href="#"><i class="bi bi-pencil me-2"></i>Edit</a></li>
    //                                            <li><a class="dropdown-item delete-plan text-danger" data-id="${plan.id}" href="#"><i class="bi bi-trash me-2"></i>Delete</a></li>
    //                                        </ul>
    //                                    </div>
    //                                </div>

    //                                <div class="mb-3">
    //                                    <div class="d-flex justify-content-between mb-2">
    //                                        <span class="text-muted small">Protein</span>
    //                                        <strong class="small">${plan.proteinInGrams}g (${plan.proteinPercentage}%)</strong>
    //                                    </div>
    //                                    <div class="progress mb-2" style="height: 6px;">
    //                                        <div class="progress-bar bg-danger" style="width: 30%;"></div>
    //                                    </div>

    //                                    <div class="d-flex justify-content-between mb-2">
    //                                        <span class="text-muted small">Carbs</span>
    //                                        <strong class="small">${plan.carbInGrams}g (${plan.carbPercentage}%)</strong>
    //                                    </div>
    //                                    <div class="progress mb-2" style="height: 6px;">
    //                                        <div class="progress-bar bg-warning" style="width: 40%;"></div>
    //                                    </div>

    //                                    <div class="d-flex justify-content-between mb-2">
    //                                        <span class="text-muted small">Fats</span>
    //                                        <strong class="small">${plan.fatsInGrams}g (${plan.fatPercentage}%)</strong>
    //                                    </div>
    //                                    <div class="progress" style="height: 6px;">
    //                                        <div class="progress-bar bg-success" style="width: 20%;"></div>
    //                                    </div>
    //                                </div>

    //                                <button class="btn btn-primary-custom btn-sm w-100 view-plan" data-id="${plan.id}">
    //                                    <i class="bi bi-eye me-1"></i> View Full Plan
    //                                </button>
    //                            </div>
    //                        </div>
    //                    </div>
    //                `);
    //            });
    //        },
    //        error: function (xhr) {
    //            console.error("Error loading diet plans:", xhr);
    //            swal("Error", "Failed to load diet plans.", "error");
    //        }
    //    });
    //}
    $('#searchDietPlan').on('keyup', function () {
        let searchText = $(this).val();
        loadDietPlans(searchText);
    });

    // 🟢 دالة تحميل كل الخطط مع إمكانية البحث
    function loadDietPlans(search = "") {
        $.ajax({
            url: '/DietPlan/GetAll',
            method: 'GET',
            data: { search: search }, // 🟢 نمرر كلمة البحث هنا
            success: function (response) {
                $('#dietPlansContainer').empty();

                if (response.data.length === 0) {
                    $('#dietPlansContainer').append(`
                        <div class="text-center text-muted py-4">
                            <i class="bi bi-search"></i> No diet plans found.
                        </div>
                    `);
                    return;
                }


                            response.data.forEach(function (plan) {
                                $('#dietPlansContainer').append(`
                                    <div class="col-lg-4 col-md-6 mb-4">
                                        <div class="card-custom h-100 shadow-sm">
                                            <div class="card-body-custom">
                                                <div class="d-flex justify-content-between align-items-start mb-3">
                                                    <div>
                                                        <h5 class="fw-bold mb-1">${plan.Name}</h5>
                                                        <small class="text-muted">${plan.TotalCal} calories/day</small>
                                                    </div>
                                                    <div class="dropdown">
                                                        <button class="btn btn-sm btn-outline-custom" data-bs-toggle="dropdown">
                                                            <i class="bi bi-three-dots-vertical"></i>
                                                        </button>
                                                        <ul class="dropdown-menu">
                                                            <li><a class="dropdown-item view-plan" data-id="${plan.Id}" href="#"><i class="bi bi-eye me-2"></i>View Details</a></li>
                                                            <li><a class="dropdown-item edit-plan" data-id="${plan.Id}" href="#"><i class="bi bi-pencil me-2"></i>Edit</a></li>
                                                            <li><a class="dropdown-item delete-plan text-danger" data-id="${plan.Id}" href="#"><i class="bi bi-trash me-2"></i>Delete</a></li>
                                                        </ul>
                                                    </div>
                                                </div>

                                                <div class="mb-3">
                                                    <div class="d-flex justify-content-between mb-2">
                                                        <span class="text-muted small">Protein</span>
                                                        <strong class="small">${plan.ProteinInGrams}g (${plan.ProteinPercentage}%)</strong>
                                                    </div>
                                                    <div class="progress mb-2" style="height: 6px;">
                                                        <div class="progress-bar bg-danger" style="width: 30%;"></div>
                                                    </div>

                                                    <div class="d-flex justify-content-between mb-2">
                                                        <span class="text-muted small">Carbs</span>
                                                        <strong class="small">${plan.CarbInGrams}g (${plan.CarbPercentage}%)</strong>
                                                    </div>
                                                    <div class="progress mb-2" style="height: 6px;">
                                                        <div class="progress-bar bg-warning" style="width: 40%;"></div>
                                                    </div>

                                                    <div class="d-flex justify-content-between mb-2">
                                                        <span class="text-muted small">Fats</span>
                                                        <strong class="small">${plan.FatsInGrams}g (${plan.FatPercentage}%)</strong>
                                                    </div>
                                                    <div class="progress" style="height: 6px;">
                                                        <div class="progress-bar bg-success" style="width: 20%;"></div>
                                                    </div>
                                                </div>

                                                <button class="btn btn-primary-custom btn-sm w-100 view-plan" data-id="${plan.Id}">
                                                    <i class="bi bi-eye me-1"></i> View Full Plan
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                `);
                });
            },
            error: function (xhr) {
                console.error("Error loading diet plans:", xhr);
                swal("Error", "Failed to load diet plans.", "error");
            }
        });
    }

    // 🟣 حذف خطة
    $(document).on('click', '.delete-plan', function (e) {
        e.preventDefault();
        let id = $(this).data('id');

        swal({
            title: "Are you sure?",
            text: "Once deleted, you will not be able to recover this diet plan!",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        }).then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    url: `/DietPlan/Delete/${id}`,
                    method: 'POST',
                    success: function (res) {
                        swal("Deleted!", res.message, "success");
                        loadDietPlans();
                    },
                    error: function () {
                        swal("Error", "Error deleting diet plan.", "error");
                    }
                });
            } else {
                swal("Your diet plan is safe!");
            }
        });
    });

    // 🟢 عرض تفاصيل خطة
    $(document).on('click', '.view-plan', function (e) {
        e.preventDefault();
        let id = $(this).data('id');

        $.ajax({
            url: `/DietPlan/GetById/${id}`,
            method: 'GET',
            success: function (plan) {
                $('#viewDietPlanModal .modal-title').text(plan.Goal + " Plan");
                $('#viewDietPlanModal small').text(`${plan.TotalCal} calories/day • Protein: ${plan.ProteinInGrams}g • Carbs: ${plan.CarbInGrams}g • Fats: ${plan.FatsInGrams}g`);
                $('#viewDietPlanModal').modal('show');
            },
            error: function () {
                swal("Error", "Failed to load diet plan details.", "error");
            }
        });
    });

    // 🟢 إضافة دايت بلان جديد
    $('#saveDietPlanBtn').on('click', function () {

        const dietPlan = {
            TotalCal: parseFloat($('#totalCal').val()),
            ProteinInGrams: parseFloat($('#protein').val()),
            CarbInGrams: parseFloat($('#carbs').val()),
            FatsInGrams: parseFloat($('#fats').val()),
            Goal: $('#goal').val(),
            Name: $('#planName').val(),
            Age: parseInt($('#age').val()),
            Gender: $('#gender').val(),
            ClientId: $('#clientId').val(), // 🟢 مهم جدًا
            ActivityMultiplier: parseFloat($('#activityMultiplier').val() || 1.2),
            Weight: parseFloat($('#weight').val()),
            Height: parseFloat($('#height').val())

        };

        if (!dietPlan.Name || !dietPlan.Goal || !dietPlan.TotalCal) {
            swal("Warning", "Please fill in all required fields!", "warning");
            return;
        }

        $.ajax({
            url: '/DietPlan/Add',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(dietPlan),
            success: function (res) {
                if (res.success) {
                    swal("Success", res.message, "success");
                    $('#createDietPlanModal').modal('hide');
                    $('#dietPlanForm')[0].reset();
                    loadDietPlans();
                } else {
                    swal("Error", "Failed to add diet plan!", "error");
                }
            },
            error: function (xhr) {
                console.error(xhr);
                swal("Error", "An error occurred while adding the diet plan.", "error");
            }
        });
    });

    // 🟢 فتح مودال التعديل
    $(document).on('click', '.edit-plan', function (e) {
        e.preventDefault();
        e.stopPropagation(); // مهم عشان dropdown

        let id = $(this).data('id');

        $.ajax({
            url: `/DietPlan/GetById/${id}`,
            method: 'GET',
            success: function (plan) {
                $('#editPlanId').val(plan.Id);
                $('#editPlanName').val(plan.Name);
                $('#editGoal').val(plan.Goal);
                $('#editTotalCal').val(plan.TotalCal);
                $('#editProtein').val(plan.ProteinInGrams);
                $('#editCarbs').val(plan.CarbInGrams);
                $('#editFats').val(plan.FatsInGrams);
                $('#editage').val(plan.Age);
                $('#editgender').val(plan.Gender);
                $('#editweight').val(plan.Weight);
                $('#editheight').val(plan.Height);



                $('#editDietPlanModal').modal('show');
            },
            error: function () {
                swal("Error", "Failed to load diet plan for editing.", "error");
            }
        });
    });

    // 🟢 تحديث خطة الدايت
    $('#updateDietPlanBtn').on('click', function () {
        const dietPlan = {
            Id: parseInt($('#editPlanId').val()),
            Name: $('#editPlanName').val(),
            Goal: $('#editGoal').val(),
            TotalCal: parseFloat($('#editTotalCal').val()),
            ProteinInGrams: parseFloat($('#editProtein').val()),
            CarbInGrams: parseFloat($('#editCarbs').val()),
            FatsInGrams: parseFloat($('#editFats').val()),
            Age: parseInt($('#editage').val()),
            Gender: $('#editgender').val(),
            Weight: parseFloat($('#editweight').val()),
            Height: parseFloat($('#editheight').val())


        };

        if (!dietPlan.Name || !dietPlan.Goal || !dietPlan.TotalCal) {
            swal("Warning", "Please fill in all required fields!", "warning");
            return;
        }

        $.ajax({
            url: '/DietPlan/Update',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(dietPlan),
            success: function (res) {
                if (res.success) {
                    swal("Success", res.message, "success");
                    $('#editDietPlanModal').modal('hide');
                    loadDietPlans();
                } else {
                    swal("Error", "Failed to update diet plan!", "error");
                }
            },
            error: function () {
                swal("Error", "An error occurred while updating the diet plan.", "error");
            }
        });
    });

});


