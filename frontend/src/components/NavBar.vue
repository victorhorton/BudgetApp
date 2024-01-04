<template>
  <nav>
    <router-link to="/" active-class="active">Home</router-link> |
    <router-link to="/about" active-class="active">About</router-link>
  </nav>
</template>

<script>
export default {
  name: "NavBar"
};
</script>
