<template>
  <div class="dashboard container-fluid" v-if="show">
    <div class="row justify-content-center my-2 py-2">
      <div class="col-3" v-for="vault in userVaults" :key="vault.id">
        <router-link :to="{name: 'vault', params: {vaultId: vault.id}}">
              <h5 class="card-title">{{vault.name}}</h5>
              <p class="card-text">{{vault.description}}</p>
        </router-link>
      </div>
    </div>

    <form class="row py-2 bg-dark justify-content-center border">
      <div class="col-4">
        <input
          type="text"
          class="form-control"
          v-model="newVault.name"
          placeholder="Enter Vault Name"
        />
      </div>
      <div class="col-4">
        <input
          class="form-control"
          type="text"
          v-model="newVault.description"
          placeholder="Enter Vault Description"
        />
      </div>
      <div class="col-4">
        <button type="button" class="btn btn-primary mb-2" @click="postVault">Vault It</button>
      </div>
    </form>

    <form class="row justify-content-center bg-dark border my-2 py-2">
      <div class="col-6">
        <input type="text" class="form-control" v-model="newKeep.name" placeholder="Name" />
      </div>
      <div class="col-6">
        <input type="text" class="form-control" v-model="newKeep.img" placeholder="Image URL" />
      </div>
      <div class="col-6">
        <label for="description">DESCRIPTION</label>
        <textarea class="form-control" id="description" rows="2" v-model="newKeep.description"></textarea>
      </div>
      <div class="col-12">
        <span class="mx-3" @click="newKeep.isPrivate = !newKeep.isPrivate">
          <label class="mx-2">Private: {{newKeep.isPrivate}}</label>
          <i class="bg-info fa fa-toggle-on fa-3x" v-if="newKeep.isPrivate"></i>
          <i class="bg-info fa fa-toggle-off fa-3x" v-else></i>
        </span>
        <button type="button" class="btn btn-primary mb-2" @click="postKeep">Keep It</button>
      </div>
    </form>
  </div>
</template>

<script>
export default {
  data() {
    return {
      show: false,
      newKeep: {
        isPrivate: false,
      },
      newVault: {},
    };
  },
  methods: {
    postKeep() {
      this.newKeep.isPrivate = this.newKeep.isPrivate ? 1 : 0;
      this.$store.dispatch("addKeep", this.newKeep);
      this.newKeep = {};
    },
    postVault() {
      this.$store.dispatch("addVault", this.newVault);
    },
  },
  mounted() {
    this.$store.dispatch("getUserVaults");
    this.show = true;
  },
  computed: {
    userVaults() {
      return this.$store.state.userVaults;
    },
  },
};
</script>

<style></style>
