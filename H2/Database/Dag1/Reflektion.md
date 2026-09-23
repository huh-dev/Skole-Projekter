1. Vælg en problemstilling.

Jeg valgte et bibliotek, fordi det er et emne hvor man naturligt har flere ting der hører sammen, men som ikke skal stå i samme tabel. Der er bøger og forfattere, brugere der låner, personale der registrerer udlån, og selve lånet med start- og slutdato. Det passer godt til opgaven om normalisering, fordi jeg kan gemme forfatternavn et sted, bogoplysninger et andet sted, og kun bruge id’er når en bruger låner en bog.

Databasen db_dag_1 har derfor fem tabeller: authors, books, users, staff og loaned_books. Hver tabel har id som primærnøgle, og tabellerne er koblet med fremmednøgler, så et lån altid peger på en eksisterende bruger, bog og medarbejder.


2. Du vil blive spurgt ind til forskellige aspekter af SQL-syntakser du anvender i din .sql fil samt database normalisering du anvender til din database design. Formålet er at vurdere, hvor godt du forstår sammenhængen mellem SQL, database og dens normalform du har lavet.


SQL i db_dag_1.sql
- DROP TABLE IF EXISTS – sletter først, så scriptet kan køres igen
- CREATE TABLE – kolonner med datatyper (varchar, date, datetime, float)
- PRIMARY KEY (id) + AUTO_INCREMENT – unikt id per række
- NOT NULL – påkrævede felter (fx title, user_id, loan_start)
- FOREIGN KEY ... REFERENCES – books.author_id -> authors, loaned_books -> users/books/staff

Normalform (3NF)
- 1NF: et felt = en værdi, unikt id per række
- 2NF: primærnøgle er kun id (ingen sammensat nøgle)
- 3NF: data i den rigtige tabel – navne i users/staff/authors, boginfo i books, lån kun med id’er + datoer i loaned_books (ingen gentagne navne)

Sammenhæng
- Normalisering -> flere små tabeller i stedet for én stor
- SQL med PK og FK -> tabellerne hænger stadig sammen i databasen
