CREATE TABLE [dbo].[AdressDescription]
(
	[idAdress] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [RegionName] NCHAR(50) NULL, 
    [AreaName] NCHAR(50) NULL, 
    [CiryName] NCHAR(50) NULL, 
    [MicroAreaName] NCHAR(50) NULL, 
    [Street] NCHAR(50) NULL, 
    [House] NCHAR(10) NULL, 
    [Entrance] NCHAR(10) NULL, 
    [NumberOfDelen] NCHAR(10) NULL, 
    [Description] NCHAR(100) NULL
)
