1. Vælg data strategi

Valget her tager udgangspunkt i den samme tankegang som det samme spørgsmål i sidste opgave (Dag2)

2. Indsæt data i din database

2.1 INSERT og UPDATE med brugerinput

SQL injection er når en angriber skriver SQL ind i brugerinput, så appen ved en fejl kører deres kode i stedet for bare at gemme data. Det sker typisk, hvis man bygger SQL som én lang streng med `+` og sætter input direkte ind i query’en.

Jeg har udvidet appen med brugerinput til INSERT og UPDATE i mappen `Input`:

- `Inserts.InsertAuthor` – spørger om forfatternavn i konsollen, tjekker at navnet ikke er tomt, tjekker staff via `AuthorizationService`, og indsætter med `db.Authors.Add(...)` + `SaveChanges()`.
- `Updates.UpdateAuthor` – spørger om id og nyt navn, finder forfatteren med LINQ, opdaterer `author.Name` og kalder `SaveChanges()`.

Jeg bruger ikke raw SQL med streng-sammenkædning til INSERT/UPDATE. I stedet bruger jeg EF Core, som oversætter det til parameteriserede SQL-kommandoer mod MySQL. Brugerens navn sendes som en parameter (fx `@p0`), ikke som en del af SQL-teksten. Så selv hvis nogen skriver noget som `' OR 1=1 --` i navnet, bliver det behandlet som almindelig tekst og kan ikke “bryde ud” af query’en.

Samme idé bruger jeg ved SELECT med stored procedure i `Selects.GetSpecificAuthor`: `FromSqlRaw("CALL GetSpecificAuthor({0})", id)` – `{0}` bliver til en parameter, ikke indsat direkte i strengen.

Ekstra lag ud over SQL injection:

- Tomt navn afvises i `InsertAuthor`.
- Kun kendt staff må indsætte/opdatere (se punkt 3).

2.2 Reflektion

Truslen ved SQL injection er, at databasen kan blive læst, ændret eller slettet, hvis input og SQL bliver blandet sammen i samme streng. Beskyttelsen jeg har valgt, er parameterisering via EF Core: app-koden arbejder med objekter og LINQ, og driver/EF sender værdier adskilt fra selve kommandoen.

Hvis jeg i stedet havde skrevet fx `INSERT INTO authors (name) VALUES ('" + name + "')"`, kunne et ondsindet navn ændre meningen i SQL. Med `Add` + `SaveChanges()` eller `FromSqlRaw` med placeholders undgår jeg det mønster.

Jeg forstår SQL injection bedst som: “input må aldrig være en del af SQL-syntaksen – kun data sendt sikkert ind”. Det er derfor opgaven kræver en bevidst teknik, ikke bare at “gemme fra console”. EF Core er min teknik her, fordi den giver INSERT/UPDATE (og CALL til procedure) uden manuel string-building.

3. Rettighedsstyring

3.1 Least privilege og adgang

Least privilege betyder, at man kun får den adgang, man har brug for – ikke mere. I mit bibliotek skal en almindelig medarbejder kunne create, read og update, mens kun admin må slette en record, så data ikke kan fjernes af forkerte personer.

Appen får adgang til databasen via connection string i `appsettings.json` og EF Core `AppDbContext` (samme database som tidligere øvelser). Det er en teknisk database-bruger mod MySQL den reelle begrænsning af hvem der må gøre hvad, ligger i applikationen oven på tabellen `staff`.

Sådan styrer jeg hvem der er “logget ind” og hvad de må:

- `CurrentUserService` – holder navnet på den nuværende bruger (`SetUser` fx fra `Program.cs` til demo).
- `StaffRoles` – enum med `Librarian` og `Administrator`.
- `AuthorizationService` – slår brugeren op i `staff` på navn og læser `Role` fra databasen. `IsAuthorizedAdmin` returnerer kun true, hvis rollen er `Administrator`.

Handlinger i `Input`:

- Create: `Inserts.InsertAuthor` – brugerinput, derefter gem med EF (`Add` + `SaveChanges`). Adgang: `IsAuthorizedStaff`.
- Read: `Selects.GetSpecificAuthor` – læser via stored procedure `GetSpecificAuthor` med parameteriseret id.
- Update: `Updates.UpdateAuthor` – finder forfatter og opdaterer navn. Adgang: `IsAuthorizedStaff`.
- Delete: `Deletes.DeleteAuthor` – kun admin via `IsAuthorizedAdmin`, derefter `Remove` + `SaveChanges`.

3.2 Reflektion

Uautoriseret adgang begrænser jeg i to lag. Databasen har stadig constraints (FK, NOT NULL), så data hænger sammen. Ovenpå det tjekker appen identitet mod `staff`, så en handling som sletning ikke bare kører for alle, der kan starte console-appen.

Sletning er den mest sensitive handling i opgaven, fordi den er svær at fortryde. Derfor er det kun `Administrator`, der må slette – ikke `Librarian`. Create, read og update er mindre destructive, så de må staff uden admin-rolle udføre, når de er logget ind via `CurrentUserService`.

Det er ikke fuld database-sikkerhed alene med app-tjek (nogen med root-adgang til MySQL kunne stadig slette direkte), men for opgaven er det det rigtige sted at håndhæve “hvem må hvad” i flowet brugeren ser. Least privilege på DB-niveau ville være separate MySQL-brugere med fx kun SELECT/INSERT/UPDATE for app-brugeren og DELETE kun for admin – det har jeg ikke sat op her. Min løsning er rettighedsstyring i .NET mod `staff`-tabellen.

4. Validering af data

4.1 Behov for validering med brugerinput

Når data oprettes og opdateres fra konsollen, kan brugeren taste forkert format, tomme felter eller værdier uden for det, databasen og forretningen tillader. Validering skal derfor ske før `SaveChanges`, så dårlig data stopper i appen i stedet for at give DB-fejl – eller i værste fald gemmes som ugyldig data.

Jeg bygger videre på Dag2 constraints og forretningsregler gælder stadig (FK, NOT NULL på andre tabeller, låneperioder i `LoanService` osv.). I Dag3 fokuserer jeg på validering omkring forfatter-input i `Input`

App (forretningsregler / input-validering):

- `InsertAuthor`: `string.IsNullOrEmpty(name)` – navn skal udfyldes.
- `DeleteAuthor`: `int.TryParse` på id – id skal være et tal; tjek at forfatter findes før `Remove`.
- `UpdateAuthor`: tjek at forfatter findes på id; id læses med `int.Parse` (kræver gyldigt tal – ellers exception).
- `GetSpecificAuthor` / procedure: `GetSpecificAuthor` i SQL afviser id `<= 0` eller `NULL` med `SIGNAL` – format/regel i databasen.

Database (constraints fra tidligere øvelse):

- `authors.name` er `varchar(255)` – for lang tekst kan afvises eller afkortes af DB jeg kunne også tjekke længde i appen.
- `FOREIGN KEY` på `books.author_id` – sletning af forfatter med tilknyttede bøger fejler, hvis DB har restrict (beskyttelse mod “ulovlig” sletning i domænet).
- PK og NOT NULL på andre tabeller som før.

Lovlige tegn: jeg har ikke lavet et strict regex på navn (fx kun bogstaver), fordi forfatternavne kan indeholde mellemrum, bindestreg og specialtegn. Det vigtige er tomhed, gyldigt id og at data passer til kolonnetype/længde. Mere striks tegn-validering kunne tilføjes i appen, hvis biblioteket kræver det.

4.2 Reflektion

Validering af brugerinput er vigtig, fordi brugeren ikke tænker i tabeller og datatyper – de taster tekst. Uden tjek risikerer man tomme navne, bogstaver hvor der skal være tal, eller handlinger på id’er der ikke findes. Det ødelægger data quality og gør fejl sværere at finde.

Jeg håndhæver regler på to niveauer:

1. App før DB – hurtig feedback (`Name is required`, `Invalid id`, `Author not found`) og ingen unødig roundtrip til MySQL.
2. DB som sikkerhedsnet – constraints og procedure-regler fanger det, der slipper igennem eller kommer ind uden om appen.

Teknikkerne jeg bruger, er simple `IsNullOrEmpty`, `TryParse`/`Parse`, `FirstOrDefault` + null-tjek. Det er samme princip som Dag2 (regler i `Services`/`Input`), bare udvidet til det brugeren skriver i konsollen.

5. Stored Procedures

5.1 Valg og implementering

En stored procedure er SQL-logik, der ligger gemt i databasen og kan kaldes med `CALL` (i mit projekt MySQL – samme idé som på SQL Server i opgaveteksten). Det giver mening at lægge noget i databasen, når regler eller queries kan ændres uden at recompile appen – fx hvordan vi henter en forfatter, eller validering der skal gælde uanset om data kommer fra EF, TablePlus eller en anden klient.

Jeg har valgt at lægge read-logik for “hent forfatter på id” i en procedure, fordi det er en fast database-operation med tydelige regler for gyldigt id. CRUD med brugerinput og rettigheder bliver i C# (`Input` + `AuthorizationService`); proceduren er data/adgang, som DBA eller lærer kan rette i SQL-filen uden at røre `.cs`-filer.

Procedure: `GetSpecificAuthor` i `StoredProcedures/stored_procedures.sql`

- Parameter: `IN authorId int` – værdien sendes som parameter ved `CALL`, ikke som tekst i SQL-strengen (beskyttelse mod SQL injection når appen kalder den via `FromSqlRaw("CALL GetSpecificAuthor({0})", id)`).
- Henter data: `SELECT * FROM Authors WHERE Id = authorId` (læser én forfatter).
- Validering/fejl: `IF authorId IS NULL OR authorId <= 0 THEN SIGNAL SQLSTATE '45000' ...` – ugyldigt id stopper med fejl i stedet for stille at returnere forkert resultat.

Deploy fra .NET (samme mønster som triggers i Dag2): `StoredProcedures.CreateStoredProcedures` læser SQL-filen og kører `ExecuteSqlRaw` ved opstart (`Program.cs`). Appen kalder proceduren gennem `Selects.GetSpecificAuthor`.

5.2 Reflektion

En stored procedure er en genbrugelig “funktion” i databasen: `CREATE PROCEDURE`, parametre (`IN`), `BEGIN`/`END`, og kontrolstrukturer som `IF`/`THEN`/`END IF`. Her bruger jeg `SIGNAL` til fejlhåndtering – det svarer til at kaste en fejl, som appen kan fange (fx `MySqlException` i `Program.cs`).

Fordelen ved at have `GetSpecificAuthor` i DB er, at validering af id og selve SELECT’en ligger ét sted. Hvis biblioteket senere vil join’e flere tabeller i “hent forfatter”-viewet, kan man opdatere proceduren og deploye SQL igen, så længe signatur og `CALL` stadig matcher.

6. Fejlhåndtering

6.1 Database-exceptions i appen

Når kode taler med en database, kommer fejl sjældent som “almindelige” C#-fejl alene. EF Core og MySQL-brugeren giver exceptions, som hører til database-laget – dem skal appen fange og oversætte til beskeder brugeren forstår.

De vigtigste i mit projekt:

- `DbUpdateException` (EF Core) – kastes når `SaveChanges`/`SaveChangesAsync` fejler, fx ved `FOREIGN KEY`, `NOT NULL` eller andre constraints. Ofte ligger den rigtige MySQL-fejl i `InnerException`.
- MySQL-driver exception (fx fra `MySqlConnector`/`MySql.Data` via EF) connection fejl, SQL-syntaks, eller `SIGNAL` fra stored procedure (`ID must be greater than 0`).
- `InvalidOperationException` – fx hvis connection string mangler i `AppDbContext` (opsætning før DB-kald).

Implementering:

- `Program.cs` – `try`/`catch` omkring `Selects.GetSpecificAuthor(context, -1)`. Procedurens `SIGNAL` giver en database-relateret exception; i `catch` skriver jeg `ex.Message`, så brugeren fx ser `ID must be greater than 0` i stedet for et crash.
- Ved `SaveChanges` i `Input` lader jeg EF kaste `DbUpdateException`, hvis constraints fejler (fx slet forfatter med bøger). Det kan man fange på samme måde med `catch (DbUpdateException ex)` og `Console.WriteLine(ex.InnerException?.Message ?? ex.Message)` – jeg har valgt ikke at duplikere try/catch i hver metode, når demoen i `Program` viser princippet.

App-validering (tomt navn, ugyldigt id, author not found) giver beskeder uden exception. Database-exceptions handler jeg om, når fejlen kommer fra DB/framework.

6.2 Reflektion

Exception-håndtering mod databaser er vigtig, fordi fejl fra MySQL/EF ofte kommer som `DbUpdateException` eller inner MySQL-fejl – ikke som tekst brugeren forstår. Et `catch` omkring database-kald giver en meningsfuld besked (`ex.Message` eller inner exception).

Jeg bruger `Console.WriteLine` + `return` til forventede fejl i input (punkt 4). Til procedure-demo fanger jeg den exception, databasen sender tilbage, når id er ugyldigt. Man kunne udvide med `catch (DbUpdateException)` omkring `SaveChanges`, hvis man vil vise constraint-fejl ved delete – samme teknik.

