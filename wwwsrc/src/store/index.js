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
  },
  mutations: {
    setPublicKeeps(state, publicKeeps) {
      state.publicKeeps = publicKeeps;
    },
    setUserKeeps(state, userKeeps) {
      state.userKeeps = userKeeps;
    },
    addMyKeep(state,keep){
      state.userKeeps.unshift(keep)
    },
    setUserVaults(state, userVaults) {
      state.userVaults = userVaults;
    },
    addUserVault(state, userVault) {
      state.userVaults.unshift(userVault)
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
            console.log(res.data);
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
//#region VAULTS ---------
    getUserVaults({ commit }) {
      try {
        api.get('vaults')
          .then(res => {
            commit('setUserVaults', res.data)
            console.log(res.data);
          })
      } catch (error) {
        console.error(error);
      }
    },
    addVault({ commit },vault) {
      try {
        api.post('vaults',vault)
          .then(res => {
            commit('addUserVault', res.data)
            console.log(res.data);
          })
      } catch (error) {
        console.error(error);
      }
    },
    //#endregion --- VAULTS -----

    //#region 

    addToVault({ commit },vk) {
      try {
        api.post('vaultkeeps',vk)
          .then(res => {
            commit('addUserVault', res.data)
            console.log(res.data);
          })
      } catch (error) {
        console.error(error);
      }
    },
    //#endregion----VAULT_KEEPS ---


  }
});
