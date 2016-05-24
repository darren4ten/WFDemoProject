GO

/****** Object:  Table [dbo].[WFCurrentNodeInfo]    Script Date: 2016/5/24 17:40:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[WFCurrentNodeInfo](
	[WFinstId] [uniqueidentifier] NULL,
	[CurrentNode] [nvarchar](50) NULL,
	[EnterTime] [datetime] NULL,
	[ExitTime] [datetime] NULL
) ON [PRIMARY]

GO
