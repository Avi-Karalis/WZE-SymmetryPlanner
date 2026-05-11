<template>
    <div class="p-4 sm:p-6 max-w-4xl mx-auto">
        <PageHeader title="Weapon Special Abilities">
            <button v-if="isAdmin" class="btn-primary" @click="showCreate = true">+ New Ability</button>
        </PageHeader>

        <LoadingError :loading="loading" :error="error" />

        <!-- Mobile card view -->
        <div v-if="abilities.length" class="md:hidden flex flex-col gap-3">
            <div
                v-for="a in abilities" :key="a.id"
                class="bg-gray-100 dark:bg-gray-800 border border-gray-300 dark:border-gray-700 rounded-lg p-3"
            >
                <div class="flex items-start justify-between gap-2">
                    <div class="min-w-0">
                        <div class="font-semibold text-gray-800 dark:text-gray-100">{{ a.name }}</div>
                        <div v-if="a.valueX || a.valueY" class="text-xs text-gray-500 dark:text-gray-400 mt-0.5">
                            <span v-if="a.valueX">X: {{ a.valueX }}</span>
                            <span v-if="a.valueX && a.valueY"> · </span>
                            <span v-if="a.valueY">Y: {{ a.valueY }}</span>
                        </div>
                        <div class="text-xs text-gray-600 dark:text-gray-300 mt-1">{{ a.description }}</div>
                    </div>
                    <div v-if="isAdmin" class="flex gap-1 shrink-0">
                        <button class="btn-sm btn-secondary" @click="startEdit(a)">Edit</button>
                        <button class="btn-sm btn-danger" @click="confirmDelete(a)">Del</button>
                    </div>
                </div>
            </div>
        </div>

        <!-- Desktop table -->
        <div v-if="abilities.length" class="hidden md:block overflow-x-auto">
        <table class="w-full text-sm min-w-[480px]">
            <thead>
                <tr class="bg-gray-200 dark:bg-gray-700 text-left text-gray-700 dark:text-gray-100">
                    <th class="px-3 py-2">Name</th>
                    <th class="px-3 py-2">Value X</th>
                    <th class="px-3 py-2">Value Y</th>
                    <th class="px-3 py-2">Description</th>
                    <th class="px-3 py-2 text-center">Actions</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="a in abilities" :key="a.id" class="border-b border-gray-200 dark:border-gray-700 hover:bg-gray-100 dark:hover:bg-gray-800">
                    <td class="px-3 py-2 font-medium">{{ a.name }}</td>
                    <td class="px-3 py-2 text-gray-600 dark:text-gray-300">{{ a.valueX ?? '—' }}</td>
                    <td class="px-3 py-2 text-gray-600 dark:text-gray-300">{{ a.valueY ?? '—' }}</td>
                    <td class="px-3 py-2 text-gray-600 dark:text-gray-300 text-xs max-w-xs truncate" :title="a.description">{{ a.description }}</td>
                    <td class="px-3 py-2 text-center">
                        <RowActions v-if="isAdmin" @edit="startEdit(a)" @delete="confirmDelete(a)" />
                    </td>
                </tr>
            </tbody>
        </table>
        </div>
        <p v-else-if="!loading" class="text-gray-400">No weapon special abilities found.</p>

        <!-- Create / Edit Modal -->
        <AppModal
            v-if="showCreate || editTarget"
            :title="editTarget ? 'Edit Ability' : 'New Ability'"
            @close="closeModal"
        >
            <form @submit.prevent="saveAbility" class="flex flex-col gap-3">
                <FormField label="Name" :required="true">
                    <input v-model="form.name" class="field-input" required />
                </FormField>
                <FormField label="Value X">
                    <input v-model="form.valueX" class="field-input" />
                </FormField>
                <FormField label="Value Y">
                    <input v-model="form.valueY" class="field-input" />
                </FormField>
                <FormField label="Description" :required="true">
                    <textarea v-model="form.description" class="field-input" rows="3" required></textarea>
                </FormField>
                <div class="flex justify-end gap-3 mt-2">
                    <button type="button" class="btn-secondary" @click="closeModal">Cancel</button>
                    <button type="submit" class="btn-primary" :disabled="saving">{{ saving ? 'Saving...' : 'Save' }}</button>
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
import { ref, onMounted } from 'vue'

const { isAdmin } = useAuth()
const { abilities, loading, error, fetchAll, create, update, remove } = useWeaponSpecialAbilities()

const showCreate = ref(false)
const editTarget = ref(null)
const deleteTarget = ref(null)
const saving = ref(false)

const emptyForm = () => ({ name: '', valueX: '', valueY: '', description: '' })
const form = ref(emptyForm())

onMounted(fetchAll)

function startEdit(a) {
    editTarget.value = a
    form.value = { name: a.name, valueX: a.valueX ?? '', valueY: a.valueY ?? '', description: a.description }
}

function closeModal() {
    showCreate.value = false
    editTarget.value = null
    form.value = emptyForm()
}

async function saveAbility() {
    saving.value = true
    try {
        if (editTarget.value) {
            await update(editTarget.value.id, form.value)
        } else {
            await create(form.value)
        }
        closeModal()
    } catch (e) {
        alert('Failed to save: ' + (e.response?.data || e.message))
    } finally {
        saving.value = false
    }
}

function confirmDelete(a) { deleteTarget.value = a }

async function doDelete() {
    try {
        await remove(deleteTarget.value.id)
        deleteTarget.value = null
    } catch (e) {
        alert('Failed to delete: ' + (e.response?.data || e.message))
    }
}

definePageMeta({ layout: 'dark' })
</script>


