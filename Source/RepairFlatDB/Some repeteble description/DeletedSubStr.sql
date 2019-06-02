CREATE TABLE [dbo].[DeletedSubStr](
	[idDeleted] [uniqueidentifier] NOT NULL,
	[idThingsDelete] [uniqueidentifier] NULL,
	[TypeOfDeleted] [nvarchar](15) NULL,
	[DateOfDelete] [datetime2](7) NULL,
	[idUserDelete] [uniqueidentifier] NULL,
 CONSTRAINT [PK_DeletedSubStr] PRIMARY KEY CLUSTERED 
(
	[idDeleted] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DeletedSubStr]  WITH CHECK ADD  CONSTRAINT [FK_DeletedSubStr_User] FOREIGN KEY([idUserDelete])
REFERENCES [dbo].[User] ([idUser])
GO

ALTER TABLE [dbo].[DeletedSubStr] CHECK CONSTRAINT [FK_DeletedSubStr_User]
GO


GO

