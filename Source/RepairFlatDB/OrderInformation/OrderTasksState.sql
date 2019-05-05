﻿CREATE TABLE [dbo].[OrderTasksState]
(
	[IdState] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [IdTask] UNIQUEIDENTIFIER NULL, 
    [DateOf] DATETIME2 NULL, 
    [State] NCHAR(20) NULL, 
    [IdWorker] UNIQUEIDENTIFIER NULL, 
    [Description] NCHAR(100) NULL
)
