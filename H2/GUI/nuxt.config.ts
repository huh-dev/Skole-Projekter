// https://nuxt.com/docs/api/configuration/nuxt-config
import tailwindcss from '@tailwindcss/vite'
import type { Plugin } from 'vite'

const EXCLUDED_PAGE_FOLDERS = ['/CustomControls/', '/PartialView/']

function isExerciseFolder(file: string | undefined) {
  const normalized = file?.replaceAll('\\', '/') ?? ''
  return EXCLUDED_PAGE_FOLDERS.some(folder => normalized.includes(folder))
}

function rekaPrimitiveAttrs(): Plugin {
  return {
    name: 'reka-primitive-attrs',
    transform(code, id) {
      if (!id.includes('reka-ui') || !id.replaceAll('\\', '/').includes('/Primitive/Primitive.js')) {
        return
      }

      return code
        .replaceAll('h(asTag, attrs)', 'h(asTag, { ...attrs })')
        .replaceAll('h(props.as, attrs,', 'h(props.as, { ...attrs },')
        .replaceAll('h(Slot, attrs,', 'h(Slot, { ...attrs },')
    },
  }
}

export default defineNuxtConfig({
  compatibilityDate: '2025-07-15',
  devtools: { enabled: true },
  modules: ['@nuxtjs/i18n', 'shadcn-nuxt'],
  shadcn: {
    prefix: '',
    componentDir: '@/components/ui',
  },
  css: ['~/assets/css/main.css'],
  vite: {
    plugins: [
      tailwindcss(),
      rekaPrimitiveAttrs(),
    ],
  },
  components: [
    { path: '~/pages/CustomControls', pathPrefix: false },
    { path: '~/pages/PartialView', pathPrefix: false },
    '~/components',
  ],
  hooks: {
    'pages:extend'(pages) {
      const kept = pages.filter(page => !isExerciseFolder(page.file))
      pages.splice(0, pages.length, ...kept)
    },
  },
  i18n: {
    defaultLocale: 'da',
    strategy: 'no_prefix',
    detectBrowserLanguage: false,
    locales: [
      { code: 'da', name: 'Dansk', file: 'da.json' },
      { code: 'en', name: 'English', file: 'en.json' },
    ],
  },
})