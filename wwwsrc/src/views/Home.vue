<template>
  <div class="home bg-dark">
    <div class="container-fluid">
      <div class="row">
        <div class="col-3" v-for="pk in publicKeeps" :key="pk.id">
          <div class="card bg-info text-light">
            <img :src="pk.img" class="card-img-top img-fluid" alt />
            <div class="card-body">
              <h5 class="card-title">{{pk.name}}</h5>
              <p class="card-text">{{pk.description}}.</p>
              <!--  -->
              <button
                class="rounded btn btn-info py-1"
                data-toggle="collapse"
                :data-target="'#keep-' + pk.id"
              >Add To Vault</button>
              <!-- add task Dropdown -->
              <div :id="'keep-' + pk.id" class="bg-secondary collapse">
                <button class="btn btn-primary" @click="addToVault(pk.id)">Add</button>
              </div>

              <div class="dropdown">
                <button
                  class="btn btn-secondary dropdown-toggle"
                  type="button"
                  id="'keep-'+ pk.id"
                  data-toggle="dropdown"
                  aria-haspopup="true"
                  aria-expanded="false"
                >Dropdown button</button>
                <div class="dropdown-menu" aria-labelledby="'keep-'+ pk.id">
                  <p
                    class="dropdown-item bg-info rounded"
                    v-for="vault in userVaults"
                    :key="vault.id"
                    @click="addToVault(vault.id,pk.id)"
                  >{{vault.name}}</p>
                </div>
              </div>

              <!--  -->
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "home",
  data() {
    return {
      show: false,
    };
  },
  computed: {
    user() {
      return this.$store.state.user;
    },
    publicKeeps() {
      return this.$store.state.publicKeeps;
    },
    userVaults() {
      return this.$store.state.userVaults;
    },
  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
    },
    addToVault(vaultId, keepId) {
      console.log(keepId);
      let newVaultKeep = {
        vaultId: vaultId,
        keepId: keepId,
      };
      this.$store.dispatch("addToVault", newVaultKeep);
      $("#keep-" + keepId).collapse("toggle");
    },
  },
  mounted() {
    this.$store.dispatch("getPublicKeeps");
    if (this.$auth.isAuthenticated) {
      this.$store.dispatch("getUserVaults");
    }
    this.show = true;
  },
};
</script>