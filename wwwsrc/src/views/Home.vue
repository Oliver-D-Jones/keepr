<template>
  <div class="home">
    <div class="container-fluid">
      <div class="row">
        <Keep :key="'homeKeep'" />
      </div>
    </div>
  </div>
</template>

<script>
import Keep from "../components/keep";
export default {
  name: "home",
  data() {
    return {};
  },
  computed: {
    user() {
      return this.$store.state.user;
    },
  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
    },
  },
  mounted() {},
  components: { Keep },
};
</script>
<style>
svg {
  display: inline;
}
</style>