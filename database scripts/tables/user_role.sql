
/***************************************************************
Derrick Nagy
Created: 2022/02/07

Description:
File containing the user role table
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
UserRole Table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating UserRole table ***'
CREATE TABLE [dbo].[UserRole] (
	[UserID]			[int] 						NOT NULL,
	[RoleID]			[nvarchar](50)				NOT NULL		
	
	CONSTRAINT [pk_UserID_RoleID] PRIMARY KEY([UserID],[RoleID])
	, CONSTRAINT [fk_UserRole_UserID] FOREIGN KEY([UserID])
		REFERENCES [dbo].[Users]([UserID]) ON UPDATE CASCADE
	, CONSTRAINT [fk_UserRole_RoleID] FOREIGN KEY([RoleID])
		REFERENCES [dbo].[Role]([RoleID]) ON UPDATE CASCADE
)
GO

/***************************************************************
 Derrick Nagy
 Created: 2022/02/07
 
 Description:
 Test records for role table
***************************************************************
<name>
 Updated: <date>

 Description: 

****************************************************************/
print '' print '*** test records for UserRole table ***'
GO
INSERT INTO [dbo].[UserRole] (
	[UserID]
	,[RoleID]	
)
	VALUES 
	(100000, 'Event Planner')
	,(100000, 'Supplier')
	,(100001, 'Event Planner')
	,(100001, 'Event Manager')
	,(100002, 'Attendee')
	,(100000, 'Open Volunteer')
GO
