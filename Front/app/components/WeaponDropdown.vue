<script setup>
import { ref, watch } from 'vue';

const props = defineProps({
  weapons: Array,
  modelValue: Array
});

const emit = defineEmits(['update:modelValue']);

const selected = ref([...props.modelValue]);

const toggleWeapon = (weapon) => {
  const index = selected.value.findIndex(w => w.id === weapon.id);
  if (index === -1) {
    selected.value.push(weapon);
  } else {
    selected.value.splice(index, 1);
  }
  emit('update:modelValue', selected.value);
};

watch(() => props.modelValue, (val) => selected.value = [...val]);
</script>

<template>
  <div class="flex flex-col gap-2">
    <label>Weapons</label>
    <div class="grid grid-cols-2 gap-2">
      <button
        v-for="weapon in weapons"
        :key="weapon.id"
        type="button"
        @click="toggleWeapon(weapon)"
        :class="{ 'is-selected': selected.find(w => w.id === weapon.id) }"
        class="faction-choice px-2 py-1 rounded"
      >
        {{ weapon.name }}
      </button>
    </div>
  </div>
</template>
