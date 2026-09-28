export const LIBRARY_TABLES = [
  { value: 'authors', label: 'Forfattere' },
  { value: 'books', label: 'Bøger' },
  { value: 'users', label: 'Brugere' },
  { value: 'staff', label: 'Personale' },
  { value: 'loans', label: 'Udlån' },
  { value: 'logs', label: 'Udlånslog' },
] as const

export type LibraryTable = (typeof LIBRARY_TABLES)[number]['value']
