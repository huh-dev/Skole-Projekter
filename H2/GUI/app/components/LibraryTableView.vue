<template>
  <div class="flex w-full max-w-2xl flex-col items-center">
    <h1 class="text-[2rem] leading-tight font-semibold tracking-tight">
      {{ $t(`tables.${table}`) }}
    </h1>
    <p class="mt-1.5 text-base text-muted-foreground">
      {{ $t('tablePageNote') }}
    </p>

    <div
      v-if="status === 'pending'"
      class="mt-6 flex items-center gap-2 text-muted-foreground"
    >
      <Spinner />
      {{ $t('loading') }}
    </div>

    <Alert
      v-else-if="error"
      variant="destructive"
      class="mt-6 w-full"
    >
      <AlertTitle>{{ $t('errorTitle') }}</AlertTitle>
      <AlertDescription>
        {{ error.statusMessage || error.message }}
      </AlertDescription>
    </Alert>

    <p
      v-else-if="!rows.length"
      class="mt-6 text-muted-foreground"
    >
      {{ $t('empty') }}
    </p>

    <ul
      v-else
      class="mt-6 w-full space-y-2 text-left"
    >
      <li
        v-for="row in rows"
        :key="row.id"
      >
        <Card>
          <CardContent class="py-3 text-sm leading-6">
            {{ Object.values(row).join(' | ') }}
          </CardContent>
        </Card>
      </li>
    </ul>
  </div>
</template>

<script setup lang="ts">
import type { LibraryTable } from '#shared/utils/library'

interface LibraryRow {
  id: number
  [key: string]: unknown
}

const props = defineProps<{
  table: LibraryTable
}>()

const { data, error, status } = await useFetch<LibraryRow[]>(() => `/api/library/${props.table}`)

const rows = computed(() => data.value ?? [])
</script>
