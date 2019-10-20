CREATE PROCEDURE [dbo].[RemoveSetFiche]
	@elementId int = 0
AS
BEGIN

DELETE [tse]
FROM [FicheTeachState] [tse]
JOIN [Fiche] [f] on [f].[Id] =[tse].[IdFiche]
WHERE [f].[IdSetFiche]=@elementId

DELETE [tse]
FROM [FicheAnswer] [tse]
JOIN [Fiche] [f] on [f].[Id] =[tse].[IdFiche]
WHERE [f].[IdSetFiche]=@elementId

DELETE [tse]
FROM [FicheResponses] [tse]
JOIN [Fiche] [f] on [f].[Id] =[tse].[IdFiche]
WHERE [f].[IdSetFiche]=@elementId

DELETE [TB]
FROM [TeachBags] [TB]
JOIN [TeachSetsFiche] [TS] ON [TB].[IdTeachSet]=[TS].[Id]
WHERE [TS].[IdSetFiche]=@elementId

DELETE [Fiche]
WHERE [IdSetFiche]=@elementId

DELETE [TeachSetsFiche] 
WHERE [IdSetFiche]=@elementId

DELETE [SetsFiche]
WHERE [Id]=@elementId
END
