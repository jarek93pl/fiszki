CREATE PROCEDURE [dbo].[SearchFiches]
	@SetFicheId INT NULL=NULL,
	@FicheId INT NULL=NULL
AS
SELECT	[F].[Prompt][Prompt],
		[F].[Response][Response],
		[F].[Id],
		[F].[TypePrompt],
		[F].[IdSetFiche],
		[F].[IdFile],
		[DT].[Name][NameTypePrompt]
FROM [Fiche] [F]
JOIN [DictionaryTypeContent] [DT] ON [F].[TypePrompt]=[DT].[Id]
WHERE	( [F].[IdSetFiche]=@SetFicheId OR @SetFicheId IS NULL )  AND 
		( [F].[Id]=@FicheId OR @FicheId IS NULL )  
