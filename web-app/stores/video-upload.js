import {defineStore} from "pinia";
import {useRuntimeConfig} from "nuxt/app";


export const useVideosStore = defineStore({
    id: 'videos-store',
    state: () => ({
        uploadPromise: null,
        active: false,
        type: "",
        step: 1,
    }),
    actions: {
        startVideoUpload(form) {
            const config = useRuntimeConfig()
            this.uploadPromise = $fetch(config.public.apiBase + "/api/videos", {method: 'POST', body: form});
            this.step++
        },
        async createTrick({trick}) {
            const config = useRuntimeConfig()
            await $fetch(config.public.apiBase + "/api/tricks", {method: 'POST', body: trick})
            this.step++
            // const trickStore = useTricksStore()
            // await trickStore.fetchTricks()
        },
        toggleActivity() {
            this.active = !this.active
            if (!this.active) {
                this.$reset();
            }
        },
        setType({type}) {
            this.type = type
            this.step++
        }
    }
})