<script setup>
import { useRoute } from 'vue-router';

const route = useRoute();
const { user, logout, isLoggedIn } = useAuth();

const isActive = (path) => route.path.startsWith(path);
</script>

<template>
  <header class="bg-white dark:bg-gray-900 text-gray-900 dark:text-white shadow border-b border-gray-200 dark:border-gray-700">
    <nav class="max-w-6xl mx-auto px-6 py-4 flex items-center justify-between">
      <div class="text-xl font-bold tracking-wide">
        Warzone: Eternal Symmetry Planner
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

      <div class="flex items-center gap-3">
        <Switch />
        <template v-if="isLoggedIn && user">
          <img
            v-if="user.pictureUrl"
            :src="user.pictureUrl"
            :alt="user.name"
            class="w-8 h-8 rounded-full border border-gray-300 dark:border-gray-600"
          />
          <span class="text-sm text-gray-600 dark:text-gray-300 hidden sm:inline">{{ user.name }}</span>
          <button
            @click="logout"
            class="text-sm text-red-500 hover:text-red-400 transition-colors"
          >
            Logout
          </button>
        </template>
      </div>
    </nav>
  </header>
</template>

<style scoped>

</style>
