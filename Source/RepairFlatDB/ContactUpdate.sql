﻿CREATE TABLE [dbo].[ContactUpdate]
(
	[idContactUpdate] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [idContact] UNIQUEIDENTIFIER NULL, 
    [DataOfUpdate] DATETIME2 NULL, 
    [idUser] UNIQUEIDENTIFIER NULL
)
