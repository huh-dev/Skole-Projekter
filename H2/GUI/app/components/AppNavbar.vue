<template>
  <header class="border-b border-border bg-background">
    <div class="mx-auto flex max-w-5xl flex-col gap-3 px-6 py-4 sm:flex-row sm:items-center sm:justify-between">
      <div class="flex flex-col gap-2">
        <p class="text-xs font-medium tracking-wide text-muted-foreground uppercase">
          {{ $t('nav.label') }}
        </p>
        <NavigationMenu>
          <NavigationMenuList class="flex-wrap gap-1">
            <NavigationMenuItem>
              <NavigationMenuLink
                as-child
                :active="isActive('/')"
              >
                <NuxtLink
                  to="/"
                  :class="linkClass(isActive('/'))"
                >
                  {{ $t('nav.home') }}
                </NuxtLink>
              </NavigationMenuLink>
            </NavigationMenuItem>
            <NavigationMenuItem
              v-for="table in LIBRARY_TABLES"
              :key="table"
            >
              <NavigationMenuLink
                as-child
                :active="isActive(LIBRARY_TABLE_PATHS[table])"
              >
                <NuxtLink
                  :to="LIBRARY_TABLE_PATHS[table]"
                  :class="linkClass(isActive(LIBRARY_TABLE_PATHS[table]))"
                >
                  {{ $t(`tables.${table}`) }}
                </NuxtLink>
              </NavigationMenuLink>
            </NavigationMenuItem>
          </NavigationMenuList>
        </NavigationMenu>
      </div>

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
    </div>
  </header>
</template>

<script setup lang="ts">
import { ChevronDownIcon } from '@lucide/vue'
import { cn } from '@/lib/utils'
import { LIBRARY_TABLE_PATHS, LIBRARY_TABLES } from '#shared/utils/library'
import { navigationMenuTriggerStyle } from '@/components/ui/navigation-menu'

const route = useRoute()
const { locales, locale, setLocale } = useI18n()

const currentLocaleName = computed(() => {
  const match = locales.value.find(item => item.code === locale.value)
  return match?.name ?? locale.value
})

function onLocaleChange(code: string) {
  setLocale(code)
}

function isActive(path: string) {
  return route.path === path
}

function linkClass(active: boolean) {
  return cn(navigationMenuTriggerStyle(), active && 'bg-muted')
}
</script>
