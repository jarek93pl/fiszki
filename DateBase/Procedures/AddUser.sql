CREATE PROCEDURE [dbo].[AddUser]
@Login VARCHAR(20),
@Password VARCHAR(40)
AS
BEGIN
INSERT INTO [Users] ([Login],[Password]) VALUES (@Login,@Password)
SELECT SCOPE_IDENTITY()
END