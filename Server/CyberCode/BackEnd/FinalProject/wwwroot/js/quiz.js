function submitQuiz() {
    // Correct answers
    const correctAnswers = {
        q1: 'A cyber attack using fake websites',
        q2: 'Unexpected attachments',
        q3: 'An attack to disrupt service',
        q4: 'Virtual Private Network',
        q5: 'P@ssw0rd!',
        q6: 'Malicious software',
        q7: 'To block unauthorized access',
        q8: 'A two-step process for logging in',
        q9: 'Software that demands payment',
        q10: 'HyperText Transfer Protocol Secure',
        q11: 'Double-check the email ID and the content, then contact your supervisor or the IT department to confirm its authenticity.',
        q12: 'You are redirected to a website that asks you to enter sensitive information or directs you to download an attachment.',
        q13: 'All of the above.'
    };

    const resultsContainer = document.getElementById('results-container');
    const formElements = document.getElementById('quiz-form').elements;
    let userAnswers = {};
    let correctCount = 0;

    // Collect user answers
    for (let i = 0; i < formElements.length; i++) {
        if (formElements[i].type === 'radio' && formElements[i].checked) {
            userAnswers[formElements[i].name] = formElements[i].value;
        }
    }

    // Display results
    let resultsHTML = '<h2>Results</h2>';

    for (const [question, correctAnswer] of Object.entries(correctAnswers)) {
        const userAnswer = userAnswers[question];
        const result = userAnswer === correctAnswer ? 'Correct' : 'Incorrect';
        const resultColor = userAnswer === correctAnswer ? 'green' : 'red';
        resultsHTML += `<p>${question}: Your answer: ${userAnswer || 'No answer'
            } - <span style="color: ${resultColor}">${result}</span>. Correct answer: ${correctAnswer}</p>`;
        if (userAnswer === correctAnswer) {
            correctCount++;
        }
    }

    resultsHTML += `<p>Total Correct: ${correctCount} out of ${Object.keys(correctAnswers).length
        }</p>`;
    resultsContainer.innerHTML = resultsHTML;
}