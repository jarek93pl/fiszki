﻿CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Login] VARCHAR(20) NOT NULL,
	[Password] VARCHAR(40)
    UNIQUE ([Login])
)
