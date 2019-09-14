﻿CREATE PROCEDURE [dbo].[SaveFile]
	@FileExtension VARCHAR(10)
AS
BEGIN
INSERT INTO [dbo].[File]([FileExtension]) VALUES(@FileExtension)
SELECT CAST( SCOPE_IDENTITY() AS INT)
END
RETURN 0
