CREATE PROCEDURE [dbo].[NextFicheToTeach]
	@IdTeachSet int = 0
AS
BEGIN
DECLARE @Fiches as TABLE
(
	[Number] INT PRIMARY KEY IDENTITY(0,1),
	[IdFiche] INT,
	[NumberAnswer] INT
	
);
DECLARE @IdSet INT;
DECLARE @LimitTimeSekFirst INT;
DECLARE @TypeAnswerFirst INT;

SELECT @IdSet = [ts].[IdSetFiche],
@TypeAnswerFirst=[ts].[FirstTypeAnswer],
@LimitTimeSekFirst=[ts].[LimitTimeSek]
FROM [TeachSetsFiche] [ts]
WHERE [ts].[Id]= @IdTeachSet

INSERT INTO @Fiches([IdFiche],[NumberAnswer])
SELECT [f].[Id],[ts].[NumberCorect]
FROM [Fiche] [f] 
LEFT JOIN [FicheTeachState] [ts] ON  ([ts].IdFiche =[f].[Id] and [ts].[IdTeachSet]= @IdTeachSet)
WHERE 
([ts].[IdFiche] IS NULL OR
( [ts].[NextTry]<GETDATE() AND [ts].[IsDone]=0 ))AND 
[f].[IdSetFiche] =@IdSet

DECLARE @Size INT = (SELECT COUNT (1) FROM @Fiches);

IF @Size =0
BEGIN
SELECT -1[IdFiche],0[IdTeachSet],0[TypeAnswer],0[LimitTimeSek]
END
ELSE
BEGIN
DECLARE @RandId INT= ABS(CHECKSUM(NewId())) % @Size;
SELECT [f].[IdFiche]
,@IdTeachSet[IdTeachSet] ,
CASE WHEN [tb].[TypeAnswer] IS NULL  THEN @TypeAnswerFirst ELSE [tb].[TypeAnswer] END [TypeAnswer],
CASE WHEN [tb].[LimitTimeSek] IS NULL  THEN @LimitTimeSekFirst ELSE [tb].[LimitTimeSek] END [LimitTimeSek]
FROM @Fiches [f]
LEFT JOIN [TeachBags] [tb] ON [tb].[Number]=[f].[NumberAnswer] AND [tb].[IdTeachSet]=@IdTeachSet
WHERE [f].[Number] = @RandId
END
END
