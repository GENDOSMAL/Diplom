CREATE TABLE [dbo].[WorkersPayGive]
(
	[idGive] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [idWorkerMake] UNIQUEIDENTIFIER NULL, 
    [idWorkerAdresat] UNIQUEIDENTIFIER NULL, 
    [Size] MONEY NULL, 
    [Data] DATETIME2 NULL, 
    [Descriptiom] NCHAR(100) NULL
)
