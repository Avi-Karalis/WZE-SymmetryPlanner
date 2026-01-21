<template>
  <div class="p-4">
    <h1 class="text-2xl font-bold mb-4">Test Units</h1>
    <AppAlert>Total number of Units = {{ unitsCount }}</AppAlert>
    
    <div v-if="loading" class="mb-4">Loading units...</div>
    <div v-if="error" class="text-red-500 mb-4">{{ error }}</div>

    <table
      v-if="units.length"
      class="table-auto border-collapse border border-gray-300 w-full"
    >
      <thead>
        <tr class="bg-gray-100">
          <th class="border border-gray-300 px-4 py-2">ID</th>
          <th class="border border-gray-300 px-4 py-2">Name</th>
          <th class="border border-gray-300 px-4 py-2">Designations</th>
          <th class="border border-gray-300 px-4 py-2">Weapons</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="unit in units" :key="unit.id" class="hover:bg-gray-50">
          <td class="border border-gray-300 px-4 py-2">{{ unit.id }}</td>
          <td class="border border-gray-300 px-4 py-2">
            {{ unit.faction }} {{ unit.unitType }}
          </td>
          <td class="border border-gray-300 px-4 py-2">
            {{ unit.designation?.join(", ") }}
          </td>
          <td class="border border-gray-300 px-4 py-2">
            <span v-for="uw in unit.unitWeapon" :key="uw.weapon.id">
              {{ uw.weapon.name }}<span v-if="!isLast(uw, unit.unitWeapon)">, </span>
            </span>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup>
import { ref, onMounted } from "vue";
import AppAlert from "~/components/AppAlert.vue";
const { units, fetchAll, loading, error } = useUnits()
const unitsCount = ref(0);

const isLast = (item, list) => list.indexOf(item) === list.length - 1;

onMounted(async () => {
  loading.value = true;
  try {
    await fetchAll();
    unitsCount.value = units.value.length;
  } catch (err) {
    console.error(err);
    error.value = "Failed to load units";
  } finally {
    loading.value = false;
  }
});

definePageMeta({
  layout: "dark",
});
</script>

<style scoped>
table {
  border-spacing: 0;
  border: 1px solid #ccc;
}
th,
td {
  text-align: left;
  padding: 0.5rem;
}
</style>
