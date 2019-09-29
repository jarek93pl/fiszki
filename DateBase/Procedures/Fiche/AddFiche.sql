CREATE PROCEDURE [dbo].[AddFiche]
	@param1 int = 0,
	@param2 int
AS
	SELECT @param1, @param2
RETURN 0
---https://docs.microsoft.com/en-us/sql/t-sql/statements/merge-transact-sql?view=sql-server-2017