CREATE TABLE [dbo].[OurServices](
	[idServis] [uniqueidentifier] NOT NULL,
	[Nomination] [nchar](50) NULL,
	[TypeOfServices] [nchar](10) NULL,
	[UnitOfMeasue] [nchar](20) NULL,
	[Cost] [money] NULL,
	[Description] [nchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[idServis] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
