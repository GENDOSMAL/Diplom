CREATE TABLE [dbo].[TaskServis](
	[idOfCompare] [uniqueidentifier] NOT NULL,
	[idTask] [uniqueidentifier] NULL,
	[idOrderServis] [uniqueidentifier] NULL,
 CONSTRAINT [PK_TaskServis] PRIMARY KEY CLUSTERED 
(
	[idOfCompare] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TaskServis]  WITH CHECK ADD  CONSTRAINT [FK_TaskServis_OrderServises] FOREIGN KEY([idOrderServis])
REFERENCES [dbo].[OrderServises] ([IdServises])
GO

ALTER TABLE [dbo].[TaskServis] CHECK CONSTRAINT [FK_TaskServis_OrderServises]
GO
ALTER TABLE [dbo].[TaskServis]  WITH CHECK ADD  CONSTRAINT [FK_TaskServis_OrderTasks] FOREIGN KEY([idTask])
REFERENCES [dbo].[OrderTasks] ([IdTask])
GO

ALTER TABLE [dbo].[TaskServis] CHECK CONSTRAINT [FK_TaskServis_OrderTasks]