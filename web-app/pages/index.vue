<template>
  <v-app>
    <v-layout>
      <v-app-bar>
        <v-switch @click="toggleTheme"/>
      </v-app-bar>
      <v-main>
        <v-file-input accept="video/*" @change="handleFile">

        </v-file-input>
        <div>{{ indexStore.getMessage }}</div>
        <v-btn @click="fetchMessage()">
          fetch
        </v-btn>
        <div v-if="tricksStore.tricks">
          <p v-for="t in tricksStore.tricks">
            {{ t.name }}
          </p>
        </div>
        <div>
          <v-text-field v-model="trickName" label="Tricking Name"/>
          <v-btn @click="saveTrick()">Save Trick</v-btn>
        </div>
        <v-btn @click="resetState()">
          Reset Message
        </v-btn>
        <v-btn @click="resetTricks()">
          Reset Tricks
        </v-btn>
      </v-main>
    </v-layout>
  </v-app>
</template>

<script>
import {useTheme} from 'vuetify'
import {useIndexStore} from "../stores/index";
import {useTricksStore} from "../stores/tricks";
import Axios from "axios";

export default {
  name: "index",
  setup() {
    const theme = useTheme()
    const indexStore = useIndexStore()
    const tricksStore = useTricksStore()

    return {
      theme,
      indexStore,
      tricksStore,
      toggleTheme: () => theme.global.name.value = theme.global.current.value.dark ? 'light' : 'dark'
      // this.icon  =(this.icon ? 'mdi-weather-night' :'mdi-white-balance-sunny')}

    }
  },
  data: () => ({
    trickName: ""
  }),
  computed: {
    // getMessage(){
    //   return this.getMessage();
    // }
  },
  methods: {
    fetchMessage() {
      this.indexStore.fetchMessage();
    },
    resetState() {
      this.indexStore.$reset();
    },
    async saveTrick() {
      await this.tricksStore.createTrick({trick: {name: this.trickName}});
      this.trickName = ""
    },
    resetTricks() {
      this.tricksStore.$reset();
    },
    async handleFile(file) {
      if (!file) return;

      const form = new FormData();
      form.append("video", file)
      
      const result = await Axios.post("https://localhost:7146/api/videos", form);
      // console.log(result)
    }
  }
}
</script>
<style scoped>

</style>