import Vue from "vue";
import Vuex from "vuex";
import Axios from "axios";
import router from "../router";

Vue.use(Vuex);

let baseUrl = location.host.includes("localhost")
  ? "https://localhost:5001/"
  : "/";

let api = Axios.create({
  baseURL: baseUrl + "api/",
  timeout: 3000,
  withCredentials: true
});

export default new Vuex.Store({
  state: {
    publicKeeps: [],
    userKeeps: [],
    userVaults: [],
    vaultKeeps: [],
    activeVault: {},
  },
  mutations: {
    setPublicKeeps(state, publicKeeps) {
      state.publicKeeps = publicKeeps;
    },
    setUserKeeps(state, userKeeps) {
      state.userKeeps = userKeeps;
    },
    addMyKeep(state, keep) {
      state.userKeeps.unshift(keep)
    },
    updatePublicKeep(state,keep){
      let keepIndex = state.publicKeeps.findIndex(k=>{
        return k.id == keep.id
      })
      state.publicKeeps[keepIndex] = keep;
    },
    setUserVaults(state, userVaults) {
      state.userVaults = userVaults;
    },
    addUserVault(state, userVault) {
      state.userVaults.unshift(userVault)
    },
    setVaultKeeps(state, keeps) {
      state.vaultKeeps = keeps;
    },
    setActiveVault(state, vault) {
      state.activeVault = vault;
    },
  },
  actions: {
    setBearer({ }, bearer) {
      api.defaults.headers.authorization = bearer;
    },
    resetBearer() {
      api.defaults.headers.authorization = "";
    },
    getPublicKeeps({ commit }) {
      try {
        api.get('keeps')
          .then(res => {
            commit('setPublicKeeps', res.data)
          })
      } catch (error) {
        console.error(error);
      }
    },
    getUserKeeps({ commit }) {
      try {
        api.get('keeps/user')
          .then(res => {
            commit('setUserKeeps', res.data)
          })
      } catch (error) {
        console.error(error);
      }
    },
    addKeep({ commit }, KeepData) {
      try {
        api.post('keeps', KeepData)
          .then(res => {
            commit("addMyKeep");
          })
      } catch (error) {
        console.error(error)
      }
    },
    viewKeep({ commit }, KeepData) {
      try {
        api.put('keeps/' + KeepData.id, KeepData)
          .then(res => {
            commit("updatePublicKeep",res.data);
            return res.data;
          })
      } catch (error) {
        console.error(error)
      }
    },
    //#region VAULTS ---------
    getUserVaults({ commit }) {
      try {
        api.get('vaults')
          .then(res => {
            commit('setUserVaults', res.data)
          })
      } catch (error) {
        console.error(error);
      }
    },
    addVault({ commit }, vault) {
      try {
        api.post('vaults', vault)
          .then(res => {
            commit('addUserVault', res.data)
          })
      } catch (error) {
        console.error(error);
      }
    },
    getActiveVault({ commit }, vaultId) {
      try {
        api.get('vaults/' + vaultId)
          .then(res => {
            commit("setActiveVault", res.data)
            console.log("ACTIVE VAULT", res.data);
          })
      } catch (error) {
        console.error(error);
      }
    },
    //#endregion --- VAULTS -----

    //#region -- VAULT KEEPS ----

    getVaultKeeps({ commit }, vaultId) {
      try {
        api.get('vaultkeeps/' + vaultId)
          .then(res => {
            commit('setVaultKeeps', res.data)
            console.log(res.data);
          })
      } catch (error) {
        console.error(error);
      }
    },
    addToVault({ commit }, vk) {
      try {
        api.post('vaultkeeps', vk)
          .then(res => {
            commit('addUserVault', res.data)
            console.log(res.data);
          })
      } catch (error) {
        console.error(error);
      }
    },
    removeFromVault({ commit }, vkId) {
      try {
        api.delete('vaultkeeps/' + vkId)
          .then(res => {
            // commit('removeFromActiveVault', vkId)
            console.log(res.data);
          })
      } catch (error) {
        console.error(error);
      }
    },
    //#endregion----VAULT_KEEPS ---


  }
});
