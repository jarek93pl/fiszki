﻿/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
INSERT INTO [DictionaryTypeContent]([Id],[Name])VALUES(1,'Text');
INSERT INTO [DictionaryTypeContent]([Id],[Name])VALUES(2,'Film');
INSERT INTO [DictionaryTypeContent]([Id],[Name])VALUES(3,'Muzyka');
INSERT INTO [DictionaryTypeContent]([Id],[Name])VALUES(4,'Obrazek');