<script setup>
import { useRoute } from 'vue-router';

const route = useRoute();

const isActive = (path) => route.path.startsWith(path);
</script>

<template>
  <header class="bg-gray-900 text-white shadow">
    <nav class="max-w-6xl mx-auto px-6 py-4 flex items-center justify-between">
      <div class="text-xl font-bold tracking-wide">
        Army Builder
      </div>

      <div class="flex gap-6">
        <NuxtLink
          to="/units"
          class="nav-link"
          :class="{ active: isActive('/units') }"
        >
          Units
        </NuxtLink>

        <NuxtLink
          to="/weapons"
          class="nav-link"
          :class="{ active: isActive('/weapons') }"
        >
          Weapons
        </NuxtLink>
      </div>
      <Switch />
    </nav>
  </header>
</template>

<style scoped>

</style>
