CREATE TABLE [dbo].[OurMaterials](
	[idMaterials] [uniqueidentifier] NOT NULL,
	[NameOfMaterial] [nchar](30) NULL,
	[UnitOfMeasue] [nchar](10) NULL,
	[Cost] [money] NULL,
	[Description] [nchar](100) NULL,
	[TypeOfMaterial] [nchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[idMaterials] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
