CREATE TABLE [dbo].[ClientDetails](
	[IdClient] [uniqueidentifier] NOT NULL,
	[Description] [nchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdClient] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ClientDetails]  WITH CHECK ADD  CONSTRAINT [FK_ClientDetails_User] FOREIGN KEY([IdClient])
REFERENCES [dbo].[User] ([idUser])
GO

ALTER TABLE [dbo].[ClientDetails] CHECK CONSTRAINT [FK_ClientDetails_User]
GO

