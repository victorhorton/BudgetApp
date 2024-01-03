<template>
  <div class="about">
    <h1>This is an about page</h1>
  </div>
  <ul class="list-group">
    <li
      class="list-group-item"
      v-for="weatherItem in weather"
      :key="weatherItem.date"
    >
      {{ weatherItem }}
    </li>
  </ul>
</template>

<script>
import axios from "axios";

export default {
  name: "AboutView",
  data() {
    return {
      weather: [],
    };
  },
  mounted() {
    axios({
      method: "get",
      url: "http://localhost:5286/WeatherForecast",
      responseType: "json",
    }).then((resp) => {
      this.weather = resp.data;
    });
  },
};
</script>
