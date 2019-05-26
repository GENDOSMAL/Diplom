CREATE TABLE [dbo].[ContactUpdate](
	[idContactUpdate] [uniqueidentifier] NOT NULL,
	[idContact] [uniqueidentifier] NULL,
	[DataOfUpdate] [datetime2](7) NULL,
	[idUser] [uniqueidentifier] NULL,
	[TypeOfUpdate] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[idContactUpdate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ContactUpdate]  WITH CHECK ADD  CONSTRAINT [FK_ContactUpdate_TypeOfContact] FOREIGN KEY([idContact])
REFERENCES [dbo].[TypeOfContact] ([idContact])
GO

ALTER TABLE [dbo].[ContactUpdate] CHECK CONSTRAINT [FK_ContactUpdate_TypeOfContact]
GO


GO
ALTER TABLE [dbo].[ContactUpdate]  WITH CHECK ADD  CONSTRAINT [FK_ContactUpdate_User] FOREIGN KEY([idUser])
REFERENCES [dbo].[User] ([idUser])
GO

ALTER TABLE [dbo].[ContactUpdate] CHECK CONSTRAINT [FK_ContactUpdate_User]
GO

