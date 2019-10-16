CREATE PROCEDURE [dbo].[AddTeachSetFiche]
	@IdSetFiche INT = 0,
	@Name VARCHAR(50),
	@FirstTypeAnswer INT,
	@LimitTimeSek INT
AS
BEGIN
	INSERT INTO [TeachSetsFiche]([DateCreated],[Name],[IdSetFiche],[FirstTypeAnswer],[LimitTimeSek]) VALUES 
	(GETDATE(),@Name,@IdSetFiche,@FirstTypeAnswer,@LimitTimeSek)
    SELECT CAST(SCOPE_IDENTITY() AS INT)
END