CREATE TABLE [dbo].[OrderServises]
(
	[IdServises] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [IdOrder] UNIQUEIDENTIFIER NULL, 
    [idServis] UNIQUEIDENTIFIER NULL, 
    [Count] FLOAT NULL, 
	[Cost] MONEY NULL,
    [Description] NCHAR(10) NULL, 
    [DatePlaneStart] DATETIME2 NULL
  
)
