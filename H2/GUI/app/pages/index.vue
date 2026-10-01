<template>
  <div class="flex w-full max-w-2xl flex-col items-center text-center">
    <h1 class="text-[2rem] leading-tight font-semibold tracking-tight">
      {{ $t('title') }}
    </h1>
    <p class="mt-1.5 text-base text-muted-foreground">
      {{ $t('titleNote') }}
    </p>
    <p class="mt-6 text-sm text-muted-foreground">
      {{ $t('nav.hint') }}
    </p>
  </div>
</template>
