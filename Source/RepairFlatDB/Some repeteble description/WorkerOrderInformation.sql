CREATE TABLE [dbo].[WorkerOrderInformation](
	[Id] [uniqueidentifier] NOT NULL,
	[idWorker] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[WorkerOrderInformation]  WITH CHECK ADD  CONSTRAINT [FK_WorkerOrderInformation_WorkerDetails] FOREIGN KEY([idWorker])
REFERENCES [dbo].[WorkerDetails] ([IdWorker])
GO

ALTER TABLE [dbo].[WorkerOrderInformation] CHECK CONSTRAINT [FK_WorkerOrderInformation_WorkerDetails]
GO

