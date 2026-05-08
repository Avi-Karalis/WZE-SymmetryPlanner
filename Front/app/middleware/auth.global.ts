export default defineNuxtRouteMiddleware((to) => {
	if (!process.client) return;
	if (to.path === "/login") return;
	const token = localStorage.getItem("wze_auth_token");
	if (!token) {
		return navigateTo("/login");
	}
});
