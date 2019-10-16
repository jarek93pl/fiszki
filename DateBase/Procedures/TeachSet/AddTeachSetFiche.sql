CREATE PROCEDURE [dbo].[AddTeachSetFiche]
	@IdSetFiche INT = 0,
	@Name VARCHAR(50),
	@FirstTypeAnswear INT,
	@LimitTimeSek INT
AS
BEGIN
	INSERT INTO [TeachSetsFiche]([DateCreated],[Name],[IdSetFiche],[FirstTypeAnswear],[LimitTimeSek]) VALUES 
	(GETDATE(),@Name,@IdSetFiche,@FirstTypeAnswear,@LimitTimeSek)
    SELECT CAST(SCOPE_IDENTITY() AS INT)
END