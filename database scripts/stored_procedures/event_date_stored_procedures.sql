USE [tadpole_db]
GO

/***************************************************************
Derrick Nagy
Created: 2022/01/30

Description:
File containing the stored procedures for event dates

**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

/***************************************************************
Derrick Nagy
Created: 2022/01/30

Description:
Stored procedure to insert an event date into the event date table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_insert_event_date ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_event_date]
(
	@EventDateID			[Date] 
	,@EventID			[int] 
	,@StartTime			[Time](0) 
	,@EndTime			[Time](0) 
)
AS
	BEGIN
		INSERT INTO [dbo].[EventDate]
		(
			[EventDateID]
			,[EventID]	
			,[StartTime] 
			,[EndTime]	 
		)
		VALUES
		(
			@EventDateID
			,@EventID
			,@StartTime
			,@EndTime
		)
	END	
GO

/***************************************************************
Derrick Nagy
Created: 2022/01/30

Description:
Stored procedure to select the information about the dates for events
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_select_event_dates_by_eventID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_event_dates_by_eventID](
	@EventID			[int] 
)
AS
	BEGIN
		SELECT 
			[EventDateID]
			,[EventID]	
			,[StartTime] 
			,[EndTime]			
		FROM [dbo].[EventDate]
		WHERE [Active] = 1	
			AND [EventID] = @EventID
	END	
GO


/***************************************************************
Emma Pollock
Created: 2022/02/02

Description:
Stored procedure to select the information about a specific date 
	for an event
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_select_event_date_by_event_dateID_and_eventID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_event_date_by_event_dateID_and_eventID](
	@EventDateID		[Date]
	,@EventID			[int] 
)
AS
	BEGIN
		SELECT 
			[StartTime] 
			,[EndTime]			
		FROM [dbo].[EventDate]
		WHERE [Active] = 1
			AND [EventDateID] = @EventDateID
			AND [EventID] = @EventID
	END	
GO

/***************************************************************
Jace Pettinger
Created: 2022/02/08

Description:
Stored procedure to update an event date from the EventDate table

**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_update_event_date ***'
GO
CREATE PROCEDURE [dbo].[sp_update_event_date]
(
	@EventID 				[int],
	@OldEventDateID			[Date],
	@OldStartTime			[Time](0),
	@OldEndTime				[Time](0),
	@OldActive				[bit],
	@NewEventDateID			[Date],
	@NewStartTime			[Time](0),
	@NewEndTime				[Time](0),
	@NewActive				[bit]
)
AS
	BEGIN
		UPDATE	[EventDate]
		SET	
			[EventDateID] = @NewEventDateID,
			[StartTime] = @NewStartTime,
			[EndTime] = @NewEndTime,
			[Active] = @NewActive
		WHERE
			[EventID] = @EventID
		  AND
			@OldEventDateID = [EventDateID]
		  AND	
			@OldStartTime = [StartTime]
		  AND
			@OldEndTime = [EndTime]
		  AND
			@OldActive = [Active]
		RETURN @@ROWCOUNT
	END
GO


/***************************************************************
Austin Timmerman
Created: 2022/02/10

Description:
Stored procedure to select the event dates associated with a 
locationID
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_select_event_dates_by_location_id ***'
GO
CREATE PROCEDURE [dbo].[sp_select_event_dates_by_location_id](
	@LocationID			[int] 
)
AS
	BEGIN
		SELECT 
			[EventDateID],
			[Event].[EventID],
			[EventName],
			[StartTime],
			[EndTime]
		FROM [dbo].[EventDate]
		JOIN [Event] ON [EventDate].[EventID] = [Event].[EventID]
		WHERE [Event].[Active] = 1
		AND   [LocationID] = @LocationID
	END	
GO

/***************************************************************
/ Austin Timmerman
/ Created: 2022/03/31
/ 
/ Description: Creating Stored Procedure to select any volunteers
/ participating for an event by their task
/
***************************************************************
/ <Updater Name>
/ Updated: yyyy/mm/dd
/
/ Description: 
****************************************************************/

print '' print '*** creating sp_select_event_date_by_userID_and_date ***'
GO
CREATE PROCEDURE [dbo].[sp_select_event_date_by_userID_and_date]
(
	@UserID 	[int],
	@EventDate	[date]
)
AS
	BEGIN
	
		SELECT 
			[Event].[EventName],
			[EventDate].[EventID],
			[EventDate].[StartTime],
			[EventDate].[EndTime]	
		FROM [dbo].[EventDate] 
		JOIN [Event] ON [Event].[EventID] = [EventDate].[EventID]
		JOIN [Task] ON [Task].[EventID] = [EventDate].[EventID]
		JOIN [TaskAssignment] ON [TaskAssignment].[TaskID] = [Task].[TaskID]
		WHERE [TaskAssignment].[UserID] = @UserID AND [EventDateID] = @EventDate AND [Event].[Active] = 1
	END
GO