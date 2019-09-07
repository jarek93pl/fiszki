CREATE PROCEDURE [dbo].[AutorizeUser]
	@Login VARCHAR(20),
	@Password VARCHAR(40) 
AS
BEGIN
	DECLARE @returned INT=0;
		SELECT @returned= [u].Id
		FROM [Users] [u]
		WHERE [u].[Login]=@Login AND [u].[Password]= @Password

		SELECT @returned
END


