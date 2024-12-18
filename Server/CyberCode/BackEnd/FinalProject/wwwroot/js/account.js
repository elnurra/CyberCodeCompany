document.addEventListener('DOMContentLoaded', () => {
    const loadingScreen = document.getElementById('loading-screen');
    const loginPage = document.getElementById('login-page');

    setTimeout(() => {
        loadingScreen.classList.add('hidden');
        loginPage.classList.remove('hidden');
    }, 3000); // 3 seconds delay
});
