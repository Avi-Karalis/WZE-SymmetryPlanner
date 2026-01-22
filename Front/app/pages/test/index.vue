<script setup>
import { ref } from "vue";
import { useClipboard } from "@vueuse/core";

const toast = useToast();
const { copy } = useClipboard();

const data = ref([
    {
        id: "4600",
        date: "2024-03-11T15:30:00",
        status: "paid",
        email: "james.anderson@example.com",
        amount: 594,
    },
    {
        id: "4599",
        date: "2024-03-11T10:10:00",
        status: "failed",
        email: "mia.white@example.com",
        amount: 276,
    },
    {
        id: "4598",
        date: "2024-03-11T08:50:00",
        status: "refunded",
        email: "william.brown@example.com",
        amount: 315,
    },
    {
        id: "4597",
        date: "2024-03-10T19:45:00",
        status: "paid",
        email: "emma.davis@example.com",
        amount: 529,
    },
    {
        id: "4596",
        date: "2024-03-10T15:55:00",
        status: "paid",
        email: "ethan.harris@example.com",
        amount: 639,
    },
]);

const sorting = ref([]);

function getStatusColor(status) {
    return {
        paid: "success",
        failed: "error",
        refunded: "neutral",
    }[status];
}

function copyPaymentId(id) {
    copy(id);
    toast.add({
        title: "Payment ID copied to clipboard!",
        color: "success",
        icon: "i-lucide-circle-check",
    });
}

const columns = [
    { accessorKey: "id" },
    { accessorKey: "date" },
    { accessorKey: "status" },
    { accessorKey: "email" },
    {
        accessorKey: "amount",
        meta: { class: { th: "text-right", td: "text-right font-medium" } },
    },
    { accessorKey: "actions", meta: { class: { td: "text-right" } } },
];
</script>

<template>
    <UTable
        :data="data"
        :columns="columns"
        v-model:sorting="sorting"
        class="flex-1"
    >
        <!-- ID Column with sorting -->
        <template #header-id="{ column }">
            <UDropdownMenu
                :content="{ align: 'start' }"
                aria-label="Actions dropdown"
            >
                <template #trigger>
                    <UButton
                        color="neutral"
                        variant="ghost"
                        :label="'ID'"
                        :icon="
                            column.getIsSorted() === 'asc'
                                ? 'i-lucide-arrow-up-narrow-wide'
                                : column.getIsSorted() === 'desc'
                                  ? 'i-lucide-arrow-down-wide-narrow'
                                  : 'i-lucide-arrow-up-down'
                        "
                        class="-mx-2.5 data-[state=open]:bg-elevated"
                        @click="
                            column.toggleSorting(column.getIsSorted() === 'asc')
                        "
                    />
                </template>
                <UDropdownMenuItem
                    v-for="option in ['asc', 'desc']"
                    :key="option"
                    type="checkbox"
                    :icon="
                        option === 'asc'
                            ? 'i-lucide-arrow-up-narrow-wide'
                            : 'i-lucide-arrow-down-wide-narrow'
                    "
                    :checked="column.getIsSorted() === option"
                    @select="() => column.toggleSorting(option === 'desc')"
                >
                    {{ option.toUpperCase() }}
                </UDropdownMenuItem>
            </UDropdownMenu>
        </template>

        <!-- Date Column -->
        <template #header-date> Date </template>

        <!-- Status Column -->
        <template #cell-status="{ row }">
            <UBadge
                :variant="'subtle'"
                :color="getStatusColor(row.status)"
                class="capitalize"
            >
                {{ row.status }}
            </UBadge>
        </template>
        <template #header-status="{ column }">
            <UDropdownMenu
                :content="{ align: 'start' }"
                aria-label="Actions dropdown"
            >
                <template #trigger>
                    <UButton
                        color="neutral"
                        variant="ghost"
                        label="Status"
                        :icon="
                            column.getIsSorted() === 'asc'
                                ? 'i-lucide-arrow-up-narrow-wide'
                                : column.getIsSorted() === 'desc'
                                  ? 'i-lucide-arrow-down-wide-narrow'
                                  : 'i-lucide-arrow-up-down'
                        "
                        class="-mx-2.5 data-[state=open]:bg-elevated"
                    />
                </template>
                <UDropdownMenuItem
                    v-for="option in ['asc', 'desc']"
                    :key="option"
                    type="checkbox"
                    :icon="
                        option === 'asc'
                            ? 'i-lucide-arrow-up-narrow-wide'
                            : 'i-lucide-arrow-down-wide-narrow'
                    "
                    :checked="column.getIsSorted() === option"
                    @select="() => column.toggleSorting(option === 'desc')"
                >
                    {{ option.toUpperCase() }}
                </UDropdownMenuItem>
            </UDropdownMenu>
        </template>

        <!-- Amount Column -->
        <template #header-amount> Amount </template>
        <template #cell-amount="{ row }">
            <div class="text-right font-medium">{{ row.amount }}</div>
        </template>

        <!-- Actions Column -->
        <template #cell-actions="{ row }">
            <UDropdownMenu
                :content="{ align: 'end' }"
                aria-label="Actions dropdown"
            >
                <template #trigger>
                    <UButton
                        icon="i-lucide-ellipsis-vertical"
                        color="neutral"
                        variant="ghost"
                        aria-label="Actions dropdown"
                    />
                </template>
                <UDropdownMenuItem label="Actions" type="label" />
                <UDropdownMenuItem
                    @select="() => copyPaymentId(row.original.id)"
                >
                    Copy payment ID
                </UDropdownMenuItem>
                <UDropdownMenuItem type="separator" />
                <UDropdownMenuItem>View customer</UDropdownMenuItem>
                <UDropdownMenuItem>View payment details</UDropdownMenuItem>
            </UDropdownMenu>
        </template>
    </UTable>
</template>
