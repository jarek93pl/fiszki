CREATE PROCEDURE [dbo].[DeleteResponse]
	@id INT = 0
AS
	DELETE [FicheResponses]
	WHERE [Id]=@id