import Axios from 'axios'
import {defineStore} from "pinia";

export const useIndexStore = defineStore('index-store',{
    state:()=>{
        return {
            message: "init"
        }
    },
    actions:{
        async fetchMessage(){
            console.log("action called")
            this.$pinia.state = (await Axios.get("https://localhost:7146/api/home")).data;
        }
    }
})
