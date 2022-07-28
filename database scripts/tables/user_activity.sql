USE [tadpole_db]
GO
/***************************************************************
Logan Baccam
Created: 2022/02/14

Description:
UserActivitytable
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating UserActivity table ***'

CREATE TABLE [dbo].[UserActivity] (
	[UserActivityID]	[int] IDENTITY(100000,1) NOT NULL
	,[UserID]	[int] 			NOT NULL
	,[RoleID]	[nvarchar](50)	NOT NULL
	,[ActivityID]	[int]		NOT NULL
	CONSTRAINT [pk_UserActivityID] PRIMARY KEY([UserActivityID])
	, CONSTRAINT [fk_UserActivity_UserID] FOREIGN KEY([UserID])
		REFERENCES [dbo].[Users]([UserID]) ON UPDATE CASCADE
		
	, CONSTRAINT [fk_UserActivity_RoleID] FOREIGN KEY([RoleID])
	REFERENCES [dbo].[Role]([RoleID]) ON UPDATE CASCADE
	
	, CONSTRAINT [fk_UserActivity_ActivityID] FOREIGN KEY([ActivityID])
		REFERENCES [dbo].[Activity]([ActivityID])

)
GO

print '' print '*** test records for UserActivity table ***'
GO
INSERT INTO [dbo].[UserActivity] (
	[UserID], [RoleID], [ActivityID]
	
	)VALUES 
	(100000, 'Event Planner', 100000)
	,(100000, 'Event Planner', 100001)
	,(100000, 'Event Planner', 100002)
	,(100001, 'Event Manager', 100000)
	,(100001, 'Event Manager', 100001)
	,(100001, 'Event Manager', 100002)
	,(100000, 'Event Planner', 100006)
	

GO