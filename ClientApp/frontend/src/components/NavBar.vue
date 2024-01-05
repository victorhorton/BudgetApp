<template>
  <nav class="navbar navbar-expand-lg">
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
        <ul class="navbar-nav justify-content-center flex-grow-1 pe-3">
          <li class="nav-item">
            <router-link to="/" active-class="active" class="nav-link"
              >Home</router-link
            >
          </li>
          <li class="nav-item">
            <router-link to="/about" active-class="active" class="nav-link"
              >About</router-link
            >
          </li>
          <li class="nav-item">
            <router-link to="/budget" active-class="active" class="nav-link"
              >Budget</router-link
            >
          </li>
        </ul>
      </div>
    </div>
  </nav>
</template>

<script>
export default {
  name: "NavBar"
};
</script>

<style>
.navbar-toggler {
  border: none !important;

  &:focus {
    box-shadow: none !important;
  }
}
</style>
