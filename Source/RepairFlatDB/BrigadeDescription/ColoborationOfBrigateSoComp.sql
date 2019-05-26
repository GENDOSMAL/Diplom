CREATE TABLE [dbo].[ColoborationOfBrigateSoComp](
	[Id] [uniqueidentifier] NOT NULL,
	[IdBrigate] [uniqueidentifier] NULL,
	[IdColoboration] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ColoborationOfBrigateSoComp]  WITH CHECK ADD  CONSTRAINT [FK_ColoborationOfBrigateSoComp_BrigateSeparation] FOREIGN KEY([IdBrigate])
REFERENCES [dbo].[BrigateSeparation] ([IdBrigate])
GO

ALTER TABLE [dbo].[ColoborationOfBrigateSoComp] CHECK CONSTRAINT [FK_ColoborationOfBrigateSoComp_BrigateSeparation]
GO


GO
ALTER TABLE [dbo].[ColoborationOfBrigateSoComp]  WITH CHECK ADD  CONSTRAINT [FK_ColoborationOfBrigateSoComp_ColoborationOfBrigade] FOREIGN KEY([IdColoboration])
REFERENCES [dbo].[ColoborationOfBrigade] ([IdColoboration])
GO

ALTER TABLE [dbo].[ColoborationOfBrigateSoComp] CHECK CONSTRAINT [FK_ColoborationOfBrigateSoComp_ColoborationOfBrigade]
GO

