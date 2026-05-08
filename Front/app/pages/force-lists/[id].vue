<template>
	<div class="p-4 sm:p-6 max-w-7xl mx-auto">
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
			<div class="flex flex-col gap-3 sm:flex-row sm:items-start sm:justify-between mb-4">
				<div class="flex-1 min-w-0">
					<h1 class="text-xl sm:text-2xl font-bold truncate">{{ forceList.name }}</h1>
					<div
						class="text-sm text-gray-400 mt-1 flex flex-wrap gap-3"
					>
						<span
							>Faction:
							<span class="text-gray-700 dark:text-gray-300">{{
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
						<span
							>SP:
							<span :class="spClass"
								>{{ spUsed }} / {{ spAvailable }}</span
							></span
						>
					</div>
				</div>
				<div class="flex gap-2 shrink-0">
					<button
						class="btn-validate flex-1 sm:flex-none"
						@click="runValidation"
						:disabled="validating"
					>
						{{ validating ? "Validating..." : "Validate List" }}
					</button>
					<button class="btn-secondary flex-1 sm:flex-none" @click="showRoster = true">View Roster</button>
				</div>
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
									<span v-if="unit.designationTypeLimit" class="ml-1 text-xs text-yellow-500 dark:text-yellow-300">({{ unit.designationLimitValue }}: {{ unit.designationTypeLimit }})</span>
								</div>
								<div class="flex items-center gap-3">
								<span class="text-xs bg-gray-300 dark:bg-gray-700 px-2 py-0.5 rounded">{{ unit.dpCost }} DP</span>
								<span v-if="unit.spCost > 0" class="text-xs bg-green-100 dark:bg-green-900/40 text-green-700 dark:text-green-300 px-2 py-0.5 rounded">+{{ unit.spCost }} SP</span>
							<span v-if="unit.spCost < 0" class="text-xs bg-yellow-100 dark:bg-yellow-900/40 text-yellow-700 dark:text-yellow-300 px-2 py-0.5 rounded">{{ -unit.spCost }} SP cost</span>
									<button
										class="btn-sm btn-danger transition opacity-100 sm:opacity-0 sm:group-hover:opacity-100"
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
									class="grid grid-cols-5 sm:grid-cols-9 gap-1 text-center text-xs mb-2"
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
									class="text-xs text-gray-600 dark:text-gray-300 mb-1"
								>
									<span class="text-gray-500 dark:text-gray-400">Weapons: </span
									>{{
										unit.weapons
											.map((w) => w.name)
											.join(", ")
									}}
								</div>
								<div
									v-if="unit.unitSpecialAbilities?.length"
									class="text-xs text-gray-600 dark:text-gray-300"
								>
									<span class="text-gray-500 dark:text-gray-400"
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
					<div class="flex flex-col sm:flex-row gap-2 mb-3">
						<input
							v-model="unitSearch"
							class="field-input text-sm flex-1"
							placeholder="Search..."
						/>
						<select
							v-model="filterDesignation"
							class="field-input text-sm sm:max-w-36"
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
								<span v-if="unit.designationTypeLimit" class="ml-1 text-xs text-yellow-500 dark:text-yellow-300">({{ unit.designationLimitValue }}: {{ unit.designationTypeLimit }})</span>
								</div>
								<div class="flex items-center gap-2">
								<span class="text-xs bg-gray-300 dark:bg-gray-700 px-2 py-0.5 rounded">{{ unit.dpCost }} DP</span>
								<span v-if="unit.spCost > 0" class="text-xs bg-green-100 dark:bg-green-900/40 text-green-700 dark:text-green-300 px-2 py-0.5 rounded">+{{ unit.spCost }} SP</span>
								<span v-if="unit.spCost < 0" class="text-xs bg-yellow-100 dark:bg-yellow-900/40 text-yellow-700 dark:text-yellow-300 px-2 py-0.5 rounded">{{ unit.spCost }} SP</span>
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

		<!-- Roster View Modal -->
		<Teleport to="body">
			<div v-if="showRoster" class="fixed inset-0 z-50 flex items-start justify-center bg-black/70 overflow-y-auto py-8">
				<div id="roster-print-area" class="bg-white dark:bg-gray-900 text-gray-800 dark:text-gray-100 rounded-xl w-full max-w-4xl mx-4 p-6 shadow-2xl">
					<div class="flex items-center justify-between mb-6">
						<h2 class="text-xl font-bold">{{ forceList?.name }} — Roster</h2>
						<div class="flex items-center gap-3">
							<button class="btn-primary text-sm" @click="printRoster">Save as PDF</button>
							<button class="text-gray-400 hover:text-gray-700 dark:hover:text-white text-2xl leading-none" @click="showRoster = false">✕</button>
						</div>
					</div>

					<div class="flex flex-col gap-6">
						<div v-for="unit in forceList.units" :key="unit.id" class="border border-gray-200 dark:border-gray-700 rounded-lg p-4">
							<!-- Unit header -->
							<div class="flex items-center justify-between mb-3">
								<div>
									<span class="font-bold text-base">{{ unit.faction }} {{ unit.unitType }}</span>
									<span class="ml-2 text-xs text-gray-500 dark:text-gray-400">{{ unit.designation?.join(', ') }}</span>								<span v-if="unit.designationTypeLimit" class="ml-1 text-xs text-yellow-600 dark:text-yellow-300">({{ unit.designationLimitValue }}: {{ unit.designationTypeLimit }})</span>								</div>
								<div class="flex gap-2">
									<span class="text-xs bg-gray-200 dark:bg-gray-700 px-2 py-0.5 rounded">{{ unit.dpCost }} DP</span>
									<span v-if="unit.spCost > 0" class="text-xs bg-green-100 dark:bg-green-900/40 text-green-700 dark:text-green-300 px-2 py-0.5 rounded">+{{ unit.spCost }} SP</span>
								<span v-if="unit.spCost < 0" class="text-xs bg-yellow-100 dark:bg-yellow-900/40 text-yellow-700 dark:text-yellow-300 px-2 py-0.5 rounded">{{ -unit.spCost }} SP cost</span>
								</div>
							</div>

							<!-- Stat grid -->
							<div class="grid grid-cols-5 sm:grid-cols-9 gap-1 text-center text-xs mb-4">
								<div v-for="stat in ['MV','MW','CC','ST','DEF','AR','W','PW','LD']" :key="stat" class="bg-gray-100 dark:bg-gray-800 rounded py-1">
									<div class="text-gray-500 dark:text-gray-400">{{ stat }}</div>
									<div class="font-bold">{{ unit[stat.toLowerCase()] }}</div>
								</div>
							</div>

							<!-- Weapons -->
							<div v-if="unit.weapons?.length" class="mb-3">
								<div class="text-xs font-semibold text-gray-500 dark:text-gray-400 uppercase tracking-wider mb-2">Weapons</div>
								<div class="flex flex-col gap-2">
									<div v-for="w in unit.weapons" :key="w.id" class="bg-gray-50 dark:bg-gray-800 rounded p-2 text-xs">
										<div class="font-semibold mb-1">{{ w.name }}<span v-if="w.dynamicDAM || w.dynamicRange" class="ml-2 text-yellow-600 dark:text-yellow-300">({{ [w.dynamicDAM ? 'Dynamic DAM' : '', w.dynamicRange ? 'Dynamic Range' : ''].filter(Boolean).join(' · ') }})</span></div>
										<div class="flex flex-col gap-0.5">
											<template v-if="w.ccMod != null || w.ccDam != null">
												<div class="flex gap-4">
													<span><span class="text-gray-500 dark:text-gray-400">CC Mod </span>{{ w.ccMod ?? '—' }}</span>
													<span><span class="text-gray-500 dark:text-gray-400">CC DAM </span>{{ w.ccDam != null ? (w.dynamicDAM ? w.ccDam + unit.st : w.ccDam) : '—' }}</span>
													<span><span class="text-gray-500 dark:text-gray-400">Crit Fail </span>{{ w.critFail }}</span>
												</div>
											</template>
											<template v-if="w.shortRange != null">
												<div class="flex gap-4">
													<span><span class="text-gray-500 dark:text-gray-400">SR </span>{{ w.dynamicRange ? w.shortRange + unit.st : w.shortRange }}</span>
													<span><span class="text-gray-500 dark:text-gray-400">SR Mod </span>{{ w.shortRangeMod ?? '—' }}</span>
													<span><span class="text-gray-500 dark:text-gray-400">SR DAM </span>{{ w.shortRangeDam != null ? (w.dynamicDAM ? w.shortRangeDam + unit.st : w.shortRangeDam) : '—' }}</span>
												</div>
											</template>
											<template v-if="w.longRange != null">
												<div class="flex gap-4">
													<span><span class="text-gray-500 dark:text-gray-400">LR </span>{{ w.dynamicRange ? w.longRange + unit.st : w.longRange }}</span>
													<span><span class="text-gray-500 dark:text-gray-400">LR Mod </span>{{ w.longRangeMod ?? '—' }}</span>
													<span><span class="text-gray-500 dark:text-gray-400">LR DAM </span>{{ w.longRangeDam != null ? (w.dynamicDAM ? w.longRangeDam + unit.st : w.longRangeDam) : '—' }}</span>
												</div>
											</template>
											<template v-if="w.ccMod == null && w.ccDam == null">
												<div><span class="text-gray-500 dark:text-gray-400">Crit Fail </span>{{ w.critFail }}</div>
											</template>
										</div>
									<div v-if="w.weaponSpecialAbilities?.length" class="mt-2">
										<div class="flex flex-wrap gap-1 mb-1">
											<span v-for="sa in w.weaponSpecialAbilities" :key="sa.id" class="sa-badge">
												{{ sa.name }}<span v-if="sa.valueX" class="text-yellow-600 dark:text-yellow-300 ml-1">({{ sa.valueX }})</span>
											</span>
										</div>
										<div v-for="sa in w.weaponSpecialAbilities" :key="'wdesc-'+sa.id" class="text-gray-500 dark:text-gray-400 italic" style="font-size:10px; line-height:1.3;">
											<span class="font-semibold not-italic text-gray-700 dark:text-gray-300">{{ sa.name }}<span v-if="sa.valueX"> ({{ sa.valueX }})</span>:</span> {{ sa.description }}
										</div>
										</div>
									</div>
								</div>
							</div>

							<!-- Special Abilities -->
							<div v-if="unit.unitSpecialAbilities?.length">
								<div class="text-xs font-semibold text-gray-500 dark:text-gray-400 uppercase tracking-wider mb-2">Special Abilities</div>
								<div class="flex flex-wrap gap-1 mb-1">
									<span v-for="sa in unit.unitSpecialAbilities" :key="sa.id" class="sa-badge">
										{{ sa.name }}<span v-if="sa.valueX || sa.valueY" class="text-yellow-600 dark:text-yellow-300 ml-1">({{ [sa.valueX, sa.valueY].filter(Boolean).join(', ') }})</span>
									</span>
								</div>
								<div v-for="sa in unit.unitSpecialAbilities" :key="'desc-'+sa.id" class="text-gray-500 dark:text-gray-400 italic" style="font-size:10px; line-height:1.3;">
									<span class="font-semibold not-italic text-gray-700 dark:text-gray-300">{{ sa.name }}<span v-if="sa.valueX || sa.valueY"> ({{ [sa.valueX, sa.valueY].filter(Boolean).join(', ') }})</span>:</span> {{ sa.description }}
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
		</Teleport>
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
const showRoster = ref(false)

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
const spAvailable = computed(() => forceList.value?.units?.reduce((s, u) => s + (u.spCost > 0 ? u.spCost : 0), 0) ?? 0)
const spUsed = computed(() => forceList.value?.units?.reduce((s, u) => s + (u.spCost < 0 ? -u.spCost : 0), 0) ?? 0)

const dpClass = computed(() => currentDp.value > (forceList.value?.maxDp ?? 0) ? 'text-red-400 font-bold' : 'text-green-400')
const spClass = computed(() => spUsed.value > spAvailable.value ? 'text-red-400 font-bold' : 'text-yellow-400')
const dpBarClass = computed(() => currentDp.value > (forceList.value?.maxDp ?? 0) ? 'bg-red-500' : 'bg-blue-500')
const allegianceClass = computed(() => {
  const a = forceList.value?.allegiance?.toLowerCase() ?? ''
  return a.includes('darkness') ? 'text-red-400' : 'text-yellow-400'
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

function printRoster() {
  const el = document.getElementById('roster-print-area')
  if (!el) return
  const clone = el.cloneNode(true)
  // Remove buttons from clone
  clone.querySelectorAll('button').forEach(b => b.remove())
  const win = window.open('', '_blank')
  win.document.write(`
    <!DOCTYPE html>
    <html>
    <head>
      <title>${forceList.value?.name ?? 'Roster'} - Roster</title>
      <style>
        body { font-family: system-ui, sans-serif; font-size: 12px; color: #111; margin: 24px; }
        h2 { font-size: 18px; margin-bottom: 16px; }
        .border { border: 1px solid #ccc; border-radius: 6px; padding: 12px; margin-bottom: 12px; }
        .font-bold { font-weight: bold; }
        .font-semibold { font-weight: 600; }
        .text-gray-500, .text-gray-400 { color: #6b7280; }
        .flex { display: flex; }
        .gap-4 { gap: 16px; }
        .gap-2 { gap: 8px; }
        .gap-1 { gap: 4px; }
        .grid { display: grid; }
        .grid-cols-5 { grid-template-columns: repeat(5, minmax(0, 1fr)); }
        .grid-cols-9 { grid-template-columns: repeat(9, minmax(0, 1fr)); }
        .grid > div { white-space: nowrap; }
        .text-center { text-align: center; }
        .rounded { border-radius: 4px; }
        .bg-gray-100 { background: #f3f4f6; }
        .py-1 { padding-top: 2px; padding-bottom: 2px; }
        .mb-1 { margin-bottom: 4px; }
        .mb-2 { margin-bottom: 8px; }
        .mb-3 { margin-bottom: 12px; }
        .mb-4 { margin-bottom: 16px; }
        .mb-6 { margin-bottom: 24px; }
        .mt-1 { margin-top: 4px; }
        .px-2 { padding-left: 8px; padding-right: 8px; }
        .py-0\.5 { padding-top: 2px; padding-bottom: 2px; }
        .text-xs { font-size: 11px; }
        .uppercase { text-transform: uppercase; }
        .tracking-wider { letter-spacing: 0.05em; }
        .flex-col { flex-direction: column; }
        .flex-wrap { flex-wrap: wrap; }
        .items-center { align-items: center; }
        .justify-between { justify-content: space-between; }
        .border-gray-200 { border-color: #e5e7eb; }
        .border-gray-700 { border-color: #374151; }
        .bg-gray-50 { background: #f9fafb; }
        .gap-0\.5 { gap: 2px; }
        .p-2 { padding: 8px; }
        .p-4 { padding: 16px; }
        .p-6 { padding: 24px; }
        .text-yellow-600 { color: #d97706; }
        .ml-1 { margin-left: 4px; }
        .ml-2 { margin-left: 8px; }
        .ml-2 { margin-left: 8px; }
      </style>
    </head>
    <body>${clone.innerHTML}</body>
    </html>
  `)
  win.document.close()
  win.focus()
  setTimeout(() => { win.print(); win.close() }, 300)
}
</script>

<style scoped>
@reference "../../../assets/css/main.css";
.unit-card {
  @apply bg-gray-100 border border-gray-300 text-gray-800 rounded-lg p-3 cursor-pointer hover:border-gray-400 transition
         dark:bg-gray-800 dark:border-gray-700 dark:text-gray-100 dark:hover:border-gray-500;
}
.unit-card-available {
  @apply bg-gray-100 border border-gray-300 text-gray-800 rounded px-3 py-2 hover:border-green-500 transition
         dark:bg-gray-800 dark:border-gray-700 dark:text-gray-100 dark:hover:border-green-600;
}

.sa-badge {
  position: relative;
  @apply bg-gray-200 dark:bg-gray-600 rounded px-1.5 py-0.5 text-xs cursor-default;
}

.sa-badge::after {
  content: attr(data-tooltip);
  position: absolute;
  bottom: calc(100% + 6px);
  left: 50%;
  transform: translateX(-50%);
  width: 14rem;
  white-space: normal;
  text-align: center;
  @apply bg-gray-900 text-gray-100 text-xs rounded px-2 py-1 z-50;
  opacity: 0;
  pointer-events: none;
  transition: opacity 0.15s;
}

.sa-badge:hover::after {
  opacity: 1;
}
</style>

