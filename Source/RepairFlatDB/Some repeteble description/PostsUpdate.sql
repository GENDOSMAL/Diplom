CREATE TABLE [dbo].[PostsUpdate](
	[idUpdatePos] [uniqueidentifier] NOT NULL,
	[idPost] [uniqueidentifier] NULL,
	[DateUpdate] [datetime2](7) NULL,
	[TypeOfUpdate] [nvarchar](20) NULL,
	[IdUpdateUser] [uniqueidentifier] NULL,
 CONSTRAINT [PK_PostsUpdate] PRIMARY KEY CLUSTERED 
(
	[idUpdatePos] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PostsUpdate]  WITH CHECK ADD  CONSTRAINT [FK_PostsUpdate_User] FOREIGN KEY([IdUpdateUser])
REFERENCES [dbo].[User] ([idUser])
GO

ALTER TABLE [dbo].[PostsUpdate] CHECK CONSTRAINT [FK_PostsUpdate_User]
GO


GO
ALTER TABLE [dbo].[PostsUpdate]  WITH CHECK ADD  CONSTRAINT [FK_PostsUpdate_WorkerPosts] FOREIGN KEY([idPost])
REFERENCES [dbo].[WorkerPosts] ([idPost])
GO

ALTER TABLE [dbo].[PostsUpdate] CHECK CONSTRAINT [FK_PostsUpdate_WorkerPosts]
GO

