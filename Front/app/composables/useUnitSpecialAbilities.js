import { ref } from 'vue'

export function useUnitSpecialAbilities() {
    const { $axios } = useNuxtApp()

    const abilities = ref([])
    const loading = ref(false)
    const error = ref(null)

    const fetchAll = async () => {
        if (process.server) return

        loading.value = true
        error.value = null

        try {
            const res = await $axios.get('/UnitSpecialAbility')
            abilities.value = res.data
        } catch (err) {
            error.value = err
            console.error('Failed to fetch unit special abilities', err)
        } finally {
            loading.value = false
        }
    }

    const create = async (ability) => {
        if (process.server) return null

        try {
            const res = await $axios.post('/UnitSpecialAbility', ability)
            abilities.value.push(res.data)
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
        create
    }
}
