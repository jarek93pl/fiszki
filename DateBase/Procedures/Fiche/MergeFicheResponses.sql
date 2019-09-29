CREATE PROCEDURE [dbo].[MergeFicheResponses]
	@TableFicheResponse [FicheResponseType] READONLY,
	@IDFiche INT
AS
BEGIN
DECLARE @CountChange INT=0
MERGE [FicheResponses] [fr]
 USING @TableFicheResponse [tf] ON [fr].id= [tf].id
 WHEN MATCHED  THEN UPDATE SET
	[fr].[IdFile] =[tf].[IdFile],
	[fr].[TypePrompt] = [tf].[IdFile],
	[fr].[Name] = [tf].[IdFile],
	@CountChange+=1
  WHEN NOT MATCHED THEN 
  INSERT ([IdFile],[TypePrompt],[Name],[IdFiche])
  VALUES ([tf].[IdFile],[tf].[TypePrompt],[tf].[Name],@IDFiche);
  SELECT @CountChange
END

