CREATE TABLE [dbo].[OrderElementOfMeasurments](
	[idElement] [uniqueidentifier] NOT NULL,
	[idMeasurements] [uniqueidentifier] NULL,
	[TypeOfElement] [nchar](20) NULL,
	[Lenght] [float] NULL,
	[Height] [float] NULL,
	[Width] [float] NULL,
	[POfElement] [float] NULL,
	[WidthOfSlope] [float] NULL,
	[Description] [nchar](50) NULL,
 CONSTRAINT [PK__OrderEle__58DE12035767AAAF] PRIMARY KEY CLUSTERED 
(
	[idElement] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[OrderElementOfMeasurments]  WITH CHECK ADD  CONSTRAINT [FK_OrderElementOfMeasurments_OrderMeasurements] FOREIGN KEY([idMeasurements])
REFERENCES [dbo].[OrderMeasurements] ([idMeasurements])
GO

ALTER TABLE [dbo].[OrderElementOfMeasurments] CHECK CONSTRAINT [FK_OrderElementOfMeasurments_OrderMeasurements]
GO

