DROP INDEX IF EXISTS idx_books_price ON books;
DROP INDEX IF EXISTS idx_loaned_end_start ON loaned_books;

-- Query A – før index
ANALYZE
SELECT b.title, b.price, a.name AS author_name
FROM books b
INNER JOIN authors a ON b.author_id = a.id
WHERE b.price > 250
ORDER BY b.price DESC;

EXPLAIN
SELECT b.title, b.price, a.name AS author_name
FROM books b
INNER JOIN authors a ON b.author_id = a.id
WHERE b.price > 250
ORDER BY b.price DESC;

SET profiling = 1;

SELECT b.title, b.price, a.name AS author_name
FROM books b
INNER JOIN authors a ON b.author_id = a.id
WHERE b.price > 250
ORDER BY b.price DESC;

SHOW PROFILES;

FLUSH STATUS;

SELECT b.title, b.price, a.name AS author_name
FROM books b
INNER JOIN authors a ON b.author_id = a.id
WHERE b.price > 250
ORDER BY b.price DESC;

SHOW SESSION STATUS LIKE 'Handler_read%';
SHOW SESSION STATUS LIKE 'Sort%';

SET profiling = 0;

-- Query A – index + efter måling
CREATE INDEX idx_books_price ON books (price DESC);

ANALYZE
SELECT b.title, b.price, a.name AS author_name
FROM books b
INNER JOIN authors a ON b.author_id = a.id
WHERE b.price > 250
ORDER BY b.price DESC;

ANALYZE
SELECT b.title, b.price, a.name AS author_name
FROM books b
STRAIGHT_JOIN authors a ON b.author_id = a.id
WHERE b.price > 250
ORDER BY b.price DESC;

SET profiling = 1;

SELECT b.title, b.price, a.name AS author_name
FROM books b
STRAIGHT_JOIN authors a ON b.author_id = a.id
WHERE b.price > 250
ORDER BY b.price DESC;

SHOW PROFILES;

SET profiling = 0;

-- Query B – før index
ANALYZE
SELECT u.name, b.title, lb.loan_start, lb.loan_end
FROM loaned_books lb
INNER JOIN users u ON lb.user_id = u.id
INNER JOIN books b ON lb.book_id = b.id
WHERE lb.loan_end >= DATE_SUB(NOW(), INTERVAL 6 MONTH)
ORDER BY lb.loan_start DESC
LIMIT 500;

SET profiling = 1;

SELECT u.name, b.title, lb.loan_start, lb.loan_end
FROM loaned_books lb
INNER JOIN users u ON lb.user_id = u.id
INNER JOIN books b ON lb.book_id = b.id
WHERE lb.loan_end >= DATE_SUB(NOW(), INTERVAL 6 MONTH)
ORDER BY lb.loan_start DESC
LIMIT 500;

SHOW PROFILES;

SET profiling = 0;

-- Query B – index + efter måling
CREATE INDEX idx_loaned_end_start ON loaned_books (loan_end, loan_start);

ANALYZE
SELECT u.name, b.title, lb.loan_start, lb.loan_end
FROM loaned_books lb
INNER JOIN users u ON lb.user_id = u.id
INNER JOIN books b ON lb.book_id = b.id
WHERE lb.loan_end >= DATE_SUB(NOW(), INTERVAL 6 MONTH)
ORDER BY lb.loan_start DESC
LIMIT 500;

SET profiling = 1;

SELECT u.name, b.title, lb.loan_start, lb.loan_end
FROM loaned_books lb
INNER JOIN users u ON lb.user_id = u.id
INNER JOIN books b ON lb.book_id = b.id
WHERE lb.loan_end >= DATE_SUB(NOW(), INTERVAL 6 MONTH)
ORDER BY lb.loan_start DESC
LIMIT 500;

SHOW PROFILES;

SET profiling = 0;
