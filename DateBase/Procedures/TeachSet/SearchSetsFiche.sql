CREATE PROCEDURE [dbo].[SearchTeachSetsFiche]
	@UserId INT = NULL,
	@SetTeachFicheId INT =NULL

AS
BEGIN
SELECT	[t].[Id],
		[t].[Name],
		[t].[DateCreated],
		[s].[Name][NameSet],
		[s].[Id][IdSet]
FROM [SetsFiche] [s]
JOIN [TeachSetsFiche] [t] ON [t].[IdSetFiche] = [s].[Id]
WHERE 
	([s].[UserId] = @UserId OR @UserId IS NULL)  AND
	([t].[Id] = @SetTeachFicheId OR @SetTeachFicheId IS NULL )
END