CREATE TABLE [dbo].[WorkersOperats]
(
	[idOperate] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [idWorker] UNIQUEIDENTIFIER NULL, 
    [idEstabilisment] UNIQUEIDENTIFIER NULL, 
    [TypeOfOperate] INT NULL
)
