<template>
    <div class="p-4">
        <h1 class="text-2xl font-bold mb-4">Test Weapons</h1>
        <AppAlert>Total number of Weapons = {{ weaponsCount }}</AppAlert>

        <div v-if="loading" class="mb-4">Loading weapons...</div>
        <div v-if="error" class="text-red-500 mb-4">{{ error }}</div>

        <table
            v-if="weapons.length"
            class="table-auto border-collapse border border-gray-300 w-full"
        >
            <thead>
                <tr class="bg-gray-100">
                    <th class="border border-gray-300 px-4 py-2">ID</th>
                    <th class="border border-gray-300 px-4 py-2">Name</th>
                </tr>
            </thead>
            <tbody>
                <tr
                    v-for="weapon in weapons"
                    :key="weapon.id"
                    class="hover:bg-gray-50"
                >
                    <td class="border border-gray-300 px-4 py-2">
                        {{ weapon.id }}
                    </td>
                    <td class="border border-gray-300 px-4 py-2">
                        {{ weapon.name }}
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script setup>
import { ref, onMounted } from "vue";
import AppAlert from "~/components/AppAlert.vue";
const { weapons, fetchAll, loading, error } = useWeapons();
const weaponsCount = ref(0);


const isLast = (item, list) => list.indexOf(item) === list.length - 1;

onMounted(async () => {
    loading.value = true;
    try {
        await fetchAll();

        weapons.value.sort(
            (a, b) =>
                a.faction.localeCompare(b.faction) ||
                a.unitType.localeCompare(b.unitType),
        );
        weaponsCount.value = weapons.value.length;
    } catch (err) {
        console.error(err);
        error.value = "Failed to load weapons";
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
