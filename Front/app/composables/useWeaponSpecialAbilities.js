import { ref } from 'vue'

export function useWeaponSpecialAbilities() {
    const { $axios } = useNuxtApp()

    const abilities = ref([])
    const loading = ref(false)
    const error = ref(null)

    const fetchAll = async () => {
        if (process.server) return

        loading.value = true
        error.value = null

        try {
            const res = await $axios.get('/WeaponSpecialAbility')
            abilities.value = res.data
        } catch (err) {
            error.value = err
            console.error('Failed to fetch weapon special abilities', err)
        } finally {
            loading.value = false
        }
    }

    const create = async (ability) => {
        if (process.server) return null

        try {
            const res = await $axios.post('/WeaponSpecialAbility', ability)
            abilities.value.push(res.data)
            return res.data
        } catch (err) {
            error.value = err
            throw err
        }
    }

    const remove = async (id) => {
        if (process.server) return
        try {
            await $axios.delete(`/WeaponSpecialAbility/${id}`)
            abilities.value = abilities.value.filter(a => a.id !== id)
        } catch (err) {
            error.value = err
            throw err
        }
    }

    const update = async (id, dto) => {
        if (process.server) return null
        try {
            const res = await $axios.patch(`/WeaponSpecialAbility/update/${id}`, dto)
            const idx = abilities.value.findIndex(a => a.id === id)
            if (idx !== -1) abilities.value[idx] = res.data
            return res.data
        } catch (err) {
            error.value = err
            throw err
        }
    }

    return {
        abilities,
        loading,
        error,
        fetchAll,
        create,
        remove,
        update,
    }
}
