CREATE TABLE [dbo].[OrderPayment](
	[IdPayment] [uniqueidentifier] NOT NULL,
	[IdOrder] [uniqueidentifier] NULL,
	[IdWorkerMake] [uniqueidentifier] NULL,
	[idDocPayment] [uniqueidentifier] NULL,
	[Description] [nchar](100) NULL,
 CONSTRAINT [PK__OrderPay__613289C0861E1C36] PRIMARY KEY CLUSTERED 
(
	[IdPayment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[OrderPayment]  WITH CHECK ADD  CONSTRAINT [FK_OrderPayment_OrderInformation] FOREIGN KEY([IdOrder])
REFERENCES [dbo].[OrderInformation] ([IdOrder])
GO

ALTER TABLE [dbo].[OrderPayment] CHECK CONSTRAINT [FK_OrderPayment_OrderInformation]
GO


GO
ALTER TABLE [dbo].[OrderPayment]  WITH CHECK ADD  CONSTRAINT [FK_OrderPayment_WorkerDetails] FOREIGN KEY([IdWorkerMake])
REFERENCES [dbo].[WorkerDetails] ([IdWorker])
GO

ALTER TABLE [dbo].[OrderPayment] CHECK CONSTRAINT [FK_OrderPayment_WorkerDetails]
GO
ALTER TABLE [dbo].[OrderPayment]  WITH CHECK ADD  CONSTRAINT [FK_OrderPayment_DocPayment] FOREIGN KEY([idDocPayment])
REFERENCES [dbo].[DocPayment] ([idDocPayment])
GO

ALTER TABLE [dbo].[OrderPayment] CHECK CONSTRAINT [FK_OrderPayment_DocPayment]