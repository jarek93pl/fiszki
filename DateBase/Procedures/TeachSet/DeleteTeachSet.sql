CREATE PROCEDURE [dbo].[DeleteTeachSet]
	@Id INT = 0
AS
BEGIN
DELETE [TeachBags]
WHERE [IdTeachSet] =@Id

DELETE [TeachSetsFiche] 
WHERE [Id]=@Id
END
