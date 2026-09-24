1. Vælg en problemstilling.

Jeg valgte et bibliotek, fordi det er et emne, hvor flere ting hører sammen, men ikke skal stå i samme tabel. Der er bøger og forfattere, brugere der låner, personale der registrerer udlån, og selve lånet med start- og slutdato. Det passer godt til normalisering: forfatternavn gemmes ét sted, bogoplysninger et andet, og når en bruger låner en bog, bruger jeg kun id’er.

Databasen `db_dag_1` har fem tabeller: `authors`, `books`, `users`, `staff` og `loaned_books`. Hver tabel har `id` som primærnøgle, og tabellerne er koblet med fremmednøgler, så et lån altid peger på en eksisterende bruger, bog og medarbejder.

2. SQL og normalisering

2.1 SQL i `db_dag_1.sql`

Scriptet ligger i `db_dag_1.sql` og køres fra appen via `ExecuteSQL`. De vigtigste dele jeg bruger:

- `DROP TABLE IF EXISTS` – sletter tabeller først, så scriptet kan køres igen uden fejl
- `CREATE TABLE` – kolonner med datatyper (`varchar`, `date`, `datetime`, `float`)
- `PRIMARY KEY` (`id`) + `AUTO_INCREMENT` – unikt id per række
- `NOT NULL` – påkrævede felter (fx `title`, `user_id`, `loan_start`)
- `FOREIGN KEY ... REFERENCES` – fx `books.author_id` → `authors`, og `loaned_books` → `users` / `books` / `staff`

2.2 Normalform (3NF)

- 1NF: et felt = én værdi, unikt id per række
- 2NF: primærnøgle er kun `id` (ingen sammensat nøgle)
- 3NF: data i den rigtige tabel – navne i `users` / `staff` / `authors`, boginfo i `books`, lån kun med id’er og datoer i `loaned_books` (ingen gentagne navne på tværs af rækker)

2.3 Sammenhæng

Normalisering betyder flere små tabeller i stedet for én stor. SQL med `PRIMARY KEY` og `FOREIGN KEY` sørger for, at tabellerne stadig hænger sammen i databasen, så jeg får både orden i data og relationer mellem dem.
