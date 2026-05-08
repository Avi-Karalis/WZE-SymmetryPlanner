<template>
    <div class="p-4 sm:p-6 max-w-6xl mx-auto">
        <PageHeader title="Weapons">
            <button v-if="isAdmin" class="btn-primary" @click="showCreate = true">+ New Weapon</button>
        </PageHeader>

        <LoadingError :loading="loading" :error="error" />

        <!-- Mobile card view -->
        <div v-if="weapons.length" class="md:hidden flex flex-col gap-3">
            <div
                v-for="w in weapons" :key="w.id"
                class="bg-gray-100 dark:bg-gray-800 border border-gray-300 dark:border-gray-700 rounded-lg p-3"
            >
                <div class="flex items-start justify-between gap-2 mb-2">
                    <NuxtLink :to="`/weapons/${w.id}`" class="text-blue-400 hover:underline font-semibold text-sm">{{ w.name }}</NuxtLink>
                    <div class="flex gap-1 shrink-0">
                        <button v-if="isAdmin" class="btn-sm btn-secondary" @click="startEdit(w)">Edit</button>
                        <button v-if="isAdmin" class="btn-sm btn-danger" @click="confirmDelete(w)">Del</button>
                    </div>
                </div>
                <div class="grid grid-cols-3 gap-1 text-xs text-center mb-2">
                    <div v-if="w.ccMod != null" class="bg-gray-200 dark:bg-gray-700 rounded py-1">
                        <div class="text-gray-500">CC Mod</div><div class="font-bold">{{ w.ccMod }}</div>
                    </div>
                    <div v-if="w.shortRange != null" class="bg-gray-200 dark:bg-gray-700 rounded py-1">
                        <div class="text-gray-500">SR</div><div class="font-bold">{{ w.shortRange }}</div>
                    </div>
                    <div v-if="w.shortRangeDam != null" class="bg-gray-200 dark:bg-gray-700 rounded py-1">
                        <div class="text-gray-500">SR Dam</div><div class="font-bold">{{ w.shortRangeDam }}</div>
                    </div>
                    <div v-if="w.longRange != null" class="bg-gray-200 dark:bg-gray-700 rounded py-1">
                        <div class="text-gray-500">LR</div><div class="font-bold">{{ w.longRange }}</div>
                    </div>
                    <div v-if="w.longRangeDam != null" class="bg-gray-200 dark:bg-gray-700 rounded py-1">
                        <div class="text-gray-500">LR Dam</div><div class="font-bold">{{ w.longRangeDam }}</div>
                    </div>
                    <div class="bg-gray-200 dark:bg-gray-700 rounded py-1">
                        <div class="text-gray-500">Crit</div><div class="font-bold">{{ w.critFail }}</div>
                    </div>
                </div>
                <div v-if="w.weaponSpecialAbilities?.length" class="text-xs text-gray-500 dark:text-gray-400 truncate">
                    {{ w.weaponSpecialAbilities.map(a => a.name).join(', ') }}
                </div>
            </div>
        </div>

        <!-- Desktop table -->
        <div v-if="weapons.length" class="hidden md:block overflow-x-auto">
            <table class="w-full text-sm">
                <thead>
                    <tr class="bg-gray-200 dark:bg-gray-700 text-left text-gray-700 dark:text-gray-100">
                        <th class="px-3 py-2">Name</th>
                        <th class="px-3 py-2 text-center">CC Mod</th>
                        <th class="px-3 py-2 text-center">CC Dam</th>
                        <th class="px-3 py-2 text-center">SR</th>
                        <th class="px-3 py-2 text-center">SR Mod</th>
                        <th class="px-3 py-2 text-center">SR Dam</th>
                        <th class="px-3 py-2 text-center">LR</th>
                        <th class="px-3 py-2 text-center">LR Mod</th>
                        <th class="px-3 py-2 text-center">LR Dam</th>
                        <th class="px-3 py-2 text-center">Crit</th>
                        <th class="px-3 py-2">Abilities</th>
                        <th class="px-3 py-2 text-center">Actions</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="w in weapons" :key="w.id" class="border-b border-gray-200 dark:border-gray-700 hover:bg-gray-100 dark:hover:bg-gray-800">
                        <td class="px-3 py-2 font-medium">
                            <NuxtLink :to="`/weapons/${w.id}`" class="text-blue-400 hover:underline">{{ w.name }}</NuxtLink>
                        </td>
                        <td class="px-3 py-2 text-center">{{ w.ccMod ?? '—' }}</td>
                        <td class="px-3 py-2 text-center">{{ w.ccDam ?? '—' }}</td>
                        <td class="px-3 py-2 text-center">{{ w.shortRange ?? '—' }}</td>
                        <td class="px-3 py-2 text-center">{{ w.shortRangeMod ?? '—' }}</td>
                        <td class="px-3 py-2 text-center">{{ w.shortRangeDam ?? '—' }}</td>
                        <td class="px-3 py-2 text-center">{{ w.longRange ?? '—' }}</td>
                        <td class="px-3 py-2 text-center">{{ w.longRangeMod ?? '—' }}</td>
                        <td class="px-3 py-2 text-center">{{ w.longRangeDam ?? '—' }}</td>
                        <td class="px-3 py-2 text-center">{{ w.critFail }}</td>
                        <td class="px-3 py-2 text-xs text-gray-600 dark:text-gray-300">
                            {{ w.weaponSpecialAbilities?.map(a => a.name).join(', ') || '—' }}
                        </td>
                        <td class="px-3 py-2 text-center">
                            <RowActions v-if="isAdmin" @edit="startEdit(w)" @delete="confirmDelete(w)" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <p v-else-if="!loading" class="text-gray-400">No weapons found.</p>

        <!-- Create / Edit Modal -->
        <AppModal
            v-if="showCreate || editTarget"
            :title="editTarget ? 'Edit Weapon' : 'New Weapon'"
            @close="closeModal"
        >
            <form @submit.prevent="saveWeapon">
                <div class="grid grid-cols-1 sm:grid-cols-2 gap-3">
                    <FormField label="Name" :required="true" class="sm:col-span-2">
                        <input v-model="form.name" class="field-input" required />
                    </FormField>
                    <FormField label="CC Mod"><input v-model.number="form.ccMod" type="number" class="field-input" /></FormField>
                    <FormField label="CC Dam"><input v-model.number="form.ccDam" type="number" class="field-input" /></FormField>
                    <FormField label="Short Range"><input v-model.number="form.shortRange" type="number" class="field-input" /></FormField>
                    <FormField label="SR Mod"><input v-model.number="form.shortRangeMod" type="number" class="field-input" /></FormField>
                    <FormField label="SR Dam"><input v-model.number="form.shortRangeDam" type="number" class="field-input" /></FormField>
                    <FormField label="Long Range"><input v-model.number="form.longRange" type="number" class="field-input" /></FormField>
                    <FormField label="LR Mod"><input v-model.number="form.longRangeMod" type="number" class="field-input" /></FormField>
                    <FormField label="LR Dam"><input v-model.number="form.longRangeDam" type="number" class="field-input" /></FormField>
                    <FormField label="Crit Fail"><input v-model.number="form.critFail" type="number" class="field-input" /></FormField>
                    <div class="flex items-center gap-2 pt-4">
                        <input id="dynamicDam" v-model="form.dynamicDAM" type="checkbox" />
                        <label for="dynamicDam" class="field-label mb-0">Dynamic DAM</label>
                    </div>
                    <div class="flex items-center gap-2 pt-4">
                        <input id="dynamicRange" v-model="form.dynamicRange" type="checkbox" />
                        <label for="dynamicRange" class="field-label mb-0">Dynamic Range</label>
                    </div>
                    <FormField label="Special Abilities" class="sm:col-span-2">
                        <div class="border border-gray-300 dark:border-gray-600 rounded p-2 max-h-32 overflow-y-auto">
                            <label v-for="ability in allAbilities" :key="ability.id" class="flex items-center gap-2 py-1 cursor-pointer hover:bg-gray-100 dark:hover:bg-gray-700 px-1 rounded">
                                <input type="checkbox" :value="ability.id" v-model="form.weaponSpecialAbilityIds" />
                                <span class="text-sm">{{ ability.name }}</span>
                            </label>
                        </div>
                    </FormField>
                </div>
                <div class="flex justify-end gap-3 mt-5">
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
const { weapons, loading, error, fetchAll, create, update, remove } = useWeapons()
const { abilities: allAbilities, fetchAll: fetchAbilities } = useWeaponSpecialAbilities()

const showCreate = ref(false)
const editTarget = ref(null)
const deleteTarget = ref(null)
const saving = ref(false)

const emptyForm = () => ({
    name: '',
    ccMod: null,
    ccDam: null,
    shortRange: null,
    shortRangeMod: null,
    shortRangeDam: null,
    longRange: null,
    longRangeMod: null,
    longRangeDam: null,
    critFail: 20,
    dynamicDAM: false,
    dynamicRange: false,
    weaponSpecialAbilityIds: [],
})
const form = ref(emptyForm())

onMounted(async () => {
    await Promise.all([fetchAll(), fetchAbilities()])
})

function startEdit(w) {
    editTarget.value = w
    form.value = {
        name: w.name,
        ccMod: w.ccMod,
        ccDam: w.ccDam,
        shortRange: w.shortRange,
        shortRangeMod: w.shortRangeMod,
        shortRangeDam: w.shortRangeDam,
        longRange: w.longRange,
        longRangeMod: w.longRangeMod,
        longRangeDam: w.longRangeDam,
        critFail: w.critFail,
        dynamicDAM: w.dynamicDAM,
        dynamicRange: w.dynamicRange,
        weaponSpecialAbilityIds: w.weaponSpecialAbilities?.map(a => a.id) ?? [],
    }
}

function closeModal() {
    showCreate.value = false
    editTarget.value = null
    form.value = emptyForm()
}

async function saveWeapon() {
    saving.value = true
    try {
        if (editTarget.value) {
            await update(editTarget.value.id, form.value)
            await fetchAll()
        } else {
            await create(form.value)
        }
        closeModal()
    } catch (e) {
        alert('Failed to save weapon: ' + (e.response?.data || e.message))
    } finally {
        saving.value = false
    }
}

function confirmDelete(w) {
    deleteTarget.value = w
}

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
