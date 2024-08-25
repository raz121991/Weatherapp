<template>
     <div :class="['weather-card', { selected: isSelected }]" @click="$emit('show-details', weather)">
    <h3>{{ weather.name }}</h3>
    <p>Temperature: {{ weather.temperature }}°C</p>
  </div>
  </template>
  
  <script>
  export default {
    props: {
    weather: Object,
    isSelected: Boolean
  },
    methods: {
      showDetails() {
        this.$emit('show-details', this.weather);
      }
    }
  }
  </script>