CREATE TABLE [dbo].[OrderMeasurements]
(
	[idMeasurements] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [IdOrder] UNIQUEIDENTIFIER NULL, 
    [idPremisesType] UNIQUEIDENTIFIER NULL, 
    [Description] NCHAR(50) NULL, 
    [Lenght] FLOAT NULL, 
    [Width] FLOAT NULL, 
    [Height] FLOAT NULL, 
    [Pwalls] FLOAT NULL, 
    [PCelling] FLOAT NULL, 
    [Swalls] FLOAT NULL, 
    [Sfloor] FLOAT NULL
)
