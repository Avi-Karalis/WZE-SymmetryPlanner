<template>
	<div class="p-6 max-w-7xl mx-auto">
		<!-- Header -->
		<div class="flex items-center gap-3 mb-2">
			<NuxtLink
				to="/force-lists"
				class="text-gray-400 hover:text-white text-sm"
				>← Force Lists</NuxtLink
			>
		</div>

		<div
			v-if="loading && !forceList"
			class="text-center py-16 text-gray-400"
		>
			Loading force list...
		</div>
		<LoadingError :error="loadError" />

		<template v-if="forceList">
			<!-- Title bar -->
			<div class="flex items-start justify-between mb-4">
				<div>
					<h1 class="text-2xl font-bold">{{ forceList.name }}</h1>
					<div
						class="text-sm text-gray-400 mt-1 flex flex-wrap gap-4"
					>
						<span
							>Faction:
							<span class="text-gray-200">{{
								forceList.faction
							}}</span></span
						>
						<span
							>Allegiance:
							<span :class="allegianceClass">{{
								forceList.allegiance
							}}</span></span
						>
						<span
							>DP:
							<span :class="dpClass"
								>{{ currentDp }} / {{ forceList.maxDp }}</span
							></span
						>
						<span v-if="forceList.maxSp > 0"
							>SP:
							<span :class="spClass"
								>{{ currentSp }} / {{ forceList.maxSp }}</span
							></span
						>
					</div>
				</div>
				<button
					class="btn-validate"
					@click="runValidation"
					:disabled="validating"
				>
					{{ validating ? "Validating..." : "Validate List" }}
				</button>
			</div>

			<!-- DP progress bar -->
			<div class="w-full bg-gray-700 rounded-full h-2 mb-6">
				<div
					class="h-2 rounded-full transition-all"
					:class="dpBarClass"
					:style="{
						width:
							Math.min(100, (currentDp / forceList.maxDp) * 100) +
							'%',
					}"
				></div>
			</div>

			<!-- Validation result -->
			<div
				v-if="validationResult"
				class="mb-6 rounded-lg p-4"
				:class="
					validationResult.isValid
						? 'bg-green-900 border border-green-600'
						: 'bg-red-900 border border-red-600'
				"
			>
				<div class="font-semibold mb-1">
					{{
						validationResult.isValid
							? "✓ Force List is Valid"
							: "✗ Validation Failed"
					}}
				</div>
				<ul
					v-if="validationResult.errors?.length"
					class="list-disc list-inside text-sm space-y-1"
				>
					<li v-for="err in validationResult.errors" :key="err">
						{{ err }}
					</li>
				</ul>
			</div>

			<div class="grid grid-cols-1 lg:grid-cols-2 gap-6">
				<!-- LEFT: Current roster -->
				<div>
					<h2 class="text-lg font-semibold mb-3">
						Roster
						<span class="text-gray-400 text-sm"
							>({{ forceList.units?.length ?? 0 }} units)</span
						>
					</h2>

					<p
						v-if="!forceList.units?.length"
						class="text-gray-500 text-sm"
					>
						No units added yet.
					</p>

					<div class="flex flex-col gap-2">
						<div
							v-for="unit in forceList.units"
							:key="unit.id"
							class="unit-card group"
							@click="
								selectedUnit =
									selectedUnit?.id === unit.id ? null : unit
							"
						>
							<div class="flex items-center justify-between">
								<div>
									<span class="font-medium"
										>{{ unit.faction }}
										{{ unit.unitType }}</span
									>
									<span class="ml-2 text-xs text-gray-400">{{
										unit.designation?.join(", ")
									}}</span>
								</div>
								<div class="flex items-center gap-3">
									<span class="text-xs bg-gray-700 px-2 py-0.5 rounded">{{ unit.dpCost }} DP</span>
									<span v-if="unit.spCost" class="text-xs bg-gray-700 px-2 py-0.5 rounded text-yellow-300">{{ unit.spCost }} SP</span>
									<button
										class="btn-sm btn-danger opacity-0 group-hover:opacity-100 transition"
										@click.stop="removeUnit(unit)"
									>
										Remove
									</button>
								</div>
							</div>

							<!-- Stat grid when selected -->
							<div
								v-if="selectedUnit?.id === unit.id"
								class="mt-3 border-t border-gray-600 pt-3"
							>
								<div
									class="grid grid-cols-9 gap-1 text-center text-xs mb-2"
								>
									<div
										v-for="stat in [
											'MV',
											'MW',
											'CC',
											'ST',
											'DEF',
											'AR',
											'W',
											'PW',
											'LD',
										]"
										:key="stat"
									>
										<div class="text-gray-400">
											{{ stat }}
										</div>
										<div class="font-bold">
											{{ unit[stat.toLowerCase()] }}
										</div>
									</div>
								</div>
								<div
									v-if="unit.weapons?.length"
									class="text-xs text-gray-300 mb-1"
								>
									<span class="text-gray-400">Weapons: </span
									>{{
										unit.weapons
											.map((w) => w.name)
											.join(", ")
									}}
								</div>
								<div
									v-if="unit.unitSpecialAbilities?.length"
									class="text-xs text-gray-300"
								>
									<span class="text-gray-400"
										>Abilities: </span
									>{{
										unit.unitSpecialAbilities
											.map((a) => a.name)
											.join(", ")
									}}
								</div>
							</div>
						</div>
					</div>
				</div>

				<!-- RIGHT: Available units to add -->
				<div>
					<h2 class="text-lg font-semibold mb-2">Available Units</h2>
					<div class="flex gap-2 mb-3">
						<input
							v-model="unitSearch"
							class="field-input text-sm"
							placeholder="Search..."
						/>
						<select
							v-model="filterDesignation"
							class="field-input text-sm max-w-36"
						>
							<option value="">All</option>
							<option value="Trooper">Troopers</option>
							<option value="Leader">Leaders</option>
							<option value="Specialist">Specialists</option>
							<option value="Support">Support</option>
							<option value="Unique">Unique</option>
							<option value="Ally">Allies</option>
						</select>
					</div>

					<div v-if="loadingUnits" class="text-gray-400 text-sm">
						Loading units...
					</div>

					<div
						class="flex flex-col gap-1 max-h-[60vh] overflow-y-auto pr-1"
					>
						<div
							v-for="unit in filteredAvailable"
							:key="unit.id"
							class="unit-card-available group cursor-pointer"
							@click="addUnit(unit)"
						>
							<div class="flex items-center justify-between">
								<div>
									<span class="font-medium text-sm"
										>{{ unit.faction }}
										{{ unit.unitType }}</span
									>
									<span
										class="ml-2 text-xs"
										:class="allyBadgeClass(unit)"
										>{{
											unit.designation?.join(", ")
										}}</span
									>
								</div>
								<div class="flex items-center gap-2">
									<span class="text-xs bg-gray-700 px-2 py-0.5 rounded">{{ unit.dpCost }} DP</span>
									<span v-if="unit.spCost" class="text-xs bg-gray-700 px-2 py-0.5 rounded text-yellow-300">{{ unit.spCost }} SP</span>
									<span
										class="text-xs text-green-400 opacity-0 group-hover:opacity-100 transition"
										>+ Add</span
									>
								</div>
							</div>
						</div>
						<p
							v-if="!loadingUnits && !filteredAvailable.length"
							class="text-gray-500 text-sm"
						>
							No units match the filter.
						</p>
					</div>
					</div>
			</div>
		</template>
	</div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'
import { useRoute } from 'vue-router'

const route = useRoute()
const { getById, addUnit: apiAddUnit, removeUnit: apiRemoveUnit, validate, loading } = useForceLists()
const { units: allUnits, fetchAll: fetchAllUnits } = useUnits()

const forceList = ref(null)
const loadError = ref(null)
const selectedUnit = ref(null)
const validationResult = ref(null)
const validating = ref(false)
const loadingUnits = ref(false)
const unitSearch = ref('')
const filterDesignation = ref('')

onMounted(async () => {
  await loadForceList()
  loadingUnits.value = true
  try {
    await fetchAllUnits()
  } finally {
    loadingUnits.value = false
  }
})

async function loadForceList() {
  forceList.value = await getById(route.params.id)
  if (!forceList.value) loadError.value = 'Force list not found.'
}

const currentDp = computed(() => forceList.value?.units?.reduce((s, u) => s + (u.dpCost ?? 0), 0) ?? 0)
const currentSp = computed(() => forceList.value?.units?.reduce((s, u) => s + (u.spCost > 0 ? u.spCost : 0), 0) ?? 0)

const dpClass = computed(() => currentDp.value > (forceList.value?.maxDp ?? 0) ? 'text-red-400 font-bold' : 'text-green-400')
const spClass = computed(() => currentSp.value > (forceList.value?.maxSp ?? 0) ? 'text-red-400 font-bold' : 'text-yellow-300')
const dpBarClass = computed(() => currentDp.value > (forceList.value?.maxDp ?? 0) ? 'bg-red-500' : 'bg-blue-500')
const allegianceClass = computed(() => {
  const a = forceList.value?.allegiance?.toLowerCase() ?? ''
  return a.includes('darkness') ? 'text-red-400' : 'text-yellow-300'
})

const ALLY_DESIGNATIONS = ['Advisor', 'Seconding', 'Dark Cult']

function isAlly(unit) {
  return unit.designation?.some((d) => ALLY_DESIGNATIONS.some((a) => d.toLowerCase() === a.toLowerCase()))
}

function allyBadgeClass(unit) {
  if (isAlly(unit)) return 'text-purple-300'
  if (unit.designation?.some((d) => d.toLowerCase() === 'leader')) return 'text-yellow-300'
  if (unit.designation?.some((d) => d.toLowerCase() === 'specialist')) return 'text-blue-300'
  return 'text-gray-400'
}

const allegianceType = computed(() => {
  const a = forceList.value?.allegiance?.toLowerCase() ?? ''
  if (a.includes('darkness')) return 1
  return 0
})

const availableUnits = computed(() => {
  if (!forceList.value || !allUnits.value.length) return []
  const faction = forceList.value.faction
  return allUnits.value.filter((unit) => {
    if (unit.faction?.toLowerCase() === faction?.toLowerCase()) return true
    if (!isAlly(unit)) return false
    const hasDarkCult = unit.designation?.some((d) => d.toLowerCase() === 'dark cult')
    const hasSeconding = unit.designation?.some((d) => d.toLowerCase() === 'seconding')
    const hasAdvisor = unit.designation?.some((d) => d.toLowerCase() === 'advisor')
    if (allegianceType.value === 1) return hasDarkCult
    if (allegianceType.value === 0) return hasSeconding || hasAdvisor
    return false
  })
})

const filteredAvailable = computed(() => {
  let list = availableUnits.value
  if (unitSearch.value) {
    const q = unitSearch.value.toLowerCase()
    list = list.filter((u) => u.unitType?.toLowerCase().includes(q) || u.faction?.toLowerCase().includes(q))
  }
  if (filterDesignation.value) {
    if (filterDesignation.value === 'Ally') {
      list = list.filter(isAlly)
    } else {
      const d = filterDesignation.value.toLowerCase()
      list = list.filter((u) => u.designation?.some((des) => des.toLowerCase() === d))
    }
  }
  return list
})

async function addUnit(unit) {
  try {
    await apiAddUnit(forceList.value.id, unit.id)
    await loadForceList()
    validationResult.value = null
  } catch (e) {
    alert('Failed to add unit: ' + (e.response?.data || e.message))
  }
}

async function removeUnit(unit) {
  try {
    await apiRemoveUnit(forceList.value.id, unit.id)
    if (selectedUnit.value?.id === unit.id) selectedUnit.value = null
    await loadForceList()
    validationResult.value = null
  } catch (e) {
    alert('Failed to remove unit: ' + (e.response?.data || e.message))
  }
}

async function runValidation() {
  validating.value = true
  try {
    validationResult.value = await validate(forceList.value.id)
  } catch (e) {
    alert('Validation failed: ' + (e.response?.data || e.message))
  } finally {
    validating.value = false
  }
}

definePageMeta({ layout: 'dark' })
</script>

<style scoped>
@reference "../../../assets/css/main.css";
.unit-card {
  @apply bg-gray-800 border border-gray-700 rounded-lg p-3 cursor-pointer hover:border-gray-500 transition;
}
.unit-card-available {
  @apply bg-gray-800 border border-gray-700 rounded px-3 py-2 hover:border-green-600 transition;
}
</style>

