CREATE TABLE [dbo].[OrderMeasurements](
	[idMeasurements] [uniqueidentifier] NOT NULL,
	[IdOrder] [uniqueidentifier] NULL,
	[idPremisesType] [uniqueidentifier] NULL,
	[Description] [nchar](50) NULL,
	[Lenght] [float] NULL,
	[Width] [float] NULL,
	[Height] [float] NULL,
	[Pwalls] [float] NULL,
	[PCelling] [float] NULL,
	[Swalls] [float] NULL,
	[Sfloor] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[idMeasurements] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[OrderMeasurements]  WITH CHECK ADD  CONSTRAINT [FK_OrderMeasurements_OrderInformation] FOREIGN KEY([IdOrder])
REFERENCES [dbo].[OrderInformation] ([IdOrder])
GO

ALTER TABLE [dbo].[OrderMeasurements] CHECK CONSTRAINT [FK_OrderMeasurements_OrderInformation]
GO


GO
ALTER TABLE [dbo].[OrderMeasurements]  WITH CHECK ADD  CONSTRAINT [FK_OrderMeasurements_PremisesType] FOREIGN KEY([idPremisesType])
REFERENCES [dbo].[PremisesType] ([idPremises])
GO

ALTER TABLE [dbo].[OrderMeasurements] CHECK CONSTRAINT [FK_OrderMeasurements_PremisesType]
GO

