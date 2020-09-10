<template>
  <div class="vault container-fluid" v-if="vault.name">
    <div class="row justify-content-center">
      <div class="col-12" style="text-align:center;">
        <h1>{{vault.name}}</h1>
        <h5>{{vault.description}}</h5>
      </div>
    </div>

    <div class="row">
      <div class="col-3 invisible my-3 shadow" v-for="mk in keeps" :key="keyId()">
        <div
          class="bg-light text-dark visible card text-light rounded"
          style="box-shadow: black 12px 12px 12px 4px;"
        >
          <img :src="mk.img" class="card-img-top img-fluid" style="height:33vh" />
          <div class="row justify-content-center text-center mt-1 py-1">
            <div class="col-4">
              <svg
                width="1.9rem"
                height="1.9rem"
                viewBox="0 0 16 16"
                class="bi bi-eye"
                fill="blue"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path
                  fill-rule="evenodd"
                  d="M16 8s-3-5.5-8-5.5S0 8 0 8s3 5.5 8 5.5S16 8 16 8zM1.173 8a13.134 13.134 0 0 0 1.66 2.043C4.12 11.332 5.88 12.5 8 12.5c2.12 0 3.879-1.168 5.168-2.457A13.134 13.134 0 0 0 14.828 8a13.133 13.133 0 0 0-1.66-2.043C11.879 4.668 10.119 3.5 8 3.5c-2.12 0-3.879 1.168-5.168 2.457A13.133 13.133 0 0 0 1.172 8z"
                />
                <path
                  fill-rule="evenodd"
                  d="M8 5.5a2.5 2.5 0 1 0 0 5 2.5 2.5 0 0 0 0-5zM4.5 8a3.5 3.5 0 1 1 7 0 3.5 3.5 0 0 1-7 0z"
                />
              </svg>
              <span class="d-block block">{{mk.views}}</span>
            </div>
            <div class="col-4">
              <svg
                width="1.9rem"
                height="1.9rem"
                viewBox="0 0 16 16"
                class="bi bi-archive"
                fill="blue"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path
                  fill-rule="evenodd"
                  d="M0 2a1 1 0 0 1 1-1h14a1 1 0 0 1 1 1v2a1 1 0 0 1-1 1v7.5a2.5 2.5 0 0 1-2.5 2.5h-9A2.5 2.5 0 0 1 1 12.5V5a1 1 0 0 1-1-1V2zm2 3v7.5A1.5 1.5 0 0 0 3.5 14h9a1.5 1.5 0 0 0 1.5-1.5V5H2zm13-3H1v2h14V2zM5 7.5a.5.5 0 0 1 .5-.5h5a.5.5 0 0 1 0 1h-5a.5.5 0 0 1-.5-.5z"
                />
              </svg>
              <span class="d-block block">{{mk.keeps}}</span>
            </div>
            <div class="col-4">
              <svg
                width="1.9rem"
                height="1.9rem"
                viewBox="0 0 16 16"
                class="bi bi-share"
                fill="blue"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path
                  fill-rule="evenodd"
                  d="M13.5 1a1.5 1.5 0 1 0 0 3 1.5 1.5 0 0 0 0-3zM11 2.5a2.5 2.5 0 1 1 .603 1.628l-6.718 3.12a2.499 2.499 0 0 1 0 1.504l6.718 3.12a2.5 2.5 0 1 1-.488.876l-6.718-3.12a2.5 2.5 0 1 1 0-3.256l6.718-3.12A2.5 2.5 0 0 1 11 2.5zm-8.5 4a1.5 1.5 0 1 0 0 3 1.5 1.5 0 0 0 0-3zm11 5.5a1.5 1.5 0 1 0 0 3 1.5 1.5 0 0 0 0-3z"
                />
              </svg>
              <span class="d-block block">{{mk.shares}}</span>
            </div>
          </div>
          <div class="card-body">
            <h5 class="card-title">{{mk.name}}</h5>
            <p class="card-text">{{mk.description}}.</p>
            <button
              class="btn btn-warning btn-block my-2"
              @click="removeFromVault(mk.vaultKeepId)"
            >Remove From Vault</button>
            <div class="dropdown">
              <button
                class="btn btn-secondary btn-block dropdown-toggle text-dark"
                type="button"
                :id="'keep-'+ mk.id"
                data-toggle="dropdown"
                aria-haspopup="true"
                aria-expanded="false"
              >Add To Other Vault</button>
              <div
                class="dropdown-menu bg-info"
                :aria-labelledby="'keep-'+ mk.id"
                style="width: -webkit-fill-available;"
              >
                <p class="bg-light text-dark" style="text-align: center;">--- Your Vaults ---</p>
                <h6
                  class="dropdown-item bg-dark text-light rounded"
                  v-for="vault in userVaults"
                  :key="keyId()"
                  @click="addToVault(vault.id,mk.id)"
                >{{vault.name}}</h6>
              </div>
            </div>
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
    userVaults() {
      return this.$store.state.userVaults;
    },
    addToVault(vaultId, keepId) {
      let newVaultKeep = {
        VaultId: vaultId,
        KeepId: keepId,
      };
      this.$store.dispatch("addToVault", newVaultKeep);
    },
  },
  mounted() {
    this.$store.dispatch("getActiveVault", this.$route.params.vaultId);
    this.$store.dispatch("getVaultKeeps", this.$route.params.vaultId);
    this.$store.dispatch("getUserVaults");
  },
  methods: {
    keyId() {
      return "id" + Math.random().toString(36).substr(2, 16);
    },
    removeFromVault(vkId) {
      let vaultId = this.$route.params.vaultId
      console.log(vaultId);
      this.$store.dispatch("removeFromVault", vkId,vaultId);
    },
  },
  components: {},
  beforeDestroy() {
    this.$store.state.vaultKeeps = [];
    this.$store.state.activeVault = {};
  },
};
</script>


<style scoped>
.dropdown-toggle::after {
  margin-left: 25%;
}
</style>