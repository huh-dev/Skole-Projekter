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

Til mundtlig kan jeg vise flowet i `Program.cs` (kalder `InsertAuthor` / `UpdateAuthor`), forklare hvad brugeren taster, og pege på at SQL’en parameteriseres i EF/driver-laget – ikke at navnet kopieres ind i en rå INSERT-streng.

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

Til mundtlig demo: `SetUser` med en librarian (fx fra `staff` i databasen) – create/read/update virker, delete afvises. Skift til en bruger med rolle `Administrator` – delete af en forfatter-record lykkes.

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

