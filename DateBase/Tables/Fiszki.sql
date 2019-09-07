CREATE TABLE [dbo].[Fiszki]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Response] VARCHAR(50),
	[Request] VARCHAR(50),
	[TypeResponse] INT,
	[IsManyAnswer] BIT
)
