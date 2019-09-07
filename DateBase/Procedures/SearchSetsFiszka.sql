CREATE PROCEDURE [dbo].[SearchSetsFiszka]
	@UserId int = 0
AS
BEGIN
SELECT	[s].[Id][id],
		[s].[Name][Name],
		[s].[UserId][UserId],
		[u].[Login][UserName]
FROM [SetsFiszka] [s]
JOIN [Users] [u] ON [u].Id=[s].UserId
END