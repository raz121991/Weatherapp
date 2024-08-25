<template>
    <div class="weather-details">
      <h2>{{ weather.name }} Weather Details:</h2>
      <p><strong>Temperature:</strong> {{ weather.temperature }}°C</p>
      <p><strong>Humidity:</strong> {{ weather.humidity }}%</p>
      <p><strong>Wind Speed:</strong> {{ weather.windSpeed }} m/s</p>
      <p><strong>Description:</strong> {{ weather.weatherDescription}}</p>
    </div>
  </template>
  
  <script>
  export default {
    props: {
      weather: {
        type: Object,
        required: true
      }
    }
  };
  </script>
  
  <style scoped>
  .weather-details {
    background: white;
    padding: 20px;
    border-radius: 10px;
    box-shadow: 0px 4px 6px rgba(0, 0, 0, 0.1);
    max-width: 400px;
    margin: 50px auto 0; 
    text-align: left;
    animation: fadeIn 0.5s ease-in-out;
  }
  
  .weather-details h2 {
    margin-bottom: 15px;
    color: #007BFF;
    font-size: 1.5em;
    border-bottom: 2px solid #007BFF;
    padding-bottom: 5px;
  }
  
  .weather-details p {
    margin: 10px 0;
    font-size: 1.1em;
    line-height: 1.5;
  }
  
  .weather-details p strong {
    color: #333;
  }
  
  /* Optional animation for a smoother appearance */
  @keyframes fadeIn {
    from { opacity: 0; transform: translateY(10px); }
    to { opacity: 1; transform: translateY(0); }
  }
  </style>