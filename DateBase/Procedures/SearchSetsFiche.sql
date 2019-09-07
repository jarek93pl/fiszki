CREATE PROCEDURE [dbo].[SearchSetsFiche]
	@UserId int = 0
AS
BEGIN
SELECT	[s].[Id][id],
		[s].[Name][Name],
		[s].[UserId][UserId],
		[u].[Login][UserName]
FROM [SetsFiche] [s]
JOIN [Users] [u] ON [u].Id=[s].UserId
END