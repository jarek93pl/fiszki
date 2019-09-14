CREATE PROCEDURE [dbo].[RemoveSetFiche]
	@elementId int = 0
AS
BEGIN
	DELETE [SetsFiche]
	WHERE [Id]=@elementId
END
