<template>
  <div class="p-4">
    <h1 class="text-2xl font-bold mb-4">Weapon Details</h1>

    <div v-if="loading">Loading...</div>
    <div v-if="error" class="text-red-500">{{ error }}</div>

    <div v-if="weapon">
      <p><strong>ID:</strong> {{ weapon.id }}</p>
      <p><strong>Name:</strong> {{ weapon.name }}</p>

      <p>
        <strong>Special Abilities:</strong>
        <span v-if="weapon.weaponWeaponSpecialAbility?.length">
          <span
            v-for="(wa, index) in weapon.weaponWeaponSpecialAbility"
            :key="wa.weaponSpecialAbility.id"
          >
            {{ wa.weaponSpecialAbility.name }}
            <span v-if="index !== weapon.weaponWeaponSpecialAbility.length - 1">, </span>
          </span>
        </span>
        <span v-else>None</span>
      </p>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from "vue";
import { useRoute } from "vue-router";
import { useWeapons } from "@/composables/useWeapons";

const weapon = ref(null);
const route = useRoute();
const { getById, loading, error } = useWeapons();

onMounted(async () => {
  const id = route.params.id;
  loading.value = true;
  try {
    weapon.value = await getById(id);
    console.log("Loaded weapon:", weapon.value);
  } catch (err) {
    console.error(err);
    error.value = "Failed to load weapon details";
  } finally {
    loading.value = false;
  }
});
</script>

<style scoped>
</style>
