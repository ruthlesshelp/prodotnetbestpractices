IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dal_Application_Create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[dal_Application_Create]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dal_Application_Create] 
	@StudentId int,
	@Principal decimal(9, 2),
	@AnnualPercentageRate decimal(6,4),
	@TotalPayments int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Id int
    SET @Id = 0

    -- Statements for procedure here
    INSERT INTO [dbo].[Application]
           ([StudentId]
           ,[Principal]
           ,[AnnualPercentageRate]
           ,[TotalPayments])
    VALUES
           (@StudentId
           ,@Principal
           ,@AnnualPercentageRate
           ,@TotalPayments)
           
    SET @Id = SCOPE_IDENTITY()

    SELECT [Id]
    FROM [dbo].[Application]
    WHERE
        [Id] = @Id
END
GO
