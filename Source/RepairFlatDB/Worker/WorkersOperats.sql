CREATE TABLE [dbo].[WorkersOperats](
	[idOperate] [uniqueidentifier] NOT NULL,
	[idWorker] [uniqueidentifier] NULL,
	[idEstabilisment] [uniqueidentifier] NULL,
	[TypeOfOperate] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idOperate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[WorkersOperats]  WITH CHECK ADD  CONSTRAINT [FK_WorkersOperats_EstabilismentPost] FOREIGN KEY([idEstabilisment])
REFERENCES [dbo].[EstabilismentPost] ([idEstabilisment])
GO

ALTER TABLE [dbo].[WorkersOperats] CHECK CONSTRAINT [FK_WorkersOperats_EstabilismentPost]
GO


GO


GO
ALTER TABLE [dbo].[WorkersOperats]  WITH CHECK ADD  CONSTRAINT [FK_WorkersOperats_WorkerDetails] FOREIGN KEY([idWorker])
REFERENCES [dbo].[WorkerDetails] ([IdWorker])
GO

ALTER TABLE [dbo].[WorkersOperats] CHECK CONSTRAINT [FK_WorkersOperats_WorkerDetails]
GO


GO

