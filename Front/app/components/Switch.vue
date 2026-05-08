<template>
  <label class="theme-switch">
    <input type="checkbox" :checked="isDark" @change="toggle" />
    <span class="slider"></span>
    <span class="label">{{ isDark ? 'Dark' : 'Light' }}</span>
  </label>
</template>

<script setup>
const colorMode = useColorMode()
const isDark = computed(() => colorMode.value === 'dark')

function toggle() {
  colorMode.preference = isDark.value ? 'light' : 'dark'
}
</script>

<style scoped>
.theme-switch {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  cursor: pointer;
}

.theme-switch input {
  display: none;
}

.slider {
  width: 42px;
  height: 22px;
  background: #ccc;
  border-radius: 999px;
  position: relative;
  transition: background 0.3s;
}

.slider::after {
  content: '';
  position: absolute;
  top: 3px;
  left: 3px;
  width: 16px;
  height: 16px;
  background: white;
  border-radius: 50%;
  transition: transform 0.3s;
}

input:checked + .slider {
  background: #4f46e5;
}

input:checked + .slider::after {
  transform: translateX(20px);
}

.label {
  font-size: 0.75rem;
  color: currentColor;
}
</style>

<style scoped>
.theme-switch {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  cursor: pointer;
}

.theme-switch input {
  display: none;
}

.slider {
  width: 42px;
  height: 22px;
  background: #ccc;
  border-radius: 999px;
  position: relative;
  transition: background 0.3s;
}

.slider::before {
  content: "";
  position: absolute;
  width: 18px;
  height: 18px;
  top: 2px;
  left: 2px;
  background: white;
  border-radius: 50%;
  transition: transform 0.3s;
}

input:checked + .slider {
  background: #4f46e5;
}

input:checked + .slider::before {
  transform: translateX(20px);
}

.label {
  font-size: 0.9rem;
}
</style>
