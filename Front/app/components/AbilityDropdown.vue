<script setup>
import { ref, watch } from 'vue';

const props = defineProps({
  abilities: Array,
  modelValue: Array
});

const emit = defineEmits(['update:modelValue']);

const selected = ref([...props.modelValue]);

const toggleAbility = (ability) => {
  const index = selected.value.findIndex(a => a.id === ability.id);
  if (index === -1) {
    selected.value.push(ability);
  } else {
    selected.value.splice(index, 1);
  }
  emit('update:modelValue', selected.value);
};

watch(() => props.modelValue, (val) => selected.value = [...val]);
</script>

<template>
  <div class="flex flex-col gap-2">
    <label>Abilities</label>
    <div class="grid grid-cols-2 gap-2">
      <button
        v-for="ability in abilities"
        :key="ability.id"
        type="button"
        @click="toggleAbility(ability)"
        :class="selected.find(a => a.id === ability.id) ? 'bg-blue-500 text-white' : 'bg-gray-200'"
        class="px-2 py-1 rounded"
      >
        {{ ability.name }}
      </button>
    </div>
  </div>
</template>
