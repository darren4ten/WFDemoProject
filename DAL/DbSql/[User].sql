GO

/****** Object:  Table [dbo].[User]    Script Date: 2016/5/24 17:39:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Age] [int] NULL,
	[FullName] [nvarchar](50) NULL,
	[Department] [nvarchar](50) NULL,
	[WorkflowInstId] [uniqueidentifier] NULL
) ON [PRIMARY]

GO


