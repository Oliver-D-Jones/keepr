<template>
  <div class="vaults container-fluid" v-if="show">
    <form class="row my-3 py-2 bg-light justify-content-center border">
      <div class="col-6">
        <input
          type="text"
          class="form-control"
          v-model="newVault.name"
          placeholder="Enter Vault Name"
        />
      </div>
      <div class="col-6">
        <input
          class="form-control"
          type="text"
          v-model="newVault.description"
          placeholder="Enter Vault Description"
        />
      </div>
      <div class="col-12 mt-3" style="text-align:center;">
        <button type="button" class="btn btn-primary mb-2" @click="postVault">Create Vault</button>
      </div>
    </form>
    <div class="row justify-content-center my-2 py-2">
      <div v-if="userVaults == ''" class="col-12">No Vaults yet.</div>
      <div
        v-else
        class="col-3 border rounded bg-light text-dark"
        style="text-align:center;"
        v-for="vault in userVaults"
        :key="vault.id"
      >
        <router-link :to="{name: 'vault', params: {vaultId: vault.id}}">
          <h5 class>
            {{vault.name}}
            <br />
            <span>{{vault.description}}</span>
          </h5>
        </router-link>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  name: "vaults",
  data() {
    return {
      show: false,
      newVault: {},
    };
  },
  computed: {
    userVaults() {
      return this.$store.state.userVaults;
    },
  },
  methods: {
    postVault() {
      this.$store.dispatch("addVault", this.newVault);
    },
  },
  beforeDestroy() {
    this.$store.state.userVaults = [];
  },
  mounted() {
    this.$store.dispatch("getUserVaults");
    this.show = true;
  },
  components: {},
};
</script>


<style scoped>
</style>