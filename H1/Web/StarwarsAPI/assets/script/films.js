async function fetchFilms() {
    const response = await fetch("https://swapi.py4e.com/api/films/");
    const data = await response.json();
    return data.results;
}

function getPreviewText(crawl) {
    const cleanText = crawl.replace(/\r\n/g, ' ');
    
    const truncatedText = cleanText.substring(0, 100);
    
    const words = truncatedText.split(' ');
    
    const preview = words.join(' ') + '...';
    
    return preview;
}

function displayFilms(films) {
    const filmGrid = document.querySelector('.film-grid');
    const templateCard = filmGrid.querySelector('.film-card');
    filmGrid.innerHTML = '';

    films.forEach(film => {
        const newCard = templateCard.cloneNode(true);
        const [title, episode, preview, button] = newCard.querySelectorAll('h2, .episode, .preview, .read-more');
        
        title.textContent = film.title;
        episode.textContent = `Released: ${film.release_date}`;
        preview.textContent = getPreviewText(film.opening_crawl);
        
        button.addEventListener('click', () => showModal(film));
        
        filmGrid.appendChild(newCard);
    });
}

function showModal(film) {
    const modal = document.getElementById('crawlModal');
    const modalTitle = modal.querySelector('.modal-title');
    const crawlText = modal.querySelector('.crawl-text');
    
    modalTitle.textContent = film.title;
    crawlText.textContent = film.opening_crawl;
    modal.style.display = 'flex';
}

function setupModal() {
    const modal = document.getElementById('crawlModal');
    const closeButton = modal.querySelector('.close-button');
    
    closeButton.addEventListener('click', () => {
        modal.style.display = 'none';
    });
    
    window.addEventListener('click', (e) => {
        if (e.target === modal) {
            modal.style.display = 'none';
        }
    });
}

fetchFilms().then(films => {
    displayFilms(films);
    setupModal();
});