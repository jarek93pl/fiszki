CREATE PROCEDURE [dbo].[SearchResponse]
	@IdFiche INT = 0
AS
SELECT [fr].Id,[fr].[IdFile],[fr].[Name],[fr].[TypePrompt],[fr].[IsCorect]
FROM [FicheResponses] [fr]
WHERE [fr].[IdFiche]=@IdFiche
