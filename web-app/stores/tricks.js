import {defineStore} from "pinia";
import Axios from "axios";


export const useTricksStore = defineStore({
    id: 'tricks-store',
    state: () => ({
       tricks:[] 
    }),
    getters:{
        getTricks(state){
            return state.tricks
        }
    },
    actions:{
        async fetchTricks(){
                    this.tricks = (await Axios.get("https://localhost:7146/api/tricks")).data;
        },
        async createTrick({trick}){
            await Axios.post("https://localhost:7146/api/tricks",trick)
            await this.fetchTricks()
        }
    }
})