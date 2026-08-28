# Aflevering — felter og adgang

Alle felter er **private**. `protected` og `internal` bruges ikke: der er ingen subklasse, og andre klasser skal ikke røre den rå tilstand. Udefra bruges properties/metoder.

## Book

| Felt | Læses udefra | Ændres udefra | Modifier | Hvorfor |
|---|---|---|---|---|
| `_title` | ja (`Title`) | ja (med validering) | private | Validering i setteren må ikke omgås |
| `_author` | ja (`Author`) | ja (med validering) | private | Samme som titel |
| `_isbn` | ja (`Isbn`) | nej (kun konstruktør) | private | Identitet, må ikke skiftes bagefter |
| `_publicationYear` | ja (`PublicationYear`) | nej (kun konstruktør) | private | Stamdata, ingen skrivning udefra |
| `_isOnLoan` | ja (`IsOnLoan`) | nej (kun `CheckOut`/`Return`) | private | Ellers kan man udlåne samme bog to gange |

## Borrower

| Felt | Læses udefra | Ændres udefra | Modifier | Hvorfor |
|---|---|---|---|---|
| `_name` | ja (`Name`) | ja (med validering) | private | Tomt navn skal kunne afvises |
| `BorrowerNumber` | ja | nej (kun konstruktør) | private backing, public get | Identitet, må aldrig ændres |
| `NumberOfBooksLoaned` | ja | nej (kun `BorrowBook`/`ReturnBook`) | private set | Ellers kan man springe maks. 5 over |
| `MaxNumberOfBooksLoaned` | nej | nej | private | Intern regel, ikke en del af kontrakten |
