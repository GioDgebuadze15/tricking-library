<template>
  <div>
    <div class="ml-4">
      <v-switch @click="toggleTheme">
      </v-switch>
    </div>
    <div class="ml-4">
      {{message}}
    </div>
  </div>
</template>

<script>
import {useTheme} from 'vuetify'
import Axios from "axios"
import {useIndexStore} from "../stores/index";
import {mapState,mapActions} from 'pinia'

export default {
  name: "index",
  setup() {
    const theme = useTheme()
    return {
      theme,
      toggleTheme: () => theme.global.name.value = theme.global.current.value.dark ? 'light' : 'dark'
      // this.icon  =(this.icon ? 'mdi-weather-night' :'mdi-white-balance-sunny')}

    }
  },
  computed:{
    ...mapState(useIndexStore,["message"])
  },
  methods:{
    ...mapActions(useIndexStore,["fetchMessage"])
  }
}
</script>
<style scoped>

</style>