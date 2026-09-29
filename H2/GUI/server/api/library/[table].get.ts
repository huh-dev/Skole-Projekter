const queries = {
  authors: () => prisma.author.findMany(),
  books: () => prisma.book.findMany(),
  users: () => prisma.user.findMany(),
  staff: () => prisma.staff.findMany(),
  loans: () => prisma.loanedBook.findMany(),
  logs: () => prisma.loanLog.findMany(),
}

type TableName = keyof typeof queries

function isTableName(value: string | undefined): value is TableName {
  return !!value && value in queries
}

export default defineEventHandler((event) => {
  const table = getRouterParam(event, 'table')

  if (!isTableName(table)) {
    throw createError({
      statusCode: 400,
      statusMessage: 'Unknown table',
    })
  }

  return queries[table]()
})
