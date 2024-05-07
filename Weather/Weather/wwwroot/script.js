document.getElementById('searchBtn').addEventListener('click', function() {
    const countryCode = document.getElementById('countryInput').value;
    const cityCode = document.getElementById('cityInput').value;
    const apiKey = '331cd2bed8f2df7a972e67ce7090282b';

    const weatherUrl = `https://api.openweathermap.org/data/2.5/weather?q=${cityCode},${countryCode}&appid=${apiKey}&units=metric`;
    const hourlyUrl = `https://api.openweathermap.org/data/2.5/forecast?q=${cityCode},${countryCode}&appid=${apiKey}&units=metric`;

    const requestWeather = new XMLHttpRequest();
    requestWeather.open('GET', weatherUrl, true);
    requestWeather.onload = function() {
        if (requestWeather.status >= 200 && requestWeather.status < 400) {
            const weatherData = JSON.parse(requestWeather.responseText);
            displayWeather(weatherData);

            const requestHourly = new XMLHttpRequest();
            requestHourly.open('GET', hourlyUrl, true);
            requestHourly.onload = function () {
                if (requestHourly.status >= 200 && requestHourly.status < 400) {
                    const hourlyData = JSON.parse(requestHourly.responseText);
                    displayHourly(hourlyData);
                } else {
                    displayError();
                }
            };
            requestHourly.send();
        } else {
            displayError();
        }
    };
    requestWeather.send();

    function displayWeather(data) {
        const weatherContainer = document.getElementById('currentWeatherContainer');
        weatherContainer.innerHTML = renderWeatherData(data);
    }

    function renderWeatherData(data) {
        return `
            <div class="weather-info">
                <div class="info-flex">
                    <h3>${data.sys.country} • ${data.name}</h3>
                    <h3>${new Date().toLocaleDateString()}</h3>
                </div>
                <div class="info-flex">
                    <div>
                        <div>
                            <h4>${data.weather[0].description}</h4>
                        </div>

                        <div class="grid-container">
                            <div>
                                <img src="https://openweathermap.org/img/wn/${data.weather[0].icon}.png" alt="Weather">
                            </div>
                            <div>
                                <h1>${data.main.temp}°C</h3>
                            </div>
                        </div>
                    </div>
                    <div class='grid-container'>
                        <p>Min temperature</p>
                        <p>${data.main.temp_min}°C</p>
                        <p>Max temperature</p>
                        <p>${data.main.temp_max}°C</p>
                        <p>Wind speed (km/h)</p>
                        <p>${data.wind.speed}</p>
                    </div>
                </div>
            </div>
        `;
    }

    function displayHourly(data) {
        const hourlyContainer = document.getElementById('hourlyWeatherContainer');
        hourlyContainer.innerHTML = renderHourlyData(data);
    }

    function renderHourlyData(data) {
        let html = `<div class="weather-info"><h3>Hourly</h3><table><thead><tr>`;
        const descriptions = ["Forecast", "Temp (°C)", "Wind (m/s)"];
        const dayOfWeek = new Date().toLocaleDateString('en-US', { weekday: 'long' });

        for (let i = 0; i < 7; i++) {
            const item = data.list[i];
            let headerCell = `<th>${new Date(item.dt * 1000).toLocaleTimeString()}`;
            if (i == 0) {
                headerCell = `<th>${dayOfWeek}`;
            }
            html += headerCell;
        }
        html += `</tr></thead><tbody>`;
        
        for (let j = 0; j < descriptions.length; j++) {
            html += `<tr><td>${descriptions[j]}</td>`;
            for (let k = 0; k < 6; k++) {
                const item = data.list[k];
                let dataCell = '';
                if (j === 0) {
                    dataCell = `<td>${item.weather[0].description}</td>`;
                } else if (j === 1) {
                    dataCell = `<td>${item.main.temp}</td>`;
                } else {
                    dataCell = `<td>${item.wind.speed}</td>`;
                }
                html += dataCell;
            }
            html += `</tr>`;
        }
        html += `</tbody></table></div>`;
        return html;
    }

    function displayError() {
        const weatherContainer = document.getElementById('currentWeatherContainer');
        const hourlyContainer = document.getElementById('hourlyWeatherContainer');
        hourlyContainer.innerHTML = '';

        weatherContainer.innerHTML = `
            <div class="weather-error">
                <h1>404</h1>
                <h3>NOT FOUND</h3>
                <h4>Please, enter a different country or city</h4>
            </div>
        `;
    }
});
