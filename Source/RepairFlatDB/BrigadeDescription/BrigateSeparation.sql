CREATE TABLE [dbo].[BrigateSeparation]
(
	[IdBrigate] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] NCHAR(30) NULL, 
    [Description] NCHAR(100) NULL, 
    [DateStart] DATE NULL
)
