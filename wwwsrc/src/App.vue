<template>
  <div id="app" class="bg-info text-dark" style="min-height:100vh;">
    <navbar />
    <router-view />
  </div>
</template>

<script>
import Navbar from "@/components/navbar";
import { onAuth } from "@bcwdev/auth0-vue";
export default {
  name: "App",
  async beforeCreate() {
    await onAuth();
    this.$store.dispatch("setBearer", this.$auth.bearer);
  },
  components: {
    Navbar
  }
};
</script>

<style lang="scss">
@import "./assets/_overrides.scss";
@import "bootstrap";
@import "./assets/_variables.scss";
</style>
