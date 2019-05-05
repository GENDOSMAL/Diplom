CREATE TABLE [dbo].[OurServices]
(
	[idServis] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Nomination] NCHAR(50) NULL, 
    [TypeOfServices] NCHAR(10) NULL, 
    [UnitOfMeasue] NCHAR(20) NULL, 
    [Cost] MONEY NULL, 
    [Description] NCHAR(100) NULL
)
