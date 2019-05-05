CREATE TABLE [dbo].[OurMaterials]
(
	[idMaterials] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [NameOfMaterial] NCHAR(30) NULL, 
    [UnitOfMeasue] NCHAR(10) NULL, 
    [Cost] MONEY NULL, 
    [Description] NCHAR(100) NULL, 
    [TypeOfMaterial] NCHAR(100) NULL
)
