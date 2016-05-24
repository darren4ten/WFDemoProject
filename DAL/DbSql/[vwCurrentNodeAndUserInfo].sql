

/****** Object:  View [dbo].[vwCurrentNodeAndUserInfo]    Script Date: 2016/5/24 17:36:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwCurrentNodeAndUserInfo]
	AS 
	
	SELECT cn.*,u.Name,u.FullName FROM WFCurrentNodeInfo cn join [User] u on cn.WFinstId=u.WorkflowInstId;

GO


