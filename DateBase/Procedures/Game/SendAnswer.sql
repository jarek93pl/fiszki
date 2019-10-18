CREATE PROCEDURE [dbo].[SendAnswer]
	@IdTeachSet INT,
	@IdFiche INT,
	@IsCorrect BIT
AS
BEGIN
INSERT INTO [FicheAnswer]([IdTeachSet] ,[IdFiche] ,[IsCorrect],[DateAnswering]) VALUES (@IdTeachSet,@IdFiche,@IsCorrect,GETDATE())
IF @IsCorrect=1
BEGIN

DECLARE @IsDone BIT=1;
DECLARE @DeltaTime TIME;
DECLARE @NumberCorrect INT=0;

SELECT @NumberCorrect = [ts].[NumberCorect]
FROM [FicheTeachState] [ts]

SELECT @DeltaTime= [tb].[PeriodTime],
		@IsDone=0
FROM  [TeachBags] [tb]
WHERE [tb].[IdTeachSet] = @IdTeachSet AND [tb].[Number] = @NumberCorrect

DECLARE @TableId AS TABLE
(
	[IdTeachSet] INT,
	[IdFiche] INT
)
INSERT INTO @TableId ([IdTeachSet], [IdFiche]) VALUES (@IdTeachSet,@IdFiche)

MERGE [FicheTeachState] [ts]
USING @TableId [tb]
ON [ts].[IdTeachSet] = [tb].[IdTeachSet] AND
 [ts].[IdFiche] = [tb].[IdFiche] 
 WHEN MATCHED THEN UPDATE SET
 [ts].[NumberCorect]+=1,
 [ts].[IsDone]=@IsDone,
 [ts].[NextTry]=DATEADD(minute,datediff(minute, '00:00:00', @DeltaTime),GETDATE())
 WHEN NOT MATCHED THEN
 INSERT ([IdFiche],[IdTeachSet],[NumberCorect],[NextTry],[IsDone]) VALUES
 (@IdFiche,@IdTeachSet,1,DATEADD(minute,datediff(minute, '00:00:00', @DeltaTime),GETDATE()),@IsDone);
	

END
ELSE
BEGIN
UPDATE [FicheTeachState]
SET [NumberCorect] =CASE [NumberCorect] WHEN 0 THEN 0 ELSE [NumberCorect] -1 END
 WHERE
  [IdTeachSet] = @IdTeachSet AND
  [IdFiche] = @IdFiche


END

END