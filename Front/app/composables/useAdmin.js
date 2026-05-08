import { ref } from 'vue'

export function useAdmin() {
    const { $axios } = useNuxtApp()

    const deletedLists = ref([])
    const users = ref([])
    const loading = ref(false)
    const error = ref(null)

    const fetchDeletedLists = async () => {
        loading.value = true
        error.value = null
        try {
            const res = await $axios.get('/admin/force-lists/deleted')
            deletedLists.value = res.data
        } catch (err) {
            error.value = err
            console.error('Failed to fetch deleted force lists', err)
        } finally {
            loading.value = false
        }
    }

    const restoreForceList = async (id) => {
        await $axios.patch(`/admin/force-lists/${id}/restore`)
        deletedLists.value = deletedLists.value.filter(l => l.id !== id)
    }

    const fetchUsers = async () => {
        loading.value = true
        error.value = null
        try {
            const res = await $axios.get('/admin/users')
            users.value = res.data
        } catch (err) {
            error.value = err
            console.error('Failed to fetch users', err)
        } finally {
            loading.value = false
        }
    }

    const updateUserRole = async (userId, role) => {
        const res = await $axios.patch(`/admin/users/${userId}/role`, { role })
        const idx = users.value.findIndex(u => u.id === userId)
        if (idx !== -1) users.value[idx] = { ...users.value[idx], role: res.data.role }
        return res.data
    }

    return {
        deletedLists,
        users,
        loading,
        error,
        fetchDeletedLists,
        restoreForceList,
        fetchUsers,
        updateUserRole,
    }
}
