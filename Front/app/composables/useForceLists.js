import { ref } from 'vue'

export function useForceLists() {
    const { $axios } = useNuxtApp()

    const forceLists = ref([])
    const loading = ref(false)
    const error = ref(null)

    const fetchAll = async () => {
        loading.value = true
        error.value = null
        try {
            const res = await $axios.get('/force-lists')
            forceLists.value = res.data
        } catch (err) {
            error.value = err
            console.error('Failed to fetch force lists', err)
        } finally {
            loading.value = false
        }
    }

    const getById = async (id) => {
        loading.value = true
        error.value = null
        try {
            const res = await $axios.get(`/force-lists/${id}`)
            return res.data
        } catch (err) {
            error.value = err
            console.error(`Failed to fetch force list ${id}`, err)
            return null
        } finally {
            loading.value = false
        }
    }

    const getFactions = async () => {
        const res = await $axios.get('/force-lists/factions')
        return res.data
    }

    const getUnitsForFaction = async (faction) => {
        const res = await $axios.get(`/force-lists/units/${encodeURIComponent(faction)}`)
        return res.data
    }

    const create = async (dto) => {
        loading.value = true
        error.value = null
        try {
            const res = await $axios.post('/force-lists/create', dto)
            return res.data
        } catch (err) {
            error.value = err
            throw err
        } finally {
            loading.value = false
        }
    }

    const remove = async (forceListId) => {
        await $axios.delete(`/force-lists/${forceListId}`)
    }

    const addUnit = async (forceListId, unitId) => {
        await $axios.post(`/force-lists/${forceListId}/units`, null, { params: { unitId } })
    }

    const removeUnit = async (forceListId, unitId) => {
        await $axios.post(`/force-lists/${forceListId}/units/rem`, null, { params: { unitId } })
    }

    const validate = async (forceListId) => {
        const res = await $axios.post(`/force-lists/${forceListId}/validate`)
        return res.data
    }

    return {
        forceLists,
        loading,
        error,
        fetchAll,
        getById,
        getFactions,
        getUnitsForFaction,
        create,
        remove,
        addUnit,
        removeUnit,
        validate,
    }
}
