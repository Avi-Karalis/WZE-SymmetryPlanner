<template>
    <div class="p-6 max-w-7xl mx-auto">
        <PageHeader title="Units">
            <button class="btn-primary" @click="showCreate = true">+ New Unit</button>
        </PageHeader>

        <LoadingError :loading="loading" :error="error" />

        <div class="flex gap-3 mb-4">
            <input v-model="filterText" class="field-input max-w-xs" placeholder="Filter by name or faction..." />
        </div>

        <table v-if="filteredUnits.length" class="w-full text-sm">
            <thead>
                <tr class="bg-gray-700 text-left">
                    <th class="px-3 py-2">Faction / Type</th>
                    <th class="px-3 py-2">Designation</th>
                    <th class="px-3 py-2 text-center">DP</th>
                    <th class="px-3 py-2 text-center">SP</th>
                    <th class="px-3 py-2 text-center">MV</th>
                    <th class="px-3 py-2 text-center">MW</th>
                    <th class="px-3 py-2 text-center">CC</th>
                    <th class="px-3 py-2 text-center">ST</th>
                    <th class="px-3 py-2 text-center">DEF</th>
                    <th class="px-3 py-2 text-center">AR</th>
                    <th class="px-3 py-2 text-center">W</th>
                    <th class="px-3 py-2 text-center">LD</th>
                    <th class="px-3 py-2">Weapons</th>
                    <th class="px-3 py-2 text-center">Actions</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="u in filteredUnits" :key="u.id" class="border-b border-gray-700 hover:bg-gray-800">
                    <td class="px-3 py-2">
                        <NuxtLink :to="`/units/${u.id}`" class="text-blue-400 hover:underline font-medium">{{ u.faction }} {{ u.unitType }}</NuxtLink>
                    </td>
                    <td class="px-3 py-2 text-xs text-gray-300">{{ u.designation?.join(', ') }}</td>
                    <td class="px-3 py-2 text-center">{{ u.dpCost }}</td>
                    <td class="px-3 py-2 text-center">{{ u.spCost }}</td>
                    <td class="px-3 py-2 text-center">{{ u.mv }}</td>
                    <td class="px-3 py-2 text-center">{{ u.mw }}</td>
                    <td class="px-3 py-2 text-center">{{ u.cc }}</td>
                    <td class="px-3 py-2 text-center">{{ u.st }}</td>
                    <td class="px-3 py-2 text-center">{{ u.def }}</td>
                    <td class="px-3 py-2 text-center">{{ u.ar }}</td>
                    <td class="px-3 py-2 text-center">{{ u.w }}</td>
                    <td class="px-3 py-2 text-center">{{ u.ld }}</td>
                    <td class="px-3 py-2 text-xs text-gray-300">{{ u.weapons?.map(w => w.name).join(', ') || '—' }}</td>
                    <td class="px-3 py-2 text-center">
                        <RowActions @edit="startEdit(u)" @delete="confirmDelete(u)" />
                    </td>
                </tr>
            </tbody>
        </table>
        <p v-else-if="!loading" class="text-gray-400">No units found.</p>

        <!-- Create / Edit Modal -->
        <AppModal
            v-if="showCreate || editTarget"
            :title="editTarget ? 'Edit Unit' : 'New Unit'"
            max-width="max-w-2xl"
            @close="closeModal"
        >
            <form @submit.prevent="saveUnit">
                <div class="grid grid-cols-2 gap-3">
                    <FormField label="Faction" :required="true"><input v-model="form.faction" class="field-input" required /></FormField>
                    <FormField label="Unit Type" :required="true"><input v-model="form.unitType" class="field-input" required /></FormField>
                    <FormField label="Designations (comma-separated)" :required="true" class="col-span-2">
                        <input v-model="designationInput" class="field-input" placeholder="e.g. Trooper, Leader" required />
                    </FormField>
                    <FormField label="Designation Type Limit">
                        <input v-model="form.designationTypeLimit" class="field-input" placeholder="e.g. Trooper or Any" />
                    </FormField>
                    <FormField label="Designation Limit Value">
                        <input v-model.number="form.designationLimitValue" type="number" class="field-input" />
                    </FormField>
                    <FormField label="DP Cost" :required="true"><input v-model.number="form.dpCost" type="number" class="field-input" required /></FormField>
                    <FormField label="SP Cost"><input v-model.number="form.spCost" type="number" class="field-input" /></FormField>
                    <FormField label="MV"><input v-model.number="form.mv" type="number" class="field-input" /></FormField>
                    <FormField label="MW"><input v-model.number="form.mw" type="number" class="field-input" /></FormField>
                    <FormField label="CC"><input v-model.number="form.cc" type="number" class="field-input" /></FormField>
                    <FormField label="ST"><input v-model.number="form.st" type="number" class="field-input" /></FormField>
                    <FormField label="DEF"><input v-model.number="form.def" type="number" class="field-input" /></FormField>
                    <FormField label="AR"><input v-model.number="form.ar" type="number" class="field-input" /></FormField>
                    <FormField label="W"><input v-model.number="form.w" type="number" class="field-input" /></FormField>
                    <FormField label="PW"><input v-model.number="form.pw" type="number" class="field-input" /></FormField>
                    <FormField label="LD"><input v-model.number="form.ld" type="number" class="field-input" /></FormField>
                    <FormField label="Base"><input v-model.number="form.base" type="number" class="field-input" /></FormField>
                    <FormField label="Faction Availabilities (comma-separated)" class="col-span-2">
                        <input v-model="factionAvailInput" class="field-input" placeholder="e.g. Capitol, Bauhaus" />
                    </FormField>
                    <FormField label="Weapons" class="col-span-2">
                        <div class="border border-gray-600 rounded p-2 max-h-32 overflow-y-auto">
                            <label v-for="w in allWeapons" :key="w.id" class="flex items-center gap-2 py-1 cursor-pointer hover:bg-gray-700 px-1 rounded">
                                <input type="checkbox" :value="w.id" v-model="form.weaponIds" />
                                <span class="text-sm">{{ w.name }}</span>
                            </label>
                        </div>
                    </FormField>
                    <FormField label="Unit Special Abilities" class="col-span-2">
                        <div class="border border-gray-600 rounded p-2 max-h-32 overflow-y-auto">
                            <label v-for="a in allUnitAbilities" :key="a.id" class="flex items-center gap-2 py-1 cursor-pointer hover:bg-gray-700 px-1 rounded">
                                <input type="checkbox" :value="a.id" v-model="form.unitSpecialAbilityIds" />
                                <span class="text-sm">{{ a.name }}</span>
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
            :name="`${deleteTarget.faction} ${deleteTarget.unitType}`"
            @confirm="doDelete"
            @cancel="deleteTarget = null"
        />
    </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'

const { units, loading, error, fetchAll, create, update, remove } = useUnits()
const { weapons: allWeapons, fetchAll: fetchWeapons } = useWeapons()
const { abilities: allUnitAbilities, fetchAll: fetchUnitAbilities } = useUnitSpecialAbilities()

const filterText = ref('')
const showCreate = ref(false)
const editTarget = ref(null)
const deleteTarget = ref(null)
const saving = ref(false)
const designationInput = ref('')
const factionAvailInput = ref('')

const filteredUnits = computed(() => {
    if (!filterText.value) return units.value
    const q = filterText.value.toLowerCase()
    return units.value.filter(u =>
        u.faction?.toLowerCase().includes(q) ||
        u.unitType?.toLowerCase().includes(q) ||
        u.designation?.some(d => d.toLowerCase().includes(q))
    )
})

const emptyForm = () => ({
    faction: '',
    unitType: '',
    designation: [],
    designationTypeLimit: '',
    designationLimitValue: 0,
    dpCost: 0,
    spCost: 0,
    mv: 0, mw: 0, cc: 0, st: 0, def: 0, ar: 0, w: 1, pw: 0, ld: 0, base: 25,
    weaponIds: [],
    unitSpecialAbilityIds: [],
    factionAvailabilities: [],
})
const form = ref(emptyForm())

onMounted(async () => {
    await Promise.all([fetchAll(), fetchWeapons(), fetchUnitAbilities()])
})

function startEdit(u) {
    editTarget.value = u
    designationInput.value = u.designation?.join(', ') ?? ''
    factionAvailInput.value = u.factionAvailabilities?.join(', ') ?? ''
    form.value = {
        faction: u.faction,
        unitType: u.unitType,
        designation: [...(u.designation ?? [])],
        designationTypeLimit: u.designationTypeLimit ?? '',
        designationLimitValue: u.designationLimitValue ?? 0,
        dpCost: u.dpCost, spCost: u.spCost,
        mv: u.mv, mw: u.mw, cc: u.cc, st: u.st, def: u.def,
        ar: u.ar, w: u.w, pw: u.pw, ld: u.ld, base: u.base,
        weaponIds: u.weapons?.map(w => w.id) ?? [],
        unitSpecialAbilityIds: u.unitSpecialAbilities?.map(a => a.id) ?? [],
        factionAvailabilities: [...(u.factionAvailabilities ?? [])],
    }
}

function closeModal() {
    showCreate.value = false
    editTarget.value = null
    designationInput.value = ''
    factionAvailInput.value = ''
    form.value = emptyForm()
}

async function saveUnit() {
    saving.value = true
    form.value.designation = designationInput.value.split(',').map(s => s.trim()).filter(Boolean)
    form.value.factionAvailabilities = factionAvailInput.value.split(',').map(s => s.trim()).filter(Boolean)
    try {
        if (editTarget.value) {
            await update(editTarget.value.id, form.value)
            await fetchAll()
        } else {
            await create(form.value)
        }
        closeModal()
    } catch (e) {
        alert('Failed to save unit: ' + (e.response?.data || e.message))
    } finally {
        saving.value = false
    }
}

function confirmDelete(u) { deleteTarget.value = u }

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
