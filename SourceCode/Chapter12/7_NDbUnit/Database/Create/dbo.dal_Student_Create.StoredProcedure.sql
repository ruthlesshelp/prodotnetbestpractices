IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dal_Student_Create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[dal_Student_Create]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dal_Student_Create] 
	@Id int,
	@HighSchoolName nvarchar(40),
	@HighSchoolCity nvarchar(40),
	@HighSchoolState nvarchar(2)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Statements for procedure here
	INSERT INTO [dbo].[Student]
           ([Id]
           ,[HighSchoolName]
           ,[HighSchoolCity]
           ,[HighSchoolState])
     VALUES
           (@Id
           ,@HighSchoolName
           ,@HighSchoolCity
           ,@HighSchoolState)

    SELECT [Id]
    FROM [dbo].[Student]
    WHERE
        [Id] = @Id
END
GO
