import Vue from "vue";
import Router from "vue-router";
// @ts-ignore
import Home from "./views/Home.vue";
// @ts-ignore
import Dashboard from "./views/Dashboard.vue";
// @ts-ignore
import Vault from "./views/Vault.vue";
// @ts-ignore
import Vaults from "./views/Vaults.vue";
// @ts-ignore
import MyKeeps from "./views/MyKeeps.vue";

import { authGuard } from "@bcwdev/auth0-vue";

Vue.use(Router);

export default new Router({
  routes: [
    {
      path: "/",
      name: "home",
      component: Home
    },
    {
      path: "/dashboard",
      name: "dashboard",
      component: Dashboard,
      beforeEnter: authGuard
    },
    {
      path: '/vaults',
      name: 'vaults',
      component: Vaults,
      beforeEnter: authGuard
    },
    {
      path: '/vault/:vaultId',
      name: 'vault',
      component: Vault,
      beforeEnter: authGuard
    },
    {
      path: "/mykeeps",
      name: "mykeeps",
      component: MyKeeps,
      beforeEnter: authGuard
    },

  ]
});
