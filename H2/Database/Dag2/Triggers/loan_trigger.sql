CREATE TRIGGER trg_add_loan_logs $$
AFTER INSERT ON loaned_books FOR EACH ROW
BEGIN
    INSERT INTO loan_log (loan_id, book_id, user_id, action)
    VALUES (NEW.id, NEW.book_id, NEW.user_id, 'INSERT');
END $$