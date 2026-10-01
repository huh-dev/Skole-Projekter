import type { LibraryOverview } from '#shared/utils/library'

let refreshInFlight: Promise<void> | null = null

function readErrorMessage(cause: unknown) {
  if (cause && typeof cause === 'object' && 'statusMessage' in cause) {
    const statusMessage = (cause as { statusMessage?: unknown }).statusMessage
    if (typeof statusMessage === 'string' && statusMessage.length > 0) {
      return statusMessage
    }
  }

  if (cause instanceof Error && cause.message) {
    return cause.message
  }

  return 'Request failed'
}

export function useDatabaseService() {
  const overview = useState<LibraryOverview | null>('library-overview', () => null)
  const loadError = useState<string | null>('library-overview-error', () => null)
  const pending = useState('library-overview-pending', () => true)

  async function refresh() {
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
      .catch((cause: unknown) => {
        loadError.value = readErrorMessage(cause)
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
