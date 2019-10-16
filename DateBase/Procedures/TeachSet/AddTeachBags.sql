CREATE PROCEDURE [dbo].[AddTeachBags] 
@IdTeachFiche INT,
@TeachBags TeachBags READONLY
AS 
BEGIN

INSERT INTO [TeachBags]([IdTeachSet],[LimitTimeSek],[PeriodTime],[TypeAnswer],[Number]) 
SELECT 
@IdTeachFiche,
[tb].[LimitTimeSek],
[tb].[PeriodTime],
[tb].[TypeAnswer],
[tb].[Number]
FROM @TeachBags [tb]
END

