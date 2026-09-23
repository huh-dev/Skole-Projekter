1. Valg af data strategi.

Jeg har valgt Entity Framework Core som data strategi. Det gør jeg primært, fordi vi tidligere har arbejdet med den tilgang, så jeg kender den godt. Jeg overvejede kraftigt et hybridt setup med en kombination af EF Core og Dapper, så jeg kunne få de lette LINQ-integrationer fra EF Core og den hurtigere adgang til data, som Dapper giver. Til denne opgave valgte jeg alligevel at holde programmet simpelt og vente med et hybridt setup til større projekter, hvor det giver mere mening.

2. Implementer seeding function i din app

Jeg har implementeret seeding i `Database.cs` i metoden `SeedData`. Metoden læser data fra fire JSON-filer i mappen Data (authors, staff, users og books), deserialiserer dem til mine entity-klasser, og tilføjer dem til databasen med `AddRange` på de tilhørende DbSets. Til sidst kalder den `SaveChangesAsync`, så ændringerne bliver gemt i MySQL.

3. Dataintegriteten

3.2 Forretningsregler

Forretningsregler er de regler, der gælder i mit bibliotekssystem – altså hvad der er tilladt og ikke tilladt, uanset om data kommer fra min console app eller direkte mod databasen. Mange regler kan håndhæves med constraints i databasen, men logik der kræver kontekst (fx hvem der er logget ind) hører ikke hjemme i SQL alene.

Jeg startede med at scaffolde `db_dag_1` fra Dag2-mappen med EF Core Tools:

```
dotnet ef dbcontext scaffold "Server=localhost;Database=db_dag_1;User=root;Password=;Port=3306;" MySql.EntityFrameworkCore -o Entities -c AppDbContext
```

Kommandoen genererede entity-klasserne og `AppDbContext` i mappen `Entities`, så jeg har et lag til data og adgang til tabellerne. Forretningsregler og handlinger har jeg lagt i `Services` selv – det er OOP og inkapsulering nok her. I en console app er dependency injection ikke standard, men fx får `AuthorService` stadig sin `AppDbContext` via konstruktøren.

Mindst fire regler (én per CRUD):

- Create: Et udlån må kun oprettes med en gyldig låneperiode – `loan_start` skal ikke ligge efter `loan_end`. Det tjekker jeg i `LoanService.Create` før rækken gemmes. Databasen sikrer samtidig via FK, at bruger, bog og medarbejder findes.
- Read: Kun personale må slå et udlån op. I `LoanService.GetById` kræver jeg et `Staff`-objekt med rolle; ellers kastes der en fejl.
- Update: En bogs pris må ikke sættes negativ. Det ligger i `BookService.SetPrice`, som kun opdaterer `Price`, når værdien er valid.
- Delete: En forfatter må ikke slettes, hvis der stadig er bøger knyttet til vedkommende. `AuthorService.Delete` tæller bøger først og stopper med en fejl, hvis count > 0. Det understøttes også af `FOREIGN KEY` og `Restrict` på `books.author_id`.

3.3 Reflektion

Jeg placerer forretningsregler i service-laget, fordi de beskriver domænet (biblioteket), ikke bare tabeller. Databasen bruger jeg til det, den er stærk til: dataintegritet med PK, FK, NOT NULL og restrict ved sletning, så data hænger sammen og ikke kan blive “løs” på en måde, appen ikke forventer.

Sammen giver det to lag af sikkerhed: constraints fanger fx ugyldige id’er og påkrævede felter, mens services fanger regler der kræver beregning eller brugerrolle (låneperiode, staff-only læsning, forfatter-sletning). Hvis jeg senere tilføjede dependency injection, ville services stadig være det naturlige sted for reglerne – DI ville primært styre, hvordan jeg får `AppDbContext` og services ind i `Program`, ikke hvor reglerne er defineret.

4. Constraints

4.1 Forskellen og valgt område

Constraints og forretningsregler kan beskrive den samme idé, men de håndhæves forskelligt. En constraint ligger i databasen og gælder altid – også hvis nogen indsætter data uden om min app (fx direkte i TablePlus). En forretningsregel i applikationen kører kun, når koden kalder den rigtige service-metode, og den kan bruge kontekst databasen ikke kender (fx om den nuværende bruger er staff).

Et konkret eksempel fra mit projekt: i `LoanService.Create` tjekker jeg, at `loan_start` ikke ligger efter `loan_end`. Det er en forretningsregel i C#. Databasen kan ikke vide, hvem der opretter udlånet, men den kan stadig sikre, at `user_id`, `book_id` og `staff_id` peger på rigtige rækker – det gør `FOREIGN KEY`-constraints på `loaned_books`. Hvis jeg prøver at indsætte et udlån med et bog-id, der ikke findes, afviser MySQL det, uanset om det kommer fra EF Core eller manuel SQL.

Jeg har valgt referentiel integritet på udlån som mit constraint-fokus, fordi et bibliotekssystem kun giver mening, hvis hvert udlån er koblet til en eksisterende bruger, bog og medarbejder. Det er implementeret i `db_dag_1` med constraints som:

- `PRIMARY KEY` på alle tabeller – unikt id per række
- `NOT NULL` på fx `books.title`, `books.price` og alle id-felter i `loaned_books` – påkrævede felter må ikke mangle
- `FOREIGN KEY` på `loaned_books` mod `users`, `books` og `staff`, og på `books.author_id` mod `authors`

I Dag2 genbruger jeg det via scaffold: `AppDbContext` mapper constraints (fx `DeleteBehavior.Restrict` på relationerne), så EF og databasen er enige om, at man ikke bare kan slette data, som andre rækker stadig peger på.

4.2 Reflektion

De regler jeg har identificeret for mine data, falder i to kategorier. Strukturelle regler (unikke id’er, påkrævede felter, gyldige relationer) håndhæver jeg primært med constraints i databasen, fordi de er simple, stabile og gælder for alle klienter. Domæneregler med logik eller brugerrolle (gyldig låneperiode, kun staff må læse udlån, forfatter må ikke slettes med bøger) ligger i `Services`, fordi de er nemmere at læse, teste og udvide i C#.

Jeg har bevidst ikke lagt fx låneperioden som `CHECK`-constraint i SQL. Databasen kunne tjekke `loan_end >= loan_start`, men så ville reglen være duplikeret ved siden af `LoanService` – og fejlbeskederne er ofte tydeligere i appen. Til gengæld er FK på udlån et sted, hvor constraint er det rigtige valg: ingen service kan “overtale” databasen til at gemme et udlån til en ikke-eksisterende bog.

Samlet set bruger jeg constraints som det nederste sikkerhedsnet, så data altid er konsistent og normaliseret, og forretningsregler som ekstra lag, hvor systemet skal opføre sig bestemt ud fra bibliotekets regler – ikke kun tabellernes form.

5. Automatisering

5.1 Trigger og automatisering

En trigger er SQL-kode i databasen, som kører automatisk når noget sker i en tabel – fx `INSERT`, `UPDATE` eller `DELETE`. Automatisering betyder, at databasen selv laver det næste step uden at appen skal huske det.

I mit bibliotek vil jeg have history, når der oprettes udlån i `loaned_books`. En trigger er god til det, fordi den også kører, hvis data kommer ind uden om min C#-kode (fx raw SQL eller TablePlus). Det er database-side log – se også punkt 6 om fil-log fra appen.

Min trigger hedder `trg_add_loan_logs` (`Triggers/loan_trigger.sql`). Den er `AFTER INSERT` på `loaned_books`. Efter en ny række laver den `INSERT` i `loan_log` med `loan_id`, `book_id`, `user_id` og action `'INSERT'` (via `NEW`).

Opgaven kræver, at triggeren oprettes fra .NET:

- `Triggers.CreateTriggers` læser SQL-filen
- `ExecuteSqlRaw` kører SQL mod databasen
- `Program.cs` kalder `database.CreateTriggers()` ved start

Triggeren ligger altså i projektet som fil, og appen deployer den – ikke kun manuelt i TablePlus.

5.2 Reflektion

Triggeren er bundet til tabellen: hver `INSERT` i `loaned_books` → ny række i `loan_log`. Jeg skal ikke kalde log-kode i `LoanService.Create` efter `SaveChangesAsync` for at få den del af historikken.

Jeg valgte `INSERT` på udlån, fordi det er det vigtigste event at spore. Triggeren er bevidst simpel. Tung business logic ville jeg stadig lægge i `Services`.

Triggeren dækker automation og log i databasen. I punkt 6 tilføjer jeg log til fil, som opgaven også kræver – så jeg har to måder at lave data history på, som supplerer hinanden.

6. Data historic/log

6.1 Logning til fil

Opgaven kræver logning af operationer mod databasen som data history, og at det sker mod en fil. Det har jeg lavet i klassen `Logs` med metoden `Save`.

Når appen har lavet en operation (fx oprette udlån), kan den kalde:

`Logs.Save(dato, action, recordId, table)`

Metoden appender en linje til `logs.txt` i projektmappen med `File.AppendAllText`. Formatet er:

`dato - action - recordId - table`

Eksempel fra min test:

`9/22/2026 10:21:43 AM - Create - 2 - loaned_books`

I `Program.cs` kalder jeg `LoanService.Create` og derefter `Logs.Save` (lige nu udkommenteret til test). Parameteren `action` er fx `Create`, `recordId` er id på den nye række, og `table` er hvilken tabel der blev ændret.

Sammen med triggeren i punkt 5 betyder det:

- Trigger (`trg_add_loan_logs`) → history i `loan_log` (automatisk i DB)
- `Logs.Save` → history i `logs.txt` (fra appen, når jeg vælger at logge)

6.2 Reflektion

Dataintegritet handler ikke kun om, at data er korrekte lige nu (constraints, `FK`, `NOT NULL`). Det handler også om, at man kan stole på og efterprøve data over tid. Log og history hjælper med det.

Hvis noget ser forkert ud i `loaned_books`, kan jeg sammenligne med `loan_log` og `logs.txt` og se hvornår der skete en `Create`, og hvilket id det gav. Det er audit – jeg mister ikke konteksten, selv om nogen senere laver `UPDATE` eller `DELETE` i databasen.

Fil-log er simpel at læse og ligger ved siden af appen i git/demo. DB-log via trigger fanger også ændringer uden om appen. Ingen af delene erstatter constraints, men de gør det nemmere at finde fejl og holde systemet accountable – det er en del af den samlede data integrity i mit biblioteksprojekt.
