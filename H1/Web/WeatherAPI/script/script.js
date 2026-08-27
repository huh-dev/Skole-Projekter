const apiKey = "34aebcf9290dd0470af4eaa1ccb87cf1";

async function getWeather() {
    const location = document.querySelector(".location").value;
    // const locationData = await getLocation(location);
    fetchWeather(location);
}


async function fetchWeather(location) {
    // const lat = location.lat;
    // const lon = location.lon;
    // const response = await fetch(`https://api.openweathermap.org/data/2.5/forecast?lat=${lat}&lon=${lon}&appid=${apiKey}`);
    const response = await fetch(`https://api.openweathermap.org/data/2.5/forecast?q=${location}&appid=${apiKey}`);
    const data = await response.json();
    
    const currentWeather = data.list[0];
    
    document.querySelector('.weather-card').style.display = "flex";
    document.querySelector('.city-name').textContent = data.city.name;
    document.querySelector('.date').textContent = new Date(currentWeather.dt * 1000).toLocaleDateString();
    document.querySelector('.temp').textContent = `${Math.round(currentWeather.main.temp - 273.15)}°C`;
    document.querySelector('.feels-like').textContent = `Feels like: ${Math.round(currentWeather.main.feels_like - 273.15)}°C`;
    document.querySelector('.weather-icon').src = `../icons/${currentWeather.weather[0].icon}.png`;
    document.querySelector('.description').textContent = currentWeather.weather[0].description;
    document.querySelector('.humidity').textContent = `Humidity: ${currentWeather.main.humidity}%`;
    document.querySelector('.wind').textContent = `Wind: ${currentWeather.wind.speed} m/s`;
}

async function getLocation(location) {
    // const response = await fetch(`http://ip-api.com/json/${location}`);
    const response = await fetch(`https://api.openweathermap.org/data/2.5/forecast?q=${location}&appid=${apiKey}`);
    const data = await response.json();
    console.log(data);
    // return data;
}

