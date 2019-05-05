CREATE TABLE [dbo].[MaterialsUpdate]
(
	[idMaterialUpdate] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [idMaterials] UNIQUEIDENTIFIER NULL, 
    [DateOfUpdate] DATETIME2 NULL, 
    [IdUser] UNIQUEIDENTIFIER NULL
)
