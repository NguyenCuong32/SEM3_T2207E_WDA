USE [JIRA]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 9/25/2023 9:08:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PHAN_QUYEN_PROJECT]    Script Date: 9/25/2023 9:08:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PHAN_QUYEN_PROJECT](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](30) NOT NULL,
	[projectId] [int] NOT NULL,
	[createDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PROJECT]    Script Date: 9/25/2023 9:08:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PROJECT](
	[ProjectID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectKey] [varchar](10) NOT NULL,
	[projectName] [nvarchar](128) NOT NULL,
	[projectLead] [varchar](128) NOT NULL,
	[userCrate] [varchar](128) NOT NULL,
 CONSTRAINT [PK_PROJECT] PRIMARY KEY CLUSTERED 
(
	[ProjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PROJECT_JOB]    Script Date: 9/25/2023 9:08:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PROJECT_JOB](
	[TaskId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectKey] [varchar](128) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[TitleTask] [nvarchar](512) NOT NULL,
	[DescriptionTask] [ntext] NULL,
	[TypeTask] [nvarchar](64) NULL,
	[DeadLineTask] [date] NULL,
	[PriorityTask] [nvarchar](30) NOT NULL,
	[LevelTask] [nvarchar](30) NOT NULL,
	[UserCreate] [varchar](30) NOT NULL,
	[UserImplement] [varchar](30) NOT NULL,
	[TaskCreateDate] [datetime] NOT NULL,
	[TaskUpdateDate] [datetime] NULL,
	[StatusTask] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK__TASK__7C6949B18C0D22F4] PRIMARY KEY CLUSTERED 
(
	[TaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[USERS]    Script Date: 9/25/2023 9:08:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[USERS](
	[username] [varchar](128) NOT NULL,
	[password] [nvarchar](512) NOT NULL,
	[email] [nvarchar](128) NOT NULL,
	[active] [bit] NOT NULL,
	[ngayTao] [datetime] NOT NULL,
	[ngayCapNhat] [datetime] NOT NULL,
	[fullName] [nvarchar](128) NOT NULL,
	[avatar] [nvarchar](256) NULL,
 CONSTRAINT [PK__USERS__F3DBC573FE346B55] PRIMARY KEY CLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[PHAN_QUYEN_PROJECT] ADD  DEFAULT (getdate()) FOR [createDate]
GO
ALTER TABLE [dbo].[PROJECT_JOB] ADD  CONSTRAINT [DF__TASK__TaskCreate__2D27B809]  DEFAULT (getdate()) FOR [TaskCreateDate]
GO
ALTER TABLE [dbo].[PROJECT_JOB] ADD  CONSTRAINT [DF_TASK_StatusTask]  DEFAULT (N'To do') FOR [StatusTask]
GO
ALTER TABLE [dbo].[USERS] ADD  CONSTRAINT [DF__USERS__active__145C0A3F]  DEFAULT ((1)) FOR [active]
GO
ALTER TABLE [dbo].[USERS] ADD  CONSTRAINT [DF__USERS__ngayTao__15502E78]  DEFAULT (getdate()) FOR [ngayTao]
GO
ALTER TABLE [dbo].[USERS] ADD  CONSTRAINT [DF__USERS__ngayCapNh__164452B1]  DEFAULT (getdate()) FOR [ngayCapNhat]
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetListProjectForLayout]    Script Date: 9/25/2023 9:08:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Proc [dbo].[Proc_GetListProjectForLayout]
@username varchar(30)
as
BEGIN
	SELECT * FROm PROJECT
END
GO
/****** Object:  StoredProcedure [dbo].[Proc_ListProjectJob]    Script Date: 9/25/2023 9:08:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROC [dbo].[Proc_ListProjectJob]
@username nvarchar(128)
as
BEGIN
	SELECT [TaskId]
      ,[ProjectKey]
      ,[ProjectId]
      ,[TitleTask]
      ,[DescriptionTask]
      ,[TypeTask]
      ,[DeadLineTask]
      ,[PriorityTask]
      ,[LevelTask]
      ,[UserCreate]
      ,[UserImplement]
	  ,UserImplement.fullName AS UserImplementFullName
      ,[TaskCreateDate]
      ,[TaskUpdateDate]
      ,[StatusTask] FROM PROJECT_JOB 
	  INNER JOIN USERS UserImplement ON UserImplement.username = PROJECT_JOB.[UserImplement]
	  where (PROJECT_JOB.UserCreate = @username OR PROJECT_JOB.UserImplement = @username)
END
GO
