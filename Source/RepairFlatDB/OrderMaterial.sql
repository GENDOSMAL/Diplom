CREATE TABLE [dbo].[OrderMaterial]
(
	[idMaterialINOrder] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [IdOrder] UNIQUEIDENTIFIER NULL, 
    [idMaterial] UNIQUEIDENTIFIER NULL, 
    [Count] FLOAT NULL,
	[Cost] MONEY NULL,
    [Description] NCHAR(50) NULL
)
