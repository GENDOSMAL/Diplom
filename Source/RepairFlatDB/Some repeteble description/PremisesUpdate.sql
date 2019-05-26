CREATE TABLE [dbo].[PremisesUpdate](
	[idPremisesUpdate] [uniqueidentifier] NOT NULL,
	[idPremises] [uniqueidentifier] NULL,
	[DateOfUpdate] [datetime2](7) NULL,
	[IdUser] [uniqueidentifier] NULL,
	[TypeOfUpdate] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[idPremisesUpdate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[PremisesUpdate]  WITH CHECK ADD  CONSTRAINT [FK_PremisesUpdate_PremisesType] FOREIGN KEY([idPremises])
REFERENCES [dbo].[PremisesType] ([idPremises])
GO

ALTER TABLE [dbo].[PremisesUpdate] CHECK CONSTRAINT [FK_PremisesUpdate_PremisesType]
GO


GO
ALTER TABLE [dbo].[PremisesUpdate]  WITH CHECK ADD  CONSTRAINT [FK_PremisesUpdate_User] FOREIGN KEY([IdUser])
REFERENCES [dbo].[User] ([idUser])
GO

ALTER TABLE [dbo].[PremisesUpdate] CHECK CONSTRAINT [FK_PremisesUpdate_User]
GO

