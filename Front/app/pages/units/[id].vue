<template>
  <div class="p-6 max-w-2xl mx-auto">
    <div class="mb-4">
      <NuxtLink to="/units" class="text-gray-400 hover:text-white text-sm">← Units</NuxtLink>
    </div>

    <div v-if="loading" class="text-gray-400">Loading...</div>
    <div v-if="error" class="text-red-400">{{ error }}</div>

    <div v-if="unit" class="bg-gray-100 dark:bg-gray-800 rounded-lg p-6 text-gray-800 dark:text-gray-100">
      <h1 class="text-2xl font-bold">{{ unit.faction }} {{ unit.unitType }}</h1>
      <p class="text-gray-400 text-sm mt-1 mb-4">
        {{ unit.designation?.join(', ') }}
        <span v-if="unit.designationTypeLimit" class="ml-2 text-yellow-600 dark:text-yellow-300">({{ unit.designationLimitValue }}: {{ unit.designationTypeLimit }})</span>
      </p>

      <!-- Stat grid -->
      <div class="grid grid-cols-9 gap-2 text-center text-sm mb-6">
        <div v-for="stat in ['MV','MW','CC','ST','DEF','AR','W','PW','LD']" :key="stat" class="bg-gray-200 dark:bg-gray-900 rounded py-1">
          <div class="text-gray-400 text-xs">{{ stat }}</div>
          <div class="font-bold text-base text-gray-900 dark:text-white">{{ unit[stat.toLowerCase()] }}</div>
        </div>
      </div>

      <!-- Cost badges -->
      <div class="flex gap-3 mb-6">
        <span class="bg-gray-200 dark:bg-gray-700 px-3 py-1 rounded text-sm">{{ unit.dpCost }} DP</span>
        <span v-if="unit.spCost" class="bg-gray-200 dark:bg-gray-700 px-3 py-1 rounded text-sm text-yellow-600 dark:text-yellow-300">{{ unit.spCost }} SP</span>
      </div>

      <!-- Weapons -->
      <div class="mb-6">
        <h2 class="text-sm font-semibold text-gray-400 uppercase tracking-wider mb-2">Weapons</h2>
        <div v-if="unit.weapons?.length" class="flex flex-col gap-2">
          <div
            v-for="w in unit.weapons"
            :key="w.id"
            class="bg-gray-200 dark:bg-gray-700 rounded text-sm cursor-pointer select-none"
            @click="expandedWeapon = expandedWeapon === w.id ? null : w.id"
          >
            <!-- Weapon header row -->
            <div class="flex items-center justify-between px-3 py-2">
              <span class="font-medium">{{ w.name }}</span>
              <span class="text-gray-400 text-xs">{{ expandedWeapon === w.id ? '▲' : '▼' }}</span>
            </div>

            <!-- Expanded detail -->
            <div v-if="expandedWeapon === w.id" class="border-t border-gray-300 dark:border-gray-600 px-3 py-3">
              <!-- Stats table -->
              <div class="grid grid-cols-4 gap-x-4 gap-y-1 text-xs mb-3">
                <template v-if="w.ccMod != null || w.ccDam != null">
                  <div class="text-gray-500 dark:text-gray-400 col-span-4 font-semibold mt-1">Close Combat</div>
                  <div><span class="text-gray-500 dark:text-gray-400">Mod </span>{{ w.ccMod ?? '—' }}</div>
                  <div><span class="text-gray-500 dark:text-gray-400">DAM </span>{{ w.ccDam != null ? (w.dynamicDAM ? w.ccDam + unit.st : w.ccDam) : '—' }}</div>
                </template>
                <template v-if="w.shortRange != null">
                  <div class="text-gray-500 dark:text-gray-400 col-span-4 font-semibold mt-1">Short Range</div>
                  <div><span class="text-gray-500 dark:text-gray-400">RNG </span>{{ w.dynamicRange ? w.shortRange + unit.st : w.shortRange }}</div>
                  <div><span class="text-gray-500 dark:text-gray-400">Mod </span>{{ w.shortRangeMod ?? '—' }}</div>
                  <div><span class="text-gray-500 dark:text-gray-400">DAM </span>{{ w.shortRangeDam != null ? (w.dynamicDAM ? w.shortRangeDam + unit.st : w.shortRangeDam) : '—' }}</div>
                </template>
                <template v-if="w.longRange != null">
                  <div class="text-gray-500 dark:text-gray-400 col-span-4 font-semibold mt-1">Long Range</div>
                  <div><span class="text-gray-500 dark:text-gray-400">RNG </span>{{ w.dynamicRange ? w.longRange + unit.st : w.longRange }}</div>
                  <div><span class="text-gray-500 dark:text-gray-400">Mod </span>{{ w.longRangeMod ?? '—' }}</div>
                  <div><span class="text-gray-500 dark:text-gray-400">DAM </span>{{ w.longRangeDam != null ? (w.dynamicDAM ? w.longRangeDam + unit.st : w.longRangeDam) : '—' }}</div>
                </template>
                <div class="text-gray-500 dark:text-gray-400 col-span-4 font-semibold mt-1">Other</div>
                <div><span class="text-gray-500 dark:text-gray-400">Crit Fail </span>{{ w.critFail }}</div>
                <div v-if="w.dynamicDAM || w.dynamicRange"><span class="text-yellow-600 dark:text-yellow-300">{{ [w.dynamicDAM ? 'Dynamic DAM' : '', w.dynamicRange ? 'Dynamic Range' : ''].filter(Boolean).join(' · ') }}</span></div>
              </div>

              <!-- Special abilities -->
              <div v-if="w.weaponSpecialAbilities?.length">
                <div class="text-gray-400 text-xs font-semibold mb-1">Special Abilities</div>
                <div class="flex flex-wrap gap-1">
                  <div
                    v-for="sa in w.weaponSpecialAbilities"
                    :key="sa.id"
                    class="bg-gray-300 dark:bg-gray-600 rounded px-2 py-0.5 text-xs"
                  >
                    {{ sa.name }}<span v-if="sa.valueX" class="text-yellow-600 dark:text-yellow-300 ml-1">({{ sa.valueX }})</span>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <p v-else class="text-gray-500 text-sm">No weapons.</p>
      </div>

      <!-- Special Abilities -->
      <div>
        <h2 class="text-sm font-semibold text-gray-400 uppercase tracking-wider mb-2">Special Abilities</h2>
        <div v-if="unit.unitSpecialAbilities?.length" class="flex flex-wrap gap-2">
          <div
            v-for="sa in unit.unitSpecialAbilities"
            :key="sa.id"
            class="bg-gray-200 dark:bg-gray-700 rounded px-3 py-1 text-sm"
          >
            <span class="font-medium">{{ sa.name }}</span><span v-if="sa.valueX || sa.valueY" class="text-yellow-600 dark:text-yellow-300 ml-1">({{ [sa.valueX, sa.valueY].filter(Boolean).join(', ') }})</span>
            <p v-if="sa.description" class="text-xs text-gray-500 dark:text-gray-400 mt-0.5">{{ sa.description }}</p>
          </div>
        </div>
        <p v-else class="text-gray-500 text-sm">No special abilities.</p>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useRoute } from 'vue-router'

const route = useRoute()
const { getById, loading, error } = useUnits()
const unit = ref(null)
const expandedWeapon = ref(null)

onMounted(async () => {
  unit.value = await getById(route.params.id)
})

definePageMeta({ layout: 'dark' })
</script>

