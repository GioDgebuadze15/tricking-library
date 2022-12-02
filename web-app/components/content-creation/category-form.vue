<template>
  <v-card>
    <v-card-title>Create Category</v-card-title>
    <v-card-text>
      <v-text-field v-model="form.name" label="Name"/>
      <v-text-field v-model="form.description" label="Description"/>
    </v-card-text>
    <v-card-actions>
      <v-btn @click="save">Save</v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
import {useRuntimeConfig} from "nuxt/app";

const initState = () => ({
  form: {
    name: "",
    description: "",
  },
})

export default {
  name: "category-form",
  data: initState,
  methods: {
    save() {
      const config = useRuntimeConfig()
      $fetch(config.public.apiBase + "/api/categories",{method:'POST', body: this.form} )
    }
  } 
}
</script>

<style scoped>

</style>