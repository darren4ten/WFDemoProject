GO

/****** Object:  Table [dbo].[WFInstances]    Script Date: 2016/5/24 17:40:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[WFInstances](
	[WfInstanceId] [uniqueidentifier] NULL,
	[User] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[SubmitTime] [datetime] NULL,
	[ApproveTime] [datetime] NULL,
	[ApproveUser] [nvarchar](50) NULL,
	[ShareUsers] [nvarchar](250) NULL,
	[CurrentNode] [nvarchar](50) NULL
) ON [PRIMARY]

GO
