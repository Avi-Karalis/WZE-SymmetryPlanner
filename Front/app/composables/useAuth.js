const TOKEN_KEY = 'wze_auth_token'
const USER_KEY = 'wze_auth_user'

const token = ref(null)
const user = ref(null)

if (process.client) {
    token.value = localStorage.getItem(TOKEN_KEY)
    const stored = localStorage.getItem(USER_KEY)
    if (stored) {
        try { user.value = JSON.parse(stored) } catch { /* ignore */ }
    }
}

export function useAuth() {
    const { $axios } = useNuxtApp()
    const router = useRouter()

    const isLoggedIn = computed(() => !!token.value)
    const isAdmin = computed(() => ['Admin', 'SuperAdmin'].includes(user.value?.role))
    const isSuperAdmin = computed(() => user.value?.role === 'SuperAdmin')

    async function login(googleIdToken) {
        const { data } = await $axios.post('/auth/google-login', { idToken: googleIdToken })
        token.value = data.token
        user.value = data.user
        if (process.client) {
            localStorage.setItem(TOKEN_KEY, data.token)
            localStorage.setItem(USER_KEY, JSON.stringify(data.user))
        }
        return data.user
    }

    function logout() {
        token.value = null
        user.value = null
        if (process.client) {
            localStorage.removeItem(TOKEN_KEY)
            localStorage.removeItem(USER_KEY)
        }
        router.push('/login')
    }

    function getToken() {
        return token.value
    }

    return { token, user, isLoggedIn, isAdmin, isSuperAdmin, login, logout, getToken }
}
