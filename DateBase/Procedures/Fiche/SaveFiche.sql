CREATE PROCEDURE [dbo].[SaveFiche]
	@Prompt VARCHAR(50),
	@Response VARCHAR(50),
	@TypePrompt INT,
	@Id INT,
	@IdPromptFile INT NULL=NULL,
	@IdFicheSet INT

AS
BEGIN
IF @Id is null or @Id =0  
    BEGIN
      INSERT INTO [dbo].[Fiche] ([Prompt],[Response],[TypePrompt],[IdFile],[IdSetFiche]) VALUES(@Prompt,@Response,@TypePrompt,@IdPromptFile,@IdFicheSet)
      SELECT CAST( SCOPE_IDENTITY() AS INT)
  END
ELSE
  BEGIN
  UPDATE [Fiche]
    SET
      [Prompt]= @Prompt,
      [Response]=@Response,
      [TypePrompt]=@TypePrompt,
      [IdFile]=@IdPromptFile,
      [IdSetFiche] =@IdFicheSet
    WHERE [Id]=@Id
	SELECT @Id
  END
END