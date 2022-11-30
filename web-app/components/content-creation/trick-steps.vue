<template>
  <v-card height="400px">
    <div class="d-flex flex-row align-center ml-3">
      <div>
        <v-chip>{{ step }}</v-chip>
      </div>
      <div>
        <v-card-title v-if="step===1">Trick Information</v-card-title>

        <v-card-title v-if="step===2">Review</v-card-title>
      </div>
    </div>
    <v-divider/>


    <v-card-item v-if="step===1">
      <div>
        <v-text-field v-model="form.name" label="Tricking Name"/>
        <v-btn @click="step++">Next</v-btn>
      </div>
    </v-card-item>
    <v-card-item v-if="step===2">
      <div>
        <v-btn @click="save">Save</v-btn>
      </div>
    </v-card-item>
  </v-card>
</template>

<script>

import {useTricksStore} from "../../stores/tricks";

const initState = () => ({
  step: 1,
  form: {
    name: "",
  },
})

export default {
  name: "trick-steps",
  setup() {
    const tricksStore = useTricksStore()

    return {
      tricksStore,
    }
  },
  data: initState,
  watch:{
    'active': function (newValue){
      if(!newValue){
        Object.assign(this.$data, initState())
      }
    }
  },
  methods: {
    async save() {
      await this.tricksStore.createTrick({form: this.form});
      // todo close dialog & reset component state
      this.tricksStore.$reset()
      Object.assign(this.$data, initState())
    },
  }
}
</script>

<style scoped>

</style>