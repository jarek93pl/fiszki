CREATE PROCEDURE [dbo].[RemoveSetFiszka]
	@elementId int = 0
AS
BEGIN
	DELETE [SetsFiszka]
	WHERE [Id]=@elementId
END
