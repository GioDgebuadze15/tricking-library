import {defineNuxtComponent, useRuntimeConfig} from "nuxt/app";

export default defineNuxtComponent({
    fetchKey: 'test',
    async asyncData () {
        const config = useRuntimeConfig()
        return {
            test: await $fetch(config.public.apiBase + "/api/submissions", {method: 'GET', body: null})
        }
    }
})