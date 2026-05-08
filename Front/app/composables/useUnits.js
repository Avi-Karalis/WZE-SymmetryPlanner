// composables/useUnits.js
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
      const res = await $axios.get('/Unit')
      units.value = res.data
    } catch (err) {
      error.value = err
      console.error('Failed to fetch units', err)
    } finally {
      loading.value = false
    }
  }

  const getById = async (id) => {
    if (process.server) return null
    loading.value = true
    try {
      const res = await $axios.get(`/Unit/${id}`)
      return res.data
    } catch (err) {
      error.value = err
      console.error(`Failed to fetch unit ${id}`, err)
      return null
    } finally {
      loading.value = false
    }
  }

  const create = async (unit) => {
    if (process.server) return null
    loading.value = true
    try {
      const res = await $axios.post('/Unit', unit)
      units.value.push(res.data)
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
      await $axios.delete(`/Unit/${id}`)
      units.value = units.value.filter(u => u.id !== id)
    } catch (err) {
      error.value = err
      throw err
    }
  }

  const update = async (id, dto) => {
    if (process.server) return null
    try {
      const res = await $axios.patch(`/Unit/update/${id}`, dto)
      const idx = units.value.findIndex(u => u.id === id)
      if (idx !== -1) units.value[idx] = res.data
      return res.data
    } catch (err) {
      error.value = err
      throw err
    }
  }

  return {
    units,
    loading,
    error,
    fetchAll,
    create,
    getById,
    remove,
    update,
  }
}