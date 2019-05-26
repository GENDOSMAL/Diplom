CREATE TABLE [dbo].[DialogUser](
	[idUserInDialog] [uniqueidentifier] NOT NULL,
	[idDialog] [uniqueidentifier] NULL,
	[idUser] [uniqueidentifier] NULL,
	[JoinedTime] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[idUserInDialog] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[DialogUser]  WITH CHECK ADD  CONSTRAINT [FK_DialogUser_Dialogs] FOREIGN KEY([idDialog])
REFERENCES [dbo].[Dialogs] ([idDialog])
GO

ALTER TABLE [dbo].[DialogUser] CHECK CONSTRAINT [FK_DialogUser_Dialogs]
GO


GO
ALTER TABLE [dbo].[DialogUser]  WITH CHECK ADD  CONSTRAINT [FK_DialogUser_User] FOREIGN KEY([idUser])
REFERENCES [dbo].[User] ([idUser])
GO

ALTER TABLE [dbo].[DialogUser] CHECK CONSTRAINT [FK_DialogUser_User]
GO

