CREATE PROCEDURE [dbo].[DeleteTeachSet]
	@Id INT = 0
AS
BEGIN

DELETE [FicheAnswer]
WHERE [IdTeachSet]=@Id

DELETE [FicheTeachState]
WHERE [IdTeachSet]=@Id

DELETE [TeachBags]
WHERE [IdTeachSet] =@Id

DELETE [TeachSetsFiche] 
WHERE [Id]=@Id


END
