<template>
    <div class="p-6 max-w-4xl mx-auto">
        <PageHeader title="Force Lists">
            <NuxtLink to="/force-lists/create" class="btn-primary">+ New Force List</NuxtLink>
        </PageHeader>

        <LoadingError :loading="loading" :error="error" />

        <div v-if="forceLists.length" class="grid gap-4">
            <div
                v-for="fl in forceLists"
                :key="fl.id"
                class="bg-gray-800 border border-gray-700 rounded-lg p-4 flex items-center justify-between hover:border-gray-500 transition"
            >
                <div>
                    <h2 class="text-lg font-semibold">{{ fl.name }}</h2>
                    <div class="text-sm text-gray-400 mt-1 flex gap-4">
                        <span>Faction: <span class="text-gray-200">{{ fl.faction }}</span></span>
                        <span>Allegiance: <span class="text-gray-200">{{ fl.allegiance }}</span></span>
                        <span>Max DP: <span class="text-gray-200">{{ fl.maxDp }}</span></span>
                        <span>Units: <span class="text-gray-200">{{ fl.units?.length ?? 0 }}</span></span>
                    </div>
                </div>
                <div class="flex items-center gap-2">
                    <NuxtLink :to="`/force-lists/${fl.id}`" class="btn-primary text-sm">Open</NuxtLink>
                    <button class="btn-danger text-sm" @click="confirmDelete(fl)">Delete</button>
                </div>
            </div>
        </div>
        <p v-else-if="!loading" class="text-gray-400">No force lists yet. Create one to get started.</p>

        <ConfirmDelete v-if="deleteTarget" :name="deleteTarget.name" @confirm="doDelete" @cancel="deleteTarget = null" />
    </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'

const { forceLists, loading, error, fetchAll, remove } = useForceLists()
const deleteTarget = ref(null)

onMounted(fetchAll)

function confirmDelete(fl) {
    deleteTarget.value = fl
}

async function doDelete() {
    await remove(deleteTarget.value.id)
    deleteTarget.value = null
    await fetchAll()
}

definePageMeta({ layout: 'dark' })
</script>
