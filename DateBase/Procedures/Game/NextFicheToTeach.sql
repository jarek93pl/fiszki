CREATE PROCEDURE [dbo].[NextFicheToTeach]
	@IdTeachSet int = 0
AS
BEGIN
DECLARE @Fiches as TABLE
(
	[Number] INT PRIMARY KEY IDENTITY(0,1),
	[IdFiche] INT
);

DECLARE @IdSet INT;

SELECT @IdSet = [ts].[IdSetFiche]
FROM [TeachSetsFiche] [ts]
WHERE [ts].[Id]= @IdTeachSet

INSERT INTO @Fiches([IdFiche])
SELECT [f].[Id]
FROM [Fiche] [f] 
LEFT JOIN [FicheTeachState] [ts] ON [ts].IdFiche =[f].[Id]
WHERE 
([ts].[IdFiche] IS NULL OR
( [ts].[NextTry]<GETDATE() AND [ts].[IsDone]=0 ))AND 
[f].[IdSetFiche] =@IdSet

DECLARE @Size INT = (SELECT COUNT (1) FROM @Fiches);

IF @Size =0
BEGIN
SELECT -1
END
ELSE
BEGIN
DECLARE @RandId INT= ABS(CHECKSUM(NewId())) % @Size;
SELECT [IdFiche]
FROM @Fiches
WHERE [Number] = @RandId
END
END
