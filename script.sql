USE [master]
GO
/****** Object:  Database [BCMS]    Script Date: 8/1/2023 10:21:23 PM ******/
CREATE DATABASE [BCMS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BCMS', FILENAME = N'M:\system\sql_server\MSSQL16.SQLEXPRESS\MSSQL\DATA\BCMS.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BCMS_log', FILENAME = N'M:\system\sql_server\MSSQL16.SQLEXPRESS\MSSQL\DATA\BCMS_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [BCMS] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BCMS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BCMS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BCMS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BCMS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BCMS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BCMS] SET ARITHABORT OFF 
GO
ALTER DATABASE [BCMS] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [BCMS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BCMS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BCMS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BCMS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BCMS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BCMS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BCMS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BCMS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BCMS] SET  ENABLE_BROKER 
GO
ALTER DATABASE [BCMS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BCMS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BCMS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BCMS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BCMS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BCMS] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [BCMS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BCMS] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BCMS] SET  MULTI_USER 
GO
ALTER DATABASE [BCMS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BCMS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BCMS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BCMS] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BCMS] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BCMS] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [BCMS] SET QUERY_STORE = ON
GO
ALTER DATABASE [BCMS] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [BCMS]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 8/1/2023 10:21:23 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Birds]    Script Date: 8/1/2023 10:21:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Birds](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Species] [nvarchar](max) NOT NULL,
	[ProfilePicture] [varbinary](max) NULL,
 CONSTRAINT [PK_Birds] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlogCategories]    Script Date: 8/1/2023 10:21:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlogCategories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_BlogCategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Blogs]    Script Date: 8/1/2023 10:21:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[BlogCategoryId] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Contents] [nvarchar](max) NOT NULL,
	[Status] [nvarchar](3) NOT NULL,
	[Thumbnail] [varbinary](max) NULL,
 CONSTRAINT [PK_Blogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 8/1/2023 10:21:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[BlogId] [int] NOT NULL,
	[Contents] [nvarchar](1000) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedDate] [datetime2](7) NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feedbacks]    Script Date: 8/1/2023 10:21:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedbacks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Contents] [nvarchar](1000) NOT NULL,
 CONSTRAINT [PK_Feedbacks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FieldTripRegistrations]    Script Date: 8/1/2023 10:21:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FieldTripRegistrations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[FieldTripId] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[PaymentReceived] [bit] NOT NULL,
 CONSTRAINT [PK_FieldTripRegistrations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FieldTrips]    Script Date: 8/1/2023 10:21:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FieldTrips](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[RegOpenDate] [datetime2](7) NULL,
	[RegCloseDate] [datetime2](7) NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[ExpectedEndDate] [datetime2](7) NULL,
	[Address] [nvarchar](255) NOT NULL,
	[RegLimit] [int] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Fee] [int] NOT NULL,
	[Status] [nvarchar](3) NOT NULL,
	[Highlights] [nvarchar](max) NULL,
 CONSTRAINT [PK_FieldTrips] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MeetingRegistrations]    Script Date: 8/1/2023 10:21:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeetingRegistrations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[MeetingId] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[PaymentReceived] [bit] NOT NULL,
 CONSTRAINT [PK_MeetingRegistrations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Meetings]    Script Date: 8/1/2023 10:21:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Meetings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[RegOpenDate] [datetime2](7) NULL,
	[RegCloseDate] [datetime2](7) NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[ExpectedEndDate] [datetime2](7) NULL,
	[Address] [nvarchar](255) NOT NULL,
	[RegLimit] [int] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Fee] [int] NOT NULL,
	[Status] [nvarchar](3) NOT NULL,
	[Highlights] [nvarchar](max) NULL,
 CONSTRAINT [PK_Meetings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MembershipRequests]    Script Date: 8/1/2023 10:21:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MembershipRequests](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Address] [nvarchar](255) NOT NULL,
	[PhoneNumber] [nvarchar](15) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Status] [nvarchar](3) NOT NULL,
 CONSTRAINT [PK_MembershipRequests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TournamentRegistrations]    Script Date: 8/1/2023 10:21:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TournamentRegistrations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BirdId] [int] NOT NULL,
	[TournamentId] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[PaymentReceived] [bit] NOT NULL,
 CONSTRAINT [PK_TournamentRegistrations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tournaments]    Script Date: 8/1/2023 10:21:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tournaments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[RegOpenDate] [datetime2](7) NULL,
	[RegCloseDate] [datetime2](7) NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[ExpectedEndDate] [datetime2](7) NULL,
	[Address] [nvarchar](255) NOT NULL,
	[RegLimit] [int] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Fee] [int] NOT NULL,
	[Status] [nvarchar](3) NOT NULL,
	[Highlights] [nvarchar](max) NULL,
 CONSTRAINT [PK_Tournaments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TournamentStandings]    Script Date: 8/1/2023 10:21:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TournamentStandings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TournamentId] [int] NOT NULL,
	[BirdId] [int] NOT NULL,
	[Placement] [nvarchar](3) NOT NULL,
 CONSTRAINT [PK_TournamentStandings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 8/1/2023 10:21:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](86) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Address] [nvarchar](255) NOT NULL,
	[Phone] [nvarchar](15) NOT NULL,
	[Role] [nvarchar](3) NOT NULL,
	[ProfilePicture] [varbinary](max) NULL,
	[JoinDate] [datetime2](7) NOT NULL,
	[LastLogin] [datetime2](7) NULL,
	[ResetPasswordRequestTime] [datetime2](7) NULL,
	[ResetPasswordCode] [nvarchar](6) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_Birds_UserId]    Script Date: 8/1/2023 10:21:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_Birds_UserId] ON [dbo].[Birds]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Blogs_BlogCategoryId]    Script Date: 8/1/2023 10:21:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_Blogs_BlogCategoryId] ON [dbo].[Blogs]
(
	[BlogCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Blogs_UserId]    Script Date: 8/1/2023 10:21:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_Blogs_UserId] ON [dbo].[Blogs]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Comments_BlogId]    Script Date: 8/1/2023 10:21:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_Comments_BlogId] ON [dbo].[Comments]
(
	[BlogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Comments_UserId]    Script Date: 8/1/2023 10:21:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_Comments_UserId] ON [dbo].[Comments]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Feedbacks_UserId]    Script Date: 8/1/2023 10:21:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_Feedbacks_UserId] ON [dbo].[Feedbacks]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_FieldTripRegistrations_FieldTripId]    Script Date: 8/1/2023 10:21:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_FieldTripRegistrations_FieldTripId] ON [dbo].[FieldTripRegistrations]
(
	[FieldTripId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_FieldTripRegistrations_UserId]    Script Date: 8/1/2023 10:21:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_FieldTripRegistrations_UserId] ON [dbo].[FieldTripRegistrations]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_MeetingRegistrations_MeetingId]    Script Date: 8/1/2023 10:21:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_MeetingRegistrations_MeetingId] ON [dbo].[MeetingRegistrations]
(
	[MeetingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_MeetingRegistrations_UserId]    Script Date: 8/1/2023 10:21:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_MeetingRegistrations_UserId] ON [dbo].[MeetingRegistrations]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_TournamentRegistrations_BirdId]    Script Date: 8/1/2023 10:21:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_TournamentRegistrations_BirdId] ON [dbo].[TournamentRegistrations]
(
	[BirdId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_TournamentRegistrations_TournamentId]    Script Date: 8/1/2023 10:21:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_TournamentRegistrations_TournamentId] ON [dbo].[TournamentRegistrations]
(
	[TournamentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_TournamentStandings_BirdId]    Script Date: 8/1/2023 10:21:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_TournamentStandings_BirdId] ON [dbo].[TournamentStandings]
(
	[BirdId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_TournamentStandings_TournamentId]    Script Date: 8/1/2023 10:21:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_TournamentStandings_TournamentId] ON [dbo].[TournamentStandings]
(
	[TournamentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Users_Email]    Script Date: 8/1/2023 10:21:23 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_Email] ON [dbo].[Users]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[FieldTrips] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [StartDate]
GO
ALTER TABLE [dbo].[Meetings] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [StartDate]
GO
ALTER TABLE [dbo].[Tournaments] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [StartDate]
GO
ALTER TABLE [dbo].[Birds]  WITH CHECK ADD  CONSTRAINT [FK_Birds_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Birds] CHECK CONSTRAINT [FK_Birds_Users_UserId]
GO
ALTER TABLE [dbo].[Blogs]  WITH CHECK ADD  CONSTRAINT [FK_Blogs_BlogCategories_BlogCategoryId] FOREIGN KEY([BlogCategoryId])
REFERENCES [dbo].[BlogCategories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Blogs] CHECK CONSTRAINT [FK_Blogs_BlogCategories_BlogCategoryId]
GO
ALTER TABLE [dbo].[Blogs]  WITH CHECK ADD  CONSTRAINT [FK_Blogs_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Blogs] CHECK CONSTRAINT [FK_Blogs_Users_UserId]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Blogs_BlogId] FOREIGN KEY([BlogId])
REFERENCES [dbo].[Blogs] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Blogs_BlogId]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Users_UserId]
GO
ALTER TABLE [dbo].[Feedbacks]  WITH CHECK ADD  CONSTRAINT [FK_Feedbacks_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Feedbacks] CHECK CONSTRAINT [FK_Feedbacks_Users_UserId]
GO
ALTER TABLE [dbo].[FieldTripRegistrations]  WITH CHECK ADD  CONSTRAINT [FK_FieldTripRegistrations_FieldTrips_FieldTripId] FOREIGN KEY([FieldTripId])
REFERENCES [dbo].[FieldTrips] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FieldTripRegistrations] CHECK CONSTRAINT [FK_FieldTripRegistrations_FieldTrips_FieldTripId]
GO
ALTER TABLE [dbo].[FieldTripRegistrations]  WITH CHECK ADD  CONSTRAINT [FK_FieldTripRegistrations_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FieldTripRegistrations] CHECK CONSTRAINT [FK_FieldTripRegistrations_Users_UserId]
GO
ALTER TABLE [dbo].[MeetingRegistrations]  WITH CHECK ADD  CONSTRAINT [FK_MeetingRegistrations_Meetings_MeetingId] FOREIGN KEY([MeetingId])
REFERENCES [dbo].[Meetings] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MeetingRegistrations] CHECK CONSTRAINT [FK_MeetingRegistrations_Meetings_MeetingId]
GO
ALTER TABLE [dbo].[MeetingRegistrations]  WITH CHECK ADD  CONSTRAINT [FK_MeetingRegistrations_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MeetingRegistrations] CHECK CONSTRAINT [FK_MeetingRegistrations_Users_UserId]
GO
ALTER TABLE [dbo].[TournamentRegistrations]  WITH CHECK ADD  CONSTRAINT [FK_TournamentRegistrations_Birds_BirdId] FOREIGN KEY([BirdId])
REFERENCES [dbo].[Birds] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TournamentRegistrations] CHECK CONSTRAINT [FK_TournamentRegistrations_Birds_BirdId]
GO
ALTER TABLE [dbo].[TournamentRegistrations]  WITH CHECK ADD  CONSTRAINT [FK_TournamentRegistrations_Tournaments_TournamentId] FOREIGN KEY([TournamentId])
REFERENCES [dbo].[Tournaments] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TournamentRegistrations] CHECK CONSTRAINT [FK_TournamentRegistrations_Tournaments_TournamentId]
GO
ALTER TABLE [dbo].[TournamentStandings]  WITH CHECK ADD  CONSTRAINT [FK_TournamentStandings_Birds_BirdId] FOREIGN KEY([BirdId])
REFERENCES [dbo].[Birds] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TournamentStandings] CHECK CONSTRAINT [FK_TournamentStandings_Birds_BirdId]
GO
ALTER TABLE [dbo].[TournamentStandings]  WITH CHECK ADD  CONSTRAINT [FK_TournamentStandings_Tournaments_TournamentId] FOREIGN KEY([TournamentId])
REFERENCES [dbo].[Tournaments] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TournamentStandings] CHECK CONSTRAINT [FK_TournamentStandings_Tournaments_TournamentId]
GO
USE [master]
GO
ALTER DATABASE [BCMS] SET  READ_WRITE 
GO
