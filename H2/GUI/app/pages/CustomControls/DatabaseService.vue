<template>
  <section
    class="mt-10 w-full max-w-2xl"
    :aria-label="$t('databaseService.title')"
  >
    <div class="overflow-hidden rounded-xl border border-border bg-card text-card-foreground">
      <div class="border-b border-border px-4 py-3 text-center">
        <h2 class="text-base font-semibold">
          {{ $t('databaseService.title') }}
        </h2>
        <p class="mt-1 flex items-center justify-center gap-2 text-xs text-muted-foreground">
          <span
            class="relative flex size-2"
            aria-hidden="true"
          >
            <span class="absolute inline-flex size-full animate-ping rounded-full bg-foreground/40" />
            <span class="relative inline-flex size-2 rounded-full bg-foreground" />
          </span>
          <span>{{ $t('databaseService.live') }}</span>
          <span v-if="overview">· {{ $t('databaseService.tableCount', { count: overview.tableCount }) }}</span>
        </p>
      </div>

      <div
        v-if="pending && !overview"
        class="flex items-center justify-center gap-2 px-4 py-6 text-sm text-muted-foreground"
      >
        <Spinner />
        {{ $t('loading') }}
      </div>

      <Alert
        v-else-if="loadError && !overview"
        variant="destructive"
        class="m-4"
      >
        <AlertTitle>{{ $t('databaseService.error') }}</AlertTitle>
        <AlertDescription>{{ loadError }}</AlertDescription>
      </Alert>

      <div v-else-if="overview">
        <p
          v-if="loadError"
          class="border-b border-border px-4 py-2 text-center text-xs text-destructive"
        >
          {{ $t('databaseService.error') }}
        </p>
        <table class="w-full border-collapse text-sm">
          <thead>
            <tr class="bg-muted text-left">
              <th
                scope="col"
                class="px-4 py-2 font-medium"
              >
                {{ $t('tableLabel') }}
              </th>
              <th
                scope="col"
                class="px-4 py-2 text-right font-medium"
              >
                {{ $t('databaseService.rowCount') }}
              </th>
            </tr>
          </thead>
          <tbody>
            <tr
              v-for="item in overview.tables"
              :key="item.table"
              class="border-t border-border transition-colors"
              :class="changedTables.includes(item.table) ? 'bg-muted' : ''"
            >
              <th
                scope="row"
                class="px-4 py-2 text-left font-normal"
              >
                {{ $t(`tables.${item.table}`) }}
              </th>
              <td class="px-4 py-2 text-right tabular-nums">
                {{ item.rowCount }}
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </section>
</template>

<script setup lang="ts">
import type { LibraryTable } from '#shared/utils/library'

const HIGHLIGHT_MS = 1200

const { overview, loadError, pending, refresh } = useDatabaseService()

if (import.meta.server) {
  await refresh()
}
const changedTables = ref<LibraryTable[]>([])
const knownCounts = new Map<LibraryTable, number>()
let highlightTimer = 0

//Super simple way to highlight the tables that have changed.
watch(overview, (next) => {
  if (!next) {
    return
  }

  const updates: LibraryTable[] = []

  for (const item of next.tables) {
    const previous = knownCounts.get(item.table)
    if (previous !== undefined && previous !== item.rowCount) {
      updates.push(item.table)
    }
    knownCounts.set(item.table, item.rowCount)
  }

  if (!updates.length) {
    return
  }

  changedTables.value = updates
  window.clearTimeout(highlightTimer)
  highlightTimer = window.setTimeout(() => {
    changedTables.value = []
  }, HIGHLIGHT_MS)
})

onUnmounted(() => {
  window.clearTimeout(highlightTimer)
})
</script>
