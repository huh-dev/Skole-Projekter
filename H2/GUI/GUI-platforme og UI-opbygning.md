1. Statisk/dynamisk rendering i GUI

Rendering er, når appen laver state om til det, jeg ser i browseren. Delvis rendering opdaterer kun den del, der er ændret, uden at loade hele dokumentet forfra.

DOM er browserens træ af rigtige HTML-elementer. Render tree er det virtuelle træ, Vue bygger først. Vue sammenligner det med det forrige træ og patcher kun forskellen ind i DOM.

Derfor kan overskriften og menuen blive stående, mens tabel-indholdet skiftes ud.
Overskriften på forsiden siger selv, at den ikke ændrer sig.

Jeg bruger Nuxt/Vue’s indbyggede rendering og `useFetch`, ikke `$.ajax` eller HTMX. `useFetch` kalder fetch, men jeg opdaterer ikke DOM selv. Vue gør det, når data lander i state.

Tabellerne står i `LIBRARY_TABLES` i `shared/utils/library.ts`. API’et `server/api/library/[table].get.ts` henter rækkerne med Prisma.

Senere opgaver flyttede `select` fra forsiden. Valget ligger nu i `AppNavbar.vue`. Visningen er partial view `LibraryTableView.vue`, brugt af `[table].vue`. Forsiden beholder den statiske overskrift. Layoutet skifter kun `<NuxtPage />`.

Nyt tabelnavn får `useFetch` til at hente igen. Komponenten renderer kun sig selv: `v-if` vælger loading, fejl eller rækker. Menuen bliver i DOM.

2. Rendering med Session

2.1 Dropdown’en er `LanguageSelect.vue`, sat ind som menu-item i toolbaren i `AppNavbar.vue`. Den lister Dansk og English. Knappen viser det valgte sprogs navn. Et valg kalder `setLocale`.

2.2 Session/state er `locale` fra `useI18n`. Den værdi er betingelsen for sproget: `$t()` læser `da.json` eller `en.json`, så titlen skifter. Default er `da` i `nuxt.config.ts`.

Sprogskiftet er delvis rendering. Kun de oversatte tekster opdateres. Tabelrækkerne kommer fra API’et og hentes ikke igen, så tabelvisningen bliver ikke forstyrret.

Vælger jeg en ny tabel, huskes sproget. `locale` ligger i i18n-staten, ikke på siden, så den falder ikke tilbage til default.

2.3 I opgave 1 udskifter delvis rendering tabel-indholdet i DOM, når jeg vælger en tabel. Render tree for listen skiftes, mens menuen bliver.

Her udskifter delvis rendering kun tekster ud fra session/state. Samme tabel-data bliver i DOM, mens titlen følger det sprog, sessionen husker.
