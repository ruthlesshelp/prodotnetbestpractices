IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Application_Student]') AND parent_object_id = OBJECT_ID(N'[dbo].[Application]'))
ALTER TABLE [dbo].[Application] DROP CONSTRAINT [FK_Application_Student]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Application]') AND type in (N'U'))
DROP TABLE [dbo].[Application]
GO
