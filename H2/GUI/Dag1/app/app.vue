<template>
  <div>
    <NuxtRouteAnnouncer />
    <iframe
      src="http://localhost:3000/f/odh8twtqxgpsladeqvsf1ad0?embed=1"
      title="Forespørgselsformular"
      style="width: 100%; min-height: 760px; border: 0"
      loading="lazy"
    />
  </div>
</template>

<script setup lang="ts">
const embedOrigin = 'http://localhost:3000'
const iframeSrcPrefix = `${embedOrigin}/f/odh8twtqxgpsladeqvsf1ad0`

function onEmbedMessage(event: MessageEvent) {
  if (
    event.origin !== embedOrigin ||
    !event.data ||
    event.data.type !== 'movea:lead-form:height'
  ) {
    return
  }
  document
    .querySelectorAll(`iframe[src^="${iframeSrcPrefix}"]`)
    .forEach((frame) => {
      ;(frame as HTMLIFrameElement).style.height = `${event.data.height}px`
    })
}

onMounted(() => window.addEventListener('message', onEmbedMessage))
onUnmounted(() => window.removeEventListener('message', onEmbedMessage))
</script>
