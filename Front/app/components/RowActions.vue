<!-- Edit + Delete action buttons for table rows.
     Emits : edit, delete -->
<template>
    <div class="flex gap-2 justify-center">
        <button type="button" class="btn-sm btn-secondary" @click="$emit('edit')">Edit</button>
        <button type="button" class="btn-sm btn-danger" @click="$emit('delete')">Delete</button>
    </div>
</template>

<script setup>
defineEmits(['edit', 'delete'])
</script>
