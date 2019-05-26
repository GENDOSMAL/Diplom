CREATE TABLE [dbo].[OrderTasks](
	[IdTask] [uniqueidentifier] NOT NULL,
	[IdOrder] [uniqueidentifier] NULL,
	[Description] [nchar](200) NULL,
	[DateStart] [datetime2](7) NULL,
	[DeadEnd] [datetime2](7) NULL,
	[SummaAboutTask] [money] NULL,
	[idBrigade] [uniqueidentifier] NULL,
 CONSTRAINT [PK__OrderTas__9FCAD1C5A820E811] PRIMARY KEY CLUSTERED 
(
	[IdTask] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[OrderTasks]  WITH CHECK ADD  CONSTRAINT [FK_OrderTasks_OrderInformation1] FOREIGN KEY([IdOrder])
REFERENCES [dbo].[OrderInformation] ([IdOrder])
GO

ALTER TABLE [dbo].[OrderTasks] CHECK CONSTRAINT [FK_OrderTasks_OrderInformation1]
GO
ALTER TABLE [dbo].[OrderTasks]  WITH CHECK ADD  CONSTRAINT [FK_OrderTasks_BrigateSeparation] FOREIGN KEY([idBrigade])
REFERENCES [dbo].[BrigateSeparation] ([IdBrigate])
GO

ALTER TABLE [dbo].[OrderTasks] CHECK CONSTRAINT [FK_OrderTasks_BrigateSeparation]