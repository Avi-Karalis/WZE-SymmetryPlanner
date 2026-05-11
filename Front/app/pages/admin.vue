<template>
    <div class="p-4 sm:p-6 max-w-5xl mx-auto">
        <PageHeader title="Admin Panel" />

        <!-- Tab nav -->
        <div class="flex gap-2 mb-6 border-b border-gray-300 dark:border-gray-700">
            <button
                class="px-4 py-2 text-sm font-medium transition-colors"
                :class="tab === 'deleted' ? 'border-b-2 border-blue-500 text-blue-500' : 'text-gray-500 hover:text-gray-700 dark:hover:text-gray-300'"
                @click="tab = 'deleted'"
            >
                Deleted Lists
            </button>
            <button
                v-if="isSuperAdmin"
                class="px-4 py-2 text-sm font-medium transition-colors"
                :class="tab === 'users' ? 'border-b-2 border-blue-500 text-blue-500' : 'text-gray-500 hover:text-gray-700 dark:hover:text-gray-300'"
                @click="switchToUsers"
            >
                Users
            </button>
        </div>

        <LoadingError :loading="loading" :error="error" />

        <!-- Deleted Force Lists tab -->
        <div v-if="tab === 'deleted'">
            <div v-if="deletedLists.length" class="grid gap-4">
                <div
                    v-for="fl in deletedLists"
                    :key="fl.id"
                    class="bg-gray-100 dark:bg-gray-800 border border-gray-300 dark:border-gray-700 rounded-lg p-4 flex flex-col sm:flex-row sm:items-start sm:justify-between gap-3"
                >
                    <div>
                        <h2 class="text-lg font-semibold text-gray-800 dark:text-gray-100">{{ fl.name }}</h2>
                        <div class="text-sm text-gray-500 dark:text-gray-400 mt-1 flex flex-wrap gap-4">
                            <span>Faction: <span class="text-gray-700 dark:text-gray-200">{{ fl.faction }}</span></span>
                            <span>Allegiance: <span class="text-gray-700 dark:text-gray-200">{{ fl.allegiance }}</span></span>
                            <span>Max DP: <span class="text-gray-700 dark:text-gray-200">{{ fl.maxDp }}</span></span>
                            <span>Units: <span class="text-gray-700 dark:text-gray-200">{{ fl.units?.length ?? 0 }}</span></span>
                        </div>
                        <div class="text-xs text-gray-400 mt-1">
                            Owner: <span class="text-gray-600 dark:text-gray-300">{{ fl.userName }}</span>
                            ({{ fl.userEmail }}) &mdash;
                            Deleted: {{ formatDate(fl.deletedAt) }}
                        </div>
                    </div>
                    <button class="btn-primary text-sm shrink-0 self-start" @click="restore(fl.id)">Restore</button>
                </div>
            </div>
            <p v-else-if="!loading" class="text-gray-400">No deleted force lists.</p>
        </div>

        <!-- Users tab (SuperAdmin only) -->
        <div v-if="tab === 'users' && isSuperAdmin">
            <div v-if="users.length" class="grid gap-3">
                <div
                    v-for="u in users"
                    :key="u.id"
                    class="bg-gray-100 dark:bg-gray-800 border border-gray-300 dark:border-gray-700 rounded-lg p-4 flex flex-col sm:flex-row sm:items-center sm:justify-between gap-3"
                >
                    <div class="flex items-center gap-3">
                        <img
                            v-if="u.pictureUrl"
                            :src="u.pictureUrl"
                            :alt="u.name"
                            class="w-9 h-9 rounded-full border border-gray-300 dark:border-gray-600"
                        />
                        <div>
                            <div class="font-medium text-gray-800 dark:text-gray-100">{{ u.name }}</div>
                            <div class="text-xs text-gray-400">{{ u.email }}</div>
                        </div>
                    </div>
                    <div class="flex items-center gap-3">
                        <span class="text-xs text-gray-400 hidden sm:block">
                            Last login: {{ u.lastLogin ? formatDate(u.lastLogin) : 'Never' }}
                        </span>
                        <select
                            :value="u.role"
                            :disabled="u.role === 'SuperAdmin'"
                            class="text-sm rounded border border-gray-300 dark:border-gray-600 bg-white dark:bg-gray-700 text-gray-800 dark:text-gray-100 px-2 py-1 disabled:opacity-50 disabled:cursor-not-allowed"
                            @change="changeRole(u.id, $event.target.value)"
                        >
                            <option value="User">User</option>
                            <option value="Admin">Admin</option>
                            <option value="SuperAdmin" disabled>SuperAdmin</option>
                        </select>
                    </div>
                </div>
            </div>
            <p v-else-if="!loading" class="text-gray-400">No users found.</p>
        </div>
    </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'

const { isSuperAdmin } = useAuth()
const { deletedLists, users, loading, error, fetchDeletedLists, restoreForceList, fetchUsers, updateUserRole } = useAdmin()

const tab = ref('deleted')

onMounted(fetchDeletedLists)

async function switchToUsers() {
    tab.value = 'users'
    if (!users.value.length) await fetchUsers()
}

async function restore(id) {
    await restoreForceList(id)
}

async function changeRole(userId, role) {
    try {
        await updateUserRole(userId, role)
    } catch (e) {
        console.error('Failed to update role', e)
    }
}

function formatDate(iso) {
    return new Date(iso).toLocaleDateString(undefined, { year: 'numeric', month: 'short', day: 'numeric' })
}

definePageMeta({ layout: 'dark' })
</script>
