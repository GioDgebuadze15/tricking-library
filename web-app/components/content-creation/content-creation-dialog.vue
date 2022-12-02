<template>
  <v-dialog v-model="videosStore.active" persistent width="500">
    <template v-slot:activator>

      <v-menu
          open-on-hover
      >
        <template v-slot:activator="{ props }">
          <v-btn
              depressed
              v-bind="props"
          >
            Create
          </v-btn>
        </template>

        <v-list>
          <v-list-item
              v-for="(item, i) in menuItems"
              :key="`ccd-menu-${i}`"
              @click="videosStore.activate({component: item.component})"
          >
            <v-list-item-title>{{ item.title }}</v-list-item-title>
          </v-list-item>
        </v-list>
      </v-menu>

    </template>
    
    <div v-if="videosStore.component">
      <component :is="videosStore.component" ></component>
    </div>

    <div class="d-flex justify-center my-2">
      <v-btn @click="videosStore.$reset()">
        Close
      </v-btn>
    </div>
  </v-dialog>
</template>

<script>
import TrickSteps from "./trick-steps";
import {useVideosStore} from "../../stores/video-upload";
import SubmissionSteps from "./submission-steps";
import {useTricksStore} from "../../stores/tricks";
import DifficultyForm from "./difficulty-form";
import CategoryForm from "./category-form";

export default {
  name: "content-creation-dialog",
  components: {CategoryForm, DifficultyForm, TrickSteps, SubmissionSteps},
  setup() {
    const videosStore = useVideosStore()
    const tricksStore = useTricksStore()

    return {videosStore, tricksStore}
  },
  computed: {
    menuItems() {
      return [
        {component: TrickSteps, title: "Trick"},
        {component: SubmissionSteps, title: "Submission"},
        {component: CategoryForm, title: "Category"},
        {component: DifficultyForm, title: "Difficulty"},
      ]
    }
  }
}
</script>

<style scoped>

</style>