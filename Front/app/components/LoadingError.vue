<!-- Inline loading spinner + error message.
     Props : loading (bool), error (string | null) -->
<template>
    <div>
        <div v-if="loading" class="text-center py-8 text-gray-400">Loading...</div>
        <div v-if="error" class="text-red-400 mb-4 text-sm">{{ error }}</div>
    </div>
</template>

<script setup>
defineProps({
    loading: { type: Boolean, default: false },
    error: { type: [String, Object, null], default: null },
})
</script>
