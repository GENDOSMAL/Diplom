CREATE TABLE [dbo].[OrderInformation]
(
	[IdOrder] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [IdAdress] UNIQUEIDENTIFIER NULL, 
    [IdWorkerMake] UNIQUEIDENTIFIER NULL, 
    [idClient] UNIQUEIDENTIFIER NULL, 
    [DateStart] DATETIME2 NULL, 
    [Status] INT NULL, 
    [AllSumma] MONEY NULL, 
    [Description] NCHAR(50) NULL, 
    [IdColoboration] UNIQUEIDENTIFIER NULL, 
    [Number] INT NULL
)
