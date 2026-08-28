using System;

namespace Lesson02.Library
{
    /// <summary>
    /// Repræsenterer en bog i skolebibliotekets system.
    /// Klassen demonstrerer klassedesign, overloadede konstruktører,
    /// properties og indkapsling: alle felter er private, og
    /// lånestatus kan kun ændres gennem klassens egne metoder.
    /// </summary>
    public class Book
    {
        // ----- Felter (private) -----
        // Alle felter er private: ingen kode uden for klassen skal kunne
        // ændre dem direkte. Al adgang udefra går gennem properties/metoder
        // nedenfor, så vi kan validere og kontrollere tilstanden.
        private string _title;
        private string _author;
        private string _isbn;
        private int _publicationYear;
        private bool _isOnLoan;

        // ----- Konstruktører (overloadede) -----

        /// <summary>
        /// Hovedkonstruktør: opretter en bog med alle oplysninger kendt.
        /// De øvrige konstruktører kalder denne via "this(...)", så al
        /// den "rigtige" initialiseringslogik kun findes ét sted.
        /// </summary>
        public Book(string title, string author, string isbn, int publicationYear)
        {
            // Vi bruger property-setterne (Title = ...) i stedet for felterne
            // direkte, så validering i setterne også gælder ved oprettelse.
            Title = title;
            Author = author;
            _isbn = isbn;
            _publicationYear = publicationYear;
            _isOnLoan = false; // en ny bog er aldrig udlånt fra start
        }

        /// <summary>
        /// Overload: udgivelsesår kendes ikke endnu ved registrering.
        /// Bruger 0 som markør for "ukendt udgivelsesår".
        /// </summary>
        public Book(string title, string author, string isbn)
            : this(title, author, isbn, 0)
        {
        }

        /// <summary>
        /// Overload: hurtig registrering, kun titel og forfatter kendes.
        /// </summary>
        public Book(string title, string author)
            : this(title, author, "ukendt", 0)
        {
        }

        // ----- Properties -----

        /// <summary>
        /// Fuld property med validering: en titel må aldrig være tom.
        /// </summary>
        public string Title
        {
            get { return _title; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Titel må ikke være tom.", nameof(value));
                }
                _title = value;
            }
        }

        public string Author
        {
            get { return _author; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Forfatter må ikke være tom.", nameof(value));
                }
                _author = value;
            }
        }

        // Kun get udefra - ISBN sættes kun via konstruktøren i denne simple version.
        // (Se opgave.md, Udfordring A, for en udvidet version med ISBN-validering.)
        public string Isbn => _isbn;

        public int PublicationYear => _publicationYear;

        // Kun get: lånestatus må ALDRIG sættes direkte udefra.
        // Den ændres udelukkende gennem metoderne CheckOut() og Return() nedenfor.
        // Det er selve pointen med indkapsling: vi garanterer, at status kun
        // ændres på en kontrolleret måde (fx kan man ikke udlåne samme bog to gange).
        public bool IsOnLoan => _isOnLoan;

        // ----- Metoder (den kontrollerede adgang til at ændre tilstand) -----

        /// <summary>
        /// Markerer bogen som udlånt. Kaster en exception, hvis bogen
        /// allerede er udlånt, så fejlen opdages med det samme og præcis
        /// dér, hvor den forkerte brug sker.
        /// </summary>
        public void CheckOut()
        {
            if (_isOnLoan)
            {
                throw new InvalidOperationException(
                    $"Bogen '{_title}' er allerede udlånt og kan ikke lånes igen, før den er afleveret.");
            }

            _isOnLoan = true;
        }

        /// <summary>
        /// Markerer bogen som afleveret/til rådighed igen.
        /// </summary>
        public void Return()
        {
            _isOnLoan = false;
        }

        public override string ToString()
        {
            string status = _isOnLoan ? "udlånt" : "på hylden";
            return $"{_title} af {_author} ({_publicationYear}) - {status}";
        }
    }
}
