async function fetchPlanets() {
    const response = await fetch("https://swapi.py4e.com/api/planets/");
    const data = await response.json();
    return data.results;
}

function setupClimateFilter(planets) {
    const select = document.getElementById('planet-select');
    
    const allClimates = planets.map(planet => {
        return planet.climate.split(',').map(climate => {
            return climate.trim();
        });
    });

    const flattenedClimates = allClimates.flat();

    const uniqueClimates = new Set(flattenedClimates);

    const climates = Array.from(uniqueClimates).sort();
    
    select.innerHTML = '<option value="">All climates</option>';
    
    climates.forEach(climate => {
        const option = document.createElement('option');
        option.value = climate;
        option.textContent = climate;
        select.appendChild(option);
    });
    
    select.addEventListener('change', (e) => {
        const selectedClimate = e.target.value;
        const filteredPlanets = selectedClimate 
            ? planets.filter(planet => planet.climate.includes(selectedClimate))
            : planets;
        displayPlanets(filteredPlanets);
    });
}

function displayPlanets(planets) {
    const planetGrid = document.querySelector('.planet-grid');
    const templateCard = planetGrid.querySelector('.planet-card');
    planetGrid.innerHTML = '';

    planets.forEach(planet => {
        const newCard = templateCard.cloneNode(true);
        const [name, climate, terrain, population, rotation] = newCard.querySelectorAll('h2, p');
        
        name.textContent = planet.name;
        climate.textContent = `Climate: ${planet.climate}`;
        terrain.textContent = `Terrain: ${planet.terrain}`;
        population.textContent = `Population: ${planet.population === "unknown" ? "unknown" : 
            Number(planet.population).toLocaleString()}`;
        
        planetGrid.appendChild(newCard);
    });
}

fetchPlanets().then(planets => {
    setupClimateFilter(planets);
    displayPlanets(planets);
});
