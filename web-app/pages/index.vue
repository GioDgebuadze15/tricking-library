<template>
  <v-app>
    <v-app-bar :elevation="8">
      <v-app-bar-title>Tricking Library</v-app-bar-title>
      <v-spacer></v-spacer>
      <v-menu left bottom>
        <template v-slot:activator>
          <v-switch @click="toggleTheme"/>
        </template>
      </v-menu>


    <video-upload/>
    </v-app-bar>

    <v-main class="d-flex flex-column mt-5">
      <div v-if="tricksStore.tricks">
        <div v-for="trick in tricksStore.tricks">
          {{ trick.name }}
          <div>
            <video width="400" controls :src="`https://localhost:7146/api/videos/${trick.video}`"/>
          </div>
        </div>
      </div>

    </v-main>
  </v-app>
</template>

<script>
import {useTheme} from 'vuetify'
import VideoUpload from "../components/video-upload";
import {useVideosStore} from "../stores/video-upload";
import {useTricksStore} from "../stores/tricks";

export default {
  name: "index",
  setup() {
    const theme = useTheme()
    const videosStore = useVideosStore()
    const tricksStore = useTricksStore()
    return {
      theme,
      videosStore,
      tricksStore,
      toggleTheme: () => theme.global.name.value = theme.global.current.value.dark ? 'light' : 'dark'
      // this.icon  =(this.icon ? 'mdi-weather-night' :'mdi-white-balance-sunny')}

    }
  },
  components: {
    VideoUpload
  },
  computed:{
  }
}
</script>
<style scoped>

</style>