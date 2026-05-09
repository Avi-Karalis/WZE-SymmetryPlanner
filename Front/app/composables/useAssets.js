// composables/useAssets.js
import { ref } from 'vue'

export function useAssets() {
  const { $axios } = useNuxtApp()
  const assets = ref([])
  const loading = ref(false)
  const error = ref(null)

  const fetchAll = async () => {
    if (process.server) return
    loading.value = true
    error.value = null
    try {
      const res = await $axios.get('/Asset')
      assets.value = res.data
    } catch (err) {
      error.value = err
      console.error('Failed to fetch Assets', err)
    } finally {
      loading.value = false
    }
  }

  const getById = async (id) => {
    if (process.server) return null
    loading.value = true
    try {
      const res = await $axios.get(`/Asset/${id}`)
      return res.data
    } catch (err) {
      error.value = err
      console.error(`Failed to fetch asset ${id}`, err)
      return null
    } finally {
      loading.value = false
    }
  }

  const create = async (unit) => {
    if (process.server) return null
    loading.value = true
    try {
      const res = await $axios.post('/Asset', unit)
      assets.value.push(res.data)
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
      await $axios.delete(`/Asset/${id}`)
      assets.value = assets.value.filter(a => a.id !== id)
    } catch (err) {
      error.value = err
      throw err
    }
  }

  const update = async (id, dto) => {
    if (process.server) return null
    try {
      const res = await $axios.put(`/Asset/${id}`, dto)
      const idx = assets.value.findIndex(a => a.id === id)
      if (idx !== -1) assets.value[idx] = res.data
      return res.data
    } catch (err) {
      error.value = err
      throw err
    }
  }

  return {
    assets,
    loading,
    error,
    fetchAll,
    create,
    getById,
    remove,
    update,
  }
}