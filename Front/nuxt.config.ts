// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
    compatibilityDate: "2025-07-15",
    devtools: { enabled: true },
    css: ["@/../assets/css/main.css"],
    modules: ["@nuxt/ui", "@nuxtjs/color-mode"],
    colorMode: {
        classSuffix: '',
        preference: 'dark',
        fallback: 'dark',
    },
    runtimeConfig: {
        public: {
            apiBase: "https://localhost:7095/api",
        },
    },

});
