CREATE TABLE [dbo].[WorkerDetails](
	[IdWorker] [uniqueidentifier] NOT NULL,
	[idAdress] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdWorker] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[WorkerDetails]  WITH CHECK ADD  CONSTRAINT [FK_WorkerDetails_AdressDescription] FOREIGN KEY([idAdress])
REFERENCES [dbo].[AdressDescription] ([idAdress])
GO

ALTER TABLE [dbo].[WorkerDetails] CHECK CONSTRAINT [FK_WorkerDetails_AdressDescription]
GO


GO


GO
ALTER TABLE [dbo].[WorkerDetails]  WITH CHECK ADD  CONSTRAINT [FK_WorkerDetails_User] FOREIGN KEY([IdWorker])
REFERENCES [dbo].[User] ([idUser])
GO

ALTER TABLE [dbo].[WorkerDetails] CHECK CONSTRAINT [FK_WorkerDetails_User]
GO


GO

