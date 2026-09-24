-- Clustered index (InnoDB: PRIMARY KEY – rækker lagres sorteret efter id)
SHOW INDEX FROM books;

ANALYZE
SELECT id, title, price
FROM books
WHERE id = 25000;

-- Nonclustered index – users.email
DROP INDEX IF EXISTS idx_users_email ON users;

ANALYZE
SELECT id, name, email
FROM users
WHERE email = 'perf_u1500@test.dk';

CREATE INDEX idx_users_email ON users (email);

ANALYZE
SELECT id, name, email
FROM users
WHERE email = 'perf_u1500@test.dk';

-- Nonclustered index – authors.name
DROP INDEX IF EXISTS idx_authors_name ON authors;

ANALYZE
SELECT id, name
FROM authors
WHERE name = 'Forfatter 250';

CREATE INDEX idx_authors_name ON authors (name);

ANALYZE
SELECT id, name
FROM authors
WHERE name = 'Forfatter 250';

-- Nonclustered index – loaned_books (søgning på udløbsdato)
DROP INDEX IF EXISTS idx_loaned_end_start ON loaned_books;

ANALYZE
SELECT id, user_id, book_id, loan_start, loan_end
FROM loaned_books
WHERE loan_end >= DATE_SUB(NOW(), INTERVAL 6 MONTH)
ORDER BY loan_start DESC
LIMIT 100;

CREATE INDEX idx_loaned_end_start ON loaned_books (loan_end, loan_start);

ANALYZE
SELECT id, user_id, book_id, loan_start, loan_end
FROM loaned_books
WHERE loan_end >= DATE_SUB(NOW(), INTERVAL 6 MONTH)
ORDER BY loan_start DESC
LIMIT 100;

SHOW INDEX FROM users;
SHOW INDEX FROM authors;
SHOW INDEX FROM loaned_books;
