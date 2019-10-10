CREATE PROCEDURE [dbo].[AddTeachBags] 
@IdTeachFiche INT,
@TeachBags TeachBags READONLY
AS 
BEGIN

INSERT INTO [TeachBags]([IdTeachSet],[LimitTimeSek],[PeriodTime],[TypeAnswear],[Number]) 
SELECT 
@IdTeachFiche,
[tb].[LimitTimeSek],
[tb].[PeriodTime],
[tb].[TypeAnswear],
[tb].[Number]
FROM @TeachBags [tb]
END

