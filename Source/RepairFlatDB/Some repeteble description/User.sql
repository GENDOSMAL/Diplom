CREATE TABLE [dbo].[User]
(
	[idUser] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] NCHAR(50) NULL, 
    [LastName] NCHAR(50) NULL, 
    [Patronymic] NCHAR(50) NULL, 
    [Pasport] NCHAR(10) NULL, 
    [Female] INT NULL, 
    [BirstDay] DATE NULL, 
    [TypeOfUser] NCHAR(2) NULL
)
