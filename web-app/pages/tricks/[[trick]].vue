<template>
  <div class="d-flex justify-center align-start">
    <div class="mx-2" v-if="submissionsStore.submissions">
      <div v-for="submission in submissionsStore.submissions">
        {{ submission.id }} - {{ submission.description }} - {{ submission.trickId }}
        <div>
          <video width="400" controls :src="`https://localhost:7146/api/videos/${submission.video}`"/>
        </div>
      </div>
    </div>

    <div class="mx-2 sticky">
      Trick: {{ $route.params.trick }}
    </div>
  </div>
</template>

<script>
import {useSubmissionsStore} from "../../stores/submissions";
import {useRoute} from "nuxt/app";

export default {
  setup() {
    const submissionsStore = useSubmissionsStore()

    return {
      submissionsStore
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