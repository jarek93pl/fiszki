CREATE PROCEDURE [dbo].[SearchResponse]
	@IdFiche INT = 0
AS
SELECT [fr].Id,[fr].[IdFile],[fr].[Name],[fr].[TypePrompt]
FROM [FicheResponses] [fr]
WHERE [fr].[IdFiche]=@IdFiche
