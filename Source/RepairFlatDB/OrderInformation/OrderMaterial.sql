CREATE TABLE [dbo].[OrderMaterial](
	[idMaterialINOrder] [uniqueidentifier] NOT NULL,
	[IdOrder] [uniqueidentifier] NULL,
	[idMaterial] [uniqueidentifier] NULL,
	[Count] [float] NULL,
	[Cost] [money] NULL,
	[Description] [nchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[idMaterialINOrder] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[OrderMaterial]  WITH CHECK ADD  CONSTRAINT [FK_OrderMaterial_OrderInformation] FOREIGN KEY([IdOrder])
REFERENCES [dbo].[OrderInformation] ([IdOrder])
GO

ALTER TABLE [dbo].[OrderMaterial] CHECK CONSTRAINT [FK_OrderMaterial_OrderInformation]
GO


GO


GO
ALTER TABLE [dbo].[OrderMaterial]  WITH CHECK ADD  CONSTRAINT [FK_OrderMaterial_OurMaterials] FOREIGN KEY([idMaterial])
REFERENCES [dbo].[OurMaterials] ([idMaterials])
GO

ALTER TABLE [dbo].[OrderMaterial] CHECK CONSTRAINT [FK_OrderMaterial_OurMaterials]
GO


GO

