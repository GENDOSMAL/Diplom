CREATE TABLE [dbo].[UserContact]
(
	[idUser] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [idType] UNIQUEIDENTIFIER NULL, 
    [Value] NCHAR(30) NULL, 
    [Description] NCHAR(50) NULL, 
    [DateAdd] DATETIME2 NULL
)
