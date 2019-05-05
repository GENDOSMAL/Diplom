CREATE TABLE [dbo].[LoginInformation]
(
	[IdLog] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Login] NCHAR(30) NULL, 
    [Password] NCHAR(500) NULL
)
