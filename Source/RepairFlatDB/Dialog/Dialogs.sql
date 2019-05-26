CREATE TABLE [dbo].[Dialogs](
	[idDialog] [uniqueidentifier] NOT NULL,
	[NameOfDialog] [nchar](100) NULL,
	[FotoOfdialog] [varbinary](max) NULL,
	[CreatedTime] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[idDialog] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
