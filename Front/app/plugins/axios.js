import axios from 'axios';

export default defineNuxtPlugin(() => {
    const config = useRuntimeConfig();
    const instance = axios.create({
        baseURL: config.public.apiBase
    });

    instance.interceptors.request.use(request => {
        const token = process.client ? localStorage.getItem('wze_auth_token') : null;
        if (token) {
            request.headers['Authorization'] = `Bearer ${token}`;
        }
        return request;
    });

    return {
        provide: {
            axios: instance
        }
    };
});