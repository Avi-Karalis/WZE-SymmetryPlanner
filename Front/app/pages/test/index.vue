<script setup lang="js">
import { h, resolveComponent } from "vue";

import { useClipboard } from "@vueuse/core";

const UButton = resolveComponent("UButton");
const UBadge = resolveComponent("UBadge");
const UDropdownMenu = resolveComponent("UDropdownMenu");

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

const columns = [
    {
        accessorKey: "id",
        header: "#",
        cell: ({ row }) => `#${row.getValue("id")}`,
    },
    {
        accessorKey: "date",
        header: "Date",
        cell: ({ row }) => {
            return new Date(row.getValue("date")).toLocaleString("en-US", {
                day: "numeric",
                month: "short",
                hour: "2-digit",
                minute: "2-digit",
                hour12: false,
            });
        },
    },
    {
        accessorKey: "status",
        header: "Status",
        cell: ({ row }) => {
            const color = {
                paid: "success",
                failed: "error",
                refunded: "neutral",
            }[row.getValue("status")];

            return h(
                UBadge,
                { class: "capitalize", variant: "subtle", color },
                () => row.getValue("status"),
            );
        },
    },
    {
        accessorKey: "email",
        header: "Email",
    },
    {
        accessorKey: "amount",
        header: "Amount",
        meta: {
            class: {
                th: "text-right",
                td: "text-right font-medium",
            },
        },
        cell: ({ row }) => {
            const amount = Number.parseFloat(row.getValue("amount"));
            return new Intl.NumberFormat("en-US", {
                style: "currency",
                currency: "EUR",
            }).format(amount);
        },
    },
    {
        id: "actions",
        meta: {
            class: {
                td: "text-right",
            },
        },
        cell: ({ row }) => {
            return h(
                UDropdownMenu,
                {
                    content: {
                        align: "end",
                    },
                    items: getRowItems(row),
                    "aria-label": "Actions dropdown",
                },
                () =>
                    h(UButton, {
                        icon: "i-lucide-ellipsis-vertical",
                        color: "neutral", 
                        variant: "ghost", 
                        size: "sm",
                        "aria-label": "Actions dropdown",
                    }),
            );
        },
    },
];

function getRowItems(row) {
    return [
        {
            type: "label",
            label: "Actions",
        },
        {
            label: "Copy payment ID",
            icon: 'i-lucide-copy',
            onSelect() {
                copy(row.original.id);

                toast.add({
                    title: "Payment ID copied to clipboard!",
                    color: "success",
                    icon: "i-lucide-circle-check",
                });
            },
        },
        {
            type: "separator",
        },
        {
            label: "View customer",
            icon: 'i-lucide-user',
        },
        {
            label: "View payment details",
            icon: 'i-lucide-info',
        },
    ];
}
</script>

<template>
    <UTable :data="data" :columns="columns" class="flex-1" />
</template>
