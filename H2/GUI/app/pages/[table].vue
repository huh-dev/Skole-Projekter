<template>
  <LibraryTableView :table="table" />
</template>

<script setup lang="ts">
import { isLibraryTable, type LibraryTable } from '#shared/utils/library'

definePageMeta({
  validate(route) {
    const value = route.params.table
    return typeof value === 'string' && isLibraryTable(value)
  },
})

const param = useRoute().params.table

if (typeof param !== 'string' || !isLibraryTable(param)) {
  throw createError({
    statusCode: 404,
    statusMessage: 'Unknown table',
  })
}

const table: LibraryTable = param
</script>
