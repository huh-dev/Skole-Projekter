<template>
  <main class="flex min-h-screen flex-col items-center bg-white px-6 pt-11 font-sans text-neutral-900">
    <div class="flex gap-2">
      <button
        v-for="item in locales"
        :key="item.code"
        type="button"
        class="rounded-lg border px-3 py-1 text-sm"
        :class="item.code === locale ? 'border-neutral-900' : 'border-neutral-300 text-neutral-500'"
        @click="setLocale(item.code)"
      >
        {{ item.name }}
      </button>
    </div>

    <h1 class="mt-6 text-[2rem] leading-tight font-semibold tracking-tight">
      {{ $t('title') }}
    </h1>
    <p class="mt-1.5 text-base text-neutral-500">
      {{ $t('titleNote') }}
    </p>
    <label class="relative mt-5">
      <span class="sr-only">{{ $t('tableLabel') }}</span>
      <select
        v-model="selectedTable"
        class="min-w-52 appearance-none rounded-lg border border-neutral-300 bg-white py-2 pr-9 pl-3 text-base"
      >
        <option
          v-for="table in LIBRARY_TABLES"
          :key="table"
          :value="table"
        >
          {{ $t(`tables.${table}`) }}
        </option>
      </select>
      <svg
        class="pointer-events-none absolute top-1/2 right-3 size-3 -translate-y-1/2 text-neutral-500"
        viewBox="0 0 12 8"
        fill="none"
        aria-hidden="true"
      >
        <path
          d="M1 1.5 6 6.5 11 1.5"
          stroke="currentColor"
          stroke-width="1.4"
          stroke-linecap="round"
          stroke-linejoin="round"
        />
      </svg>
    </label>

    <p v-if="status === 'pending'" class="mt-6 text-neutral-500">
      {{ $t('loading') }}
    </p>
    <p v-else-if="error" class="mt-6 text-red-700">
      {{ error.statusMessage || error.message }}
    </p>
    <p v-else-if="!rows.length" class="mt-6 text-neutral-500">
      {{ $t('empty') }}
    </p>
    <ul v-else class="mt-6 w-full max-w-2xl space-y-2 text-left">
      <li
        v-for="row in rows"
        :key="row.id"
        class="rounded-lg border border-neutral-200 px-4 py-3 text-sm leading-6"
      >
        {{ Object.values(row).join(' | ') }}
      </li>
    </ul>
  </main>
</template>

<script setup lang="ts">
import { LIBRARY_TABLES, type LibraryTable } from '#shared/utils/library'

interface LibraryRow {
  id: number
  [key: string]: unknown
}

const { locales, locale, setLocale } = useI18n()

useHead(() => ({
  htmlAttrs: {
    lang: locale.value,
  },
}))
const selectedTable = ref<LibraryTable>(LIBRARY_TABLES[0])

const { data, error, status } = await useFetch<LibraryRow[]>(() => `/api/library/${selectedTable.value}`)

const rows = computed(() => data.value ?? [])
</script>
