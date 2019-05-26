CREATE TABLE [dbo].[DialogMessage](
	[idMessage] [uniqueidentifier] NOT NULL,
	[idDialog] [uniqueidentifier] NULL,
	[idUser] [uniqueidentifier] NULL,
	[Text] [nvarchar](max) NULL,
	[Foto] [varbinary](max) NULL,
	[CreatedTime] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[idMessage] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[DialogMessage]  WITH CHECK ADD  CONSTRAINT [FK_DialogMessage_Dialogs] FOREIGN KEY([idDialog])
REFERENCES [dbo].[Dialogs] ([idDialog])
GO

ALTER TABLE [dbo].[DialogMessage] CHECK CONSTRAINT [FK_DialogMessage_Dialogs]
GO


GO
ALTER TABLE [dbo].[DialogMessage]  WITH CHECK ADD  CONSTRAINT [FK_DialogMessage_User] FOREIGN KEY([idUser])
REFERENCES [dbo].[User] ([idUser])
GO

ALTER TABLE [dbo].[DialogMessage] CHECK CONSTRAINT [FK_DialogMessage_User]
GO

