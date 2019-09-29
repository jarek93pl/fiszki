CREATE TABLE [dbo].[Fiche]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Response] VARCHAR(50),
	[Prompt] VARCHAR(50),
	[TypePrompt] INT, 
	[IdSetFiche] INT,
	[IdFile] INT NULL,
	FOREIGN KEY  ([TypePrompt]) REFERENCES [DictionaryTypeContent]([id]),
	FOREIGN KEY  ([IdSetFiche]) REFERENCES [SetsFiche]([id]),
	FOREIGN KEY  ([IdFile]) REFERENCES [File]([id])

)
