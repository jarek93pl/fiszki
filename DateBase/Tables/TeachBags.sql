﻿CREATE TABLE [dbo].[TeachBags]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[IdTeachSet] INT,
	[Name] VARCHAR(50),
	[TypeAnswer] INT,
	[PeriodTime] TIME,
	[LimitTimeSek] INT NULL,
	[Number] INT
	CONSTRAINT FK_IdTeachFiche FOREIGN KEY ([IdTeachSet]) REFERENCES [TeachSetsFiche]([Id]),
	CONSTRAINT FK_TeachBag FOREIGN KEY ([TypeAnswer]) REFERENCES [DictionaryTypeAnswer]([Id])

)
