document.addEventListener('DOMContentLoaded', function() {
    const numStars = 100;
    const skipButton = document.getElementById('skip-button');
    const splashScreen = document.getElementById('splash-screen');
    const mainNav = document.querySelector('.main-nav');
    const landingPage = document.querySelector('.landing-page');
    const hasSeenSplash = localStorage.getItem('hasSeenSplash');

    if (hasSeenSplash) {
        skipIntro(false);
    } else {
        splashScreen.style.display = 'block';
        
        for (let i = 0; i < numStars; i++) {
            let star = document.createElement("div");  
            star.className = "star";
            var xy = getRandomPosition();
            star.style.top = xy[0] + 'px';
            star.style.left = xy[1] + 'px';
            splashScreen.appendChild(star);
        }

        skipButton.addEventListener('click', () => skipIntro(true));
        setTimeout(() => skipIntro(true), 45000);
    }

    function skipIntro(isFirstTime) {
        if (isFirstTime) {
            localStorage.setItem('hasSeenSplash', 'true');
        }

        skipButton.removeEventListener('click', skipIntro);
        landingPage.style.display = 'block';
        splashScreen.classList.add('hidden');
        mainNav.classList.add('visible');
        
        document.body.style.backgroundColor = 'black';
        
        setTimeout(() => {
            splashScreen.style.display = 'none';
        }, 1000);
    }
});

function getRandomPosition() {  
    var y = window.innerWidth;
    var x = window.innerHeight;
    var randomX = Math.floor(Math.random()*x);
    var randomY = Math.floor(Math.random()*y);
    return [randomX,randomY];
}