CREATE TABLE [dbo].[TypeOfContact]
(
	[idContact] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Value] NCHAR(100) NULL, 
    [Description] NCHAR(100) NULL
)
