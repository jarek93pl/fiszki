CREATE PROCEDURE [dbo].[AddTeachSetFiche]
	@IdSetFiche INT = 0,
	@Name VARCHAR(50),
	@FirstTypeAnswear INT
AS
BEGIN
	INSERT INTO [TeachSetsFiche]([DateCreated],[Name],[IdSetFiche],[FirstTypeAnswear]) VALUES 
	(GETDATE(),@Name,@IdSetFiche,@FirstTypeAnswear)
    SELECT CAST(SCOPE_IDENTITY() AS INT)
END