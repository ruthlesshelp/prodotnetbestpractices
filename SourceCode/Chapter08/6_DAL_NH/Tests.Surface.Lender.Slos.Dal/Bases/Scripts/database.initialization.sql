DELETE FROM [dbo].[Application];
DBCC CHECKIDENT('[dbo].[Application]', RESEED, 0);
DELETE FROM [dbo].[Student];
DELETE FROM [dbo].[Individual];
DBCC CHECKIDENT('[dbo].[Individual]', RESEED, 0);
