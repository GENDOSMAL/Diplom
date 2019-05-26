CREATE TABLE [dbo].[DocPayment](
	[idDocPayment] [uniqueidentifier] NOT NULL,
	[DateOfDoc] [datetime2](7) NULL,
	[idWorker] [uniqueidentifier] NULL,
	[idClient] [uniqueidentifier] NULL,
	[Summa] [money] NULL,
	[idInformatioAboutPayment] [uniqueidentifier] NULL,
 CONSTRAINT [PK_DocPayment] PRIMARY KEY CLUSTERED 
(
	[idDocPayment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DocPayment]  WITH CHECK ADD  CONSTRAINT [FK_DocPayment_ClientDetails] FOREIGN KEY([idClient])
REFERENCES [dbo].[ClientDetails] ([IdClient])
GO

ALTER TABLE [dbo].[DocPayment] CHECK CONSTRAINT [FK_DocPayment_ClientDetails]
GO
ALTER TABLE [dbo].[DocPayment]  WITH CHECK ADD  CONSTRAINT [FK_DocPayment_InformatioForPayment] FOREIGN KEY([idInformatioAboutPayment])
REFERENCES [dbo].[InformatioForPayment] ([idInfPayment])
GO

ALTER TABLE [dbo].[DocPayment] CHECK CONSTRAINT [FK_DocPayment_InformatioForPayment]
GO
ALTER TABLE [dbo].[DocPayment]  WITH CHECK ADD  CONSTRAINT [FK_DocPayment_WorkerDetails] FOREIGN KEY([idWorker])
REFERENCES [dbo].[WorkerDetails] ([IdWorker])
GO

ALTER TABLE [dbo].[DocPayment] CHECK CONSTRAINT [FK_DocPayment_WorkerDetails]