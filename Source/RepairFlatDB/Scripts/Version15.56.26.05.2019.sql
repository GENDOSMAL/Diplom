USE [RepairFlatDB]
GO
/****** Object:  Table [dbo].[AdressDescription]    Script Date: 26.05.2019 15:56:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
/****** Object:  Table [dbo].[BrigateContent]    Script Date: 26.05.2019 15:56:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BrigateContent](
	[Id] [uniqueidentifier] NOT NULL,
	[idWorker] [uniqueidentifier] NULL,
	[WorkerRole] [nchar](100) NULL,
	[idColoboration] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BrigateSeparation]    Script Date: 26.05.2019 15:56:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
/****** Object:  Table [dbo].[ClientDetails]    Script Date: 26.05.2019 15:56:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientDetails](
	[IdClient] [uniqueidentifier] NOT NULL,
	[Description] [nchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdClient] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ColoborationOfBrigade]    Script Date: 26.05.2019 15:56:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ColoborationOfBrigade](
	[IdColoboration] [uniqueidentifier] NOT NULL,
	[Name] [nchar](30) NULL,
	[Description] [nchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdColoboration] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ColoborationOfBrigateSoComp]    Script Date: 26.05.2019 15:56:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ColoborationOfBrigateSoComp](
	[Id] [uniqueidentifier] NOT NULL,
	[IdBrigate] [uniqueidentifier] NULL,
	[IdColoboration] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContactUpdate]    Script Date: 26.05.2019 15:56:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactUpdate](
	[idContactUpdate] [uniqueidentifier] NOT NULL,
	[idContact] [uniqueidentifier] NULL,
	[DataOfUpdate] [datetime2](7) NULL,
	[idUser] [uniqueidentifier] NULL,
	[TypeOfUpdate] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[idContactUpdate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeletedSubStr]    Script Date: 26.05.2019 15:56:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
/****** Object:  Table [dbo].[DeleteMessage]    Script Date: 26.05.2019 15:56:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeleteMessage](
	[idDelete] [uniqueidentifier] NOT NULL,
	[idMessage] [uniqueidentifier] NULL,
	[idUser] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[idDelete] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DialogMessage]    Script Date: 26.05.2019 15:56:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DialogMessage](
	[idMessage] [uniqueidentifier] NOT NULL,
	[idDialog] [uniqueidentifier] NULL,
	[idUser] [uniqueidentifier] NULL,
	[Text] [nvarchar](max) NULL,
	[Foto] [varbinary](max) NULL,
	[CreatedTime] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[idMessage] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Dialogs]    Script Date: 26.05.2019 15:56:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
/****** Object:  Table [dbo].[DialogUser]    Script Date: 26.05.2019 15:56:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DialogUser](
	[idUserInDialog] [uniqueidentifier] NOT NULL,
	[idDialog] [uniqueidentifier] NULL,
	[idUser] [uniqueidentifier] NULL,
	[JoinedTime] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[idUserInDialog] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocPayment]    Script Date: 26.05.2019 15:56:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocPayment](
	[idDocPayment] [uniqueidentifier] NOT NULL,
	[DateOfDoc] [datetime2](7) NULL,
	[idWorker] [uniqueidentifier] NULL,
	[idClient] [uniqueidentifier] NULL,
	[Summa] [money] NULL,
	[idInformatioAboutPayment] [uniqueidentifier] NULL,
 CONSTRAINT [PK_DocPayment] PRIMARY KEY CLUSTERED 
(
	[idDocPayment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EstabilismentPost]    Script Date: 26.05.2019 15:56:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstabilismentPost](
	[idEstabilisment] [uniqueidentifier] NOT NULL,
	[idWorker] [uniqueidentifier] NULL,
	[idPost] [uniqueidentifier] NULL,
	[Salary] [money] NULL,
	[DateOfOperate] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[idEstabilisment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InformatioForPayment]    Script Date: 26.05.2019 15:56:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InformatioForPayment](
	[idInfPayment] [uniqueidentifier] NOT NULL,
	[idWorkerMake] [uniqueidentifier] NULL,
	[NameOfRecipient] [nvarchar](100) NULL,
	[InnOfOrganization] [nvarchar](20) NULL,
	[KppOfOrganization] [nvarchar](20) NULL,
	[BankOfPayment] [nvarchar](100) NULL,
	[CheckingAcount] [nvarchar](100) NULL,
	[BIK] [nvarchar](20) NULL,
	[YIN] [nvarchar](20) NULL,
	[DateOfInsert] [datetime2](7) NULL,
 CONSTRAINT [PK_InformatioForPayment] PRIMARY KEY CLUSTERED 
(
	[idInfPayment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoginInformation]    Script Date: 26.05.2019 15:56:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoginInformation](
	[IdLog] [uniqueidentifier] NOT NULL,
	[Login] [nchar](30) NULL,
	[Password] [nchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdLog] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MaterialsUpdate]    Script Date: 26.05.2019 15:56:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaterialsUpdate](
	[idMaterialUpdate] [uniqueidentifier] NOT NULL,
	[idMaterials] [uniqueidentifier] NULL,
	[DateOfUpdate] [datetime2](7) NULL,
	[IdUser] [uniqueidentifier] NULL,
	[TypeOfUpdate] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[idMaterialUpdate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderElementOfMeasurments]    Script Date: 26.05.2019 15:56:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderElementOfMeasurments](
	[idElement] [uniqueidentifier] NOT NULL,
	[idMeasurements] [uniqueidentifier] NULL,
	[TypeOfElement] [nchar](20) NULL,
	[Lenght] [float] NULL,
	[Height] [float] NULL,
	[Width] [float] NULL,
	[POfElement] [float] NULL,
	[WidthOfSlope] [float] NULL,
	[Description] [nchar](50) NULL,
 CONSTRAINT [PK__OrderEle__58DE12035767AAAF] PRIMARY KEY CLUSTERED 
(
	[idElement] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderInformation]    Script Date: 26.05.2019 15:56:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderInformation](
	[IdOrder] [uniqueidentifier] NOT NULL,
	[IdAdress] [uniqueidentifier] NULL,
	[IdWorkerMake] [uniqueidentifier] NULL,
	[idClient] [uniqueidentifier] NULL,
	[DateStart] [datetime2](7) NULL,
	[Status] [int] NULL,
	[AllSumma] [money] NULL,
	[Description] [nchar](50) NULL,
	[IdColoboration] [uniqueidentifier] NULL,
	[Number] [int] NULL,
	[MainContactID] [uniqueidentifier] NULL,
	[DateEnd] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdOrder] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderMaterial]    Script Date: 26.05.2019 15:56:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderMaterial](
	[idMaterialINOrder] [uniqueidentifier] NOT NULL,
	[IdOrder] [uniqueidentifier] NULL,
	[idMaterial] [uniqueidentifier] NULL,
	[Count] [float] NULL,
	[Cost] [money] NULL,
	[Description] [nchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[idMaterialINOrder] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderMeasurements]    Script Date: 26.05.2019 15:56:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderMeasurements](
	[idMeasurements] [uniqueidentifier] NOT NULL,
	[IdOrder] [uniqueidentifier] NULL,
	[idPremisesType] [uniqueidentifier] NULL,
	[Description] [nchar](50) NULL,
	[Lenght] [float] NULL,
	[Width] [float] NULL,
	[Height] [float] NULL,
	[Pwalls] [float] NULL,
	[PCelling] [float] NULL,
	[Swalls] [float] NULL,
	[Sfloor] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[idMeasurements] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderPayment]    Script Date: 26.05.2019 15:56:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderPayment](
	[IdPayment] [uniqueidentifier] NOT NULL,
	[IdOrder] [uniqueidentifier] NULL,
	[IdWorkerMake] [uniqueidentifier] NULL,
	[idDocPayment] [uniqueidentifier] NULL,
	[Description] [nchar](100) NULL,
 CONSTRAINT [PK__OrderPay__613289C0861E1C36] PRIMARY KEY CLUSTERED 
(
	[IdPayment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderServises]    Script Date: 26.05.2019 15:56:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderServises](
	[IdServises] [uniqueidentifier] NOT NULL,
	[IdOrder] [uniqueidentifier] NULL,
	[idServis] [uniqueidentifier] NULL,
	[Count] [float] NULL,
	[Cost] [money] NULL,
	[Description] [nchar](10) NULL,
	[DatePlaneStart] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdServises] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderTasks]    Script Date: 26.05.2019 15:56:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderTasks](
	[IdTask] [uniqueidentifier] NOT NULL,
	[IdOrder] [uniqueidentifier] NULL,
	[Description] [nchar](200) NULL,
	[DateStart] [datetime2](7) NULL,
	[DeadEnd] [datetime2](7) NULL,
	[SummaAboutTask] [money] NULL,
	[idBrigade] [uniqueidentifier] NULL,
 CONSTRAINT [PK__OrderTas__9FCAD1C5A820E811] PRIMARY KEY CLUSTERED 
(
	[IdTask] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderTasksState]    Script Date: 26.05.2019 15:56:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderTasksState](
	[IdState] [uniqueidentifier] NOT NULL,
	[IdTask] [uniqueidentifier] NULL,
	[DateOf] [datetime2](7) NULL,
	[State] [nchar](20) NULL,
	[IdWorker] [uniqueidentifier] NULL,
	[Description] [nchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdState] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OurMaterials]    Script Date: 26.05.2019 15:56:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
/****** Object:  Table [dbo].[OurServices]    Script Date: 26.05.2019 15:56:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OurServices](
	[idServis] [uniqueidentifier] NOT NULL,
	[Nomination] [nchar](50) NULL,
	[TypeOfServices] [nchar](10) NULL,
	[UnitOfMeasue] [nchar](20) NULL,
	[Cost] [money] NULL,
	[Description] [nchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[idServis] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PostsUpdate]    Script Date: 26.05.2019 15:56:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostsUpdate](
	[idUpdatePos] [uniqueidentifier] NOT NULL,
	[idPost] [uniqueidentifier] NULL,
	[DateUpdate] [datetime2](7) NULL,
	[TypeOfUpdate] [nvarchar](20) NULL,
	[IdUpdateUser] [uniqueidentifier] NULL,
 CONSTRAINT [PK_PostsUpdate] PRIMARY KEY CLUSTERED 
(
	[idUpdatePos] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PremisesType]    Script Date: 26.05.2019 15:56:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PremisesType](
	[idPremises] [uniqueidentifier] NOT NULL,
	[NameOfPremises] [nchar](100) NULL,
	[Descriprtion] [nchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[idPremises] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PremisesUpdate]    Script Date: 26.05.2019 15:56:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PremisesUpdate](
	[idPremisesUpdate] [uniqueidentifier] NOT NULL,
	[idPremises] [uniqueidentifier] NULL,
	[DateOfUpdate] [datetime2](7) NULL,
	[IdUser] [uniqueidentifier] NULL,
	[TypeOfUpdate] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[idPremisesUpdate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServicesUpdate]    Script Date: 26.05.2019 15:56:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServicesUpdate](
	[idServUpdate] [uniqueidentifier] NOT NULL,
	[IdServices] [uniqueidentifier] NULL,
	[DateOfUpdate] [datetime2](7) NULL,
	[IdUser] [uniqueidentifier] NULL,
	[TypeOfUpdate] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[idServUpdate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskMaterials]    Script Date: 26.05.2019 15:56:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskMaterials](
	[idOfCompare] [uniqueidentifier] NOT NULL,
	[idTask] [uniqueidentifier] NULL,
	[idMaterials] [uniqueidentifier] NULL,
 CONSTRAINT [PK_TaskMaterials] PRIMARY KEY CLUSTERED 
(
	[idOfCompare] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskMeasurment]    Script Date: 26.05.2019 15:56:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskMeasurment](
	[idOfCompare] [uniqueidentifier] NOT NULL,
	[idMeasurment] [uniqueidentifier] NULL,
	[idOrderTask] [uniqueidentifier] NULL,
 CONSTRAINT [PK_TaskMeasurment] PRIMARY KEY CLUSTERED 
(
	[idOfCompare] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskServis]    Script Date: 26.05.2019 15:56:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskServis](
	[idOfCompare] [uniqueidentifier] NOT NULL,
	[idTask] [uniqueidentifier] NULL,
	[idOrderServis] [uniqueidentifier] NULL,
 CONSTRAINT [PK_TaskServis] PRIMARY KEY CLUSTERED 
(
	[idOfCompare] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeOfContact]    Script Date: 26.05.2019 15:56:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeOfContact](
	[idContact] [uniqueidentifier] NOT NULL,
	[Value] [nchar](100) NULL,
	[Description] [nchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[idContact] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 26.05.2019 15:56:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
/****** Object:  Table [dbo].[UserContact]    Script Date: 26.05.2019 15:56:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserContact](
	[id] [uniqueidentifier] NOT NULL,
	[idUser] [uniqueidentifier] NOT NULL,
	[idType] [uniqueidentifier] NULL,
	[Value] [nchar](30) NULL,
	[Description] [nchar](50) NULL,
	[DateAdd] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkerCanditate]    Script Date: 26.05.2019 15:56:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkerCanditate](
	[idCandidate] [int] NOT NULL,
	[Name] [nchar](50) NULL,
	[LastName] [nchar](50) NULL,
	[Patronymic] [nchar](50) NULL,
	[BirstDay] [date] NULL,
	[Female] [int] NULL,
	[Pasport] [nchar](12) NULL,
	[Telephone] [nchar](20) NULL,
	[Mail] [nchar](30) NULL,
	[IdPost] [uniqueidentifier] NULL,
	[PlanSalary] [money] NULL,
	[IdAdress] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[idCandidate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkerDetails]    Script Date: 26.05.2019 15:56:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkerDetails](
	[IdWorker] [uniqueidentifier] NOT NULL,
	[idAdress] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdWorker] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkerOrderInformation]    Script Date: 26.05.2019 15:56:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkerOrderInformation](
	[Id] [uniqueidentifier] NOT NULL,
	[idWorker] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkerPosts]    Script Date: 26.05.2019 15:56:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkerPosts](
	[idPost] [uniqueidentifier] NOT NULL,
	[NameOfPost] [nchar](100) NULL,
	[BaseWage] [money] NULL,
PRIMARY KEY CLUSTERED 
(
	[idPost] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkersOperats]    Script Date: 26.05.2019 15:56:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkersOperats](
	[idOperate] [uniqueidentifier] NOT NULL,
	[idWorker] [uniqueidentifier] NULL,
	[idEstabilisment] [uniqueidentifier] NULL,
	[TypeOfOperate] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idOperate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkersPayGive]    Script Date: 26.05.2019 15:56:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkersPayGive](
	[idGive] [uniqueidentifier] NOT NULL,
	[idWorkerMake] [uniqueidentifier] NULL,
	[idWorkerAdresat] [uniqueidentifier] NULL,
	[Size] [money] NULL,
	[Data] [datetime2](7) NULL,
	[Descriptiom] [nchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[idGive] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BrigateContent]  WITH CHECK ADD  CONSTRAINT [FK_BrigateContent_BrigateSeparation] FOREIGN KEY([idColoboration])
REFERENCES [dbo].[BrigateSeparation] ([IdBrigate])
GO
ALTER TABLE [dbo].[BrigateContent] CHECK CONSTRAINT [FK_BrigateContent_BrigateSeparation]
GO
ALTER TABLE [dbo].[BrigateContent]  WITH CHECK ADD  CONSTRAINT [FK_BrigateContent_WorkerDetails] FOREIGN KEY([idWorker])
REFERENCES [dbo].[WorkerDetails] ([IdWorker])
GO
ALTER TABLE [dbo].[BrigateContent] CHECK CONSTRAINT [FK_BrigateContent_WorkerDetails]
GO
ALTER TABLE [dbo].[ClientDetails]  WITH CHECK ADD  CONSTRAINT [FK_ClientDetails_User] FOREIGN KEY([IdClient])
REFERENCES [dbo].[User] ([idUser])
GO
ALTER TABLE [dbo].[ClientDetails] CHECK CONSTRAINT [FK_ClientDetails_User]
GO
ALTER TABLE [dbo].[ColoborationOfBrigateSoComp]  WITH CHECK ADD  CONSTRAINT [FK_ColoborationOfBrigateSoComp_BrigateSeparation] FOREIGN KEY([IdBrigate])
REFERENCES [dbo].[BrigateSeparation] ([IdBrigate])
GO
ALTER TABLE [dbo].[ColoborationOfBrigateSoComp] CHECK CONSTRAINT [FK_ColoborationOfBrigateSoComp_BrigateSeparation]
GO
ALTER TABLE [dbo].[ColoborationOfBrigateSoComp]  WITH CHECK ADD  CONSTRAINT [FK_ColoborationOfBrigateSoComp_ColoborationOfBrigade] FOREIGN KEY([IdColoboration])
REFERENCES [dbo].[ColoborationOfBrigade] ([IdColoboration])
GO
ALTER TABLE [dbo].[ColoborationOfBrigateSoComp] CHECK CONSTRAINT [FK_ColoborationOfBrigateSoComp_ColoborationOfBrigade]
GO
ALTER TABLE [dbo].[ContactUpdate]  WITH CHECK ADD  CONSTRAINT [FK_ContactUpdate_TypeOfContact] FOREIGN KEY([idContact])
REFERENCES [dbo].[TypeOfContact] ([idContact])
GO
ALTER TABLE [dbo].[ContactUpdate] CHECK CONSTRAINT [FK_ContactUpdate_TypeOfContact]
GO
ALTER TABLE [dbo].[ContactUpdate]  WITH CHECK ADD  CONSTRAINT [FK_ContactUpdate_User] FOREIGN KEY([idUser])
REFERENCES [dbo].[User] ([idUser])
GO
ALTER TABLE [dbo].[ContactUpdate] CHECK CONSTRAINT [FK_ContactUpdate_User]
GO
ALTER TABLE [dbo].[DeletedSubStr]  WITH CHECK ADD  CONSTRAINT [FK_DeletedSubStr_User] FOREIGN KEY([idUserDelete])
REFERENCES [dbo].[User] ([idUser])
GO
ALTER TABLE [dbo].[DeletedSubStr] CHECK CONSTRAINT [FK_DeletedSubStr_User]
GO
ALTER TABLE [dbo].[DeleteMessage]  WITH CHECK ADD  CONSTRAINT [FK_DeleteMessage_DialogMessage] FOREIGN KEY([idMessage])
REFERENCES [dbo].[DialogMessage] ([idMessage])
GO
ALTER TABLE [dbo].[DeleteMessage] CHECK CONSTRAINT [FK_DeleteMessage_DialogMessage]
GO
ALTER TABLE [dbo].[DeleteMessage]  WITH CHECK ADD  CONSTRAINT [FK_DeleteMessage_User] FOREIGN KEY([idUser])
REFERENCES [dbo].[User] ([idUser])
GO
ALTER TABLE [dbo].[DeleteMessage] CHECK CONSTRAINT [FK_DeleteMessage_User]
GO
ALTER TABLE [dbo].[DialogMessage]  WITH CHECK ADD  CONSTRAINT [FK_DialogMessage_Dialogs] FOREIGN KEY([idDialog])
REFERENCES [dbo].[Dialogs] ([idDialog])
GO
ALTER TABLE [dbo].[DialogMessage] CHECK CONSTRAINT [FK_DialogMessage_Dialogs]
GO
ALTER TABLE [dbo].[DialogMessage]  WITH CHECK ADD  CONSTRAINT [FK_DialogMessage_User] FOREIGN KEY([idUser])
REFERENCES [dbo].[User] ([idUser])
GO
ALTER TABLE [dbo].[DialogMessage] CHECK CONSTRAINT [FK_DialogMessage_User]
GO
ALTER TABLE [dbo].[DialogUser]  WITH CHECK ADD  CONSTRAINT [FK_DialogUser_Dialogs] FOREIGN KEY([idDialog])
REFERENCES [dbo].[Dialogs] ([idDialog])
GO
ALTER TABLE [dbo].[DialogUser] CHECK CONSTRAINT [FK_DialogUser_Dialogs]
GO
ALTER TABLE [dbo].[DialogUser]  WITH CHECK ADD  CONSTRAINT [FK_DialogUser_User] FOREIGN KEY([idUser])
REFERENCES [dbo].[User] ([idUser])
GO
ALTER TABLE [dbo].[DialogUser] CHECK CONSTRAINT [FK_DialogUser_User]
GO
ALTER TABLE [dbo].[DocPayment]  WITH CHECK ADD  CONSTRAINT [FK_DocPayment_ClientDetails] FOREIGN KEY([idClient])
REFERENCES [dbo].[ClientDetails] ([IdClient])
GO
ALTER TABLE [dbo].[DocPayment] CHECK CONSTRAINT [FK_DocPayment_ClientDetails]
GO
ALTER TABLE [dbo].[DocPayment]  WITH CHECK ADD  CONSTRAINT [FK_DocPayment_InformatioForPayment] FOREIGN KEY([idInformatioAboutPayment])
REFERENCES [dbo].[InformatioForPayment] ([idInfPayment])
GO
ALTER TABLE [dbo].[DocPayment] CHECK CONSTRAINT [FK_DocPayment_InformatioForPayment]
GO
ALTER TABLE [dbo].[DocPayment]  WITH CHECK ADD  CONSTRAINT [FK_DocPayment_WorkerDetails] FOREIGN KEY([idWorker])
REFERENCES [dbo].[WorkerDetails] ([IdWorker])
GO
ALTER TABLE [dbo].[DocPayment] CHECK CONSTRAINT [FK_DocPayment_WorkerDetails]
GO
ALTER TABLE [dbo].[EstabilismentPost]  WITH CHECK ADD  CONSTRAINT [FK_EstabilismentPost_WorkerDetails] FOREIGN KEY([idWorker])
REFERENCES [dbo].[WorkerDetails] ([IdWorker])
GO
ALTER TABLE [dbo].[EstabilismentPost] CHECK CONSTRAINT [FK_EstabilismentPost_WorkerDetails]
GO
ALTER TABLE [dbo].[EstabilismentPost]  WITH CHECK ADD  CONSTRAINT [FK_EstabilismentPost_WorkerPosts] FOREIGN KEY([idPost])
REFERENCES [dbo].[WorkerPosts] ([idPost])
GO
ALTER TABLE [dbo].[EstabilismentPost] CHECK CONSTRAINT [FK_EstabilismentPost_WorkerPosts]
GO
ALTER TABLE [dbo].[InformatioForPayment]  WITH CHECK ADD  CONSTRAINT [FK_InformatioForPayment_WorkerDetails] FOREIGN KEY([idWorkerMake])
REFERENCES [dbo].[WorkerDetails] ([IdWorker])
GO
ALTER TABLE [dbo].[InformatioForPayment] CHECK CONSTRAINT [FK_InformatioForPayment_WorkerDetails]
GO
ALTER TABLE [dbo].[LoginInformation]  WITH CHECK ADD  CONSTRAINT [FK_LoginInformation_User] FOREIGN KEY([IdLog])
REFERENCES [dbo].[User] ([idUser])
GO
ALTER TABLE [dbo].[LoginInformation] CHECK CONSTRAINT [FK_LoginInformation_User]
GO
ALTER TABLE [dbo].[MaterialsUpdate]  WITH CHECK ADD  CONSTRAINT [FK_MaterialsUpdate_OurMaterials] FOREIGN KEY([idMaterials])
REFERENCES [dbo].[OurMaterials] ([idMaterials])
GO
ALTER TABLE [dbo].[MaterialsUpdate] CHECK CONSTRAINT [FK_MaterialsUpdate_OurMaterials]
GO
ALTER TABLE [dbo].[MaterialsUpdate]  WITH CHECK ADD  CONSTRAINT [FK_MaterialsUpdate_User] FOREIGN KEY([IdUser])
REFERENCES [dbo].[User] ([idUser])
GO
ALTER TABLE [dbo].[MaterialsUpdate] CHECK CONSTRAINT [FK_MaterialsUpdate_User]
GO
ALTER TABLE [dbo].[OrderElementOfMeasurments]  WITH CHECK ADD  CONSTRAINT [FK_OrderElementOfMeasurments_OrderMeasurements] FOREIGN KEY([idMeasurements])
REFERENCES [dbo].[OrderMeasurements] ([idMeasurements])
GO
ALTER TABLE [dbo].[OrderElementOfMeasurments] CHECK CONSTRAINT [FK_OrderElementOfMeasurments_OrderMeasurements]
GO
ALTER TABLE [dbo].[OrderInformation]  WITH CHECK ADD  CONSTRAINT [FK_OrderInformation_AdressDescription] FOREIGN KEY([IdAdress])
REFERENCES [dbo].[AdressDescription] ([idAdress])
GO
ALTER TABLE [dbo].[OrderInformation] CHECK CONSTRAINT [FK_OrderInformation_AdressDescription]
GO
ALTER TABLE [dbo].[OrderInformation]  WITH CHECK ADD  CONSTRAINT [FK_OrderInformation_ClientDetails] FOREIGN KEY([idClient])
REFERENCES [dbo].[ClientDetails] ([IdClient])
GO
ALTER TABLE [dbo].[OrderInformation] CHECK CONSTRAINT [FK_OrderInformation_ClientDetails]
GO
ALTER TABLE [dbo].[OrderInformation]  WITH CHECK ADD  CONSTRAINT [FK_OrderInformation_ColoborationOfBrigade] FOREIGN KEY([IdColoboration])
REFERENCES [dbo].[ColoborationOfBrigade] ([IdColoboration])
GO
ALTER TABLE [dbo].[OrderInformation] CHECK CONSTRAINT [FK_OrderInformation_ColoborationOfBrigade]
GO
ALTER TABLE [dbo].[OrderInformation]  WITH CHECK ADD  CONSTRAINT [FK_OrderInformation_UserContact] FOREIGN KEY([MainContactID])
REFERENCES [dbo].[UserContact] ([id])
GO
ALTER TABLE [dbo].[OrderInformation] CHECK CONSTRAINT [FK_OrderInformation_UserContact]
GO
ALTER TABLE [dbo].[OrderInformation]  WITH CHECK ADD  CONSTRAINT [FK_OrderInformation_WorkerDetails] FOREIGN KEY([IdWorkerMake])
REFERENCES [dbo].[WorkerDetails] ([IdWorker])
GO
ALTER TABLE [dbo].[OrderInformation] CHECK CONSTRAINT [FK_OrderInformation_WorkerDetails]
GO
ALTER TABLE [dbo].[OrderInformation]  WITH CHECK ADD  CONSTRAINT [FK_OrderInformation_WorkerOrderInformation] FOREIGN KEY([IdWorkerMake])
REFERENCES [dbo].[WorkerOrderInformation] ([Id])
GO
ALTER TABLE [dbo].[OrderInformation] CHECK CONSTRAINT [FK_OrderInformation_WorkerOrderInformation]
GO
ALTER TABLE [dbo].[OrderMaterial]  WITH CHECK ADD  CONSTRAINT [FK_OrderMaterial_OrderInformation] FOREIGN KEY([IdOrder])
REFERENCES [dbo].[OrderInformation] ([IdOrder])
GO
ALTER TABLE [dbo].[OrderMaterial] CHECK CONSTRAINT [FK_OrderMaterial_OrderInformation]
GO
ALTER TABLE [dbo].[OrderMaterial]  WITH CHECK ADD  CONSTRAINT [FK_OrderMaterial_OurMaterials] FOREIGN KEY([idMaterial])
REFERENCES [dbo].[OurMaterials] ([idMaterials])
GO
ALTER TABLE [dbo].[OrderMaterial] CHECK CONSTRAINT [FK_OrderMaterial_OurMaterials]
GO
ALTER TABLE [dbo].[OrderMeasurements]  WITH CHECK ADD  CONSTRAINT [FK_OrderMeasurements_OrderInformation] FOREIGN KEY([IdOrder])
REFERENCES [dbo].[OrderInformation] ([IdOrder])
GO
ALTER TABLE [dbo].[OrderMeasurements] CHECK CONSTRAINT [FK_OrderMeasurements_OrderInformation]
GO
ALTER TABLE [dbo].[OrderMeasurements]  WITH CHECK ADD  CONSTRAINT [FK_OrderMeasurements_PremisesType] FOREIGN KEY([idPremisesType])
REFERENCES [dbo].[PremisesType] ([idPremises])
GO
ALTER TABLE [dbo].[OrderMeasurements] CHECK CONSTRAINT [FK_OrderMeasurements_PremisesType]
GO
ALTER TABLE [dbo].[OrderPayment]  WITH CHECK ADD  CONSTRAINT [FK_OrderPayment_DocPayment] FOREIGN KEY([idDocPayment])
REFERENCES [dbo].[DocPayment] ([idDocPayment])
GO
ALTER TABLE [dbo].[OrderPayment] CHECK CONSTRAINT [FK_OrderPayment_DocPayment]
GO
ALTER TABLE [dbo].[OrderPayment]  WITH CHECK ADD  CONSTRAINT [FK_OrderPayment_OrderInformation] FOREIGN KEY([IdOrder])
REFERENCES [dbo].[OrderInformation] ([IdOrder])
GO
ALTER TABLE [dbo].[OrderPayment] CHECK CONSTRAINT [FK_OrderPayment_OrderInformation]
GO
ALTER TABLE [dbo].[OrderPayment]  WITH CHECK ADD  CONSTRAINT [FK_OrderPayment_WorkerDetails] FOREIGN KEY([IdWorkerMake])
REFERENCES [dbo].[WorkerDetails] ([IdWorker])
GO
ALTER TABLE [dbo].[OrderPayment] CHECK CONSTRAINT [FK_OrderPayment_WorkerDetails]
GO
ALTER TABLE [dbo].[OrderServises]  WITH CHECK ADD  CONSTRAINT [FK_OrderServises_OrderInformation] FOREIGN KEY([IdOrder])
REFERENCES [dbo].[OrderInformation] ([IdOrder])
GO
ALTER TABLE [dbo].[OrderServises] CHECK CONSTRAINT [FK_OrderServises_OrderInformation]
GO
ALTER TABLE [dbo].[OrderServises]  WITH CHECK ADD  CONSTRAINT [FK_OrderServises_OurServices] FOREIGN KEY([idServis])
REFERENCES [dbo].[OurServices] ([idServis])
GO
ALTER TABLE [dbo].[OrderServises] CHECK CONSTRAINT [FK_OrderServises_OurServices]
GO
ALTER TABLE [dbo].[OrderTasks]  WITH CHECK ADD  CONSTRAINT [FK_OrderTasks_BrigateSeparation] FOREIGN KEY([idBrigade])
REFERENCES [dbo].[BrigateSeparation] ([IdBrigate])
GO
ALTER TABLE [dbo].[OrderTasks] CHECK CONSTRAINT [FK_OrderTasks_BrigateSeparation]
GO
ALTER TABLE [dbo].[OrderTasks]  WITH CHECK ADD  CONSTRAINT [FK_OrderTasks_OrderInformation1] FOREIGN KEY([IdOrder])
REFERENCES [dbo].[OrderInformation] ([IdOrder])
GO
ALTER TABLE [dbo].[OrderTasks] CHECK CONSTRAINT [FK_OrderTasks_OrderInformation1]
GO
ALTER TABLE [dbo].[OrderTasksState]  WITH CHECK ADD  CONSTRAINT [FK_OrderTasksState_OrderTasks] FOREIGN KEY([IdTask])
REFERENCES [dbo].[OrderTasks] ([IdTask])
GO
ALTER TABLE [dbo].[OrderTasksState] CHECK CONSTRAINT [FK_OrderTasksState_OrderTasks]
GO
ALTER TABLE [dbo].[OrderTasksState]  WITH CHECK ADD  CONSTRAINT [FK_OrderTasksState_WorkerDetails] FOREIGN KEY([IdWorker])
REFERENCES [dbo].[WorkerDetails] ([IdWorker])
GO
ALTER TABLE [dbo].[OrderTasksState] CHECK CONSTRAINT [FK_OrderTasksState_WorkerDetails]
GO
ALTER TABLE [dbo].[PostsUpdate]  WITH CHECK ADD  CONSTRAINT [FK_PostsUpdate_User] FOREIGN KEY([IdUpdateUser])
REFERENCES [dbo].[User] ([idUser])
GO
ALTER TABLE [dbo].[PostsUpdate] CHECK CONSTRAINT [FK_PostsUpdate_User]
GO
ALTER TABLE [dbo].[PostsUpdate]  WITH CHECK ADD  CONSTRAINT [FK_PostsUpdate_WorkerPosts] FOREIGN KEY([idPost])
REFERENCES [dbo].[WorkerPosts] ([idPost])
GO
ALTER TABLE [dbo].[PostsUpdate] CHECK CONSTRAINT [FK_PostsUpdate_WorkerPosts]
GO
ALTER TABLE [dbo].[PremisesUpdate]  WITH CHECK ADD  CONSTRAINT [FK_PremisesUpdate_PremisesType] FOREIGN KEY([idPremises])
REFERENCES [dbo].[PremisesType] ([idPremises])
GO
ALTER TABLE [dbo].[PremisesUpdate] CHECK CONSTRAINT [FK_PremisesUpdate_PremisesType]
GO
ALTER TABLE [dbo].[PremisesUpdate]  WITH CHECK ADD  CONSTRAINT [FK_PremisesUpdate_User] FOREIGN KEY([IdUser])
REFERENCES [dbo].[User] ([idUser])
GO
ALTER TABLE [dbo].[PremisesUpdate] CHECK CONSTRAINT [FK_PremisesUpdate_User]
GO
ALTER TABLE [dbo].[ServicesUpdate]  WITH CHECK ADD  CONSTRAINT [FK_ServicesUpdate_OurServices] FOREIGN KEY([IdServices])
REFERENCES [dbo].[OurServices] ([idServis])
GO
ALTER TABLE [dbo].[ServicesUpdate] CHECK CONSTRAINT [FK_ServicesUpdate_OurServices]
GO
ALTER TABLE [dbo].[ServicesUpdate]  WITH CHECK ADD  CONSTRAINT [FK_ServicesUpdate_User] FOREIGN KEY([IdUser])
REFERENCES [dbo].[User] ([idUser])
GO
ALTER TABLE [dbo].[ServicesUpdate] CHECK CONSTRAINT [FK_ServicesUpdate_User]
GO
ALTER TABLE [dbo].[TaskMaterials]  WITH CHECK ADD  CONSTRAINT [FK_TaskMaterials_OrderMaterial] FOREIGN KEY([idMaterials])
REFERENCES [dbo].[OrderMaterial] ([idMaterialINOrder])
GO
ALTER TABLE [dbo].[TaskMaterials] CHECK CONSTRAINT [FK_TaskMaterials_OrderMaterial]
GO
ALTER TABLE [dbo].[TaskMaterials]  WITH CHECK ADD  CONSTRAINT [FK_TaskMaterials_OrderTasks] FOREIGN KEY([idTask])
REFERENCES [dbo].[OrderTasks] ([IdTask])
GO
ALTER TABLE [dbo].[TaskMaterials] CHECK CONSTRAINT [FK_TaskMaterials_OrderTasks]
GO
ALTER TABLE [dbo].[TaskMeasurment]  WITH CHECK ADD  CONSTRAINT [FK_TaskMeasurment_OrderMeasurements] FOREIGN KEY([idMeasurment])
REFERENCES [dbo].[OrderMeasurements] ([idMeasurements])
GO
ALTER TABLE [dbo].[TaskMeasurment] CHECK CONSTRAINT [FK_TaskMeasurment_OrderMeasurements]
GO
ALTER TABLE [dbo].[TaskMeasurment]  WITH CHECK ADD  CONSTRAINT [FK_TaskMeasurment_OrderTasks] FOREIGN KEY([idOrderTask])
REFERENCES [dbo].[OrderTasks] ([IdTask])
GO
ALTER TABLE [dbo].[TaskMeasurment] CHECK CONSTRAINT [FK_TaskMeasurment_OrderTasks]
GO
ALTER TABLE [dbo].[TaskServis]  WITH CHECK ADD  CONSTRAINT [FK_TaskServis_OrderServises] FOREIGN KEY([idOrderServis])
REFERENCES [dbo].[OrderServises] ([IdServises])
GO
ALTER TABLE [dbo].[TaskServis] CHECK CONSTRAINT [FK_TaskServis_OrderServises]
GO
ALTER TABLE [dbo].[TaskServis]  WITH CHECK ADD  CONSTRAINT [FK_TaskServis_OrderTasks] FOREIGN KEY([idTask])
REFERENCES [dbo].[OrderTasks] ([IdTask])
GO
ALTER TABLE [dbo].[TaskServis] CHECK CONSTRAINT [FK_TaskServis_OrderTasks]
GO
ALTER TABLE [dbo].[UserContact]  WITH CHECK ADD  CONSTRAINT [FK_UserContact_TypeOfContact] FOREIGN KEY([idType])
REFERENCES [dbo].[TypeOfContact] ([idContact])
GO
ALTER TABLE [dbo].[UserContact] CHECK CONSTRAINT [FK_UserContact_TypeOfContact]
GO
ALTER TABLE [dbo].[UserContact]  WITH CHECK ADD  CONSTRAINT [FK_UserContact_User] FOREIGN KEY([idUser])
REFERENCES [dbo].[User] ([idUser])
GO
ALTER TABLE [dbo].[UserContact] CHECK CONSTRAINT [FK_UserContact_User]
GO
ALTER TABLE [dbo].[WorkerCanditate]  WITH CHECK ADD  CONSTRAINT [FK_WorkerCanditate_AdressDescription] FOREIGN KEY([IdAdress])
REFERENCES [dbo].[AdressDescription] ([idAdress])
GO
ALTER TABLE [dbo].[WorkerCanditate] CHECK CONSTRAINT [FK_WorkerCanditate_AdressDescription]
GO
ALTER TABLE [dbo].[WorkerCanditate]  WITH CHECK ADD  CONSTRAINT [FK_WorkerCanditate_WorkerPosts] FOREIGN KEY([IdPost])
REFERENCES [dbo].[WorkerPosts] ([idPost])
GO
ALTER TABLE [dbo].[WorkerCanditate] CHECK CONSTRAINT [FK_WorkerCanditate_WorkerPosts]
GO
ALTER TABLE [dbo].[WorkerDetails]  WITH CHECK ADD  CONSTRAINT [FK_WorkerDetails_AdressDescription] FOREIGN KEY([idAdress])
REFERENCES [dbo].[AdressDescription] ([idAdress])
GO
ALTER TABLE [dbo].[WorkerDetails] CHECK CONSTRAINT [FK_WorkerDetails_AdressDescription]
GO
ALTER TABLE [dbo].[WorkerDetails]  WITH CHECK ADD  CONSTRAINT [FK_WorkerDetails_User] FOREIGN KEY([IdWorker])
REFERENCES [dbo].[User] ([idUser])
GO
ALTER TABLE [dbo].[WorkerDetails] CHECK CONSTRAINT [FK_WorkerDetails_User]
GO
ALTER TABLE [dbo].[WorkerOrderInformation]  WITH CHECK ADD  CONSTRAINT [FK_WorkerOrderInformation_WorkerDetails] FOREIGN KEY([idWorker])
REFERENCES [dbo].[WorkerDetails] ([IdWorker])
GO
ALTER TABLE [dbo].[WorkerOrderInformation] CHECK CONSTRAINT [FK_WorkerOrderInformation_WorkerDetails]
GO
ALTER TABLE [dbo].[WorkersOperats]  WITH CHECK ADD  CONSTRAINT [FK_WorkersOperats_EstabilismentPost] FOREIGN KEY([idEstabilisment])
REFERENCES [dbo].[EstabilismentPost] ([idEstabilisment])
GO
ALTER TABLE [dbo].[WorkersOperats] CHECK CONSTRAINT [FK_WorkersOperats_EstabilismentPost]
GO
ALTER TABLE [dbo].[WorkersOperats]  WITH CHECK ADD  CONSTRAINT [FK_WorkersOperats_WorkerDetails] FOREIGN KEY([idWorker])
REFERENCES [dbo].[WorkerDetails] ([IdWorker])
GO
ALTER TABLE [dbo].[WorkersOperats] CHECK CONSTRAINT [FK_WorkersOperats_WorkerDetails]
GO
ALTER TABLE [dbo].[WorkersPayGive]  WITH CHECK ADD  CONSTRAINT [FK_WorkersPayGive_WorkerDetails] FOREIGN KEY([idWorkerAdresat])
REFERENCES [dbo].[WorkerDetails] ([IdWorker])
GO
ALTER TABLE [dbo].[WorkersPayGive] CHECK CONSTRAINT [FK_WorkersPayGive_WorkerDetails]
GO
ALTER TABLE [dbo].[WorkersPayGive]  WITH CHECK ADD  CONSTRAINT [FK_WorkersPayGive_WorkerDetails1] FOREIGN KEY([idWorkerMake])
REFERENCES [dbo].[WorkerDetails] ([IdWorker])
GO
ALTER TABLE [dbo].[WorkersPayGive] CHECK CONSTRAINT [FK_WorkersPayGive_WorkerDetails1]
GO
