<template>
  <div class="min-h-screen bg-background text-foreground">
    <AppNavbar />
    <main class="mx-auto flex max-w-5xl flex-col items-center px-6 py-10">
      <slot />
      <DatabaseService />
    </main>
  </div>
</template>
