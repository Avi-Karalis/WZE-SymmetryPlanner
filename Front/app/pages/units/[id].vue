<template>
  <div class="p-4">
    <h1 class="text-2xl font-bold mb-4">Unit Details</h1>

    <div v-if="loading">Loading...</div>
    <div v-if="error" class="text-red-500">{{ error }}</div>

    <div v-if="unit">
      <p><strong>ID:</strong> {{ unit.id }}</p>
      <p><strong>Name:</strong> {{ unit.faction }} {{ unit.unitType }}</p>
      <p><strong>Designations:</strong> {{ unit.designation?.join(", ") }}</p>
      <p>
        <strong>Weapons:</strong>
        <span v-for="uw in unit.unitWeapon" :key="uw.weapon.id">
          {{ uw.weapon.name }}<span v-if="!isLast(uw, unit.unitWeapon)">, </span>
        </span>
      </p>
      <p>
        <strong>Special Abilities:</strong>
        <span v-for="ua in unit.unitUnitSpecialAbilities" :key="ua.unitSpecialAbility.id">
          {{ ua.unitSpecialAbility.name }}<span v-if="!isLast(ua, unit.unitUnitSpecialAbilities)">, </span>
        </span>
      </p>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from "vue";
import { useRoute } from "vue-router";
import { useUnits } from "@/composables/useUnits";

const unit = ref(null);


const route = useRoute();
const { getById, loading, error } = useUnits();

const isLast = (item, list) => list.indexOf(item) === list.length - 1;

onMounted(async () => {
  const id = route.params.id;
  loading.value = true;
  try {
    unit.value = await getById(id); // assign the unit object directly
  } catch (err) {
    console.error(err);
    error.value = "Failed to load unit details";
  } finally {
    loading.value = false;
    console.log(unit.value);
  }
});
</script>


<style scoped>
</style>
