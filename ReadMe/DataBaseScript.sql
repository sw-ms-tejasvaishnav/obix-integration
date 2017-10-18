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
