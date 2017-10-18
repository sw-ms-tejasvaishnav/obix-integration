

/****** Object:  Database [ESD_Lutron]    Script Date: 10/10/2017 5:49:46 PM ******/
DROP DATABASE [ESD_Lutron]
GO

/****** Object:  Database [ESD_Lutron]    Script Date: 10/10/2017 5:49:46 PM ******/
CREATE DATABASE [ESD_Lutron]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ESD_Lutron', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQL2014EXP\MSSQL\DATA\ESD_Lutron.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ESD_Lutron_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQL2014EXP\MSSQL\DATA\ESD_Lutron_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [ESD_Lutron] SET COMPATIBILITY_LEVEL = 120
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ESD_Lutron].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [ESD_Lutron] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [ESD_Lutron] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [ESD_Lutron] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [ESD_Lutron] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [ESD_Lutron] SET ARITHABORT OFF 
GO

ALTER DATABASE [ESD_Lutron] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [ESD_Lutron] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [ESD_Lutron] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [ESD_Lutron] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [ESD_Lutron] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [ESD_Lutron] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [ESD_Lutron] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [ESD_Lutron] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [ESD_Lutron] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [ESD_Lutron] SET  DISABLE_BROKER 
GO

ALTER DATABASE [ESD_Lutron] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [ESD_Lutron] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [ESD_Lutron] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [ESD_Lutron] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [ESD_Lutron] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [ESD_Lutron] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [ESD_Lutron] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [ESD_Lutron] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [ESD_Lutron] SET  MULTI_USER 
GO

ALTER DATABASE [ESD_Lutron] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [ESD_Lutron] SET DB_CHAINING OFF 
GO

ALTER DATABASE [ESD_Lutron] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [ESD_Lutron] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

ALTER DATABASE [ESD_Lutron] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [ESD_Lutron] SET  READ_WRITE 
GO




USE [ESD_Lutron]
GO

/****** Object:  Table [dbo].[ObixDevice]    Script Date: 10/10/2017 5:50:57 PM ******/
DROP TABLE [dbo].[ObixDevice]
GO

/****** Object:  Table [dbo].[ObixDevice]    Script Date: 10/10/2017 5:50:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ObixDevice](
	[DeviceId] [int] IDENTITY(1,1) NOT NULL,
	[DeviceName] [varchar](50) NOT NULL,
	[DeviceType] [varchar](50) NOT NULL,
	[DeviceUrl] [varchar](200) NOT NULL,
	[Unit] [varchar](50) NULL,
	[Value] [varchar](max) NULL,
	[Status] [varchar](50) NULL,
	[ValueType] [varchar](50) NULL,
	[object_instance] [int] NOT NULL,
	[isActive] [bit] NOT NULL,
	[DateOfEntry] [datetime] NOT NULL,
 CONSTRAINT [PK_ObixDevice] PRIMARY KEY CLUSTERED 
(
	[DeviceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO
