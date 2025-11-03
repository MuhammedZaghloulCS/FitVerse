// ==========================
// DOM Elements
// ==========================
const themeSwitch = document.getElementById('theme-switch');
const sidebar = document.querySelector('.sidebar');
const menuToggle = document.querySelector('.menu-toggle');
const notificationBtn = document.querySelector('.notification-btn');
const notificationDropdown = document.querySelector('.notification-dropdown');
const searchInput = document.querySelector('.search-bar input');
const timeFilterBtns = document.querySelectorAll('.time-filter button');
const classItems = document.querySelectorAll('.class-item');
const anatomyForm = document.getElementById('anatomyForm');
const searchAnatomyInput = document.getElementById('searchAnatomy');
const deleteModal = document.getElementById('deleteModal');
const closeModalBtns = document.querySelectorAll('.close-modal');
const deleteBtns = document.querySelectorAll('.btn-icon.text-danger');
let membershipChart; // Chart instance

// ==========================
// Theme Toggle
// ==========================
function toggleTheme() {
    document.body.classList.toggle('dark-mode');
    localStorage.setItem('darkMode', document.body.classList.contains('dark-mode'));
}

// Check saved theme preference
if (localStorage.getItem('darkMode') === 'true') {
    document.body.classList.add('dark-mode');
    if (themeSwitch) themeSwitch.checked = true;
}

if (themeSwitch) {
    themeSwitch.addEventListener('change', toggleTheme);
}

// ==========================
// Sidebar Toggle (Mobile)
// ==========================
function toggleSidebar() {
    sidebar.classList.toggle('active');
}

if (menuToggle) {
    menuToggle.addEventListener('click', toggleSidebar);
}

// ==========================
// Notifications
// ==========================
function toggleNotifications() {
    notificationDropdown.classList.toggle('show');
}

if (notificationBtn) {
    notificationBtn.addEventListener('click', toggleNotifications);
}

// Close dropdown when clicking outside
document.addEventListener('click', (e) => {
    if (!e.target.closest('.notification-btn') && notificationDropdown) {
        notificationDropdown.classList.remove('show');
    }
});

// ==========================
// Time Filter Buttons
// ==========================
timeFilterBtns.forEach(btn => {
    btn.addEventListener('click', () => {
        timeFilterBtns.forEach(b => b.classList.remove('active'));
        btn.classList.add('active');
        initCharts();
    });
});

// ==========================
// Class Item Hover
// ==========================
classItems.forEach(item => {
    const menuBtn = item.querySelector('.btn-icon');
    item.addEventListener('mouseenter', () => menuBtn.style.visibility = 'visible');
    item.addEventListener('mouseleave', () => menuBtn.style.visibility = 'hidden');
});

// ==========================
// Search Debounce
// ==========================
function debounce(func, wait) {
    let timeout;
    return function executedFunction(...args) {
        clearTimeout(timeout);
        timeout = setTimeout(() => func(...args), wait);
    };
}

// ==========================
// Charts
// ==========================
function initCharts() {
    const ctx = document.getElementById('membershipChart');
    if (!ctx) return;

    if (membershipChart) membershipChart.destroy();

    const activePeriod = document.querySelector('.time-filter button.active')?.dataset.period || 'month';
    const chartData = getChartData(activePeriod);

    membershipChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: chartData.labels,
            datasets: [
                {
                    label: 'New Members',
                    data: chartData.newMembers,
                    borderColor: '#4a6cf7',
                    backgroundColor: 'rgba(74, 108, 247, 0.1)',
                    borderWidth: 2,
                    tension: 0.4,
                    fill: true,
                    pointBackgroundColor: '#fff',
                    pointBorderColor: '#4a6cf7',
                    pointBorderWidth: 2,
                    pointRadius: 4,
                    pointHoverRadius: 6
                },
                {
                    label: 'Active Members',
                    data: chartData.activeMembers,
                    borderColor: '#28a745',
                    backgroundColor: 'rgba(40, 167, 69, 0.1)',
                    borderWidth: 2,
                    tension: 0.4,
                    fill: true,
                    pointBackgroundColor: '#fff',
                    pointBorderColor: '#28a745',
                    pointBorderWidth: 2,
                    pointRadius: 4,
                    pointHoverRadius: 6
                }
            ]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: { display: false },
                tooltip: {
                    backgroundColor: '#fff',
                    titleColor: '#1a1a1a',
                    bodyColor: '#4a4a4a',
                    borderColor: '#e5e7eb',
                    borderWidth: 1,
                    padding: 12,
                    callbacks: {
                        label: function (context) {
                            return `${context.dataset.label}: ${context.raw.toLocaleString()}`;
                        }
                    }
                }
            },
            scales: {
                x: {
                    grid: { display: false },
                    ticks: { color: '#6b7280' }
                },
                y: {
                    beginAtZero: true,
                    grid: { color: 'rgba(0, 0, 0, 0.05)' },
                    ticks: {
                        color: '#6b7280',
                        callback: value => value.toLocaleString()
                    }
                }
            },
            interaction: { intersect: false, mode: 'index' },
            animation: { duration: 1000, easing: 'easeInOutQuart' }
        }
    });
}

function getChartData(period) {
    const data = {
        week: {
            labels: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'],
            newMembers: [12, 18, 15, 22, 20, 25, 30],
            activeMembers: [45, 50, 52, 60, 65, 70, 75]
        },
        month: {
            labels: ['Week 1', 'Week 2', 'Week 3', 'Week 4'],
            newMembers: [45, 52, 68, 75],
            activeMembers: [180, 195, 210, 225]
        },
        year: {
            labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
            newMembers: [120, 150, 180, 210, 240, 270, 300, 280, 260, 290, 320, 350],
            activeMembers: [500, 550, 600, 650, 700, 750, 800, 850, 900, 950, 1000, 1050]
        }
    };
    return data[period] || data.month;
}

// ==========================
// Coaches Page
// ==========================
function setupCoachesPage() {
    const coachesContainer = document.getElementById('coachesContainer');
    if (!coachesContainer) return;

    const coachSearch = document.getElementById('coachSearch');
    const statusFilter = document.getElementById('statusFilter');
    const specialtyFilter = document.getElementById('specialtyFilter');
    const resetFiltersBtn = document.getElementById('resetFilters');
    const prevPageBtn = document.getElementById('prevPage');
    const nextPageBtn = document.getElementById('nextPage');
    const pageBtns = document.querySelectorAll('.page-btn');

    const allCoaches = Array.from(coachesContainer.querySelectorAll('.coach-card'));
    const itemsPerPage = 6;
    let currentPage = 1;

    allCoaches.forEach((card, index) => {
        const status = card.querySelector('.status-badge').classList.contains('active') ? 'active' : 'inactive';
        const specialties = Array.from(card.querySelectorAll('.specialty-tag')).map(tag => tag.textContent.toLowerCase());
        card.dataset.status = status;
        card.dataset.specialties = JSON.stringify(specialties);
        card.dataset.index = index;
    });

    function filterCoaches() {
        const searchTerm = coachSearch.value.toLowerCase();
        const statusValue = statusFilter.value;
        const specialtyValue = specialtyFilter.value;

        allCoaches.forEach(card => {
            const name = card.querySelector('h3').textContent.toLowerCase();
            const title = card.querySelector('.coach-title').textContent.toLowerCase();
            const about = card.querySelector('.coach-about').textContent.toLowerCase();
            const status = card.dataset.status;
            const specialties = JSON.parse(card.dataset.specialties);

            const matchesSearch = !searchTerm || name.includes(searchTerm) || title.includes(searchTerm) || about.includes(searchTerm) || specialties.some(s => s.includes(searchTerm));
            const matchesStatus = statusValue === 'all' || status === statusValue;
            const matchesSpecialty = specialtyValue === 'all' || specialties.some(s => s.includes(specialtyValue));

            card.style.display = (matchesSearch && matchesStatus && matchesSpecialty) ? 'flex' : 'none';
        });

        currentPage = 1;
        updatePagination();
    }

    function updatePagination() {
        const visibleCoaches = allCoaches.filter(card => card.style.display !== 'none');
        const totalPages = Math.ceil(visibleCoaches.length / itemsPerPage);
        const startIdx = (currentPage - 1) * itemsPerPage;
        const endIdx = startIdx + itemsPerPage;

        visibleCoaches.forEach((card, i) => {
            card.style.display = (i >= startIdx && i < endIdx) ? 'flex' : 'none';
        });

        pageBtns.forEach(btn => {
            const pageNum = parseInt(btn.textContent);
            if (isNaN(pageNum)) return;
            if (pageNum <= totalPages) {
                btn.style.display = 'flex';
                btn.classList.toggle('active', pageNum === currentPage);
            } else {
                btn.style.display = 'none';
            }
        });

        prevPageBtn.disabled = currentPage === 1;
        nextPageBtn.disabled = currentPage === totalPages || totalPages === 0;
    }

    coachSearch.addEventListener('input', filterCoaches);
    statusFilter.addEventListener('change', filterCoaches);
    specialtyFilter.addEventListener('change', filterCoaches);

    resetFiltersBtn.addEventListener('click', () => {
        coachSearch.value = '';
        statusFilter.value = 'all';
        specialtyFilter.value = 'all';
        filterCoaches();
    });

    prevPageBtn.addEventListener('click', () => {
        if (currentPage > 1) {
            currentPage--;
            updatePagination();
        }
    });

    nextPageBtn.addEventListener('click', () => {
        const visibleCoaches = allCoaches.filter(card => card.style.display !== 'none');
        const totalPages = Math.ceil(visibleCoaches.length / itemsPerPage);
        if (currentPage < totalPages) {
            currentPage++;
            updatePagination();
        }
    });

    pageBtns.forEach(btn => {
        btn.addEventListener('click', () => {
            const pageNum = parseInt(btn.textContent);
            if (!isNaN(pageNum)) {
                currentPage = pageNum;
                updatePagination();
            }
        });
    });

    filterCoaches();
}

// ==========================
// Animate Stats
// ==========================
function animateStats() {
    const statElements = document.querySelectorAll('[data-count]');
    statElements.forEach((stat, index) => {
        stat.style.setProperty('--delay', index);
        const target = parseInt(stat.getAttribute('data-count'));
        const isCurrency = stat.textContent.includes('$');
        const duration = 2000;
        const stepTime = 20;
        const steps = duration / stepTime;
        const increment = target / steps;
        let current = 0;

        const updateCounter = () => {
            current = Math.min(current + increment, target);
            stat.textContent = isCurrency ? `$${Math.floor(current).toLocaleString()}` : Math.floor(current).toLocaleString();
            if (current < target) setTimeout(updateCounter, stepTime);
        };

        const observer = new IntersectionObserver((entries) => {
            if (entries[0].isIntersecting) {
                updateCounter();
                observer.unobserve(stat);
            }
        });

        observer.observe(stat);
    });
}

// ==========================
// Modal Helpers
// ==========================
function hideModal(modal) {
    modal.classList.remove('show');
}

// ==========================
// File Input Change Preview
// ==========================
document.addEventListener('DOMContentLoaded', function () {
    const fileInput = document.getElementById('coachImage');
    if (fileInput) {
        fileInput.addEventListener('change', function () {
            const fileName = document.getElementById('fileName');
            fileName.textContent = this.files && this.files[0] ? this.files[0].name : 'No file chosen';
        });
    }

    initCharts();
    animateStats();
    setupCoachesPage();
});

// ==========================
// Responsive Adjustments
// ==========================
//function handleResize() {
//    if (window.innerWidth > 992) {
//        sidebar.classList.remove('active');
//    }
//}

//window.addEventListener('resize', debounce(handleResize, 250));
