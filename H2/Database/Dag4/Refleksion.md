1. Performance-analyse

1.1 Identificering og undersøgelse

Når databasen indeholder mange rækker, bliver forespørgsler der filtrerer, sorterer, joiner eller scanner store tabeller typiske kandidater til performance-problemer. I mit bibliotek er det især:

- Søgning og sortering på `books.price` kombineret med join til `authors`
- Filtrering på `loaned_books.loan_end` med sortering på `loan_start` og join til `users` og `books`

Jeg har samlet før/efter-målinger i `performance_analysis.sql`. Scriptet dropper først de sekundære indekser `idx_books_price` og `idx_loaned_end_start`, så “før”-scenariet ikke er forurenet af tidligere kørsel.

**Query A** – dyre bøger med forfatter:

```sql
SELECT b.title, b.price, a.name AS author_name
FROM books b
INNER JOIN authors a ON b.author_id = a.id
WHERE b.price > 250
ORDER BY b.price DESC;
```

**Query B** – aktuelle/nyere udlån (sidste 6 måneder), sorteret efter lånestart:

```sql
SELECT u.name, b.title, lb.loan_start, lb.loan_end
FROM loaned_books lb
INNER JOIN users u ON lb.user_id = u.id
INNER JOIN books b ON lb.book_id = b.id
WHERE lb.loan_end >= DATE_SUB(NOW(), INTERVAL 6 MONTH)
ORDER BY lb.loan_start DESC
LIMIT 500;
```

**Værktøjer jeg har brugt (MySQL i stedet for SSMS):**

| Opgavens idé (SQL Server) | Mit valg (MySQL) | Hvad det viser |
| --- | --- | --- |
| Execution plan | `EXPLAIN` / `ANALYZE` foran SELECT | Om der bruges index, join-rækkefølge, estimerede rækker, “Using filesort” osv. |
| Tid pr. query | `SET profiling = 1` → kør SELECT → `SHOW PROFILES` | Samlet varighed for den seneste forespørgsel |
| I/O / læsning af data | `FLUSH STATUS` → SELECT → `SHOW SESSION STATUS LIKE 'Handler_read%'` og `LIKE 'Sort%'` | Antal key/table reads og sort-operationer i sessionen |

Andre muligheder jeg har kigget på, men ikke brugt som hovedmetode her: **slow query log** (logger langsomme queries over en tærskel), **Performance Schema** (detaljeret timing i nyere MySQL), og i SSMS ville **Actual Execution Plan** og **SET STATISTICS IO/TIME** give tilsvarende plan + disk reads + CPU-tid.

**Potentielt problem (før index):**

- Query A: Filter på `price` og `ORDER BY price DESC` uden passende index kan give scan af mange rækker i `books` og ekstra sortering (filesort i `EXPLAIN`).
- Query B: Betingelse på `loan_end` og sortering på `loan_start` på en stor `loaned_books`-tabel kan give bred scan og sort, selv med `LIMIT 500`, hvis optimiseren ikke kan stoppe tidigt via index.

**Implementeret optimering:**

- `CREATE INDEX idx_books_price ON books (price DESC);` – understøtter filter `price > 250` og sortering efter pris.
- `CREATE INDEX idx_loaned_end_start ON loaned_books (loan_end, loan_start);` – composite index til interval på slutdato og sortering på startdato.

Efter index kører jeg samme `ANALYZE`/`EXPLAIN`, profiling og status igen. Ved Query A har jeg også testet `STRAIGHT_JOIN` efter index, så join-rækkefølgen ikke skifter til en plan, der igen scanner unødigt – det er et bevidst valg når `EXPLAIN` viser, at optimiseren vælger en dyr join-rækkefølge trods index på `books`.

1.2 Reflektion

Jeg har undersøgt performance ved at vælge to “realistiske” biblioteks-queries, køre dem uden de sekundære indekser, og dokumentere plan (`EXPLAIN`/`ANALYZE`), tid (`SHOW PROFILES`) og læse-mønster (`Handler_read%`). Problemet identificerer jeg ikke kun på “det føles langsomt”, men på tegn som full/index scan på store tabeller, filesort og høj `Handler_read_rnd_next` eller `Sort_merge_passes` før optimering.

Jeg valgte netop index på `(price)` og `(loan_end, loan_start)`, fordi de matcher **WHERE** og **ORDER BY** i de to queries – det er den klassiske regel: index skal understøtte det, query’en filtrerer og sorterer på. Composite index på `loaned_books` har `loan_end` først, fordi intervallet i `WHERE` er på slutdato; `loan_start` er med i nøglen, så sorteringen ofte kan undgå separat filesort.

Jeg er opmærksom på trade-offs: indekser gør SELECT hurtigere, men tager plads og gør INSERT/UPDATE/DELETE langsommere, fordi sekundære nøgler skal vedligeholdes. Til læse-tunge rapporter (dyre bøger, udlånslister) er det i mit bibliotek en fornuftig omkostning. Hvis jeg havde SQL Server, ville jeg have brugt samme tankegang med execution plan og `STATISTICS IO`/`TIME` i stedet for MySQL profiling og Handler-status – målet er det samme: se plan, tid og mængden af data læst fra disk/cache.

2. Indexering

2.1 Clustered og nonclustered index

**Clustered index (InnoDB / PRIMARY KEY)**

I SQL Server vælger man ofte eksplicit et clustered index. I InnoDB er clustered index altid primærnøglen: tabel-data ligger i PK-rækkefølge. Alle mine tabeller har `id` som `PRIMARY KEY` – det er allerede clustered index.

I `database_indexing.sql` demonstrerer jeg det på `books`:

- `SHOW INDEX FROM books;` – viser at `PRIMARY` på `id` er clustered (I InnoDB: `Index_type` / PK)
- Lookup på primærnøgle: `SELECT id, title, price FROM books WHERE id = 25000;` med `ANALYZE` før/efter – typisk meget få rækker læst, fordi rækken findes direkte via PK

**Nonclustered index (sekundære indekser)**

Jeg har oprettet mindst to sekundære indekser (MySQL: almindelige `CREATE INDEX`):

1. `idx_users_email ON users (email)` – opslag på unik/lokaliseret email (`perf_u1500@test.dk` i testscriptet)
2. `idx_authors_name ON authors (name)` – søgning på forfatternavn (`Forfatter 250`)
3. `idx_loaned_end_start ON loaned_books (loan_end, loan_start)` – overlapper med performance-opgaven; dækker datointerval + sortering

Scriptet kører `ANALYZE` før og efter hvert `CREATE INDEX`, så jeg kan sammenligne planen. Til sidst: `SHOW INDEX FROM users`, `authors`, `loaned_books` som dokumentation.

Sekundære indekser i InnoDB gemmer index-nøglen + pointer til primary key (`id`), så et lookup på email eller name ofte bliver: index seek → derefter PK lookup for at hente resten af rækken (covering index hvis alle kolonner er i indexet – her henter jeg også `name`/`id`, så der kan være ekstra PK-opslag).

2.2 Reflektion

Et **clustered index** bestemmer den fysiske (logiske) rækkefølge af data i tabellen. Der kan kun være ét per tabel. I mit projekt er det `PRIMARY KEY (id)` på hver tabel – godt til joins og opslag på id (fx `loaned_books.book_id` → `books.id`).

Et **nonclustered index** er en separat struktur med sorterede nøgleværdier og reference til clustered key. Jeg oprettede dem, hvor queries **ikke** starter med id, men med `email`, `name`, `price` eller lånedatoer – ellers ville InnoDB ofte scanne hele tabellen.

Betydning for min database:

- **PK/clustered**: hurtig adgang når app eller FK peger på `id`
- **idx_users_email**: login/opslag på bruger uden full scan af `users`
- **idx_authors_name**: find forfatter ved navn (supplerer Dag3-procedure, men index hjælper alle SELECT med `WHERE name = ...`)
- **idx_loaned_end_start**: bedre performance på udlåns-rapporter og filtrering på aktive/nyere udlån

Jeg ville ikke indexere hver kolonne: for mange indekser belaster skrivninger og seed/import. Valget bygger på de queries biblioteket faktisk kører – søgning på bruger/forfatter, pris-lister og udlån over tid – samme problemstillinger som i punkt 1.
