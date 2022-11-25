import {defineStore} from "pinia";
import {useRuntimeConfig} from "nuxt/app";


export const useTricksStore = defineStore({
    id: 'tricks-store',
    state: () => ({
        tricks: []
    }),
    actions: {
        async fetchTricks() {
            const config = useRuntimeConfig()
            this.tricks = await $fetch(config.public.apiBase +"/api/tricks", {method: 'GET', body: null});
        },

    }
})