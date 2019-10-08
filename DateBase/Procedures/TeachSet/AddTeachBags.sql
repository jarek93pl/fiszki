CREATE PROCEDURE [dbo].[AddTeachBags] 
@IdTeachFiche INT,
@TeachBags TeachBags READONLY
AS 
BEGIN

INSERT INTO [TeachBags]([IdTeachSet],[LimitTimeSek],[PeriodTime],[TypeAnswear]) 
SELECT 
@IdTeachFiche,
[tb].[LimitTimeSek],
[tb].[PeriodTime],
[tb].[TypeAnswear]
FROM @TeachBags [tb]
END

