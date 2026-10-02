1. Applikationens lifetime-events

I Blazor kobler man kode på `OnInitialized`, `OnParametersSet`, `OnAfterRender` og `Dispose`. I min Nuxt-app er det de samme tidspunkter, bare med andre navne.

Plugin’et `app/plugins/database-service.client.ts` kører, når appen startes. Hooket `app:mounted` er tidspunktet, hvor klienten er klar, svarende til efter første render. Her starter baggrundskørslen. `DatabaseService.vue` henter data på serveren i setup, før siden vises. `onUnmounted` rydder highlight-timeren, når komponenten fjernes. Det svarer til `Dispose`.

Servicen ligger i layoutet `app/layouts/default.vue`, under `<slot />`. Derfor bliver den stående, når jeg vælger en tabel. Mappen `CustomControls` er ikke en route. Den er registreret som komponent.

2. Custom control

2.1 Custom control’en er `DatabaseService.vue`. Den viser, hvor mange tabeller der er, og hvor mange rækker hver tabel har. Tallene kommer live fra `/api/database/overview`, som tæller med Prisma.

Baggrunden er et interval på 2 sekunder, sat i gang i `app:mounted`. `useDatabaseService` kalder `refresh` med `$fetch`. Kaldet er async, så GUI ikke venter. Et kald i gang genbruges, så to requests ikke kører oven i hinanden.

Når `overview` ændres, sammenligner `watch` de nye rækketal med de gamle. En tabel med nyt tal highlightes kort. Visningen opdaterer sig selv, uden at jeg genindlæser siden.

2.2 Async kørsel er `refresh` og API’ets `Promise.all` over tællingerne. Lifetime er: plugin ved app-start, hentning når komponenten oprettes, interval så længe appen kører, og oprydning i `onUnmounted`. Live data er staten `overview`. Kun den tabel i custom control’en, hvis tal er ændret, skifter udseende. Resten af siden, inklusiv sproget, bliver.
