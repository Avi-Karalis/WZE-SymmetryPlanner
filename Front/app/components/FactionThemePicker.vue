<template>
    <label class="flex items-center gap-2 text-xs text-gray-600 dark:text-gray-300">
        <span class="whitespace-nowrap">Faction</span>
        <select v-model="faction" class="field-input w-auto py-1.5" aria-label="Faction theme">
            <option v-for="option in options" :key="option.value" :value="option.value">{{ option.label }}</option>
        </select>
    </label>
</template>

<script setup>
const { faction, options } = useFactionTheme()
</script>
