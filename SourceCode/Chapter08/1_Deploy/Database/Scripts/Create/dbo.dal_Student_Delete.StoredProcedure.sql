IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dal_Student_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[dal_Student_Delete]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dal_Student_Delete] 
	@Id int
AS
BEGIN
    -- Statements for procedure here
	DELETE FROM [dbo].[Student]
	WHERE
		[Id] = @Id
END
GO
