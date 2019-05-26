CREATE TABLE [dbo].[TaskMeasurment](
	[idOfCompare] [uniqueidentifier] NOT NULL,
	[idMeasurment] [uniqueidentifier] NULL,
	[idOrderTask] [uniqueidentifier] NULL,
 CONSTRAINT [PK_TaskMeasurment] PRIMARY KEY CLUSTERED 
(
	[idOfCompare] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TaskMeasurment]  WITH CHECK ADD  CONSTRAINT [FK_TaskMeasurment_OrderMeasurements] FOREIGN KEY([idMeasurment])
REFERENCES [dbo].[OrderMeasurements] ([idMeasurements])
GO

ALTER TABLE [dbo].[TaskMeasurment] CHECK CONSTRAINT [FK_TaskMeasurment_OrderMeasurements]
GO
ALTER TABLE [dbo].[TaskMeasurment]  WITH CHECK ADD  CONSTRAINT [FK_TaskMeasurment_OrderTasks] FOREIGN KEY([idOrderTask])
REFERENCES [dbo].[OrderTasks] ([IdTask])
GO

ALTER TABLE [dbo].[TaskMeasurment] CHECK CONSTRAINT [FK_TaskMeasurment_OrderTasks]