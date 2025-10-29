<template>
  <header>
    <h1>My App</h1>
    <button @click="toggleTheme">{{ buttonName }}</button>
  </header>
</template>

<script setup>
const buttonName = ref("");
onMounted(() => {
  buttonName.value =
    localStorage.getItem("theme") === "dark"
      ? "Switch to Light Theme"
      : "Switch to Dark Theme";
});
function toggleTheme() {
  const body = document.body;
  const isDark = body.classList.toggle("dark-theme"); // toggles on/off

  // Optional: save user preference
  localStorage.setItem("theme", isDark ? "dark" : "light");
  buttonName.value = isDark ? "Switch to Light Theme" : "Switch to Dark Theme";
}
</script>
