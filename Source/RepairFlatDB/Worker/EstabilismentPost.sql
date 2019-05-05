CREATE TABLE [dbo].[EstabilismentPost]
(
	[idEstabilisment] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [idWorker] UNIQUEIDENTIFIER NULL, 
    [idPost] UNIQUEIDENTIFIER NULL, 
    [Salary] MONEY NULL, 
    [DateOfOperate] DATETIME2 NULL 
)
