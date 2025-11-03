$(document).ready(function() {
    // Password visibility toggle
    const togglePassword = document.getElementById('togglePassword');
    const passwordInput = document.getElementById('Password');
    const passwordIcon = document.getElementById('passwordIcon');
    
    if (togglePassword) {
        togglePassword.addEventListener('click', function() {
            const type = passwordInput.getAttribute('type') === 'password' ? 'text' : 'password';
            passwordInput.setAttribute('type', type);
            
            // Toggle icon
            if (type === 'text') {
                passwordIcon.classList.remove('bi-eye');
                passwordIcon.classList.add('bi-eye-slash');
            } else {
                passwordIcon.classList.remove('bi-eye-slash');
                passwordIcon.classList.add('bi-eye');
            }
        });
    }

    // Form submission with loading state
    const loginForm = document.getElementById('loginForm');
    const loginButton = document.getElementById('loginButton');
    const buttonText = loginButton?.querySelector('.button-text');
    const spinner = loginButton?.querySelector('.spinner-border');

    if (loginForm) {
        loginForm.addEventListener('submit', function(e) {
            // Show loading state
            if (buttonText) buttonText.textContent = 'Signing In...';
            if (spinner) spinner.classList.remove('d-none');
            if (loginButton) loginButton.disabled = true;

            // Disable form inputs during submission
            const inputs = loginForm.querySelectorAll('input');
            inputs.forEach(input => input.disabled = true);
        });
    }

    // Auto-hide alerts after 5 seconds
    const alerts = document.querySelectorAll('.alert-dismissible');
    alerts.forEach(alert => {
        setTimeout(() => {
            const bsAlert = new bootstrap.Alert(alert);
            bsAlert.close();
        }, 5000);
    });

    // Email validation feedback
    const emailInput = document.getElementById('Email');
    if (emailInput) {
        emailInput.addEventListener('blur', function() {
            const email = this.value.trim();
            const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            
            if (email && !emailRegex.test(email)) {
                this.classList.add('is-invalid');
                this.classList.remove('is-valid');
            } else if (email) {
                this.classList.add('is-valid');
                this.classList.remove('is-invalid');
            }
        });

        // Clear validation on input
        emailInput.addEventListener('input', function() {
            this.classList.remove('is-valid', 'is-invalid');
        });
    }

    // Focus on first empty field
    const firstEmptyField = loginForm?.querySelector('input:invalid');
    if (firstEmptyField) {
        setTimeout(() => firstEmptyField.focus(), 100);
    }

    // Add keyboard navigation
    document.addEventListener('keydown', function(e) {
        // Enter key on form fields
        if (e.key === 'Enter' && document.activeElement.tagName !== 'BUTTON') {
            const form = document.activeElement.closest('form');
            if (form && form.id === 'loginForm') {
                const submitButton = form.querySelector('button[type="submit"]');
                if (submitButton && !submitButton.disabled) {
                    submitButton.click();
                }
            }
        }
    });

    // Prevent resubmission on page refresh
    if (window.history.replaceState) {
        window.history.replaceState(null, null, window.location.href);
    }

    console.log('Login page initialized successfully');
});

// Additional utility functions
function showLoadingState() {
    const loginButton = document.getElementById('loginButton');
    const buttonText = loginButton?.querySelector('.button-text');
    const spinner = loginButton?.querySelector('.spinner-border');
    
    if (buttonText) buttonText.textContent = 'Signing In...';
    if (spinner) spinner.classList.remove('d-none');
    if (loginButton) loginButton.disabled = true;
}

function hideLoadingState() {
    const loginButton = document.getElementById('loginButton');
    const buttonText = loginButton?.querySelector('.button-text');
    const spinner = loginButton?.querySelector('.spinner-border');
    
    if (buttonText) buttonText.textContent = 'Sign In';
    if (spinner) spinner.classList.add('d-none');
    if (loginButton) loginButton.disabled = false;
}

function validateEmail(email) {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
}