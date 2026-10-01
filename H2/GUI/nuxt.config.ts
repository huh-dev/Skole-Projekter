// https://nuxt.com/docs/api/configuration/nuxt-config
import tailwindcss from '@tailwindcss/vite'
import type { Plugin } from 'vite'

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