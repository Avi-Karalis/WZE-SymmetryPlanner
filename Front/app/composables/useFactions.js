// composables/useUnits.js
import { ref } from 'vue'

export function useFactions() {
  const { $axios } = useNuxtApp()
  const factions = ref([])
  const loading = ref(false)
  const error = ref(null)

  const fetchAll = async () => {
    if (process.server) return
    loading.value = true
    error.value = null
    try {
      const res = await $axios.get('/factions')
      factions.value = res.data
    } catch (err) {
      error.value = err
      console.error('Failed to fetch factions', err)
    } finally {
      loading.value = false
    }
  }


  const create = async (faction) => {
    if (process.server) return null
    loading.value = true
    try {
      const res = await $axios.post('/factions', faction)
      factions.value.push(res.data)
      return res.data
    } catch (err) {
      error.value = err
      throw err
    } finally {
      loading.value = false
    }
  }

  const remove = async (id) => {
    if (process.server) return
    try {
      await $axios.delete(`/factions/${id}`)
      factions.value = factions.value.filter(f => f.id !== id)
    } catch (err) {
      error.value = err
      throw err
    }
  }

  const update = async (id, dto) => {
    if (process.server) return null
    try {
      const res = await $axios.put(`/factions/${id}`, dto)
      const idx = factions.value.findIndex(f => f.id === id)
      if (idx !== -1) factions.value[idx] = res.data
      return res.data
    } catch (err) {
      error.value = err
      throw err
    }
  }

  return {
    factions,
    loading,
    error,
    fetchAll,
    fetchFactions,
    create,
    getById,
    remove,
    update,
  }
}