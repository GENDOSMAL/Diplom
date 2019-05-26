CREATE TABLE [dbo].[OrderInformation](
	[IdOrder] [uniqueidentifier] NOT NULL,
	[IdAdress] [uniqueidentifier] NULL,
	[IdWorkerMake] [uniqueidentifier] NULL,
	[idClient] [uniqueidentifier] NULL,
	[DateStart] [datetime2](7) NULL,
	[Status] [int] NULL,
	[AllSumma] [money] NULL,
	[Description] [nchar](50) NULL,
	[IdColoboration] [uniqueidentifier] NULL,
	[Number] [int] NULL,
	[MainContactID] [uniqueidentifier] NULL,
	[DateEnd] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdOrder] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[OrderInformation]  WITH CHECK ADD  CONSTRAINT [FK_OrderInformation_AdressDescription] FOREIGN KEY([IdAdress])
REFERENCES [dbo].[AdressDescription] ([idAdress])
GO

ALTER TABLE [dbo].[OrderInformation] CHECK CONSTRAINT [FK_OrderInformation_AdressDescription]
GO


GO
ALTER TABLE [dbo].[OrderInformation]  WITH CHECK ADD  CONSTRAINT [FK_OrderInformation_ClientDetails] FOREIGN KEY([idClient])
REFERENCES [dbo].[ClientDetails] ([IdClient])
GO

ALTER TABLE [dbo].[OrderInformation] CHECK CONSTRAINT [FK_OrderInformation_ClientDetails]
GO


GO
ALTER TABLE [dbo].[OrderInformation]  WITH CHECK ADD  CONSTRAINT [FK_OrderInformation_ColoborationOfBrigade] FOREIGN KEY([IdColoboration])
REFERENCES [dbo].[ColoborationOfBrigade] ([IdColoboration])
GO

ALTER TABLE [dbo].[OrderInformation] CHECK CONSTRAINT [FK_OrderInformation_ColoborationOfBrigade]
GO


GO
ALTER TABLE [dbo].[OrderInformation]  WITH CHECK ADD  CONSTRAINT [FK_OrderInformation_UserContact] FOREIGN KEY([MainContactID])
REFERENCES [dbo].[UserContact] ([id])
GO

ALTER TABLE [dbo].[OrderInformation] CHECK CONSTRAINT [FK_OrderInformation_UserContact]
GO


GO
ALTER TABLE [dbo].[OrderInformation]  WITH CHECK ADD  CONSTRAINT [FK_OrderInformation_WorkerDetails] FOREIGN KEY([IdWorkerMake])
REFERENCES [dbo].[WorkerDetails] ([IdWorker])
GO

ALTER TABLE [dbo].[OrderInformation] CHECK CONSTRAINT [FK_OrderInformation_WorkerDetails]
GO


GO
ALTER TABLE [dbo].[OrderInformation]  WITH CHECK ADD  CONSTRAINT [FK_OrderInformation_WorkerOrderInformation] FOREIGN KEY([IdWorkerMake])
REFERENCES [dbo].[WorkerOrderInformation] ([Id])
GO

ALTER TABLE [dbo].[OrderInformation] CHECK CONSTRAINT [FK_OrderInformation_WorkerOrderInformation]
GO

