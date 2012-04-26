IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dal_Individual_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[dal_Individual_Update]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dal_Individual_Update] 
	@Id int,
	@LastName nvarchar(150),
	@FirstName nvarchar(50),
	@MiddleName nvarchar(50) = null,
	@Suffix nvarchar(50) = null,
	@DateOfBirth datetime
AS
BEGIN
    -- Statements for procedure here
	UPDATE [dbo].[Individual]
	SET [LastName] = @LastName
		,[FirstName] = @FirstName
		,[MiddleName] = @MiddleName
		,[Suffix] = @Suffix
		,[DateOfBirth] = @DateOfBirth
    WHERE
		[Id] = @Id
END
GO
