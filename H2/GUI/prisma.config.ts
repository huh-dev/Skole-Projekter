import { existsSync } from 'node:fs'
import { config } from 'dotenv'
import { defineConfig } from 'prisma/config'

if (existsSync('.env')) {
  config({ path: '.env' })
} else {
  config({ path: '.env.example' })
}

const defaultDatabaseUrl = 'mysql://root@127.0.0.1:3306/dag1'

export default defineConfig({
  schema: 'prisma/schema.prisma',
  migrations: {
    path: 'prisma/migrations',
  },
  datasource: {
    url: process.env.DATABASE_URL ?? defaultDatabaseUrl,
  },
})
