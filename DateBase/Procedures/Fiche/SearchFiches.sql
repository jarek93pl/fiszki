CREATE PROCEDURE [dbo].[SearchFiches]
	@SetFicheId INT = 0
AS
SELECT	[F].[Prompt][Prompt],
		[F].[Response][Response],
		[F].[Id],
		[F].[TypePrompt],
		[DT].[Name][NameTypePrompt]
FROM [Fiche] [F]
JOIN [DictionaryTypeContent] [DT] ON [F].[Id]=[DT].[Id]
WHERE [F].[Id]=@SetFicheId
