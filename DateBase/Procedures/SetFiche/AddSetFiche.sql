﻿CREATE PROCEDURE [dbo].[AddSetFiche]
	@Name VARCHAR(50),
	@UserId INT
AS
BEGIN
INSERT INTO [SetsFiche] ([Name],[UserId]) VALUES(@Name,@UserId)
SELECT CAST( SCOPE_IDENTITY() AS INT)
END