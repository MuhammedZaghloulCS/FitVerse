$(document).ready(function () {
    loadClients();
});

function loadClients() {
    $.ajax({
        url: '/Coach/GetMyClients',
        method: 'GET',
        success: function (response) {
            if (response.success) {
                let clients = response.clients;
                let container = $('#clientsContainer');
                container.empty();

                clients.forEach(c => {
                    let card = `
                        <div class="col-lg-4 col-md-6">
                            <div class="card-custom">
                                <div class="card-body-custom">
                                    <div class="d-flex align-items-center gap-3 mb-3">
                                        <img src="${c.Image}" alt="Client" style="width: 60px; height: 60px; border-radius: 50%;">
                                        <div class="flex-grow-1">
                                            <h5 class="fw-bold mb-1">${c.Name}</h5>
                                            <p class="text-muted small mb-0">${c.SubscriptionName}</p>
                                        </div>
                                        <span class="badge-custom ${c.IsActive ? 'badge-success' : 'badge-warning'}">
                                            ${c.IsActive ? 'Active' : 'Inactive'}
                                        </span>
                                    </div>
                                    <div class="row g-2 mb-3">
                                        <div class="col-6">
                                            <div class="p-2 text-center" style="background: #f8fafc; border-radius: 8px;">
                                                <div class="fw-bold">${c.TotalWorkouts}</div>
                                                <small class="text-muted">Workouts</small>
                                            </div>
                                        </div>
                                        <div class="col-6">
                                            <div class="p-2 text-center" style="background: #f8fafc; border-radius: 8px;">
                                                <div class="fw-bold" style="color: #10b981;">${c.ProgressPercentage}%</div>
                                                <small class="text-muted">Progress</small>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="mb-3">
                                        <div class="d-flex justify-content-between small mb-1">
                                            <span>Plan Progress</span>
                                            <span class="fw-semibold">${c.ProgressPercentage}%</span>
                                        </div>
                                        <div class="progress" style="height: 6px;">
                                            <div class="progress-bar" style="width: ${c.ProgressPercentage}%; background: #10b981;"></div>
                                        </div>
                                    </div>
                                    <div class="d-flex gap-2">
                                        <button class="btn btn-primary-custom btn-sm flex-grow-1"><i class="bi bi-eye me-1"></i> View Profile</button>
                                        <button class="btn btn-outline-custom btn-sm"><i class="bi bi-chat-dots"></i></button>
                                    </div>
                                </div>
                            </div>
                        </div>`;
                    container.append(card);
                });
            }
        },
        error: function () {
            console.error("Error loading clients");
        }
    });
}