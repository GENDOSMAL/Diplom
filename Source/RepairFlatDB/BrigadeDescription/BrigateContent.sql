CREATE TABLE [dbo].[BrigateContent](
	[Id] [uniqueidentifier] NOT NULL,
	[idWorker] [uniqueidentifier] NULL,
	[WorkerRole] [nchar](100) NULL,
	[idColoboration] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[BrigateContent]  WITH CHECK ADD  CONSTRAINT [FK_BrigateContent_BrigateSeparation] FOREIGN KEY([idColoboration])
REFERENCES [dbo].[BrigateSeparation] ([IdBrigate])
GO

ALTER TABLE [dbo].[BrigateContent] CHECK CONSTRAINT [FK_BrigateContent_BrigateSeparation]
GO


GO
ALTER TABLE [dbo].[BrigateContent]  WITH CHECK ADD  CONSTRAINT [FK_BrigateContent_WorkerDetails] FOREIGN KEY([idWorker])
REFERENCES [dbo].[WorkerDetails] ([IdWorker])
GO

ALTER TABLE [dbo].[BrigateContent] CHECK CONSTRAINT [FK_BrigateContent_WorkerDetails]
GO

