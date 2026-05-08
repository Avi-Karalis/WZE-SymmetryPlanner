<template>
    <div class="p-4 sm:p-6 max-w-lg mx-auto">
        <div class="flex items-center gap-3 mb-6">
            <NuxtLink to="/force-lists" class="text-gray-400 hover:text-white text-sm">← Force Lists</NuxtLink>
            <h1 class="text-2xl font-bold">New Force List</h1>
        </div>

        <LoadingError :loading="loadingFactions" />

        <form v-if="!loadingFactions" @submit.prevent="submit" class="bg-gray-100 dark:bg-gray-800 rounded-lg p-6 flex flex-col gap-4 text-gray-800 dark:text-gray-100">
            <FormField label="List Name" :required="true">
                <input v-model="form.name" class="field-input" placeholder="e.g. My Capitol Strike Force" required />
            </FormField>

            <FormField label="Faction" :required="true">
                <select v-model="form.faction" class="field-input" required>
                    <option value="" disabled>Select a faction...</option>
                    <option v-for="f in factions" :key="f" :value="f">{{ f }}</option>
                </select>
            </FormField>

            <FormField label="Allegiance" :required="true">
                <div class="flex flex-col sm:flex-row gap-3 mt-1">
                    <label class="flex items-center gap-2 cursor-pointer">
                        <input type="radio" :value="0" v-model="form.allegiance" />
                        <span class="text-sm">Agents of Light</span>
                        <span class="text-xs text-gray-500 dark:text-gray-400">(+ Seconding &amp; Advisor allies)</span>
                    </label>
                    <label class="flex items-center gap-2 cursor-pointer">
                        <input type="radio" :value="1" v-model="form.allegiance" />
                        <span class="text-sm">Servants of Darkness</span>
                        <span class="text-xs text-gray-500 dark:text-gray-400">(+ Dark Cult allies)</span>
                    </label>
                </div>
            </FormField>

            <FormField label="Max DP" :required="true">
                <input v-model.number="form.maxDp" type="number" min="1" max="127" class="field-input" required />
            </FormField>

            <LoadingError :error="error" />

            <div class="flex justify-end gap-3 pt-2">
                <NuxtLink to="/force-lists" class="btn-secondary">Cancel</NuxtLink>
                <button type="submit" class="btn-primary" :disabled="saving">
                    {{ saving ? 'Creating...' : 'Create Force List' }}
                </button>
            </div>
        </form>
    </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'

// Placeholder user ID - replace with real auth when available
const PLACEHOLDER_USER_ID = '00000000-0000-0000-0000-000000000001'

const router = useRouter()
const { getFactions, create, error } = useForceLists()

const factions = ref([])
const loadingFactions = ref(true)
const saving = ref(false)

const form = ref({
    name: '',
    faction: '',
    allegiance: 0,
    maxDp: 40,
    userId: PLACEHOLDER_USER_ID,
})

onMounted(async () => {
    try {
        factions.value = await getFactions()
    } finally {
        loadingFactions.value = false
    }
})

async function submit() {
    saving.value = true
    try {
        const result = await create({
            name: form.value.name,
            faction: form.value.faction,
            allegiance: form.value.allegiance,
            maxDp: form.value.maxDp,
            userId: form.value.userId,
        })
        router.push(`/force-lists/${result.forceListId}`)
    } catch (e) {
        error.value = e.response?.data || e.message || 'Failed to create force list'
    } finally {
        saving.value = false
    }
}

definePageMeta({ layout: 'dark' })
</script>
