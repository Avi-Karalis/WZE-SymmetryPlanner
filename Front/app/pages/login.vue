<template>
    <div class="min-h-screen flex items-center justify-center bg-gray-100 dark:bg-gray-900">
        <div class="bg-white dark:bg-gray-800 rounded-2xl shadow-lg p-10 flex flex-col items-center gap-6 w-full max-w-sm">
            <h1 class="text-2xl font-bold text-gray-800 dark:text-gray-100">WZE Symmetry Planner</h1>
            <p class="text-sm text-gray-500 dark:text-gray-400 text-center">Sign in to access your army lists</p>

            <div id="google-signin-btn"></div>

            <p v-if="error" class="text-red-500 text-sm text-center">{{ error }}</p>
        </div>
    </div>
</template>

<script setup>
definePageMeta({ middleware: [] })

const config = useRuntimeConfig()
const error = ref(null)
const { login } = useAuth()
const router = useRouter()

onMounted(() => {
    // Dynamically load Google Identity Services script
    if (document.getElementById('google-gsi-script')) {
        initGoogleSignIn()
        return
    }
    const script = document.createElement('script')
    script.id = 'google-gsi-script'
    script.src = 'https://accounts.google.com/gsi/client'
    script.async = true
    script.defer = true
    script.onload = initGoogleSignIn
    document.head.appendChild(script)
})

function initGoogleSignIn() {
    window.google.accounts.id.initialize({
        client_id: config.public.googleClientId,
        callback: handleCredentialResponse,
    })
    window.google.accounts.id.renderButton(
        document.getElementById('google-signin-btn'),
        { theme: 'outline', size: 'large', text: 'continue_with', width: 280 }
    )
}

async function handleCredentialResponse(response) {
    error.value = null
    try {
        await login(response.credential)
        router.push('/')
    } catch (e) {
        error.value = e?.response?.data?.message ?? 'Login failed. Please try again.'
    }
}
</script>
