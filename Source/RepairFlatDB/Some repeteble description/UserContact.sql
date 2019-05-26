CREATE TABLE [dbo].[UserContact](
	[id] [uniqueidentifier] NOT NULL,
	[idUser] [uniqueidentifier] NOT NULL,
	[idType] [uniqueidentifier] NULL,
	[Value] [nchar](30) NULL,
	[Description] [nchar](50) NULL,
	[DateAdd] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[UserContact]  WITH CHECK ADD  CONSTRAINT [FK_UserContact_TypeOfContact] FOREIGN KEY([idType])
REFERENCES [dbo].[TypeOfContact] ([idContact])
GO

ALTER TABLE [dbo].[UserContact] CHECK CONSTRAINT [FK_UserContact_TypeOfContact]
GO


GO
ALTER TABLE [dbo].[UserContact]  WITH CHECK ADD  CONSTRAINT [FK_UserContact_User] FOREIGN KEY([idUser])
REFERENCES [dbo].[User] ([idUser])
GO

ALTER TABLE [dbo].[UserContact] CHECK CONSTRAINT [FK_UserContact_User]
GO

