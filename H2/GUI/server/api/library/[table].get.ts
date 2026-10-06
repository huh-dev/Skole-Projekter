//This is a smarter way for us to get the table dynamically, so we can call it in one file instead of having to create a new file for each table.
//These calls are made through prisma, which is our ORM.

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
