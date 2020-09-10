<template>
  <div class="dashboard container" v-if="show">
    <div class="row mt-3">
      <div class="col-12" style="text-align: center;" v-if="$auth.isAuthenticated">
        <router-link
          class="d-inline nav-link mr-1"
          style="max-width:3rem"
          :to="{ name: 'mykeeps' }"
        >
          <img
            src="../assets/keep_icon.png"
            alt
            class="img-fluid border rounded"
            style="max-width: inherit;"
          />
          <span style="font-size: x-large;">eeps</span>
        </router-link>
        <router-link class="d-inline nav-link ml-1" style="max-width:3rem" :to="{ name: 'vaults' }">
          <img
            src="../assets/vault_icon.png"
            alt
            class="img-fluid border rounded"
            style="max-width: inherit;"
          />
          <span style="font-size: x-large;">aults</span>
        </router-link>
      </div>
    </div>

    <form
      class="row justify-content-center bg-secondary my-3 py-2"
      style="border: ridge .3rem blue;border-radius:1%"
    >
      <div class="col-12" style="text-align:center;">
        <h3>Create New Keep</h3>
      </div>
      <div class="col-12 mb-1">
        <input type="text" class="form-control" v-model="newKeep.name" placeholder="Name" />
      </div>
      <div class="col-12 mb-1">
        <input type="text" class="form-control" v-model="newKeep.img" placeholder="Image URL" />
      </div>
      <div class="col-12 mb-1">
        <textarea
          class="form-control"
          id="description"
          rows="2"
          v-model="newKeep.description"
          placeholder="Description"
        ></textarea>
      </div>
      <div class="col-12" style="text-align: center;">
        <div class @click="newKeep.isPrivate = !newKeep.isPrivate">
          <h5 class v-if="newKeep.isPrivate">Private</h5>
          <h5 class v-else-if="!newKeep.isPrivate">Public</h5>
          <i class="fa fa-toggle-on fa-3x" v-if="newKeep.isPrivate"></i>
          <i class="fa fa-toggle-off fa-3x" v-else></i>
        </div>
        <button type="button" class="btn btn-primary my-2" @click="postKeep">Keep It</button>
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
    };
  },
  methods: {
    postKeep() {
      this.$store.dispatch("addKeep", this.newKeep);
      this.newKeep = { isPrivate: false };
    },
  },
  mounted() {
    this.show = true;
  },
  computed: {},
};
</script>

<style scoped>
</style>
