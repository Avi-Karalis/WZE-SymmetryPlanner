import { ref } from 'vue'

export function useUnits() {
    const { $axios } = useNuxtApp()

    const units = ref([])
    const loading = ref(false)
    const error = ref(null)

    const fetchAll = async () => {
        if (process.server) return

        loading.value = true
        error.value = null

        try {
            const res = await $axios.get("/Unit");
            units.value = res.data

        } catch (err) {
            error.value = err
            console.error('Failed to fetch units', err)
        } finally {
            loading.value = false

        }
    }

    const create = async (unit) => {
        if (process.server) return null

        try {
            const res = await $axios.post('/Unit', unit)
            units.value.push(res.data)
            return res.data
        } catch (err) {
            error.value = err
            throw err
        }
    }

    const getById = async (id) => {
        if (process.server) return null
        return await $axios.get(`/Unit/${id}`).then(r => r.data)
    }

    return {
        units,
        loading,
        error,
        fetchAll,
        create,
        getById
    }
}
