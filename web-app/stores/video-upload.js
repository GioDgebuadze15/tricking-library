import {defineStore} from "pinia";
import {useRuntimeConfig} from "nuxt/app";
import {useSubmissionsStore} from "~/stores/submissions";


export const useVideosStore = defineStore({
    id: 'videos-store',
    state: () => ({
        uploadPromise: null,
        active: false,
        component: null
    }),
    actions: {
        hide(){
            this.active = false;
        },
        startVideoUpload(form) {
            const config = useRuntimeConfig()
            this.uploadPromise = $fetch(config.public.apiBase + "/api/videos", {method: 'POST', body: form});
            this.step++
        },
        activate({component}){
            this.active = true;
            this.component = component
        },
        async createSubmission({form}) {
            if (!this.uploadPromise) {
                console.log("Upload Promise is Null!");
                return;
            }
            
            const submissionsStore = useSubmissionsStore()
            form.video = await this.uploadPromise;
            await submissionsStore.createSubmission({form: this.form});
            this.$reset()
        },
    }
})