<template>
  <DropdownMenu>
    <DropdownMenuTrigger as-child>
      <Button
        variant="outline"
        size="sm"
        class="min-w-28 justify-between"
        :aria-label="$t('nav.language')"
      >
        {{ currentLocaleName }}
        <ChevronDownIcon class="size-4 opacity-60" />
      </Button>
    </DropdownMenuTrigger>
    <DropdownMenuContent
      align="end"
      class="w-40"
    >
      <DropdownMenuLabel>{{ $t('nav.language') }}</DropdownMenuLabel>
      <DropdownMenuSeparator />
      <DropdownMenuRadioGroup
        :model-value="locale"
        @update:model-value="onLocaleChange"
      >
        <DropdownMenuRadioItem
          v-for="item in locales"
          :key="item.code"
          :value="item.code"
        >
          {{ item.name }}
        </DropdownMenuRadioItem>
      </DropdownMenuRadioGroup>
    </DropdownMenuContent>
  </DropdownMenu>
</template>

<script setup lang="ts">
import { ChevronDownIcon } from '@lucide/vue'

const { locales, locale, setLocale } = useI18n()

const currentLocaleName = computed(() => {
  const match = locales.value.find(item => item.code === locale.value)
  return match?.name ?? locale.value
})

function isConfiguredLocale(code: string): code is typeof locale.value {
  return locales.value.some(item => item.code === code)
}

function onLocaleChange(code: unknown) {
  if (typeof code !== 'string' || !isConfiguredLocale(code)) {
    return
  }

  setLocale(code)
}
</script>
