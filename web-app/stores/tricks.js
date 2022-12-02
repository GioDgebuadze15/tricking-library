import {defineStore} from "pinia";
import {useRuntimeConfig} from "nuxt/app";


export const useTricksStore = defineStore({
    id: 'tricks-store',
    state: () => ({
        tricks: [],
        categories: [],
        difficulties: []
    }),
    getters: {
        trickById: state => id => state.tricks.find(x => x.id === id),
        trickItems: state => state.tricks.map(x => ({
            title: x.name,
            value: x.id
        })),
        categoryItems: state => state.categories.map(x => ({
            title: x.name,
            value: x.id
        })),
        difficultyItems: state => state.difficulties.map(x => ({
            title: x.name,
            value: x.id
        }))
    },
    actions: {
        async fetchTricks() {
            const config = useRuntimeConfig()
            this.tricks = await $fetch(config.public.apiBase + "/api/tricks", {method: 'GET', body: null});
            this.categories = await $fetch(config.public.apiBase + "/api/categories", {method: 'GET', body: null});
            this.difficulties = await $fetch(config.public.apiBase + "/api/difficulties", {method: 'GET', body: null});
        },
        createTrick({form}) {
            const config = useRuntimeConfig()
            return $fetch(config.public.apiBase + "/api/tricks", {method: 'POST', body: form})
        },

    }
})