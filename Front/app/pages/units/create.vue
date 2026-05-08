<template>
    <div class="p-6 max-w-3xl mx-auto">
        <div class="mb-4">
            <NuxtLink to="/units" class="text-gray-400 hover:text-white text-sm">← Units</NuxtLink>
        </div>
        <h1 class="text-2xl font-bold text-gray-800 dark:text-gray-100 mb-6">New Unit</h1>

        <form @submit.prevent="saveUnit" class="bg-gray-100 dark:bg-gray-800 rounded-lg p-6">
            <div class="grid grid-cols-2 gap-3">
                <FormField label="Faction" :required="true">
                    <input v-model="form.faction" class="field-input" required />
                </FormField>
                <FormField label="Unit Type" :required="true">
                    <input v-model="form.unitType" class="field-input" required />
                </FormField>
                <FormField label="Designations (comma-separated)" :required="true" class="col-span-2">
                    <input v-model="designationInput" class="field-input" placeholder="e.g. Trooper, Leader" required />
                </FormField>
                <FormField label="Designation Type Limit (comma-separated for multiple)">
                    <input v-model="form.designationTypeLimit" class="field-input" placeholder="e.g. Trooper  or  Undead Legionnaire, Necromutant" />
                </FormField>
                <FormField label="Designation Limit Value">
                    <input v-model.number="form.designationLimitValue" type="number" class="field-input" />
                </FormField>
                <FormField label="DP Cost" :required="true">
                    <input v-model.number="form.dpCost" type="number" class="field-input" required />
                </FormField>
                <FormField label="SP Cost">
                    <input v-model.number="form.spCost" type="number" class="field-input" />
                </FormField>
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
                    <div class="border border-gray-300 dark:border-gray-600 rounded p-2 max-h-48 overflow-y-auto bg-white dark:bg-gray-700">
                        <label v-for="w in sortedWeapons" :key="w.id" class="flex items-center gap-2 py-1 cursor-pointer hover:bg-gray-100 dark:hover:bg-gray-600 px-1 rounded">
                            <input type="checkbox" :value="w.id" v-model="form.weaponIds" />
                            <span class="text-sm text-gray-800 dark:text-gray-100">{{ weaponLabel(w) }}</span>
                        </label>
                    </div>
                </FormField>
                <FormField label="Unit Special Abilities" class="col-span-2">
                    <div class="border border-gray-300 dark:border-gray-600 rounded p-2 max-h-48 overflow-y-auto bg-white dark:bg-gray-700">
                        <label v-for="a in sortedAbilities" :key="a.id" class="flex items-center gap-2 py-1 cursor-pointer hover:bg-gray-100 dark:hover:bg-gray-600 px-1 rounded">
                            <input type="checkbox" :value="a.id" v-model="form.unitSpecialAbilityIds" />
                            <span class="text-sm text-gray-800 dark:text-gray-100">{{ abilityLabel(a) }}</span>
                        </label>
                    </div>
                </FormField>
            </div>
            <div class="flex justify-end gap-3 mt-6">
                <NuxtLink to="/units" class="btn-secondary">Cancel</NuxtLink>
                <button type="submit" class="btn-primary" :disabled="saving">{{ saving ? 'Saving...' : 'Create Unit' }}</button>
            </div>
        </form>
    </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'

const router = useRouter()
const { create } = useUnits()
const { weapons: allWeapons, fetchAll: fetchWeapons } = useWeapons()
const { abilities: allUnitAbilities, fetchAll: fetchUnitAbilities } = useUnitSpecialAbilities()

const saving = ref(false)
const designationInput = ref('')
const factionAvailInput = ref('')

const form = ref({
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

const sortedWeapons = computed(() =>
    [...(allWeapons.value ?? [])].sort((a, b) => a.name.localeCompare(b.name))
)

const sortedAbilities = computed(() =>
    [...(allUnitAbilities.value ?? [])].sort((a, b) => {
        const nameCompare = a.name.localeCompare(b.name)
        if (nameCompare !== 0) return nameCompare
        return (a.valueX ?? '').toString().localeCompare((b.valueX ?? '').toString())
    })
)

function weaponLabel(w) {
    const sas = w.weaponSpecialAbilities?.map(s => s.name).join(', ')
    return sas ? `${w.name} (${sas})` : w.name
}

function abilityLabel(a) {
    const parts = [a.valueX, a.valueY].filter(Boolean).join(', ')
    return parts ? `${a.name} (${parts})` : a.name
}

onMounted(async () => {
    await Promise.all([fetchWeapons(), fetchUnitAbilities()])
})

async function saveUnit() {
    saving.value = true
    form.value.designation = designationInput.value.split(',').map(s => s.trim()).filter(Boolean)
    form.value.factionAvailabilities = factionAvailInput.value.split(',').map(s => s.trim()).filter(Boolean)
    try {
        await create(form.value)
        router.push('/units')
    } catch (e) {
        alert('Failed to create unit: ' + (e.response?.data || e.message))
    } finally {
        saving.value = false
    }
}

definePageMeta({ layout: 'dark' })
</script>
