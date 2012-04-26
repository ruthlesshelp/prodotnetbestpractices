IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dal_Application_Retrieve]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[dal_Application_Retrieve]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dal_Application_Retrieve] 
	@Id int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Statements for procedure here
    SELECT [Id]
        ,[StudentId]
        ,[Principal]
        ,[AnnualPercentageRate]
        ,[TotalPayments]
    FROM [dbo].[Application]
    WHERE
        [Id] = @Id

END
GO
