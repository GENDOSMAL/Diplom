CREATE TABLE [dbo].[PremisesType](
	[idPremises] [uniqueidentifier] NOT NULL,
	[NameOfPremises] [nchar](100) NULL,
	[Descriprtion] [nchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[idPremises] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
