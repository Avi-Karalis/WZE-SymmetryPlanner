<script setup>
import { ref, computed } from 'vue'

const rawData = ref([
  { id: 1, name: 'Test Item 1' },
  { id: 2, name: 'Test Item 2' },
  { id: 3, name: 'Test Item 3' }
])

const columns = [
  {
    accessorKey: 'id',
    header: 'ID',
    enableSorting: true
  },
  {
    accessorKey: 'name',
    header: 'Name'
  }
]

// 1. Change to an Array
const sorting = ref([{ id: 'id', desc: false }])

const data = computed(() => {
  // 2. Check for array length
  if (!sorting.value.length) return rawData.value

  // 3. Destructure the first element of the array
  const { id, desc } = sorting.value[0]

  return [...rawData.value].sort((a, b) => {
    const aValue = a[id]
    const bValue = b[id]
    
    if (aValue < bValue) return desc ? 1 : -1
    if (aValue > bValue) return desc ? -1 : 1
    return 0
  })
})
</script>

<template>
  <UTable
    v-model:sorting="sorting"
    :data="data"
    :columns="columns"
  />

  <pre>{{ sorting }}</pre>
</template>