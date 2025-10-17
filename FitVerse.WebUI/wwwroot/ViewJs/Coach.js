$(function () {
    $("#coachForm").on("submit", function (e) {
        e.preventDefault(); // Prevent default form submission (page reload)

        let formData = new FormData(this);

        $.ajax({
            url: '/Coach/AddCoach', 
            method: 'POST',
            data: formData, 
            processData: false, // Prevent jQuery from processing data automatically
            contentType: false, // Prevent jQuery from setting the content type automatically
            success: function (res) {
                if (res.success) {
                    swal("✅ Success", res.message, "success");
                    $("#coachForm")[0].reset(); // Reset the form after submission
                } else {
                    swal("❌ Error", res.message, "error"); 
                }
            },
            error: function () {
                swal("⚠️ Warning", "An error occurred while sending the request.", "error");
            }
        });
    });
});
