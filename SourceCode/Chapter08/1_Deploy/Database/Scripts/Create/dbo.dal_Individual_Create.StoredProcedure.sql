IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dal_Individual_Create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[dal_Individual_Create]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dal_Individual_Create] 
	@LastName nvarchar(150),
	@FirstName nvarchar(50),
	@MiddleName nvarchar(50) = null,
	@Suffix nvarchar(50) = null,
	@DateOfBirth datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Id int
    SET @Id = 0

    -- Statements for procedure here
    INSERT INTO [dbo].[Individual]
           ([LastName]
           ,[FirstName]
           ,[MiddleName]
           ,[Suffix]
           ,[DateOfBirth])
     VALUES
           (@LastName
           ,@FirstName
           ,@MiddleName
           ,@Suffix
           ,@DateOfBirth)
           
    SET @Id = SCOPE_IDENTITY()

    SELECT [Id]
    FROM [dbo].[Individual]
    WHERE
        [Id] = @Id
END
GO
