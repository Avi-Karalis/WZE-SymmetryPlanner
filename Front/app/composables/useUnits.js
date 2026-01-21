// composables/useUnits.js
import { ref } from "vue";

export function useUnits() {
  // Reactive state
  const units = ref([]);
  const loading = ref(false);
  const error = ref(null);
  const config = useRuntimeConfig();

  const fetchAll = async () => {
    loading.value = true;
    error.value = null;

    try {
      // Use useFetch with proper error handling
      const { data, error: fetchError } = await useFetch(
        `${config.public.apiBase}/Unit`,
        {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json',
          },
        }
      );

      if (fetchError.value) {
        throw fetchError.value;
      }

      units.value = data.value || [];
    } catch (err) {
      console.error("Failed to fetch units", err);
      error.value = err.message || "Failed to load units";
    } finally {
      loading.value = false;
    }
  };

  // Fetch a single unit by ID
  const getById = async (id) => {
    loading.value = true;
    try {
      const { data, error: fetchError } = await useFetch(
        `${config.public.apiBase}/Unit/${id}`,
        {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json',
          },
        }
      );

      if (fetchError.value) {
        throw fetchError.value;
      }

      return data.value;
    } catch (err) {
      console.error(`Failed to fetch unit ${id}`, err);
      error.value = err.message || `Failed to fetch unit ${id}`;
      return null;
    } finally {
      loading.value = false;
    }
  };

  // Create a new unit
  const create = async (unit) => {
    loading.value = true;
    try {
      const { data, error: fetchError } = await useFetch(
        `${config.public.apiBase}/Unit`,
        {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(unit),
        }
      );

      if (fetchError.value) {
        throw fetchError.value;
      }

      units.value.push(data.value);
      return data.value;
    } catch (err) {
      console.error("Failed to create unit", err);
      error.value = err.message || "Failed to create unit";
      throw err;
    } finally {
      loading.value = false;
    }
  };

  return {
    units,
    loading,
    error,
    fetchAll,
    create,
    getById,
  };
}