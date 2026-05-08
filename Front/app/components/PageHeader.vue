<!-- Page-level header: title on the left, action slot on the right.
     Props : title (string) -->
<template>
    <div class="flex items-center justify-between mb-6">
        <h1 class="text-2xl font-bold">{{ title }}</h1>
        <slot />
    </div>
</template>

<script setup>
defineProps({
    title: { type: String, required: true },
})
</script>
