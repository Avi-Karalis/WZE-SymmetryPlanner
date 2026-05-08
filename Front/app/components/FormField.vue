<!-- Form field wrapper: label on top, input/select/etc. in the default slot.
     Props : label (string), required (bool) -->
<template>
    <div>
        <label class="field-label">{{ label }}<span v-if="required" class="text-red-400 ml-0.5">*</span></label>
        <slot />
    </div>
</template>

<script setup>
defineProps({
    label: { type: String, required: true },
    required: { type: Boolean, default: false },
})
</script>

<style scoped>
@reference "../../assets/css/main.css";
.field-label { @apply block text-xs text-gray-400 mb-1; }
</style>
