﻿CREATE TABLE [dbo].[WorkerPosts](
	[idPost] [uniqueidentifier] NOT NULL,
	[NameOfPost] [nchar](100) NULL,
	[BaseWage] [money] NULL,
	[MakeWork] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[idPost] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
