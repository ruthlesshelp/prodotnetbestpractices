IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Application]') AND type in (N'U'))
BEGIN
	PRINT N'!!!'
	PRINT N'Cannot create table [dbo].[Application]. It already already exists.'
END
GO

CREATE TABLE [dbo].[Application](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[Principal] [decimal](9, 2) NULL,
	[AnnualPercentageRate] [decimal](6, 4) NULL,
	[TotalPayments] [int] NULL,
 CONSTRAINT [PK_Application] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Application]  WITH CHECK ADD  CONSTRAINT [FK_Application_Student] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([Id])
GO

ALTER TABLE [dbo].[Application] CHECK CONSTRAINT [FK_Application_Student]
GO

