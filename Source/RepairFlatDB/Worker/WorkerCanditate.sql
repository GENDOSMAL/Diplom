CREATE TABLE [dbo].[WorkerCanditate]
(
	[idCandidate] INT NOT NULL PRIMARY KEY, 
    [Name] NCHAR(50) NULL, 
    [LastName] NCHAR(50) NULL, 
    [Patronymic] NCHAR(50) NULL, 
    [BirstDay] DATE NULL, 
    [Female] INT NULL, 
    [Pasport] NCHAR(12) NULL, 
    [Telephone] NCHAR(20) NULL, 
    [Mail] NCHAR(30) NULL, 
    [IdPost] UNIQUEIDENTIFIER NULL, 
    [PlanSalary] MONEY NULL, 
    [IdAdress] UNIQUEIDENTIFIER NULL
)
