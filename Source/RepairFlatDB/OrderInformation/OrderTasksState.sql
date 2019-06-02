CREATE TABLE [dbo].[OrderTasksState](
	[IdState] [uniqueidentifier] NOT NULL,
	[IdTask] [uniqueidentifier] NULL,
	[DateOf] [datetime2](7) NULL,
	[State] [nchar](20) NULL,
	[IdWorker] [uniqueidentifier] NULL,
	[Description] [nchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdState] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[OrderTasksState]  WITH CHECK ADD  CONSTRAINT [FK_OrderTasksState_OrderTasks] FOREIGN KEY([IdTask])
REFERENCES [dbo].[OrderTasks] ([IdTask])
GO

ALTER TABLE [dbo].[OrderTasksState] CHECK CONSTRAINT [FK_OrderTasksState_OrderTasks]
GO


GO


GO
ALTER TABLE [dbo].[OrderTasksState]  WITH CHECK ADD  CONSTRAINT [FK_OrderTasksState_WorkerDetails] FOREIGN KEY([IdWorker])
REFERENCES [dbo].[WorkerDetails] ([IdWorker])
GO

ALTER TABLE [dbo].[OrderTasksState] CHECK CONSTRAINT [FK_OrderTasksState_WorkerDetails]
GO


GO

