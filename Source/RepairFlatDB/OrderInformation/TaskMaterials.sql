CREATE TABLE [dbo].[TaskMaterials](
	[idOfCompare] [uniqueidentifier] NOT NULL,
	[idTask] [uniqueidentifier] NULL,
	[idMaterials] [uniqueidentifier] NULL,
 CONSTRAINT [PK_TaskMaterials] PRIMARY KEY CLUSTERED 
(
	[idOfCompare] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TaskMaterials]  WITH CHECK ADD  CONSTRAINT [FK_TaskMaterials_OrderMaterial] FOREIGN KEY([idMaterials])
REFERENCES [dbo].[OrderMaterial] ([idMaterialINOrder])
GO

ALTER TABLE [dbo].[TaskMaterials] CHECK CONSTRAINT [FK_TaskMaterials_OrderMaterial]
GO


GO
ALTER TABLE [dbo].[TaskMaterials]  WITH CHECK ADD  CONSTRAINT [FK_TaskMaterials_OrderTasks] FOREIGN KEY([idTask])
REFERENCES [dbo].[OrderTasks] ([IdTask])
GO

ALTER TABLE [dbo].[TaskMaterials] CHECK CONSTRAINT [FK_TaskMaterials_OrderTasks]
GO

