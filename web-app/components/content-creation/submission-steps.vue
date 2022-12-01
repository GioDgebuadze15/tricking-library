<template>
  <v-card height="400px">
    <div class="d-flex flex-row align-center ml-3">
      <div>
        <v-chip>{{ step }}</v-chip>
      </div>
      <div>
        <v-card-title v-if="step===1">Upload Video</v-card-title>
        <v-card-title v-if="step===2">Select Trick</v-card-title>
        <v-card-title v-if="step===3">Submission</v-card-title>
        <v-card-title v-if="step===4">Review</v-card-title>
      </div>
    </div>
    <v-divider/>


    <v-card-item v-if="step===1">
      <div>
        <v-file-input accept="video/*" @change="handleFile"/>
      </div>
    </v-card-item>
    <v-card-item v-if="step===2">
      <div>
        <v-select :items="this.tricksStore.trickItems" v-model="form.trickId" label="Select Trick"></v-select>
        <v-btn @click="step++">Next</v-btn>
      </div>
    </v-card-item>
    <v-card-item v-if="step===3">
      <div>
        <v-text-field v-model="form.description" label="Description"/>
        <v-btn @click="step++">Next</v-btn>
      </div>
    </v-card-item>
    <v-card-item v-if="step===4">
      <div>
        <v-btn @click="save">Save</v-btn>
      </div>
    </v-card-item>
  </v-card>
</template>

<script>
import {useTricksStore} from "../../stores/tricks";
import {useVideosStore} from "../../stores/video-upload";
import {useSubmissionsStore} from "../../stores/submissions";

const initState = () => ({
  step: 1,
  form: {
    trickId: "",
    video: "",
    description: ""
  }
})

export default {
  name: "submission-steps",
  setup() {
    const tricksStore = useTricksStore()
    const videosStore = useVideosStore()
    const submissionsStore = useSubmissionsStore()

    return {
      tricksStore,
      videosStore,
      submissionsStore
    }
  },
  data: initState,
  watch: { 
    'active': function (newValue) {
      if (!newValue) {
        Object.assign(this.$data, initState())
      }
    }
  },
  methods: {
    async handleFile(event) {
      const file = event.target.files[0];
      if (!file) return;

      const form = new FormData();
      form.append("video", file)
      this.videosStore.startVideoUpload(form);
      this.step++

      //todo remove this
      await this.tricksStore.fetchTricks()
    },
    save() {
      this.submissionsStore.createSubmission({form: this.form})
      this.videosStore.hide()
    },
  }
}
</script>

<style scoped>

</style>