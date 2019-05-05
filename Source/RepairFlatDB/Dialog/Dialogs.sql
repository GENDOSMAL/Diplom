CREATE TABLE [dbo].[Dialogs]
(
	[idDialog] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [NameOfDialog] NCHAR(100) NULL, 
    [FotoOfdialog] VARBINARY(MAX) NULL, 
    [CreatedTime] DATETIME2 NULL
)
