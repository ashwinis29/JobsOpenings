/****** Object:  Database [Jobs]    Script Date: 26-11-2023 00:11:29 ******/
CREATE DATABASE [Jobs]
GO

/****** Object:  Table [dbo].[Department]    Script Date: 26-11-2023 00:12:40 ******/
CREATE TABLE [dbo].[Department](
	[Id] [int] NOT NULL,
	[title] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Location]    Script Date: 26-11-2023 00:17:16 ******/
CREATE TABLE [dbo].[Location](
	[Id] [int] NOT NULL,
	[title] [varchar](50) NOT NULL,
	[city] [varchar](50) NOT NULL,
	[state] [varchar](50) NOT NULL,
	[country] [varchar](50) NOT NULL,
	[zip] [int] NOT NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[tblJobs]    Script Date: 26-11-2023 00:19:36 ******/
CREATE TABLE [dbo].[tblJobs](
	[Id] [int] NOT NULL,
	[code] [varchar](50) NULL,
	[title] [varchar](50) NOT NULL,
	[description] [varchar](250) NOT NULL,
	[locationId] [int] NULL,
	[departmentId] [int] NULL,
	[postedDate] [datetime] NOT NULL,
	[closingDate] [datetime] NOT NULL,
 CONSTRAINT [PK_tblJobs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

