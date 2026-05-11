<script setup>
import { ref } from 'vue';
import { useRoute } from 'vue-router';

const route = useRoute();
const { user, logout, isLoggedIn, isAdmin, isSuperAdmin } = useAuth();

const isActive = (path) => route.path.startsWith(path);
const menuOpen = ref(false);

watch(() => route.path, () => { menuOpen.value = false });
</script>

<template>
  <header class="bg-white dark:bg-gray-900 text-gray-900 dark:text-white shadow border-b border-gray-200 dark:border-gray-700 relative z-40">
    <nav class="max-w-6xl mx-auto px-4 sm:px-6 py-3 sm:py-4 flex items-center justify-between gap-4">
      <!-- App title -->
      <div class="text-base sm:text-xl font-bold tracking-wide shrink-0">
        WZE Symmetry Planner
      </div>

      <!-- Desktop nav links -->
      <div class="hidden md:flex gap-5 flex-1 justify-center">
        <NuxtLink to="/force-lists" class="nav-link" :class="{ active: isActive('/force-lists') }">Force Lists</NuxtLink>
        <NuxtLink to="/units" class="nav-link" :class="{ active: isActive('/units') }">Units</NuxtLink>
        <NuxtLink to="/assets" class="nav-link" :class="{ active: isActive('/assets') }">Assets</NuxtLink>
        <NuxtLink to="/weapons" class="nav-link" :class="{ active: isActive('/weapons') }">Weapons</NuxtLink>
        <NuxtLink to="/unit-special-abilities" class="nav-link" :class="{ active: isActive('/unit-special-abilities') }">Unit Abilities</NuxtLink>
        <NuxtLink to="/weapon-special-abilities" class="nav-link" :class="{ active: isActive('/weapon-special-abilities') }">Weapon Abilities</NuxtLink>
        <NuxtLink v-if="isAdmin" to="/admin" class="nav-link" :class="{ active: isActive('/admin') }">Admin</NuxtLink>
      </div>

      <!-- Right side: dark mode + user + hamburger -->
      <div class="flex items-center gap-2 sm:gap-3 shrink-0">
        <Switch />
        <template v-if="isLoggedIn && user">
          <img
            v-if="user.pictureUrl"
            :src="user.pictureUrl"
            :alt="user.name"
            class="w-8 h-8 rounded-full border border-gray-300 dark:border-gray-600 shrink-0"
          />
          <span class="text-sm text-gray-600 dark:text-gray-300 hidden lg:inline">{{ user.name }}</span>
          <span class="text-xs px-2 py-0.5 rounded-full font-medium hidden sm:inline"
            :class="{
              'bg-purple-100 text-purple-700 dark:bg-purple-900 dark:text-purple-300': user.role === 'SuperAdmin',
              'bg-blue-100 text-blue-700 dark:bg-blue-900 dark:text-blue-300': user.role === 'Admin',
              'bg-gray-100 text-gray-600 dark:bg-gray-800 dark:text-gray-400': user.role === 'User',
            }"
          >{{ user.role }}</span>
          <button @click="logout" class="hidden sm:inline text-sm text-red-500 hover:text-red-400 transition-colors">Logout</button>
        </template>

        <!-- Hamburger button (mobile) -->
        <button
          class="md:hidden p-2 rounded text-gray-600 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 transition"
          aria-label="Toggle menu"
          @click="menuOpen = !menuOpen"
        >
          <svg v-if="!menuOpen" xmlns="http://www.w3.org/2000/svg" class="w-6 h-6" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2">
            <path stroke-linecap="round" stroke-linejoin="round" d="M4 6h16M4 12h16M4 18h16" />
          </svg>
          <svg v-else xmlns="http://www.w3.org/2000/svg" class="w-6 h-6" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2">
            <path stroke-linecap="round" stroke-linejoin="round" d="M6 18L18 6M6 6l12 12" />
          </svg>
        </button>
      </div>
    </nav>

    <!-- Mobile dropdown menu -->
    <div
      v-if="menuOpen"
      class="md:hidden bg-white dark:bg-gray-900 border-t border-gray-200 dark:border-gray-700 px-4 pb-4"
    >
      <div class="flex flex-col gap-1 pt-2">
        <NuxtLink to="/force-lists" class="mobile-nav-link" :class="{ active: isActive('/force-lists') }">Force Lists</NuxtLink>
        <NuxtLink to="/units" class="mobile-nav-link" :class="{ active: isActive('/units') }">Units</NuxtLink>
        <NuxtLink to="/assets" class="mobile-nav-link" :class="{ active: isActive('/assets') }">Assets</NuxtLink>
        <NuxtLink to="/weapons" class="mobile-nav-link" :class="{ active: isActive('/weapons') }">Weapons</NuxtLink>
        <NuxtLink to="/unit-special-abilities" class="mobile-nav-link" :class="{ active: isActive('/unit-special-abilities') }">Unit Abilities</NuxtLink>
        <NuxtLink to="/weapon-special-abilities" class="mobile-nav-link" :class="{ active: isActive('/weapon-special-abilities') }">Weapon Abilities</NuxtLink>
        <NuxtLink v-if="isAdmin" to="/admin" class="mobile-nav-link" :class="{ active: isActive('/admin') }">Admin</NuxtLink>
        <div v-if="isLoggedIn && user" class="mt-3 pt-3 border-t border-gray-200 dark:border-gray-700 flex items-center justify-between">
          <div class="flex items-center gap-2">
            <img v-if="user.pictureUrl" :src="user.pictureUrl" :alt="user.name" class="w-7 h-7 rounded-full" />
            <div>
              <div class="text-sm font-medium text-gray-800 dark:text-gray-100">{{ user.name }}</div>
              <div class="text-xs"
                :class="{
                  'text-purple-600 dark:text-purple-400': user.role === 'SuperAdmin',
                  'text-blue-600 dark:text-blue-400': user.role === 'Admin',
                  'text-gray-500 dark:text-gray-400': user.role === 'User',
                }"
              >{{ user.role }}</div>
            </div>
          </div>
          <button @click="logout" class="text-sm text-red-500 hover:text-red-400 transition-colors">Logout</button>
        </div>
      </div>
    </div>
  </header>
</template>

<style scoped>
@reference "../../assets/css/main.css";
.nav-link {
  @apply text-sm text-gray-600 dark:text-gray-300 hover:text-gray-900 dark:hover:text-white transition-colors font-medium;
}
.nav-link.active {
  @apply text-blue-600 dark:text-blue-400 font-semibold;
}
.mobile-nav-link {
  @apply block px-3 py-2 rounded-md text-sm font-medium text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors;
}
.mobile-nav-link.active {
  @apply text-blue-600 dark:text-blue-400 bg-blue-50 dark:bg-blue-900/20;
}
</style>

<style scoped>

</style>
