<template>
  <div class="vault container-fluid" v-if="vault.name">
    <div class="row justify-content-center">
      <div class="col-12">
        <h1>{{vault.name}}</h1>
        <h5>{{vault.description}}</h5>
      </div>
    </div>

    <div class="row">
      <div class="col-3" v-for="keep in keeps" :key="keep.id">
        {{keep}}
        <div class="card bg-dark text-light">
          <img :src="keep.img" class="card-img-top img-fluid" alt />
          <div class="card-body">
            <h5 class="card-title">{{keep.name}}</h5>
            <p class="card-text">{{keep.description}}.</p>
            <!--  -->
            <button
              class="btn btn-secondary text-dark"
              type="button"
            >Delete From Vault</button>
            <!--  -->
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  name: "vault",
  data() {
    return {};
  },
  computed: {
    keeps() {
      return this.$store.state.vaultKeeps;
    },
    vault() {
      return this.$store.state.activeVault;
    },
  },
  mounted() {
    this.$store.dispatch("getActiveVault", this.$route.params.vaultId);
    this.$store.dispatch("getVaultKeeps", this.$route.params.vaultId);
  },
  methods: {},
  components: {},
  beforeDestroy(){
      this.$store.state.vaultKeeps = [];
      this.$store.state.activeVault = {};
  },
};
</script>


<style scoped>
</style>