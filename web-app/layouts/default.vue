<template>
  <v-app>
    <v-app-bar elevation="8">
      <v-app-bar-title>Tricking Library</v-app-bar-title>
      <v-spacer></v-spacer>
      <v-switch @click="toggleTheme"/>


      <content-creation-dialog/>
    </v-app-bar>

    <v-main>
      <slot/>
    </v-main>
  </v-app>
</template>

<script>
import {useTheme} from "vuetify";
import ContentCreationDialog from "../components/content-creation/content-creation-dialog";

export default {
  name: "default",
  setup() {
    const theme = useTheme()
    return {
      theme,
      toggleTheme: () => theme.global.name.value = theme.global.current.value.dark ? 'light' : 'dark'
      // this.icon  =(this.icon ? 'mdi-weather-night' :'mdi-white-balance-sunny')}

    }
  },
  components: {
    ContentCreationDialog
  }
}
</script>

<style scoped>

</style>