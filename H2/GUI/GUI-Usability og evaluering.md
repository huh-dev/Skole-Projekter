1. Projektstrukturering

`pages/CustomControls` holder database-servicen. `DatabaseService.vue` er en custom control, fordi den har sit eget data, sit eget interval og sin egen visning. Den sættes ind i layoutet og lever videre, når siden skifter.

`pages/PartialView` holder dele, der tegnes ind i en anden visning. Sprog-dropdown’en er en partial view, ikke en custom control. Den ejer ikke en baggrundsproces. Den er et stykke af toolbaren: `LanguageSelect.vue` i `AppNavbar.vue`. Et valg opdaterer kun sproget via `setLocale`. Derfor ligger den i `PartialView` og ikke i `CustomControls`.

Begge mapper er child directories under `pages`, men de er ikke routes. `nuxt.config.ts` registrerer dem som komponenter og filtrerer dem fra i `pages:extend`.

2. Andres vurdering af GUI

Tre fra klassen har svaret. Skalaen er 1 til 5, hvor 1 er godt design og 5 er dårligt.

| **Område**            | **Lui** | **Nicolai** | **Viktor** | **Gennemsnit** |
| :-------------------- | :-----: | :---------: | :--------: | :------------: |
| Lærbarhed             |    2    |      1      |      1     |     **1,3**    |
| Effektivitet          |    2    |      2      |      1     |     **1,7**    |
| Fejlrate              |    1    |      1      |      2     |     **1,3**    |
| Hukommelsesforbrug    |    1    |      1      |      1     |     **1,0**    |
| Tilfredshed           |    1    |      1      |      1     |     **1,0**    |
| **Samlet gennemsnit** | **1,4** |   **1,2**   |   **1,2**  |     **1,3**    |

