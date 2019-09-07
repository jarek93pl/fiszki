CREATE TABLE [dbo].[Fiche]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Response] VARCHAR(50),
	[Prompt] VARCHAR(50),
	[TypePrompt] INT,
	[IsManyAnswer] BIT,
	FOREIGN KEY  ([TypePrompt]) REFERENCES [DictionaryTypeContent]([id])

)
