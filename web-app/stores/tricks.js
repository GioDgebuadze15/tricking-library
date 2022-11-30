import {defineStore} from "pinia";
import {useRuntimeConfig} from "nuxt/app";


export const useTricksStore = defineStore({
    id: 'tricks-store',
    state: () => ({
        tricks: []
    }),
    getters: {
        trickItems: state => state.tricks.map(x => ({
            text: x.name,
            value: x.id
        }))
    },
    actions: {
        async fetchTricks() {
            const config = useRuntimeConfig()
            this.tricks = await $fetch(config.public.apiBase + "/api/tricks", {method: 'GET', body: null});
        },
        createTrick({form}) {
            const config = useRuntimeConfig()
            return $fetch(config.public.apiBase + "/api/tricks", {method: 'POST', body: form})
        },

    }
})