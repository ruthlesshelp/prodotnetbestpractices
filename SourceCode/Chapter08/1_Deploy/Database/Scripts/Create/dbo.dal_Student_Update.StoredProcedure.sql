IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dal_Student_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[dal_Student_Update]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dal_Student_Update] 
	@Id int,
	@HighSchoolName nvarchar(40),
	@HighSchoolCity nvarchar(40),
	@HighSchoolState nvarchar(2)
AS
BEGIN
    -- Statements for procedure here
	UPDATE [dbo].[Student]
	SET [HighSchoolName] = @HighSchoolName
		,[HighSchoolCity] = @HighSchoolCity
		,[HighSchoolState] = @HighSchoolState
    WHERE
		[Id] = @Id
END
GO
