CREATE TABLE [dbo].[FicheTeachState]
(
	[IdFiche] INT,
	[IdTeachSet] INT,
	[NumberCorect] INT,
	[NextTry] DATETIME,
	[IsDone] BIT
	FOREIGN KEY  ([IdFiche]) REFERENCES [Fiche]([id]),
	FOREIGN KEY  ([IdTeachSet]) REFERENCES [TeachSetsFiche]([id]),
	PRIMARY KEY ([IdTeachSet],[IdFiche])
	
)
