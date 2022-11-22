import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'
import {defineNuxtPlugin} from "nuxt/app";
import '@mdi/font/css/materialdesignicons.css' // Ensure you are using css-loader

export default defineNuxtPlugin(nuxtApp => {
    const vuetify = createVuetify({
        components,
        directives,
        theme: {
            defaultTheme: 'dark'
        },
        icons: {
            iconfont: 'mdi', // default - only for display purposes
        },
    })

    nuxtApp.vueApp.use(vuetify)
})