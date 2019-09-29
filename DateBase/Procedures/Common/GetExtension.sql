CREATE PROCEDURE [dbo].[GetExtension]
	@id int
AS
BEGIN
SELECT [F].[FileExtension]
FROM [File][F]
WHERE [F].Id=@id
END