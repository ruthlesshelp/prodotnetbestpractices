IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Individual]') AND type in (N'U'))
BEGIN
	PRINT N'!!!'
	PRINT N'Cannot create table [dbo].[Individual]. It already already exists.'
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Individual](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LastName] [nvarchar](150) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NULL,
	[Suffix] [nvarchar](50) NULL,
	[DateOfBirth] [datetime] NOT NULL,
 CONSTRAINT [PK_Individual] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
