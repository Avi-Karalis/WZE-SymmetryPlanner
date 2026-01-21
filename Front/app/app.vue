<template>
  <NuxtLayout :name="layout">
    <NuxtPage />
  </NuxtLayout>
</template>

<script setup>
onMounted(() => {
  const saved = localStorage.getItem('theme')
  if (saved === 'dark') {
    document.body.classList.add('dark-theme')
  }
})
const route = useRoute()
const layout = computed(() => route.query.theme === 'dark' ? 'dark' : 'default')
</script>