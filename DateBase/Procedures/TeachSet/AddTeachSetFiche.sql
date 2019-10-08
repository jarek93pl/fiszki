CREATE PROCEDURE [dbo].[AddTeachSetFiche]
	@IdSetFiche INT = 0,
	@Name VARCHAR(50)

AS
BEGIN
	INSERT INTO [TeachSetsFiche]([DateCreated],[Name],[IdSetFiche]) VALUES (GETDATE(),@Name,@IdSetFiche)
    SELECT CAST(SCOPE_IDENTITY() AS INT)
END