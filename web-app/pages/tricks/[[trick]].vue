<template>
  <div class="d-flex justify-center align-start">
    <div v-if="submissionsStore.submissions" class="mx-2">
      <div v-for="submission in submissionsStore.submissions">
        {{ submission.id }} - {{ submission.description }} - {{ submission.trickId }}
        <div>
          <video :src="`https://localhost:7146/api/videos/${submission.video}`" controls width="400"/>
        </div>
      </div>
    </div>

    <div class="mx-2 sticky">
      <v-sheet class="pa-3 mt-2">
        <div class="text-h6">Trick: {{ trick === undefined ? "Undefined" : trick }}</div>
        <v-divider class="my-1"/>
        <div class="text-body-2">Trick: {{ trick === undefined ? "Undefined" : trick }}</div>
        <div class="text-body-2">Trick: {{ trick === undefined ? "Undefined" : trick }}</div>
        <v-divider class="my-1"/>

        <div v-for="rd in relatedData" v-if="rd.data.length >0">
          <div class="text-subtitle-1">{{ rd.title }}</div>
          <v-chip-group v-if="trickCategories">
            <v-chip v-for="d in rd.data" :key="rd.idFactory(d)">
              {{ d.name }}
            </v-chip>
          </v-chip-group>
        </div>
      </v-sheet>
    </div>
  </div>
</template>

<script>
import {useSubmissionsStore} from "../../stores/submissions";
import {useRoute} from "nuxt/app";
import {useTricksStore} from "../../stores/tricks";

export default {
  setup() {
    const submissionsStore = useSubmissionsStore()
    const tricksStore = useTricksStore()
    tricksStore.fetchTricks()

    return {
      submissionsStore,
      tricksStore
    }
  },
  data: () => ({
    trickName: ""
  }),
  computed: {
    trick() {
      const route = useRoute();
      const trickId = route.params.trick
      this.trickName = this.tricksStore.trickById(trickId)
      // return this.tricksStore.trickById(trickId).name
      // console.log(this.trickName)
    },
    relatedData() {
      if (this.tricksStore.categories.length !== 0 && this.trick !== undefined) {
        return [
          {
            title: "Categories",
            data: this.tricksStore.categories.filter(x => this.trick.categories.indexOf(x.id) >= 0),
            idFactory: c => `category-${c.id}`
          },
          {
            title: "Prerequisites",
            data: this.trick.filter(x => this.trick.prerequisites.indexOf(x.id) >= 0),
            idFactory: t => `trick-${t.id}`
          },
          {
            title: "Progressions",
            data: this.trick.filter(x => this.trick.progressions.indexOf(x.id) >= 0),
            idFactory: t => `trick-${t.id}`
          },
        ]
      }
    },
    trickCategories() {
      if (this.tricksStore.categories.length !== 0 && this.trick !== undefined)
        return this.tricksStore.categories.filter(x => this.trick.categories.indexOf(x.id) >= 0)
    }
  },
  async asyncData() {
    console.log("reached here")
    const route = useRoute();
    const trickId = route.params.trick

    await this.submissionsStore.fetchSubmissionsForTrick({trickId})
  }
}
</script>

<style scoped>
.sticky {
  position: -webkit-sticky;
  position: sticky;
  top: 64px;
  color: red;
}
</style>