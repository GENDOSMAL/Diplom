CREATE TABLE [dbo].[BrigateSeparation](
	[IdBrigate] [uniqueidentifier] NOT NULL,
	[Name] [nchar](30) NULL,
	[Description] [nchar](100) NULL,
	[DateStart] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdBrigate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
