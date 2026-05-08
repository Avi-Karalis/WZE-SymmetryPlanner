export default defineNuxtRouteMiddleware((to) => {
	if (!process.client) return;
	if (to.path === "/login") return;

	const token = localStorage.getItem("wze_auth_token");
	if (!token) {
		return navigateTo("/login");
	}

	if (to.path.startsWith("/admin")) {
		const stored = localStorage.getItem("wze_auth_user");
		if (!stored) return navigateTo("/login");
		try {
			const user = JSON.parse(stored);
			if (!["Admin", "SuperAdmin"].includes(user?.role)) {
				return navigateTo("/force-lists");
			}
		} catch {
			return navigateTo("/login");
		}
	}
});
