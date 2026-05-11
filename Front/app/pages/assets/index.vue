<template>
    <div class="p-4 sm:p-6 max-w-5xl mx-auto">
        <PageHeader title="Assets">
            <button v-if="isAdmin" class="btn-primary" @click="startCreate">+ New Asset</button>
        </PageHeader>

        <LoadingError :loading="loading" :error="error" />

        <div class="mb-4">
            <input v-model="filterText" class="field-input w-full sm:max-w-xs" placeholder="Filter by name or faction..." />
        </div>

        <!-- Mobile cards -->
        <div v-if="filteredAssets.length" class="md:hidden flex flex-col gap-3">
            <div
                v-for="a in filteredAssets"
                :key="a.id"
                class="bg-gray-100 dark:bg-gray-800 border border-gray-300 dark:border-gray-700 rounded-lg p-3"
            >
                <div class="flex items-start justify-between gap-2">
                    <div>
                        <div class="font-semibold text-gray-800 dark:text-gray-100">{{ a.name }}</div>
                        <div class="text-xs text-gray-500 dark:text-gray-400 mt-0.5">{{ a.faction }}</div>
                        <div class="text-xs text-gray-600 dark:text-gray-300 mt-1">{{ a.description }}</div>
                    </div>
                    <div class="flex flex-col items-end gap-1 shrink-0">
                        <span class="text-xs bg-gray-300 dark:bg-gray-700 px-2 py-0.5 rounded">{{ a.dpCost }} DP</span>
                        <div v-if="isAdmin" class="flex gap-1 mt-1">
                            <button class="btn-sm btn-secondary" @click="startEdit(a)">Edit</button>
                            <button class="btn-sm btn-danger" @click="confirmDelete(a)">Del</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Desktop table -->
        <div v-if="filteredAssets.length" class="hidden md:block overflow-x-auto">
            <table class="w-full text-sm">
                <thead>
                    <tr class="bg-gray-200 dark:bg-gray-700 text-left text-gray-700 dark:text-gray-100">
                        <th class="px-3 py-2">Name</th>
                        <th class="px-3 py-2">Faction</th>
                        <th class="px-3 py-2 text-center">DP</th>
                        <th class="px-3 py-2">Description</th>
                        <th v-if="isAdmin" class="px-3 py-2 text-center">Actions</th>
                    </tr>
                </thead>
                <tbody>
                    <tr
                        v-for="a in filteredAssets"
                        :key="a.id"
                        class="border-b border-gray-200 dark:border-gray-700 hover:bg-gray-100 dark:hover:bg-gray-800"
                    >
                        <td class="px-3 py-2 font-medium">{{ a.name }}</td>
                        <td class="px-3 py-2 text-gray-600 dark:text-gray-300">{{ a.faction }}</td>
                        <td class="px-3 py-2 text-center">{{ a.dpCost }}</td>
                        <td class="px-3 py-2 text-xs text-gray-600 dark:text-gray-300 max-w-xs truncate">{{ a.description }}</td>
                        <td v-if="isAdmin" class="px-3 py-2 text-center">
                            <RowActions @edit="startEdit(a)" @delete="confirmDelete(a)" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>

        <p v-else-if="!loading" class="text-gray-400">No assets found.</p>

        <!-- Create / Edit Modal -->
        <AppModal
            v-if="editTarget !== null"
            :title="isCreating ? 'New Asset' : 'Edit Asset'"
            max-width="max-w-lg"
            @close="closeModal"
        >
            <form @submit.prevent="saveAsset">
                <div class="grid grid-cols-1 sm:grid-cols-2 gap-3">
                    <FormField label="Faction" :required="true">
                        <input v-model="form.faction" class="field-input" required />
                    </FormField>
                    <FormField label="Name" :required="true">
                        <input v-model="form.name" class="field-input" required />
                    </FormField>
                    <FormField label="DP Cost" :required="true">
                        <input v-model.number="form.dpCost" type="number" class="field-input" required />
                    </FormField>
                    <FormField label="Description" class="sm:col-span-2">
                        <textarea v-model="form.description" class="field-input" rows="3" />
                    </FormField>
                </div>
                <div class="flex justify-end gap-3 mt-5">
                    <button type="button" class="btn-secondary" @click="closeModal">Cancel</button>
                    <button type="submit" class="btn-primary" :disabled="saving">
                        {{ saving ? 'Saving...' : 'Save' }}
                    </button>
                </div>
            </form>
        </AppModal>

        <!-- Delete Confirm -->
        <ConfirmDelete
            v-if="deleteTarget"
            :name="deleteTarget.name"
            @confirm="doDelete"
            @cancel="deleteTarget = null"
        />
    </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'

const { isAdmin } = useAuth()
const { assets, loading, error, fetchAll, create, update, remove } = useAssets()

const filterText = ref('')
const editTarget = ref(null)
const deleteTarget = ref(null)
const saving = ref(false)
const isCreating = ref(false)

const filteredAssets = computed(() => {
    if (!filterText.value) return assets.value
    const q = filterText.value.toLowerCase()
    return assets.value.filter(a =>
        a.name?.toLowerCase().includes(q) ||
        a.faction?.toLowerCase().includes(q)
    )
})

const emptyForm = () => ({ faction: '', name: '', dpCost: 0, description: '' })
const form = ref(emptyForm())

onMounted(() => fetchAll())

function startCreate() {
    isCreating.value = true
    editTarget.value = {}
    form.value = emptyForm()
}

function startEdit(a) {
    isCreating.value = false
    editTarget.value = a
    form.value = { faction: a.faction, name: a.name, dpCost: a.dpCost, description: a.description ?? '' }
}

function closeModal() {
    editTarget.value = null
    isCreating.value = false
    form.value = emptyForm()
}

async function saveAsset() {
    saving.value = true
    try {
        if (isCreating.value) {
            await create(form.value)
        } else {
            await update(editTarget.value.id, form.value)
        }
        closeModal()
    } catch (e) {
        alert('Failed to save: ' + (e.response?.data || e.message))
    } finally {
        saving.value = false
    }
}

function confirmDelete(a) {
    deleteTarget.value = a
}

async function doDelete() {
    try {
        await remove(deleteTarget.value.id)
    } catch (e) {
        alert('Failed to delete: ' + (e.response?.data || e.message))
    } finally {
        deleteTarget.value = null
    }
}

definePageMeta({ layout: 'dark' })
</script>
