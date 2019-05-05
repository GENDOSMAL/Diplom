CREATE TABLE [dbo].[UserContact]
(
    [id] UNIQUEIDENTIFIER NOT NULL, 
	[idUser] UNIQUEIDENTIFIER NOT NULL , 
    [idType] UNIQUEIDENTIFIER NULL, 
    [Value] NCHAR(30) NULL, 
    [Description] NCHAR(50) NULL, 
    [DateAdd] DATETIME2 NULL, 
    PRIMARY KEY ([id])
)
