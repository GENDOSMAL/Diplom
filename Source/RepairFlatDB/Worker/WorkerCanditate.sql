CREATE TABLE [dbo].[WorkerCanditate](
	[idCandidate] [int] NOT NULL,
	[Name] [nchar](50) NULL,
	[LastName] [nchar](50) NULL,
	[Patronymic] [nchar](50) NULL,
	[BirstDay] [date] NULL,
	[Female] [int] NULL,
	[Pasport] [nchar](12) NULL,
	[Telephone] [nchar](20) NULL,
	[Mail] [nchar](30) NULL,
	[IdPost] [uniqueidentifier] NULL,
	[PlanSalary] [money] NULL,
	[IdAdress] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[idCandidate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[WorkerCanditate]  WITH CHECK ADD  CONSTRAINT [FK_WorkerCanditate_AdressDescription] FOREIGN KEY([IdAdress])
REFERENCES [dbo].[AdressDescription] ([idAdress])
GO

ALTER TABLE [dbo].[WorkerCanditate] CHECK CONSTRAINT [FK_WorkerCanditate_AdressDescription]
GO


GO
ALTER TABLE [dbo].[WorkerCanditate]  WITH CHECK ADD  CONSTRAINT [FK_WorkerCanditate_WorkerPosts] FOREIGN KEY([IdPost])
REFERENCES [dbo].[WorkerPosts] ([idPost])
GO

ALTER TABLE [dbo].[WorkerCanditate] CHECK CONSTRAINT [FK_WorkerCanditate_WorkerPosts]
GO

