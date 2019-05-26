CREATE TABLE [dbo].[EstabilismentPost](
	[idEstabilisment] [uniqueidentifier] NOT NULL,
	[idWorker] [uniqueidentifier] NULL,
	[idPost] [uniqueidentifier] NULL,
	[Salary] [money] NULL,
	[DateOfOperate] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[idEstabilisment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[EstabilismentPost]  WITH CHECK ADD  CONSTRAINT [FK_EstabilismentPost_WorkerDetails] FOREIGN KEY([idWorker])
REFERENCES [dbo].[WorkerDetails] ([IdWorker])
GO

ALTER TABLE [dbo].[EstabilismentPost] CHECK CONSTRAINT [FK_EstabilismentPost_WorkerDetails]
GO


GO
ALTER TABLE [dbo].[EstabilismentPost]  WITH CHECK ADD  CONSTRAINT [FK_EstabilismentPost_WorkerPosts] FOREIGN KEY([idPost])
REFERENCES [dbo].[WorkerPosts] ([idPost])
GO

ALTER TABLE [dbo].[EstabilismentPost] CHECK CONSTRAINT [FK_EstabilismentPost_WorkerPosts]
GO

