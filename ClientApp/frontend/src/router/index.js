import { createRouter, createWebHistory } from "vue-router";
import HomeView from "../views/HomeView.vue";
import { Offcanvas } from "bootstrap";

const routes = [
  {
    path: "/",
    name: "home",
    component: HomeView
  },
  {
    path: "/about",
    name: "about",
    component: () => import("../views/AboutView.vue")
  },
  {
    path: "/budgets",
    name: "budget",
    component: () => import("../views/Budget/IndexView.vue")
  }
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
});

router.afterEach(() => {
  const openedCanvas = Offcanvas.getInstance(
    document.getElementById("navbarOffcanvasLg")
  );

  if (openedCanvas) {
    openedCanvas.hide();
  }
});

export default router;
