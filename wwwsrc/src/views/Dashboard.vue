<template>
  <div class="dashboard container-fluid bg-info" v-if="show">
    <!-- public {{ publicKeeps }} user {{ userKeeps }} -->
    <div class="row justify-content-center">
      <div class="col-3"  v-for="vault in userVaults" :key="vault.id">{{vault.name}} | {{vault.description}}</div>
    </div>

        <form class="row justify-content-center">
      <div class="col-4">
        <input type="text" class="form-control" v-model="newVault.name" placeholder="Enter Vault Name" />
      </div>
      <div class="col-4">
        <input class="form-control" type="text" v-model="newVault.description" placeholder="Enter Vault Description" />
      </div>
      <div class="col-4">
        <button type="button" class="btn btn-primary mb-2" @click="postVault">Vault It</button>
      </div>
    </form>
    <!-- <form class="row justify-content-center">
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
        <span class="mx-3" @click="newKeep.makePublic = !newKeep.makePublic">
          <label class="mx-2" for>Make Public {{newKeep.makePublic}}</label>
          <i class="bg-info fa fa-toggle-on fa-3x" style v-if="newKeep.makePublic"></i>
          <i class="bg-info fa fa-toggle-off fa-3x" style v-else></i>
        </span>
        <button type="button" class="btn btn-primary mb-2" @click="postKeep">Keep It</button>
      </div>
    </form> -->
  </div>
</template>

<script>
export default {
  data() {
    return {
      show: false,
      newKeep: {
        makePublic: false,
      },
      newVault:{},
    };
  },
  methods: {
    postKeep() {
      this.$store.dispatch("addKeep", this.newKeep);
    },
    postVault(){
      this.$store.dispatch("addVault",this.newVault);
    },
  },
  mounted() {
    // this.$store.dispatch("getUserKeeps");
    this.$store.dispatch("getUserVaults");
    this.show = true;
  },
  computed: {
    userKeeps() {
      return this.$store.state.userKeeps;
    },
    userVaults() {
      return this.$store.state.userVaults;
    },
  },
};
</script>

<style></style>
