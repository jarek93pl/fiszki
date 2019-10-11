CREATE TABLE [dbo].[FicheAnswear]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[IdTeachSet] INT,
	[IdFiche] INT,
	[IsCorrect] BIT,
	[DateAnswearing] DATETIME
)
