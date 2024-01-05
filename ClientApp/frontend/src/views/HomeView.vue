<template>
  <div class="home">
    <img alt="Vue logo" src="../assets/logo.png" />
    <HelloWorld msg="Welcome to Your Vue.js App" />
  </div>
</template>

<script>
// @ is an alias to /src
import HelloWorld from "@/components/HelloWorld.vue";

export default {
  name: "HomeView",
  components: {
    HelloWorld
  },
  data() {
    return {
      weather: []
    };
  },
  mounted() {
    fetch("/WeatherForecast")
      .then((resp) => {
        return resp.json();
      })
      .then((data) => {
        this.weather = data;
      });
  }
};
</script>
