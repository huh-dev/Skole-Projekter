export const LIBRARY_TABLES = [
  'authors',
  'books',
  'users',
  'staff',
  'loans',
  'logs',
] as const

export type LibraryTable = (typeof LIBRARY_TABLES)[number]
