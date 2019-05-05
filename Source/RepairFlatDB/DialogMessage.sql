﻿CREATE TABLE [dbo].[DialogMessage]
(
	[idMessage] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [idDialog] UNIQUEIDENTIFIER NULL, 
    [idUser] UNIQUEIDENTIFIER NULL, 
    [Text] NVARCHAR(MAX) NULL, 
    [Foto] VARBINARY(MAX) NULL, 
    [CreatedTime] DATETIME2 NULL
)
