<template>
  <div class="home">
    <div class="container-fluid">
      <PublicKeeps :key="'homeKeep'" />
    </div>
  </div>
</template>

<script>
import PublicKeeps from "../components/publickeeps";
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
  components: { PublicKeeps },
};
</script>
<style>
svg {
  display: inline;
}
.dropdown-item:hover {
  cursor: pointer;
}
.card:hover {
  -ms-transform: scale(1.075, 1.075); /* IE 9 */
  transform: scale(1.075, 1.075); /* Standard syntax */
}
</style>