CREATE PROCEDURE [dbo].[NextFicheToTeach]
	@IdTeachSet int = 0
AS
BEGIN
DECLARE @Fiches as TABLE
(
	[Number] INT PRIMARY KEY IDENTITY(0,1),
	[IdFiche] INT,
	[NumberAnswear] INT
	
);
DECLARE @IdSet INT;
DECLARE @LimitTimeSekFirst INT;
DECLARE @TypeAnswearFirst INT;

SELECT @IdSet = [ts].[IdSetFiche],
@TypeAnswearFirst=[ts].[FirstTypeAnswear],
@LimitTimeSekFirst=[ts].[LimitTimeSek]
FROM [TeachSetsFiche] [ts]
WHERE [ts].[Id]= @IdTeachSet

INSERT INTO @Fiches([IdFiche],[NumberAnswear])
SELECT [f].[Id],[ts].[NumberCorect]
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
SELECT [f].[IdFiche]
,@IdTeachSet[IdTeachSet] ,
CASE WHEN [tb].[TypeAnswear] IS NULL  THEN @TypeAnswearFirst ELSE [tb].[TypeAnswear] END [TypeAnswear],
CASE WHEN [tb].[LimitTimeSek] IS NULL  THEN @LimitTimeSekFirst ELSE [tb].[LimitTimeSek] END [LimitTimeSek]
FROM @Fiches [f]
LEFT JOIN [TeachBags] [tb] ON [tb].[Number]=[f].[NumberAnswear] AND [tb].[IdTeachSet]=@IdTeachSet
WHERE [f].[Number] = @RandId
END
END
