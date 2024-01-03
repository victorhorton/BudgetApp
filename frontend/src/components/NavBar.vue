<script>
export default {
  name: "NavBar",
  data() {
    return {
      count: 0,
    };
  },
};
</script>

<template>
  <nav class="navbar navbar-expand-lg bg-light fixed-top">
    <router-link to="/" class="navbar-brand">Home</router-link>
    <button
      class="navbar-toggler"
      type="button"
      data-bs-toggle="offcanvas"
      data-bs-target="#navbarOffcanvasLg"
      aria-controls="navbarOffcanvasLg"
    >
      <span class="navbar-toggler-icon"></span>
    </button>
    <div
      class="offcanvas offcanvas-top"
      tabindex="-1"
      id="navbarOffcanvasLg"
      aria-labelledby="navbarOffcanvasLgLabel"
    >
      <div class="offcanvas-body">
        <ul class="navbar-nav justify-content-end flex-grow-1 pe-3">
          <li class="nav-item">
            <router-link to="/" class="nav-link active">Home</router-link>
          </li>
          <li class="nav-item">
            <router-link to="/about" class="nav-link">About</router-link>
          </li>
        </ul>
      </div>
    </div>
  </nav>
</template>
