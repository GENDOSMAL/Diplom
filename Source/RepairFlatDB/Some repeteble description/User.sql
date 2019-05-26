CREATE TABLE [dbo].[User](
	[idUser] [uniqueidentifier] NOT NULL,
	[Name] [nchar](50) NULL,
	[LastName] [nchar](50) NULL,
	[Patronymic] [nchar](50) NULL,
	[Pasport] [nchar](10) NULL,
	[Female] [int] NULL,
	[BirstDay] [date] NULL,
	[TypeOfUser] [nchar](2) NULL,
PRIMARY KEY CLUSTERED 
(
	[idUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
