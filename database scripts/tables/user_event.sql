
/***************************************************************
Derrick Nagy
Created: 2022/02/07

Description:
File containing the UserEvent table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

USE [tadpole_db]
GO

/***************************************************************
Derrick Nagy
Created: 2022/02/07

Description:
UserEvent Table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating UserEvent table ***'
CREATE TABLE [dbo].[UserEvent] (
	[UserID]	[int] 			NOT NULL
	,[RoleID]	[nvarchar](50)	NOT NULL
	,[EventID]	[int]			NOT NULL
	
	CONSTRAINT [pk_UserID_RoleID_EventID] PRIMARY KEY([UserID],[RoleID],[EventID])
	, CONSTRAINT [fk_UserEvent_UserID] FOREIGN KEY([UserID])
		REFERENCES [dbo].[Users]([UserID]) ON UPDATE CASCADE
	, CONSTRAINT [fk_UserEvent_RoleID] FOREIGN KEY([RoleID])
		REFERENCES [dbo].[Role]([RoleID]) ON UPDATE CASCADE
	, CONSTRAINT [fk_UserEvent_EventID] FOREIGN KEY([EventID])
		REFERENCES [dbo].[Event]([EventID])
)
GO

/***************************************************************
 Derrick Nagy
 Created: 2022/02/07
 
 Description:
 Test records for UserEvent table
***************************************************************
 Derrick Nagy
 Updated: 2022/02/23

 Description: 
 Added test records 
	(100000, 'Event Planner', 100003)
	(100000, 'Event Manager', 100003)

****************************************************************
Derrick Nagy
Updated: 2022/03/24

Description: 
Added test records:
	,(100000, 'Event Planner', 100003)
	,(100000, 'Event Manager', 100003)
	,(100002, 'Event Planner', 100004)
	,(100002, 'Event Planner', 100005)
	,(100002, 'Event Planner', 100006)
	,(100002, 'Event Planner', 100007)
**************************************************************
Derrick Nagy
Updated: 2023/03/27

Description: 
Added Suprise Party event to 
****************************************************************/
print '' print '*** test records for UserEvent table ***'
GO
INSERT INTO [dbo].[UserEvent] (
	[UserID], [RoleID], [EventID]
	
	)VALUES 
	(100000, 'Event Planner', 100000)
	,(100001, 'Event Manager', 100000)
	,(100002, 'Attendee', 100000)
	,(100000, 'Event Planner', 100001)
	,(100001, 'Event Manager', 100001)
	,(100002, 'Attendee', 100001)	
	,(100001, 'Event Planner', 100002)	
	,(100002, 'Attendee', 100002)
	-- ,(100002, 'Attendee', 100003) -- River does NOT want to go
	,(100002, 'Attendee', 100004)
	,(100002, 'Attendee', 100005)
	,(100002, 'Attendee', 100006)
	,(100002, 'Attendee', 100007) -- Past Event
	,(100000, 'Event Planner', 100008)-- Past Event
	,(100000, 'Event Planner', 100003)
	,(100000, 'Event Manager', 100003)
	,(100002, 'Event Planner', 100004)
	,(100002, 'Event Planner', 100005)
	,(100002, 'Event Planner', 100006)
	,(100002, 'Event Planner', 100007)
	,(100000, 'Event Manager', 100009) -- Suprise Party, no date
	,(100000, 'Event Manager', 100010) -- Pop up dance party, no date, no location

GO
