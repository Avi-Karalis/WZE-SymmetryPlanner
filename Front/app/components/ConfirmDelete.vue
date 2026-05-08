<!-- Reusable delete confirmation dialog.
     Props : name (string) — shown in the message
     Emits : confirm, cancel -->
<template>
    <AppModal title="Confirm Delete" max-width="max-w-sm" @close="$emit('cancel')">
        <p class="text-gray-300 mb-6">
            Delete <strong>{{ name }}</strong>? This cannot be undone.
        </p>
        <div class="flex justify-end gap-3">
            <button type="button" class="btn-secondary" @click="$emit('cancel')">Cancel</button>
            <button type="button" class="btn-danger" @click="$emit('confirm')">Delete</button>
        </div>
    </AppModal>
</template>

<script setup>
defineProps({
    name: { type: String, required: true },
})
defineEmits(['confirm', 'cancel'])
</script>
