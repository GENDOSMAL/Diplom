CREATE TABLE [dbo].[WorkersPayGive](
	[idGive] [uniqueidentifier] NOT NULL,
	[idWorkerMake] [uniqueidentifier] NULL,
	[idWorkerAdresat] [uniqueidentifier] NULL,
	[Size] [money] NULL,
	[Data] [datetime2](7) NULL,
	[Descriptiom] [nchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[idGive] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[WorkersPayGive]  WITH CHECK ADD  CONSTRAINT [FK_WorkersPayGive_WorkerDetails] FOREIGN KEY([idWorkerAdresat])
REFERENCES [dbo].[WorkerDetails] ([IdWorker])
GO

ALTER TABLE [dbo].[WorkersPayGive] CHECK CONSTRAINT [FK_WorkersPayGive_WorkerDetails]
GO


GO


GO
ALTER TABLE [dbo].[WorkersPayGive]  WITH CHECK ADD  CONSTRAINT [FK_WorkersPayGive_WorkerDetails1] FOREIGN KEY([idWorkerMake])
REFERENCES [dbo].[WorkerDetails] ([IdWorker])
GO

ALTER TABLE [dbo].[WorkersPayGive] CHECK CONSTRAINT [FK_WorkersPayGive_WorkerDetails1]
GO


GO

