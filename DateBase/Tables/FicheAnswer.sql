CREATE TABLE [dbo].[FicheAnswer]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[IdTeachSet] INT,
	[IdFiche] INT,
	[IsCorrect] BIT,
	[DateAnswering] DATETIME
)
