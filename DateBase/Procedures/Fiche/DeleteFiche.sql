CREATE PROCEDURE [dbo].[DeleteFiche]
	@Id int = 0
AS
BEGIN
DELETE [FicheTeachState]
WHERE [IdFiche]=@Id

DELETE [FicheAnswer]
WHERE [IdFiche]=@Id

DELETE [FicheResponses]
WHERE [IdFiche]=@Id

DELETE [Fiche] 
WHERE [Id]=@Id
END