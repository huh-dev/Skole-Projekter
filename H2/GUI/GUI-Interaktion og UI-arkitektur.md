1. Event-handling og design mønstre i GUI

1.1 Et event er noget, brugeren gør i GUI, fx et klik eller et valg. Event-handling er funktionen, der kører bagefter, uden at hele siden loades forfra.

Menuen i `AppNavbar.vue` er et event. Et klik på `NuxtLink` skifter tabel, og `LibraryTableView` henter den via `useFetch`. Sprog-dropdown’en i `LanguageSelect.vue` lytter på `update:model-value` og kalder `onLocaleChange`, som sætter `locale`. Det rører ikke tabel-data, så det valgte sprog bliver stående.

Samme proces gælder alle tabeller. `LIBRARY_TABLES`, siden `[table].vue`, `LibraryTableView` og API’et `server/api/library/[table].get.ts` er ét flow. Der er ikke særskilt kode per tabel.

Mønstret er Navigation Menu: menuen vælger Home eller en tabel, og kun side-indholdet skifter. Under det er Card Layout: hver række er et kort i `LibraryTableView`.

Indsæt, opdater og slet er ikke bygget ind i visningen. Den ensartede GUI er navigation og læsning, ikke en formular der ændrer rækker.

1.2 Event-handling sidder i lytteren. Dropdown’en har `update:model-value`. Menuen har links. Handleren ændrer kun den state, eventet gælder: sprog er `locale`, tabel er routen. De blandes ikke, så et tabel-skift ikke nulstiller sproget, og et sprog-skift ikke henter tabellen igen.

Navigation Menu betyder, at et valg i menuen skifter funktion, mens toolbaren bliver. Card Layout betyder, at én post er ét kort, så listen kan opdateres kort for kort, når data ændrer sig.
