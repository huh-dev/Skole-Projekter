export const LIBRARY_TABLES = [
  'authors',
  'books',
  'users',
  'staff',
  'loans',
  'logs',
] as const

export type LibraryTable = (typeof LIBRARY_TABLES)[number]

export const LIBRARY_TABLE_PATHS: Record<LibraryTable, string> = {
  authors: '/authors',
  books: '/books',
  users: '/users',
  staff: '/staff',
  loans: '/loans',
  logs: '/logs',
}

export function isLibraryTable(value: string): value is LibraryTable {
  return (LIBRARY_TABLES as readonly string[]).includes(value)
}

export interface LibraryTableStat {
  table: LibraryTable
  rowCount: number
}

export interface LibraryOverview {
  tableCount: number
  tables: LibraryTableStat[]
}
