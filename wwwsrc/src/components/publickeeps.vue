<template>
  <div class="publickeeps row">
    <div class="col-3 invisible my-3 shadow" v-for="pk in publicKeeps" :key="pk.id">
      <div
        class="bg-light text-dark visible card text-light rounded"
        style="box-shadow: black 12px 12px 12px 4px;"
      >
        <div class="text-dark" @click="viewKeep(pk)">
          <p class="bg-warning" style="text-align:center;">{{pk.name}}</p>
          <img :src="pk.img" class="card-img-top img-fluid" style="height:33vh" />
        </div>
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
            <span class="d-block block">{{pk.views}}</span>
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
            <span class="d-block block">{{pk.keeps}}</span>
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
            <span class="d-block block">{{pk.shares}}</span>
          </div>
        </div>
        <div class="card-body" v-if="viewed == pk.id">
          <h5 class="card-title">{{pk.name}}</h5>
          <p class="card-text">{{pk.description}}.</p>
          <div class="dropdown">
            <button
              class="btn btn-info btn-block dropdown-toggle text-dark"
              type="button"
              :id="'vault-'+ pk.id"
              data-toggle="dropdown"
              aria-haspopup="true"
              aria-expanded="false"
            >Add As Keep</button>
            <div
              class="dropdown-menu bg-info"
              :aria-labelledby="'vault-'+ pk.id"
              style="width: -webkit-fill-available;"
            >
              <p class="bg-light text-dark" style="text-align: center;font-weight: bolder;">--- Your Vaults ---</p>
              <h6
                class="dropdown-item bg-dark text-light rounded"
                v-for="vault in userVaults"
                :key="vault.id"
                @click="addToVault(vault.id,pk.id)"
              >{{vault.name}}</h6>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { onAuth } from "@bcwdev/auth0-vue";
export default {
  name: "publickeeps",
  data() {
    return {
      viewed: false,
    };
  },
  computed: {
    publicKeeps() {
      return this.$store.state.publicKeeps;
    },
    userVaults() {
      return this.$store.state.userVaults;
    },
  },
  methods: {
    addToVault(vaultId, keepId) {
      let newVaultKeep = {
        VaultId: vaultId,
        KeepId: keepId,
      };

      this.$store.dispatch("addToVault", newVaultKeep);
    },
    viewKeep(keep) {
      this.viewed = this.viewed == keep.id ? false : keep.id;
      console.log(this.viewed);
      if (this.viewed) {
        let updatedKeep = { id: keep.id, views: keep.views + 1 };
        this.$store.dispatch("updateKeep", updatedKeep);
      }
    },
  },
  async created() {
    await onAuth();
    if (this.$auth.isAuthenticated) {
      console.log(this.$auth.user);
      this.$store.dispatch("getUserVaults");
    }
  },
  mounted() {
    this.$store.dispatch("getPublicKeeps");
  },
  components: {},
};
</script>


<style scoped>
.dropdown-toggle::after {
  margin-left: 25%;
}

</style>
