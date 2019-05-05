CREATE TABLE [dbo].[OrderElementOfMeasurments]
(
	[idElement] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [idMeasurements] UNIQUEIDENTIFIER NULL, 
    [TypeOfElement] NCHAR(20) NULL, 
    [Lenght] FLOAT NULL, 
    [Width] FLOAT NULL, 
    [POfElement] FLOAT NULL, 
    [WidthOfSlope] FLOAT NULL, 
    [Description] NCHAR(50) NULL
)
