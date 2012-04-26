IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dal_Application_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[dal_Application_Update]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dal_Application_Update] 
	@Id int,
	@StudentId int,
	@Principal decimal(9,2),
	@AnnualPercentageRate decimal(6,4),
	@TotalPayments int
AS
BEGIN
    -- Statements for procedure here
	UPDATE [dbo].[Application]
	   SET [StudentId] = @StudentId
		  ,[Principal] = @Principal
		  ,[AnnualPercentageRate] = @AnnualPercentageRate
		  ,[TotalPayments] = @TotalPayments
	WHERE
		[Id] = @Id
END
GO
