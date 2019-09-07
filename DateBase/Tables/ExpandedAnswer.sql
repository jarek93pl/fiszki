CREATE TABLE [dbo].[ExpandedAnswer]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Type] INT,
	FOREIGN KEY  ([Type]) REFERENCES [DictionaryTypeContent]([id])
)
