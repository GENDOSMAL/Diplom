CREATE TABLE [dbo].[DeleteMessage](
	[idDelete] [uniqueidentifier] NOT NULL,
	[idMessage] [uniqueidentifier] NULL,
	[idUser] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[idDelete] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[DeleteMessage]  WITH CHECK ADD  CONSTRAINT [FK_DeleteMessage_DialogMessage] FOREIGN KEY([idMessage])
REFERENCES [dbo].[DialogMessage] ([idMessage])
GO

ALTER TABLE [dbo].[DeleteMessage] CHECK CONSTRAINT [FK_DeleteMessage_DialogMessage]
GO


GO
ALTER TABLE [dbo].[DeleteMessage]  WITH CHECK ADD  CONSTRAINT [FK_DeleteMessage_User] FOREIGN KEY([idUser])
REFERENCES [dbo].[User] ([idUser])
GO

ALTER TABLE [dbo].[DeleteMessage] CHECK CONSTRAINT [FK_DeleteMessage_User]
GO

