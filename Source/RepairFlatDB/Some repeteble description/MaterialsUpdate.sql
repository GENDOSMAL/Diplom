CREATE TABLE [dbo].[MaterialsUpdate](
	[idMaterialUpdate] [uniqueidentifier] NOT NULL,
	[idMaterials] [uniqueidentifier] NULL,
	[DateOfUpdate] [datetime2](7) NULL,
	[IdUser] [uniqueidentifier] NULL,
	[TypeOfUpdate] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[idMaterialUpdate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[MaterialsUpdate]  WITH CHECK ADD  CONSTRAINT [FK_MaterialsUpdate_OurMaterials] FOREIGN KEY([idMaterials])
REFERENCES [dbo].[OurMaterials] ([idMaterials])
GO

ALTER TABLE [dbo].[MaterialsUpdate] CHECK CONSTRAINT [FK_MaterialsUpdate_OurMaterials]
GO


GO
ALTER TABLE [dbo].[MaterialsUpdate]  WITH CHECK ADD  CONSTRAINT [FK_MaterialsUpdate_User] FOREIGN KEY([IdUser])
REFERENCES [dbo].[User] ([idUser])
GO

ALTER TABLE [dbo].[MaterialsUpdate] CHECK CONSTRAINT [FK_MaterialsUpdate_User]
GO

