import { ref } from 'vue'

export function useWeapons() {
    const { $axios } = useNuxtApp()

    const weapons = ref([])
    const loading = ref(false)
    const error = ref(null)

    const fetchAll = async () => {
        if (process.server) return

        loading.value = true
        error.value = null

        try {
            const res = await $axios.get('/Weapon')
            weapons.value = res.data
        } catch (err) {
            error.value = err
            console.error('Failed to fetch weapons', err)
        } finally {
            loading.value = false
        }
    }

    const create = async (weapon) => {
        if (process.server) return null

        try {
            const res = await $axios.post('/Weapon', weapon)
            weapons.value.push(res.data)
            return res.data
        } catch (err) {
            error.value = err
            throw err
        }
    }

    const getById = async (id) => {
        if (process.server) return null
        return await $axios.get(`/Weapon/${id}`).then(r => r.data)
    }

    const remove = async (id) => {
        if (process.server) return
        try {
            await $axios.delete(`/Weapon/${id}`)
            weapons.value = weapons.value.filter(w => w.id !== id)
        } catch (err) {
            error.value = err
            throw err
        }
    }

    const update = async (id, dto) => {
        if (process.server) return null
        try {
            const res = await $axios.patch(`/Weapon/update/${id}`, dto)
            const idx = weapons.value.findIndex(w => w.id === id)
            if (idx !== -1) weapons.value[idx] = res.data
            return res.data
        } catch (err) {
            error.value = err
            throw err
        }
    }

    return {
        weapons,
        loading,
        error,
        fetchAll,
        create,
        getById,
        remove,
        update,
    }
}
