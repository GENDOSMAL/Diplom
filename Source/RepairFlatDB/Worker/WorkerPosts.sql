CREATE TABLE [dbo].[WorkerPosts]
(
	[idPost] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [NameOfPost] NCHAR(100) NULL, 
    [BaseWage] MONEY NULL
)
