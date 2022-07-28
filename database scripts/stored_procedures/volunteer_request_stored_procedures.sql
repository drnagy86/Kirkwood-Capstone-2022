USE [tadpole_db]
GO
/***************************************************************
Vinayak Deshpande
Created: 2022/01/26

Description:
Stored Procedure for viewing all volunteer requests
related to event
**************************************************************
Emma Pollock
Updated: 2022/03/30

Description: 
Added EventName to Select
****************************************************************/

print '' print '*** creating sp_select_all_requests_by_eventID ***'
GO
Create PROCEDURE [dbo].[sp_select_all_requests_by_eventID]
(
	@EventID int
)
AS
	BEGIN
		SELECT 
			[VolunteerRequest].[RequestID],
			[VolunteerRequest].[VolunteerID],
			[VolunteerRequest].[TaskID],
			[VolunteerRequest].[VolunteerResponse],
			[VolunteerRequest].[EventResponse],
			CONCAT([Users].[GivenName], " ", [Users].[FamilyName]),
			[Task].[Name],
			[Event].[EventName]
		From [dbo].[VolunteerRequest]
		Join [dbo].[Task] ON [VolunteerRequest].[TaskID] = [Task].[TaskID]
		Join [dbo].[Volunteer] ON [VolunteerRequest].[VolunteerID] = [Volunteer].[VolunteerID]
		Join [dbo].[Users] ON [Volunteer].[UserID] = [Users].[UserID]
		Join [dbo].[Event] ON [Task].[EventID] = [Event].[EventID]
		Where [Task].[EventID] = @EventID
		Order By [RequestID] ASC
	END
GO

/***************************************************************
Emma Pollock
Created: 2022/03/30

Description:
Stored Procedure for viewing all volunteer requests for a 
	specific volunteer
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_select_all_requests_for_volunteer_by_volunteerID ***'
GO
Create PROCEDURE [dbo].[sp_select_all_requests_for_volunteer_by_volunteerID]
(
	@VolunteerID int
)
AS
	BEGIN
		SELECT 
			[VolunteerRequest].[RequestID],
			[VolunteerRequest].[TaskID],
			[VolunteerRequest].[VolunteerResponse],
			[VolunteerRequest].[EventResponse],
			CONCAT([Users].[GivenName], " ", [Users].[FamilyName]) AS "Volunteer Name",
			[Task].[Name] AS "Task Name",
			[Event].[EventName],
			[Event].[EventID]
		From [dbo].[VolunteerRequest]
		Join [dbo].[Task] ON [VolunteerRequest].[TaskID] = [Task].[TaskID]
		Join [dbo].[Volunteer] ON [VolunteerRequest].[VolunteerID] = [Volunteer].[VolunteerID]
		Join [dbo].[Users] ON [Volunteer].[UserID] = [Users].[UserID]
		Join [dbo].[Event] ON [Task].[EventID] = [Event].[EventID]
		Where [VolunteerRequest].[VolunteerID] = @VolunteerID 
			AND [VolunteerRequest].[EventResponse] = 1
		Order By [RequestID] ASC
	END
GO

/***************************************************************
Emma Pollock
Created: 2022/03/30

Description:
Stored Procedure for viewing all volunteer requests for a 
	specific volunteer
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_update_volunteer_request ***'
GO
Create PROCEDURE [dbo].[sp_update_volunteer_request]
(
	@RequestID				[int],
	@OldVolunteerResponse	[bit],
	@NewVolunteerResponse	[bit],
	@OldEventResponse		[bit],
	@NewEventResponse		[bit]
)
AS
	BEGIN
		UPDATE [VolunteerRequest]
		SET 
			[VolunteerResponse] = @NewVolunteerResponse,
			[EventResponse] = @NewEventResponse
		WHERE
			[RequestID] = @RequestID
		  AND	
			(
				(@OldVolunteerResponse = [VolunteerResponse] OR
					(
						@OldVolunteerResponse IS NULL AND
						[VolunteerResponse] IS NULL
					)
				)
				AND
				(@OldEventResponse = [EventResponse] OR
					(
						@OldEventResponse IS NULL AND
						[EventResponse] IS NULL
					)
				)
			)
		RETURN @@ROWCOUNT
	END
GO


/***************************************************************
Emma Pollock
Created: 2022/03/31

Description:
Stored Procedure for selecting a specific volunteer request
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_select_request_by_requestID ***'
GO
Create PROCEDURE [dbo].[sp_select_request_by_requestID]
(
	@RequestID int
)
AS
	BEGIN
		SELECT 
			[VolunteerRequest].[VolunteerID],
			[VolunteerRequest].[TaskID],
			[VolunteerRequest].[VolunteerResponse],
			[VolunteerRequest].[EventResponse],
			CONCAT([Users].[GivenName], " ", [Users].[FamilyName]) AS "Volunteer Name",
			[Task].[Name] AS "Task Name",
			[Event].[EventName],
			[Event].[EventID]
		From [dbo].[VolunteerRequest]
		Join [dbo].[Task] ON [VolunteerRequest].[TaskID] = [Task].[TaskID]
		Join [dbo].[Volunteer] ON [VolunteerRequest].[VolunteerID] = [Volunteer].[VolunteerID]
		Join [dbo].[Users] ON [Volunteer].[UserID] = [Users].[UserID]
		Join [dbo].[Event] ON [Task].[EventID] = [Event].[EventID]
		Where [VolunteerRequest].[RequestID] = @RequestID 

	END
GO