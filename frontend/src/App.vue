<template>
  <div id="app">
    <div class="weather-cards">
      <WeatherCard
        v-for="city in availableCities"
        :key="city"
        :weather="weatherData[city]"
        @show-details="displayDetails"
        :isSelected="selectedWeather && selectedWeather.name === city"
      />
    </div>

    <div class="user-weather">
      <h2>Your weather details:</h2>

      <div class="centered-card">
        <WeatherCard
          v-if="userWeather"
          :weather="userWeather"
          @show-details="displayDetails"
          :isSelected="selectedWeather && selectedWeather.name === userWeather.name"
        />
      </div>
      <div v-if="userError" class="error-message">
        <p>{{ userError }}</p>
      </div>
    </div>

    <WeatherDetails v-if="selectedWeather" :weather="selectedWeather" />
    <div v-if="cityError" class="error-message">
      <p>{{ cityError }}</p>
    </div>
  </div>
</template>

<script>
import { getWeather, getWeatherByCoordinates } from "./services/WeatherService";
import WeatherCard from "./components/WeatherCard.vue";
import WeatherDetails from "./components/WeatherDetails.vue";
import { ref, computed, onMounted } from "vue";

export default {
  components: {
    WeatherCard,
    WeatherDetails,
  },
  setup() {
    const cities = ["New York", "London","Tokyo"];
    const weatherData = ref({});
    const userWeather = ref(null);
    const selectedWeather = ref(null);
    const cityError = ref("");
    const userError = ref("");

    const availableCities = computed(() => {
      return cities.filter((city) => weatherData.value[city]);
    });

    const fetchWeather = async () => {
      try {
        await Promise.all(
          cities.map(async (city) => {
            const weather = await getWeather(city);
            weatherData.value[city] = weather;
          })
        );
        await fetchUserLocationWeather();
      } catch (error) {
        cityError.value = error.message;
      }
    };
    const fetchUserLocationWeather = async () => {
      if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(
          async (position) => {
            const { latitude, longitude } = position.coords;
            try{
              const weather = await getWeatherByCoordinates(latitude, longitude);
              userWeather.value = weather;
            }catch(error)
            {
              userError.value = error.message;
            }
           
          },
          (error) => {
            console.error(error);
            userError.value = 'Unable to retrieve your location';
          }
        );
      } else {
        userError.value = 'Geolocation is not supported by this browser';
      }
    };

    const displayDetails = (weather) => {
      selectedWeather.value = weather;
    };

    onMounted(() => {
      fetchWeather();
    });

    return {
      availableCities,
      weatherData,
      userWeather,
      selectedWeather,
      displayDetails,
      userError,
      cityError
    };
  },
};
</script>

<style scoped>
#app {
  font-family: "Arial", sans-serif;
  background: linear-gradient(to bottom, #87ceeb, #f8f9fa);
  min-height: 100vh;
  padding: 20px;
}

.weather-cards {
  display: flex;
  justify-content: space-around;
  margin-bottom: 30px;
}

.centered-card {
  display: flex;
  justify-content: center;
}

.weather-card {
  background: white;
  padding: 15px;
  border-radius: 10px;
  box-shadow: 0px 4px 6px rgba(0, 0, 0, 0.1);
  cursor: pointer;
  transition: transform 0.2s, box-shadow 0.2s;
  text-align: center;
  width: 200px;
}

.weather-card:hover {
  transform: translateY(-5px);
  box-shadow: 0px 6px 10px rgba(0, 0, 0, 0.15);
}

.weather-card.selected {
  background: #007bff;
  color: white;
}
.user-weather {
  margin-top: 30px;
  text-align: center;
}
.user-weather h2 {
  color: #007bff;
  margin-bottom: 15px;
  font-size: 1.5em;
}
</style>
