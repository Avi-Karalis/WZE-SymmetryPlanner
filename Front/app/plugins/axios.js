import axios from 'axios';

export default defineNuxtPlugin(() => {
    const instance = axios.create({
        baseURL: "https://localhost:7095/api"
    });
    return {
        provide: {
            axios: instance
        }
    };
});