CREATE TABLE [dbo].[TeachSetsFiche]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[IdSetFiche] INT,
	[Name] VARCHAR(50),
	[DateCreated] DATETIME,
	CONSTRAINT FK_TeachSetFiche FOREIGN KEY ([IdSetFiche]) REFERENCES [SetsFiche] ([Id]),
	


)
