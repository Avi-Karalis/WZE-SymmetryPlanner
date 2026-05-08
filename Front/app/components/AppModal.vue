<!-- Generic modal overlay.
     Props  : title (string), maxWidth (tailwind class, default 'max-w-lg')
     Emits  : close
     Slots  : default (body content), footer (replaces the default footer) -->
<template>
    <div class="modal-overlay" @click.self="$emit('close')">
        <div class="modal-box" :class="maxWidth">
            <div class="flex items-center justify-between mb-4">
                <h2 class="text-lg font-bold">{{ title }}</h2>
                <button type="button" class="text-gray-400 hover:text-gray-900 dark:hover:text-white text-xl leading-none" @click="$emit('close')">&times;</button>
            </div>
            <slot />
        </div>
    </div>
</template>

<script setup>
defineProps({
    title: { type: String, required: true },
    maxWidth: { type: String, default: 'max-w-lg' },
})
defineEmits(['close'])
</script>

<style scoped>
@reference "../../assets/css/main.css";
.modal-overlay { @apply fixed inset-0 bg-black/60 flex items-center justify-center z-50 p-4; }
.modal-box { @apply bg-white dark:bg-gray-800 text-gray-800 dark:text-gray-100 rounded-lg p-6 w-full shadow-xl max-h-[90vh] overflow-y-auto; }
</style>
