<script setup>
import { useRoute } from 'vue-router';

const route = useRoute();

const isActive = (path) => route.path.startsWith(path);
</script>

<template>
  <header class="bg-white dark:bg-gray-900 text-gray-900 dark:text-white shadow border-b border-gray-200 dark:border-gray-700">
    <nav class="max-w-6xl mx-auto px-6 py-4 flex items-center justify-between">
      <div class="text-xl font-bold tracking-wide">
        Army Builder
      </div>

      <div class="flex gap-6">
        <NuxtLink
          to="/force-lists"
          class="nav-link"
          :class="{ active: isActive('/force-lists') }"
        >
          Force Lists
        </NuxtLink>

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

        <NuxtLink
          to="/unit-special-abilities"
          class="nav-link"
          :class="{ active: isActive('/unit-special-abilities') }"
        >
          Unit Abilities
        </NuxtLink>

        <NuxtLink
          to="/weapon-special-abilities"
          class="nav-link"
          :class="{ active: isActive('/weapon-special-abilities') }"
        >
          Weapon Abilities
        </NuxtLink>
      </div>
      <Switch />
    </nav>
  </header>
</template>

<style scoped>

</style>
