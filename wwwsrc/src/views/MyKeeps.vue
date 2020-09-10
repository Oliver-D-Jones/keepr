<template>
  <div class="mykeeps container-fluid">
    <div class="row justify-content-center pt-3" v-if="show && userKeeps ==''">
      <div class="col-12" style="text-align:center;">
        <h4 class v-if="$auth.isAuthenticated" :class="{ active: $route.name == 'dashboard' }">
          <router-link
            class="bg-dark text-danger p-2 rounded"
            :to="{ name: 'dashboard' }"
          >No Keeps Yet. Click Here To Start Making Some Keeps!</router-link>
        </h4>
      </div>
    </div>

    <div class="row justify-content-center pt-3" v-else-if="show">
      <div class="col-sm-12 col-md-3 invisible my-3 shadow" v-for="mk in userKeeps" :key="mk.id">
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
            <div v-if="mk.isPrivate">
              <button class="btn btn-warning btn-block" @click="deleteKeep(mk.id)">Delete Keep</button>
              <button class="btn btn-danger btn-block" @click="makePublic(mk.id)">Make Public</button>
            </div>
            <div class="btn-group dropup mt-2" style="width: -webkit-fill-available;">
              <button
                class="btn btn-secondary btn-block dropdown-toggle text-dark"
                type="button"
                :id="'keep-'+ mk.id"
                data-toggle="dropdown"
                aria-haspopup="true"
                aria-expanded="false"
              >Add To Vault</button>
              <div
                class="dropdown-menu bg-info"
                :aria-labelledby="'keep-'+ mk.id"
                style="width: -webkit-fill-available;text-align:center;font-weight:bolder;"
              >
                <p>--- Your Vaults ---</p>
                <h6
                  class="dropdown-item bg-dark text-light rounded"
                  v-for="vault in userVaults"
                  :key="vault.id"
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
  name: "mykeeps",
  data() {
    return {
      show: false,
    };
  },
  computed: {
    userKeeps() {
      return this.$store.state.userKeeps;
    },
    userVaults() {
      return this.$store.state.userVaults;
    },
  },
  methods: {
    makePublic(keepId) {
      let updatedKeep = {
        keepId: keepId,
        isPrivate: false,
      };
      this.$store.dispatch("updateKeep", updatedKeep);
    },
    deleteKeep(keepId) {
      this.$store.dispatch("deleteKeep", keepId);
    },
    addToVault(vaultId, keepId) {
      let newVaultKeep = {
        vaultId: vaultId,
        keepId: keepId,
      };
      this.$store.dispatch("addToVault", newVaultKeep);

      $("#keep-" + keepId).collapse("toggle");
    },
  },
  mounted() {
    this.$store.dispatch("getUserKeeps");
    this.$store.dispatch("getUserVaults");
    this.show = true;
  },
  components: {},
};
</script>


<style scoped>
</style>