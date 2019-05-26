CREATE TABLE [dbo].[LoginInformation](
	[IdLog] [uniqueidentifier] NOT NULL,
	[Login] [nchar](30) NULL,
	[Password] [nchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdLog] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[LoginInformation]  WITH CHECK ADD  CONSTRAINT [FK_LoginInformation_User] FOREIGN KEY([IdLog])
REFERENCES [dbo].[User] ([idUser])
GO

ALTER TABLE [dbo].[LoginInformation] CHECK CONSTRAINT [FK_LoginInformation_User]
GO

