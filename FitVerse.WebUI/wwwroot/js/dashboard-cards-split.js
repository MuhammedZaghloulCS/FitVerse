// Dashboard Cards Split - Splits plan notes into separate cards
$(document).ready(function() {
    console.log('Dashboard cards split script loaded');
    
    // Function to split text and create cards
    function splitIntoCards(element, cardClass, contentClass) {
        const text = element.text().trim();
        
        // Remove "Coach Notes:" prefix if exists
        const cleanText = text.replace(/^(Coach Notes:|Strong>Coach Notes:<\/strong>)/i, '').trim();
        
        console.log('Original text:', cleanText.substring(0, 100) + '...');
        console.log('Contains ,,,?', cleanText.includes(',,,'));
        
        if (!cleanText) {
            console.log('No text found');
            return;
        }
        
        // Split by ,,, with optional spaces
        let segments = [];
        if (cleanText.includes(',,,')) {
            segments = cleanText.split(/\s*,,,\s*/);
            console.log('Split by English commas:', segments.length, 'segments');
        } else if (cleanText.includes('،،،')) {
            segments = cleanText.split(/\s*،،،\s*/);
            console.log('Split by Arabic commas:', segments.length, 'segments');
        } else {
            // No delimiter found, show as single card
            segments = [cleanText];
            console.log('No delimiter found, showing as single card');
        }
        
        // Filter empty segments
        segments = segments.filter(seg => seg.trim() !== '');
        
        if (segments.length === 0) {
            console.log('No segments after filtering');
            return;
        }
        
        console.log('Creating', segments.length, 'cards');
        
        // Create cards HTML
        let cardsHTML = '';
        segments.forEach((segment, index) => {
            cardsHTML += `
                <div class="${cardClass} mb-2">
                    <div class="${contentClass}">
                        ${segment.trim()}
                    </div>
                </div>
            `;
        });
        
        // Replace original content with cards
        element.html(cardsHTML);
        console.log('Cards created successfully');
    }
    
    // Process Exercise Plan
    const exerciseAlert = $('.plan-card .plan-header:contains("Exercise Plan")').closest('.plan-card').find('.alert');
    if (exerciseAlert.length > 0) {
        console.log('Processing Exercise Plan...');
        splitIntoCards(exerciseAlert, 'exercise-plan-card', 'exercise-plan-content');
    }
    
    // Process Diet Plan
    const dietAlert = $('.plan-card .plan-header:contains("Diet Plan")').closest('.plan-card').find('.alert');
    if (dietAlert.length > 0) {
        console.log('Processing Diet Plan...');
        splitIntoCards(dietAlert, 'diet-plan-card', 'diet-plan-content');
    }
});
