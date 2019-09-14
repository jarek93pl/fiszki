CREATE PROCEDURE [dbo].[SearchSetsFiche]
	@UserId INT = NULL,
	@SetFicheId INT =NULL

AS
BEGIN
SELECT	[s].[Id][id],
		[s].[Name][Name],
		[s].[UserId][UserId],
		[u].[Login][UserName]
FROM [SetsFiche] [s]
JOIN [Users] [u] ON [u].Id=[s].UserId
WHERE 
	([UserId] = @UserId OR [UserId] IS NULL)  AND
	([s].[Id] = @SetFicheId OR @SetFicheId IS NULL )
END