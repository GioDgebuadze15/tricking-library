import Axios from 'axios'
import {defineStore} from "pinia";

export const useIndexStore = defineStore({
    id: 'index-store',
    state: () => ({
        message: "init"
    }),
    getters: {
        getMessage(state) {
            return state.message
        }
    },
    actions: {
        async fetchMessage() {
            this.message = (await Axios.get("https://localhost:7146/api/home")).data;
        }
    }
})
