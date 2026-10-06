const POLL_INTERVAL_MS = 2000
const TIMER_KEY = '__libraryDatabaseServiceTimer'

//Found on stackoverflow to ensure the database is being refreshed every 2 seconds for the client side.

export default defineNuxtPlugin((nuxtApp) => {
  const host = window as Window & { [TIMER_KEY]?: number }
  const { refresh } = useDatabaseService()

  nuxtApp.hook('app:mounted', () => {
    if (host[TIMER_KEY]) {
      window.clearInterval(host[TIMER_KEY])
    }

    host[TIMER_KEY] = window.setInterval(() => {
      void refresh()
    }, POLL_INTERVAL_MS)
  })
})
