CREATE TABLE [dbo].[ServicesUpdate](
	[idServUpdate] [uniqueidentifier] NOT NULL,
	[IdServices] [uniqueidentifier] NULL,
	[DateOfUpdate] [datetime2](7) NULL,
	[IdUser] [uniqueidentifier] NULL,
	[TypeOfUpdate] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[idServUpdate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ServicesUpdate]  WITH CHECK ADD  CONSTRAINT [FK_ServicesUpdate_OurServices] FOREIGN KEY([IdServices])
REFERENCES [dbo].[OurServices] ([idServis])
GO

ALTER TABLE [dbo].[ServicesUpdate] CHECK CONSTRAINT [FK_ServicesUpdate_OurServices]
GO


GO
ALTER TABLE [dbo].[ServicesUpdate]  WITH CHECK ADD  CONSTRAINT [FK_ServicesUpdate_User] FOREIGN KEY([IdUser])
REFERENCES [dbo].[User] ([idUser])
GO

ALTER TABLE [dbo].[ServicesUpdate] CHECK CONSTRAINT [FK_ServicesUpdate_User]
GO

