<template>
<!--      <div v-if="tricksStore.tricks">-->
<!--        <div v-for="trick in tricksStore.tricks">-->
<!--          {{ trick.name }}-->
<!--          <div>-->
<!--            <video width="400" controls :src="`https://localhost:7146/api/videos/${trick.video}`"/>-->
<!--          </div>-->
<!--        </div>-->
<!--      </div>-->
</template>

<script>
// const data = await useFetch('https://localhost:7146/api/tricks')
// const data = await useLazyAsyncData('trick',()=> $fetch('https://localhost:7146/api/tricks'))
// console.log(data)
// export default ({
//   async setup() {
//     const config = useRuntimeConfig();
//
//     const [{ data: tricks } ] = await Promise.all([
//       useFetch(config.public.apiBase+'/api/tricks'),
//     ])
//     console.log(tricks)
//     return {
//       tricks,
//     }
//   }
// })
// export default defineNuxtComponent({
//   fetchKey: 'tricks',
//   async asyncData () {
//     const config = useRuntimeConfig()
//     return {
//       tricks: await $fetch(config.public.apiBase + "/api/submissions", {method: 'GET', body: null})
//     }
//   }
// })
// export default {
//   name: "index",
//   setup() {
//     const theme = useTheme()
//     const tricksStore = useTricksStore()
//     return {
//       theme,
//       tricksStore,
//       toggleTheme: () => theme.global.name.value = theme.global.current.value.dark ? 'light' : 'dark'
//       // this.icon  =(this.icon ? 'mdi-weather-night' :'mdi-white-balance-sunny')}
//
//     }
//   },
// }
</script>
<style scoped>

</style>