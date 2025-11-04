// Global variables for filtering and view management
let allClients = [];
let filteredClients = [];
let currentView = 'grid'; // 'grid' or 'list'
let currentFilter = 'all';

$(document).ready(function () {
    initializeClientPage();
    loadClients();
    setupEventHandlers();
});

function initializeClientPage() {
    // Show loading state initially
    $('#loadingState').show();
    $('#emptyState').hide();
    $('.clients-container').hide();
}

function setupEventHandlers() {
    // Refresh button
    $('#refreshClientsBtn').click(function() {
        loadClients();
    });

    // Search functionality
    $('#clientSearchInput').on('input', function() {
        const searchTerm = $(this).val().toLowerCase();
        filterClients(searchTerm);
    });

    // Subscription filter
    $('#subscriptionFilter').change(function() {
        const searchTerm = $('#clientSearchInput').val().toLowerCase();
        filterClients(searchTerm);
    });

    // Filter dropdown
    $('.dropdown-item[data-filter]').click(function(e) {
        e.preventDefault();
        currentFilter = $(this).data('filter');
        const searchTerm = $('#clientSearchInput').val().toLowerCase();
        filterClients(searchTerm);
    });

    // Clear filters
    $('#clearFiltersBtn, #clearFiltersFromEmpty').click(function() {
        $('#clientSearchInput').val('');
        $('#subscriptionFilter').val('');
        currentFilter = 'all';
        filterClients('');
    });

    // View toggle
    $('#gridViewBtn').click(function() {
        currentView = 'grid';
        $(this).addClass('btn-primary').removeClass('btn-outline-primary');
        $('#listViewBtn').addClass('btn-outline-primary').removeClass('btn-primary');
        renderClients(filteredClients);
    });

    $('#listViewBtn').click(function() {
        currentView = 'list';
        $(this).addClass('btn-primary').removeClass('btn-outline-primary');
        $('#gridViewBtn').addClass('btn-outline-primary').removeClass('btn-primary');
        renderClients(filteredClients);
    });
}

function loadClients() {
    // Show loading state
    $('#loadingState').show();
    $('#emptyState').hide();
    $('.clients-container').hide();

    $.ajax({
        url: '/Coach/GetMyClients',
        method: 'GET',
        success: function (response) {
            if (response.success) {
                allClients = response.clients;
                filteredClients = [...allClients];
                
                // Update stats
                updateClientStats();
                
                // Render clients
                renderClients(filteredClients);
                
                // Hide loading and show content
                $('#loadingState').hide();
                $('.clients-container').show();
            } else {
                showEmptyState();
            }
        },
        error: function () {
            console.error("Error loading clients");
            showEmptyState();
        }
    });
}

function updateClientStats() {
    const totalClients = allClients.length;
    const activeClients = allClients.filter(c => c.IsActive).length;
    const totalWorkouts = allClients.reduce((sum, c) => sum + (c.TotalWorkouts || 0), 0);
    const avgProgress = totalClients > 0 ? 
        Math.round(allClients.reduce((sum, c) => sum + (c.ProgressPercentage || 0), 0) / totalClients) : 0;

    $('#totalClientsCount').text(totalClients);
    $('#activeClientsCount').text(activeClients);
    $('#totalWorkoutsCount').text(totalWorkouts);
    $('#avgProgressCount').text(avgProgress + '%');
}

function filterClients(searchTerm = '') {
    const subscriptionFilter = $('#subscriptionFilter').val();
    
    filteredClients = allClients.filter(client => {
        // Search filter
        const matchesSearch = searchTerm === '' || 
            client.Name.toLowerCase().includes(searchTerm) ||
            (client.Email && client.Email.toLowerCase().includes(searchTerm)) ||
            client.SubscriptionName.toLowerCase().includes(searchTerm);
        
        // Subscription filter
        const matchesSubscription = subscriptionFilter === '' || 
            client.SubscriptionName === subscriptionFilter;
        
        // Status filter
        const matchesStatus = currentFilter === 'all' || 
            (currentFilter === 'active' && client.IsActive) ||
            (currentFilter === 'inactive' && !client.IsActive);
        
        return matchesSearch && matchesSubscription && matchesStatus;
    });
    
    renderClients(filteredClients);
}

function renderClients(clients) {
    const container = $('#clientsContainer');
    container.empty();
    
    if (clients.length === 0) {
        showEmptyState();
        return;
    }
    
    $('#emptyState').hide();
    $('.clients-container').show();
    
    clients.forEach(c => {
        let card;
        
        if (currentView === 'grid') {
            card = createGridCard(c);
        } else {
            card = createListCard(c);
        }
        
        container.append(card);
    });
}

function createGridCard(c) {
    return `
        <div class="col-lg-4 col-md-6">
            <div class="client-card modern-client-card">
                <div class="client-card-header">
                    <div class="d-flex align-items-center gap-3">
                        <div class="client-avatar">
                            <img src="${c.ImagePath || 'https://ui-avatars.com/api/?name=' + encodeURIComponent(c.Name) + '&background=6366f1&color=fff'}" alt="${c.Name}" class="avatar-img">
                        </div>
                        <div class="flex-grow-1">
                            <h5 class="client-name mb-1">${c.Name}</h5>
                            <p class="client-subscription mb-0">${c.SubscriptionName}</p>
                        </div>
                        <span class="status-badge ${c.IsActive ? 'status-active' : 'status-inactive'}">
                            <i class="bi bi-${c.IsActive ? 'check-circle' : 'pause-circle'} me-1"></i>
                            ${c.IsActive ? 'Active' : 'Inactive'}
                        </span>
                    </div>
                </div>
                
                <div class="client-card-body">
                    <div class="row g-2 mb-3">
                        <div class="col-6">
                            <div class="metric-card">
                                <div class="metric-value">${c.TotalWorkouts || 0}</div>
                                <div class="metric-label">Workouts</div>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="metric-card">
                                <div class="metric-value progress-value">${c.ProgressPercentage || 0}%</div>
                                <div class="metric-label">Progress</div>
                            </div>
                        </div>
                    </div>
                    
                    <div class="progress-section mb-3">
                        <div class="d-flex justify-content-between small mb-2">
                            <span class="progress-label">Plan Progress</span>
                            <span class="progress-percentage">${c.ProgressPercentage || 0}%</span>
                        </div>
                        <div class="progress modern-progress">
                            <div class="progress-bar" style="width: ${c.ProgressPercentage || 0}%"></div>
                        </div>
                    </div>
                </div>
                
                <div class="client-card-footer">
                    <div class="d-flex gap-2">
                        <button class="btn btn-primary modern-btn flex-grow-1" onclick="viewClientProfile('${c.Id}')">
                            <i class="bi bi-eye me-2"></i>View Profile
                        </button>
                        <button class="btn btn-outline-primary modern-btn" onclick="openClientChat('${c.Id}')" title="Chat">
                            <i class="bi bi-chat-dots"></i>
                        </button>
                        <div class="dropdown">
                            <button class="btn btn-outline-secondary modern-btn dropdown-toggle" type="button" data-bs-toggle="dropdown">
                                <i class="bi bi-three-dots"></i>
                            </button>
                            <ul class="dropdown-menu">
                                <li><a class="dropdown-item" href="#" onclick="editClient('${c.Id}')">
                                    <i class="bi bi-pencil me-2"></i>Edit Client
                                </a></li>
                                <li><a class="dropdown-item" href="#" onclick="assignPlan('${c.Id}')">
                                    <i class="bi bi-clipboard-plus me-2"></i>Assign Plan
                                </a></li>
                                <li><hr class="dropdown-divider"></li>
                                <li><a class="dropdown-item text-danger" href="#" onclick="deactivateClient('${c.Id}')">
                                    <i class="bi bi-person-x me-2"></i>Deactivate
                                </a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>`;
}

function createListCard(c) {
    return `
        <div class="col-12">
            <div class="client-list-item modern-list-item">
                <div class="row align-items-center g-3">
                    <div class="col-lg-4">
                        <div class="d-flex align-items-center gap-3">
                            <div class="client-avatar">
                                <img src="${c.ImagePath || 'https://ui-avatars.com/api/?name=' + encodeURIComponent(c.Name) + '&background=6366f1&color=fff'}" alt="${c.Name}" class="avatar-img">
                            </div>
                            <div>
                                <h6 class="client-name mb-1">${c.Name}</h6>
                                <p class="client-subscription mb-0">${c.SubscriptionName}</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2">
                        <span class="status-badge ${c.IsActive ? 'status-active' : 'status-inactive'}">
                            <i class="bi bi-${c.IsActive ? 'check-circle' : 'pause-circle'} me-1"></i>
                            ${c.IsActive ? 'Active' : 'Inactive'}
                        </span>
                    </div>
                    <div class="col-lg-2">
                        <div class="text-center">
                            <div class="fw-bold">${c.TotalWorkouts || 0}</div>
                            <small class="text-muted">Workouts</small>
                        </div>
                    </div>
                    <div class="col-lg-2">
                        <div class="text-center">
                            <div class="fw-bold progress-value">${c.ProgressPercentage || 0}%</div>
                            <small class="text-muted">Progress</small>
                        </div>
                    </div>
                    <div class="col-lg-2">
                        <div class="d-flex gap-1 justify-content-end">
                            <button class="btn btn-sm btn-primary modern-btn" onclick="viewClientProfile('${c.Id}')" title="View Profile">
                                <i class="bi bi-eye"></i>
                            </button>
                            <button class="btn btn-sm btn-outline-primary modern-btn" onclick="openClientChat('${c.Id}')" title="Chat">
                                <i class="bi bi-chat-dots"></i>
                            </button>
                            <div class="dropdown">
                                <button class="btn btn-sm btn-outline-secondary modern-btn dropdown-toggle" type="button" data-bs-toggle="dropdown">
                                    <i class="bi bi-three-dots"></i>
                                </button>
                                <ul class="dropdown-menu">
                                    <li><a class="dropdown-item" href="#" onclick="editClient('${c.Id}')">
                                        <i class="bi bi-pencil me-2"></i>Edit
                                    </a></li>
                                    <li><a class="dropdown-item" href="#" onclick="assignPlan('${c.Id}')">
                                        <i class="bi bi-clipboard-plus me-2"></i>Assign Plan
                                    </a></li>
                                    <li><hr class="dropdown-divider"></li>
                                    <li><a class="dropdown-item text-danger" href="#" onclick="deactivateClient('${c.Id}')">
                                        <i class="bi bi-person-x me-2"></i>Deactivate
                                    </a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>`;
}

function showEmptyState() {
    $('#loadingState').hide();
    $('.clients-container').hide();
    $('#emptyState').show();
}

// Action functions (preserve existing functionality)
function viewClientProfile(clientId) {
    console.log('View client profile:', clientId);
    // Original functionality - can be implemented based on existing routes
    window.location.href = `/Coach/ClientProfile/${clientId}`;
}

function openClientChat(clientId) {
    console.log('Open chat with client:', clientId);
    // Original functionality - can be implemented based on existing chat system
    window.location.href = `/Chat/Client/${clientId}`;
}

function editClient(clientId) {
    console.log('Edit client:', clientId);
    // Placeholder for edit functionality
}

function assignPlan(clientId) {
    console.log('Assign plan to client:', clientId);
    // Placeholder for assign plan functionality
}

function deactivateClient(clientId) {
    console.log('Deactivate client:', clientId);
    // Placeholder for deactivate functionality
}