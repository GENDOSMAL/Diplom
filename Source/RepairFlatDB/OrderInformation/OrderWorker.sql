CREATE TABLE [dbo].[OrderWorker](
	[idCmbination] [uniqueidentifier] NOT NULL,
	[idTask] [uniqueidentifier] NULL,
	[idWorker] [uniqueidentifier] NULL,
 CONSTRAINT [PK_OrderWorker] PRIMARY KEY CLUSTERED 
(
	[idCmbination] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrderWorker]  WITH CHECK ADD  CONSTRAINT [FK_OrderWorker_OrderTasks] FOREIGN KEY([idTask])
REFERENCES [dbo].[OrderTasks] ([IdTask])
GO

ALTER TABLE [dbo].[OrderWorker] CHECK CONSTRAINT [FK_OrderWorker_OrderTasks]