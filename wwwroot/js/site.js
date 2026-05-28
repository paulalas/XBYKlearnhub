// LearnHub Custom JavaScript

// Add your custom JavaScript here

console.log('LearnHub initialized');

// Get Started Form Submission
document.addEventListener('DOMContentLoaded', function () {
    const getStartedForm = document.getElementById('getStartedForm');
    
    if (getStartedForm) {
        getStartedForm.addEventListener('submit', async function (e) {
            e.preventDefault();
            
            // Get form data
            const formData = {
                name: document.getElementById('name').value.trim(),
                email: document.getElementById('email').value.trim(),
                course: document.getElementById('course').value,
                message: document.getElementById('message').value.trim(),
                agreeToTerms: document.getElementById('terms').checked
            };
            
            // Get the submit button
            const submitButton = getStartedForm.querySelector('button[type="submit"]');
            const originalButtonText = submitButton.textContent;
            
            try {
                // Disable button and show loading state
                submitButton.disabled = true;
                submitButton.innerHTML = '<span class="spinner-border spinner-border-sm me-2" role="status" aria-hidden="true"></span>Submitting...';
                
                // Send data to the API
                const response = await fetch('/api/forms/get-started', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify(formData)
                });
                
                const result = await response.json();
                
                if (result.success) {
                    // Show success message
                    showAlert('success', result.message);
                    
                    // Reset form
                    getStartedForm.reset();
                    
                    // Close modal after 2 seconds
                    setTimeout(() => {
                        const modal = bootstrap.Modal.getInstance(document.getElementById('exampleModal'));
                        if (modal) {
                            modal.hide();
                        }
                    }, 2000);
                } else {
                    // Show error message
                    const errorMessage = result.errors && result.errors.length > 0 
                        ? result.errors.join('<br>') 
                        : result.message;
                    showAlert('danger', errorMessage);
                }
            } catch (error) {
                console.error('Error submitting form:', error);
                showAlert('danger', 'An unexpected error occurred. Please try again later.');
            } finally {
                // Re-enable button
                submitButton.disabled = false;
                submitButton.textContent = originalButtonText;
            }
        });
    }
});

// Helper function to show Bootstrap alerts
function showAlert(type, message) {
    const alertPlaceholder = document.getElementById('alertPlaceholder');
    if (!alertPlaceholder) return;
    
    const wrapper = document.createElement('div');
    wrapper.innerHTML = `
        <div class="alert alert-${type} alert-dismissible fade show" role="alert">
            ${message}
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        </div>
    `;
    
    alertPlaceholder.innerHTML = '';
    alertPlaceholder.appendChild(wrapper);
    
    // Auto-dismiss after 5 seconds
    setTimeout(() => {
        const alert = bootstrap.Alert.getOrCreateInstance(wrapper.querySelector('.alert'));
        alert.close();
    }, 5000);
}

