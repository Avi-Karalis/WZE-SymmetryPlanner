<template>
  <div class="p-4 sm:p-6 max-w-2xl mx-auto">
    <div class="mb-4">
      <NuxtLink to="/weapons" class="text-gray-400 hover:text-white text-sm">← Weapons</NuxtLink>
    </div>

    <div v-if="loading" class="text-gray-400">Loading...</div>
    <div v-if="error" class="text-red-400">{{ error }}</div>

    <div v-if="weapon" class="bg-gray-100 dark:bg-gray-800 rounded-lg p-6 text-gray-800 dark:text-gray-100">
      <h1 class="text-2xl font-bold mb-4">{{ weapon.name }}</h1>

      <!-- Combat stats table -->
      <div class="overflow-x-auto mb-6">
        <table class="w-full text-sm text-center">
          <thead>
            <tr class="bg-gray-200 dark:bg-gray-700 text-gray-600 dark:text-gray-300">
              <th class="px-3 py-2">CC Mod</th>
              <th class="px-3 py-2">CC Dam</th>
              <th class="px-3 py-2">SR</th>
              <th class="px-3 py-2">SR Mod</th>
              <th class="px-3 py-2">SR Dam</th>
              <th class="px-3 py-2">LR</th>
              <th class="px-3 py-2">LR Mod</th>
              <th class="px-3 py-2">LR Dam</th>
              <th class="px-3 py-2">Crit Fail</th>
            </tr>
          </thead>
          <tbody>
            <tr class="border-t border-gray-200 dark:border-gray-700">
              <td class="px-3 py-2">{{ weapon.ccMod ?? '—' }}</td>
              <td class="px-3 py-2">{{ weapon.ccDam ?? '—' }}</td>
              <td class="px-3 py-2">{{ weapon.shortRange ?? '—' }}</td>
              <td class="px-3 py-2">{{ weapon.shortRangeMod ?? '—' }}</td>
              <td class="px-3 py-2">{{ weapon.shortRangeDam ?? '—' }}</td>
              <td class="px-3 py-2">{{ weapon.longRange ?? '—' }}</td>
              <td class="px-3 py-2">{{ weapon.longRangeMod ?? '—' }}</td>
              <td class="px-3 py-2">{{ weapon.longRangeDam ?? '—' }}</td>
              <td class="px-3 py-2">{{ weapon.critFail }}</td>
            </tr>
          </tbody>
        </table>
      </div>

      <!-- Special Abilities -->
      <div>
        <h2 class="text-sm font-semibold text-gray-500 dark:text-gray-400 uppercase tracking-wider mb-2">Special Abilities</h2>
        <div v-if="weapon.weaponSpecialAbilities?.length" class="flex flex-wrap gap-2">
          <div
            v-for="sa in weapon.weaponSpecialAbilities"
            :key="sa.id"
            class="bg-gray-200 dark:bg-gray-700 rounded px-3 py-1 text-sm"
          >
            <span class="font-medium">{{ sa.name }}</span>
            <span v-if="sa.valueX" class="text-yellow-600 dark:text-yellow-300 ml-1">({{ sa.valueX }})</span>
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
const { getById, loading, error } = useWeapons()
const weapon = ref(null)

onMounted(async () => {
  weapon.value = await getById(route.params.id)
})

definePageMeta({ layout: 'dark' })
</script>

