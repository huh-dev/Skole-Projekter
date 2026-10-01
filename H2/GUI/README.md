# H2 GUI (Nuxt)

Library table viewer with i18n (da/en) and Prisma + MariaDB.

## Setup

Run commands from this folder (`H2/GUI`), not the empty `gui` shortcut at the repo root unless that symlink exists.

```bash
cp .env.example .env
bun install
```

Edit `.env` if your MySQL user, password, or database name differs from the example.

Apply migrations (requires a running MySQL server and database):

```bash
bunx prisma migrate deploy
```

## UI (shadcn-vue)

This project uses [shadcn-vue](https://www.shadcn-vue.com/docs/installation/nuxt) with the `shadcn-nuxt` module. Add more components:

```bash
bun x shadcn-vue@latest add <component>
```

## Development

```bash
bun run dev
```

Open [http://localhost:3000](http://localhost:3000).

## Production build

```bash
bun run build
bun run preview
```
