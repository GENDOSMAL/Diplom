CREATE TABLE [dbo].[OrderServises](
	[IdServises] [uniqueidentifier] NOT NULL,
	[IdOrder] [uniqueidentifier] NULL,
	[idServis] [uniqueidentifier] NULL,
	[Count] [float] NULL,
	[Cost] [money] NULL,
	[Description] [nchar](10) NULL,
	[DatePlaneStart] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdServises] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[OrderServises]  WITH CHECK ADD  CONSTRAINT [FK_OrderServises_OrderInformation] FOREIGN KEY([IdOrder])
REFERENCES [dbo].[OrderInformation] ([IdOrder])
GO

ALTER TABLE [dbo].[OrderServises] CHECK CONSTRAINT [FK_OrderServises_OrderInformation]
GO


GO


GO
ALTER TABLE [dbo].[OrderServises]  WITH CHECK ADD  CONSTRAINT [FK_OrderServises_OurServices] FOREIGN KEY([idServis])
REFERENCES [dbo].[OurServices] ([idServis])
GO

ALTER TABLE [dbo].[OrderServises] CHECK CONSTRAINT [FK_OrderServises_OurServices]
GO


GO

