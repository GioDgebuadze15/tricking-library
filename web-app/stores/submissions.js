import {defineStore} from "pinia";
import {useRuntimeConfig} from "nuxt/app";


export const useSubmissionsStore = defineStore({
    id: 'submissions-store',
    state: () => ({
        submissions: []
    }),
    actions: {
        async fetchSubmissionsForTrick({trickId}) {
            const config = useRuntimeConfig()
            this.submissions = await $fetch(`${config.public.apiBase}/api/tricks/${trickId}/submissions`, {method: 'GET', body: null});
        },
        createSubmission({form}) {

            
            const config = useRuntimeConfig()
            return  $fetch(config.public.apiBase + "/api/submissions", {method: 'POST', body: submission})
        },
    }
})