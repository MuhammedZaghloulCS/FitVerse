$(document).ready(function () {

    loadPlans();

    // ✅ Load all exercises as checkboxes
    $.ajax({
        url: '/ExercisePlan/GetAllExercises',
        type: 'GET',
        success: function (data) {
            let html = '';
            data.forEach(ex => {
                html += `
                    <div class="col-md-4 mb-2">
                        <div class="form-check">
                            <input class="form-check-input exercise-checkbox" type="checkbox" value="${ex.Id}" id="ex_${ex.Id}">
                            <label class="form-check-label" for="ex_${ex.Id}">
                                ${ex.Name}
                            </label>
                        </div>
                    </div>
                `;
            });
            $('#exerciseCheckboxes').html(html);
        },
        error: function () {
            swal("Error", "Failed to load exercises. Please try again.", "error");
        }
    });

    // ✅ When the "Create Plan" button is clicked
    $('#savePlanBtn').click(function () {
        const selectedExercises = [];
        $('.exercise-checkbox:checked').each(function () {
            selectedExercises.push(parseInt($(this).val()));
        });

        const planData = {
            Name: $('input[name="Name"]').val(),
            Notes: $('textarea[name="Notes"]').val(),
            DurationWeeks: parseInt($('input[name="DurationWeeks"]').val()),
            SelectedExerciseIds: selectedExercises
        };

        console.log("Data sent:", planData);

        // ✅ Validation before sending
        if (!planData.Name || !planData.Notes) {
            swal("Missing Fields", "Please fill out all required fields.", "warning");
            return;
        }

        $.ajax({
            url: '/ExercisePlan/CreatePlan',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(planData),
            success: function (res) {
                swal({
                    text: res.message || "Plan created successfully!",
                    icon: "success",
                }).then(() => {
                    $('#createPlanModal').modal('hide');
                    loadPlans(); // 🔁 reload plans dynamically
                });
            },
            error: function (xhr) {
                let errMsg = "Error while saving plan.";
                try {
                    let errData = JSON.parse(xhr.responseText);
                    if (errData?.message) errMsg = errData.message;
                } catch { }
                swal("Save Failed", errMsg, "error");
            }
        });
    });
});

// ✅ Function to load all plans dynamically
function loadPlans() {
    $.ajax({
        url: '/ExercisePlan/GetAllPlans',
        type: 'GET',
        success: function (plans) {
            let html = '';
            if (!plans || plans.length === 0) {
                html = `<p class="text-muted text-center">No plans found yet.</p>`;
            } else {
                plans.forEach(p => {
                    html += `
                        <div class="col-lg-4 col-md-6">
                            <div class="workout-card">
                                <div class="workout-card-body">
                                    <h5 class="workout-card-title">${p.name}</h5>
                                    <p class="text-muted small">${p.notes}</p>
                                    <div>Duration: ${p.durationWeeks} Weeks</div>
                                    <div>Clients: ${p.clientCount}</div>
                                </div>
                            </div>
                        </div>
                    `;
                });
            }
            $('#plansContainer').html(html);
        },
        error: function () {
            $('#plansContainer').html(`<p class="text-danger text-center">Failed to load plans.</p>`);
        }
    });
}
