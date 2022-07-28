
/***************************************************************
Derrick Nagy
Created: 2022/02/07

Description:
File containing the role table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

USE [tadpole_db]
GO


/***************************************************************
 Austin Timmerman
 Created: 2022/01/29
 
 Description:
 Role table
***************************************************************
 <Updater Name>
 Updated: yyyy/mm/dd

 Description: 
****************************************************************/
print '' print '*** creating Role table ***'
GO
CREATE TABLE [dbo].[Role] (
	[RoleID]			[nvarchar](50)				NOT NULL,
	[RoleDescription]	[nvarchar](300)				NULL,
	
	CONSTRAINT [pk_RoleID] PRIMARY KEY([RoleID])
)
GO


/***************************************************************
 Austin Timmerman
 Created: 2022/01/29
 
 Description:
 Test records for Role table
***************************************************************
 Derrick Nagy
 Updated: 2022/02/07

 Description: 
 Added Event Planner, Event Manager, and Attendee
 
***************************************************************
 Christopher Repko
 Updated: 2022/03/24

 Description: 
 Added Administrator
****************************************************************/
print '' print '*** test records for Role table ***'
GO
INSERT INTO [dbo].[Role] (
	[RoleID],
	[RoleDescription]
)VALUES 
	("Open Volunteer", "Volunteer role used for any sort of volunteer work"),
	("Specific Volunteer", "Volunteer role used when a volunteer only is used for specific skills"),
	("Supply Donor", "Volunteer role used when a volunteer donates specific materials"),
	('Event Planner','Creates the event, plans for the event.'),
	('Event Manager','Handles day of activities.'),
	('Attendee','An attendee for the event.'),
	('Supplier','A supplier for events.'),
	('Administrator','An administrator for the application.')
GO
