// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
	compatibilityDate: "2025-07-15",
	devtools: { enabled: true },
	css: ["@/../assets/css/main.css"],
	modules: ["@nuxt/ui", "@nuxtjs/color-mode"],
	app: {
		head: {
			viewport: "width=device-width, initial-scale=1",
		},
	},
	colorMode: {
		classSuffix: "",
		preference: "dark",
		fallback: "dark",
	},
	runtimeConfig: {
		public: {
			apiBase:
				process.env.NUXT_PUBLIC_API_BASE ??
				"https://localhost:7095/api",
			googleClientId: process.env.NUXT_PUBLIC_GOOGLE_CLIENT_ID ?? "",
		},
	},
});
