<template>
  <header>
    <label class="theme-switch">
      <input
        type="checkbox"
        v-model="isDark"
        @change="toggleTheme"
      />
      <span class="slider"></span>
      <span class="label">
        {{ isDark ? "Dark" : "Light" }}
      </span>
    </label>
  </header>
</template>


<script setup>
import { ref, onMounted } from "vue";

const isDark = ref(false);

onMounted(() => {
  isDark.value = localStorage.getItem("theme") === "dark";
  document.body.classList.toggle("dark-theme", isDark.value);
});

function toggleTheme() {
  document.body.classList.toggle("dark-theme", isDark.value);
  localStorage.setItem("theme", isDark.value ? "dark" : "light");
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
