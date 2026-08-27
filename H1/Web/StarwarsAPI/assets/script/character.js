async function fetchCharacters() {
    const response = await fetch("https://swapi.py4e.com/api/people/");
    const data = await response.json();
    return data.results;
}

function displayCharacters(characters) {
    const characterGrid = document.querySelector('.character-grid');
    characterGrid.innerHTML = '';
    
    characters.forEach(character => {
        const newCard = document.createElement('div');
        newCard.className = 'character-card';
        
        const name = document.createElement('h2');
        const height = document.createElement('p');
        const birthYear = document.createElement('p');
        const gender = document.createElement('p');
        
        name.textContent = character.name;
        height.textContent = `Height: ${character.height} cm`;
        birthYear.textContent = `Birth Year: ${character.birth_year}`;
        gender.textContent = `Gender: ${character.gender}`;
        
        newCard.appendChild(name);
        newCard.appendChild(height);
        newCard.appendChild(birthYear);
        newCard.appendChild(gender);
        
        characterGrid.appendChild(newCard);
    });
}

function setupSearch(characters) {
    const searchInput = document.getElementById('searchInput');
    searchInput.addEventListener('input', (e) => {
        const searchTerm = e.target.value.toLowerCase();
        const filteredCharacters = characters.filter(character => 
            character.name.toLowerCase().includes(searchTerm)
        );
        displayCharacters(filteredCharacters);
    });

    if (searchInput.value === '') {
        displayCharacters(characters);
    }

}

function setupPagination(characters) {
    const pagination = document.getElementById('pagination');
    const contentPerPage = 5;
    const totalPages = Math.ceil(characters.length / contentPerPage);
    pagination.innerHTML = '';

    for (let i = 1; i <= totalPages; i++) {
        const pageButton = document.createElement('button');
        pageButton.textContent = i;
        pageButton.addEventListener('click', () => {
            displayCharacters(characters.slice((i - 1) * contentPerPage, i * contentPerPage));
        });
        pagination.appendChild(pageButton);
    }
}

fetchCharacters().then(characters => {
    displayCharacters(characters);
    setupSearch(characters);
    setupPagination(characters);
});


