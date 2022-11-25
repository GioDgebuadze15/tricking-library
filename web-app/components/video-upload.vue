<template>
  <v-dialog v-model="videosStore.active" persistent width="500">
    <template v-slot:activator>
      <v-btn depressed @click="videosStore.toggleActivity()">
        Upload
    </v-btn>
    </template>
    <v-card height="400px">
      <div class="d-flex flex-row align-center ml-3">
        <div>
          <v-chip>{{ videosStore.step }}</v-chip>
        </div>
        <div>
          <v-card-title v-if="videosStore.step===1">Select Type</v-card-title>
          <v-card-title v-if="videosStore.step===2">Upload Video</v-card-title>
          <v-card-title v-if="videosStore.step===3">Trick Information</v-card-title>
          <v-card-title v-if="videosStore.step===4">Review</v-card-title>
        </div>
      </div>
      <v-divider/>

      <v-card-item v-if="videosStore.step===1">
        <div class="d-flex flex-column align-center">
          <v-btn class="my-2" @click="videosStore.setType(uploadType.TRICK)">Trick</v-btn>
          <v-btn class="my-2" @click="videosStore.setType(uploadType.SUBMISSION)">Submission</v-btn>
        </div>
      </v-card-item>
      <v-card-item v-if="videosStore.step===2">
        <div>
          <v-file-input accept="video/*" @change="handleFile"/>
        </div>
      </v-card-item>
      <v-card-item v-if="videosStore.step===3">
        <div>
          <v-text-field v-model="trickName" label="Tricking Name"/>
          <v-btn @click="saveTrick()">Save Trick</v-btn>
        </div>
      </v-card-item>
      <v-card-item v-if="videosStore.step===4">
        <div>
          Success
        </div>
      </v-card-item>
    </v-card>
    <div class="d-flex justify-center my-2">
      <v-btn @click="videosStore.toggleActivity()">
        Close
      </v-btn>
    </div>
  </v-dialog>
</template>

<script>
import {useTricksStore} from "../stores/tricks";
import {useVideosStore} from "../stores/video-upload";
import {UPLOAD_TYPE} from "../data/enums";

export default {
  name: "video-upload",
  setup() {
    const tricksStore = useTricksStore()
    const videosStore = useVideosStore()

    return {
      tricksStore,
      videosStore,
    }
  },
  data: () => ({
    trickName: "",

  }),
  computed: {
    uploadType() {
      return {
        ...UPLOAD_TYPE
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
    },
    async saveTrick() {
      if (!this.videosStore.uploadPromise) {
        console.log("Upload Promise is Null!");
        return;
      }

      const video = await this.videosStore.uploadPromise;
      await this.videosStore.createTrick({trick: {name: this.trickName, video}});
      this.trickName = ""
      await this.tricksStore.fetchTricks()
    },
  }
}
</script>

<style scoped>

</style>