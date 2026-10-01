import { LIBRARY_TABLES, type LibraryOverview, type LibraryTable } from '#shared/utils/library'

const counters: Record<LibraryTable, () => Promise<number>> = {
  authors: () => prisma.author.count(),
  books: () => prisma.book.count(),
  users: () => prisma.user.count(),
  staff: () => prisma.staff.count(),
  loans: () => prisma.loanedBook.count(),
  logs: () => prisma.loanLog.count(),
}

export default defineEventHandler(async (event): Promise<LibraryOverview> => {
  setResponseHeader(event, 'cache-control', 'no-store')

  const tables = await Promise.all(
    LIBRARY_TABLES.map(async table => ({
      table,
      rowCount: await counters[table](),
    })),
  )

  return {
    tableCount: tables.length,
    tables,
  }
})
