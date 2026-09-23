

CREATE PROCEDURE GetSpecificAuthor(IN authorId int)
BEGIN

  If authorId IS NULL OR authorId <= 0 THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'ID must be greater than 0';
  END IF;

  SELECT * FROM Authors WHERE Id = authorId;

END