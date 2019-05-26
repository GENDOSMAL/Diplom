CREATE TABLE [dbo].[InformatioForPayment](
	[idInfPayment] [uniqueidentifier] NOT NULL,
	[idWorkerMake] [uniqueidentifier] NULL,
	[NameOfRecipient] [nvarchar](100) NULL,
	[InnOfOrganization] [nvarchar](20) NULL,
	[KppOfOrganization] [nvarchar](20) NULL,
	[BankOfPayment] [nvarchar](100) NULL,
	[CheckingAcount] [nvarchar](100) NULL,
	[BIK] [nvarchar](20) NULL,
	[YIN] [nvarchar](20) NULL,
	[DateOfInsert] [datetime2](7) NULL,
 CONSTRAINT [PK_InformatioForPayment] PRIMARY KEY CLUSTERED 
(
	[idInfPayment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[InformatioForPayment]  WITH CHECK ADD  CONSTRAINT [FK_InformatioForPayment_WorkerDetails] FOREIGN KEY([idWorkerMake])
REFERENCES [dbo].[WorkerDetails] ([IdWorker])
GO

ALTER TABLE [dbo].[InformatioForPayment] CHECK CONSTRAINT [FK_InformatioForPayment_WorkerDetails]