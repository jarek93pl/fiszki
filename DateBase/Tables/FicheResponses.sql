CREATE TABLE [dbo].[FicheResponses]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[IdFile] INT,
	[TypePrompt] INT,
	[Name] VARCHAR(50),
	[IdFiche] INT,
	[IsCorect] BIT
	FOREIGN KEY  ([TypePrompt]) REFERENCES [DictionaryTypeContent]([id]),
	FOREIGN KEY  ([IdFile]) REFERENCES [File]([Id]),
	FOREIGN KEY  ([IdFiche]) REFERENCES [Fiche]([Id])

)
