import type { LibraryOverview } from '#shared/utils/library'

let refreshInFlight: Promise<void> | null = null

export function useDatabaseService() {

  //States for the overview. This keep tracks of error, pending request and the overview itself.
  const overview = useState<LibraryOverview | null>('library-overview', () => null)
  const loadError = useState<string | null>('library-overview-error', () => null)
  const pending = useState('library-overview-pending', () => true)

  async function refresh() {
    
    //If a request is already in flight, return the promise.
    if (refreshInFlight) {
      return refreshInFlight
    }

    refreshInFlight = $fetch<LibraryOverview>('/api/database/overview', {
      cache: 'no-store',
    })
      .then((next) => {
        overview.value = next
        loadError.value = null
      })
      .catch(() => {
        loadError.value = 'Request failed'
      })
      .finally(() => {
        pending.value = false
        refreshInFlight = null
      })

    return refreshInFlight
  }

  return {
    overview,
    loadError,
    pending,
    refresh,
  }
}
