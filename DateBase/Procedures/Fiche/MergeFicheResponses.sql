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
	[fr].[TypePrompt] = [tf].[TypePrompt],
	[fr].[Name] = [tf].[Name],
	[fr].[IsCorect] = [tf].[IsCorect],
	@CountChange+=1
  WHEN NOT MATCHED THEN 
  INSERT ([IdFile],[TypePrompt],[Name],[IdFiche],[IsCorect])
  VALUES ([tf].[IdFile],[tf].[TypePrompt],[tf].[Name],@IDFiche,[tf].[IsCorect]);
  SELECT @CountChange
END

