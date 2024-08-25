import axios from 'axios';

const API_URL = 'https://localhost:7061/api/weather';

export async function getWeather(city) {
  try {
    const response = await axios.get(`${API_URL}/${city}`, {
    });
    return response.data;
  } catch (error) {
    if (error.response && error.response.status === 404) {
      console.error(`Error 404: City ${city} not found`);
      throw new Error(`City ${city} not found`);
    } else {
      console.error('Error fetching weather data for city: ${city}', error);
      throw new Error(`Error fetching weather data for city: ${city}`);
    }
  }
}

export async function getWeatherByCoordinates(lat, lon) {
  try {
    const response = await axios.get(`${API_URL}/coordinates`, {
      params: { lat, lon }
    });
    return response.data;
  } catch (error) {
    if (error.response && error.response.status === 404) {
      console.error(`Error 404: Weather data for coordinates [${lat}, ${lon}] not found`);
      throw new Error('Weather data for your location not found');
    } else {
      console.error('Error fetching weather data by coordinates:', error);
      throw new Error('Error fetching weather data by coordinates');
    }
  }
}