CREATE TABLE [dbo].[OrderPayment]
(
	[IdPayment] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [IdOrder] UNIQUEIDENTIFIER NULL, 
    [IdWorkerMake] UNIQUEIDENTIFIER NULL, 
    [DatePaymnent] DATETIME2 NULL, 
    [idDocAccepted] UNIQUEIDENTIFIER NULL, 
    [Description] NCHAR(50) NULL, 
    [Summa] MONEY NULL
)
