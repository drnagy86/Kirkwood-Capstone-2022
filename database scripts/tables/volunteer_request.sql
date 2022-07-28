/***************************************************************
Vinayak Deshpande
Created: 2022/01/26

Description:
The Volunteer Requests Table
**************************************************************
Vinayak Deshpande
Updated: 2022/02/16

Description: Changed EventID to TaskID
****************************************************************/

Use [tadpole_db]
GO

print '' print '*** creating Volunteer Request table ***'
GO
Create Table [dbo].[VolunteerRequest] (
	[RequestID]					[int] Identity(100000, 1)	Not Null	
	, [VolunteerID]				[int]						Not Null
	, [TaskID]					[int]						Not Null	
	, [VolunteerResponse]		[bit]
	, [EventResponse]			[bit]
	
	Constraint [pk_RequestID] Primary Key([RequestID])
	, Constraint [fk_Request_VolunteerID] Foreign Key([VolunteerID]) References [dbo].[Volunteer]([VolunteerID])
	, Constraint [fk_Request_TaskID] Foreign Key([TaskID]) References [dbo].[Task]([TaskID]) ON DELETE CASCADE

)
GO

/***************************************************************
Vinayak Deshpande
Created: 2022/01/26

Description:
Inserting Test Data Into The  Volunteer Requests Table
**************************************************************
Emma Pollock
Updated: 2022/04/14

Description: 
	Added more test data for other cases
****************************************************************/

print '' print '*** adding test data for Volunteer Requests Table ***'
GO
Insert Into [dbo].[VolunteerRequest] (
	[VolunteerID], [TaskID], [VolunteerResponse], [EventResponse])
	Values
		(100000, 100000, 1, 1),
		(100000, 100001, NULL, 1),
		(100000, 100002, 0, 1)
		
GO