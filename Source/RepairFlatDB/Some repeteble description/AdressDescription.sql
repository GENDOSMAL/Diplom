CREATE TABLE [dbo].[AdressDescription](
	[idAdress] [uniqueidentifier] NOT NULL,
	[RegionName] [nchar](50) NULL,
	[AreaName] [nchar](50) NULL,
	[CiryName] [nchar](50) NULL,
	[MicroAreaName] [nchar](50) NULL,
	[Street] [nchar](50) NULL,
	[House] [nchar](10) NULL,
	[Entrance] [nchar](10) NULL,
	[NumberOfDelen] [nchar](10) NULL,
	[Description] [nchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[idAdress] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
