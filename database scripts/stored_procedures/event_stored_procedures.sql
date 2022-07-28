USE [tadpole_db]
GO

/***************************************************************
Derrick Nagy
Created: 2022/01/22

Description:
File containing the stored procedures for events
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

/***************************************************************
Derrick Nagy
Created: 2022/01/22

Description:
Stored procedure to insert an event into the events table
**************************************************************
Alaina Gilson
Updated: 2022/02/22

Description: 
Added TotalBudget field. Also updated formatting
****************************************************************/
print '' print '*** creating sp_insert_event ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_event]
(
	@EventName			nvarchar(50),
	@EventDescription	nvarchar(1000),
	@TotalBudget		money
)
AS
	BEGIN
		INSERT INTO [dbo].[Event]
		(
			[EventName],			
			[EventDescription],
			[TotalBudget]
		)
		VALUES
		(
			@EventName, 
			@EventDescription,
			@TotalBudget
		)		
	END	
GO

/***************************************************************
Jace Pettinger
Created: 2022/01/23

Description:
Stored procedure to select active event from the events table
**************************************************************
<Jace Pettinger>
Updated: 2022/02/15

Description: 
Adding LocationID to the selected values
**************************************************************
Alaina Gilson
Updated: 2022/02/22

Description: 
Added TotalBudget field
****************************************************************/
print '' print '*** creating sp_select_active_events ***'
GO
CREATE PROCEDURE [dbo].[sp_select_active_events]
AS
	BEGIN
		SELECT 
			[EventID],
			[EventName],
			[EventDescription],
			[DateCreated],
			[TotalBudget],
			[LocationID]
		FROM [dbo].[Event]
		WHERE [Active] = 1	
	END	
GO

/***************************************************************
Derrick Nagy
Created: 2022/01/22

Description:
Stored procedure to select an event from the Event table by the EventTitle and EventDescription

sp_select_event_by_event_name_and_description	@EventName 	nvarchar(50)
												@EventDescription	nvarchar(1000)	
	
**************************************************************
<Jace Pettinger>
Updated: 2022/02/15

Description: 
Adding LocationID to the selected values
**************************************************************
Alaina Gilson
Updated: 2022/02/22

Description: 
Added TotalBudget field. Also updated formatting
****************************************************************/
print '' print '*** creating sp_select_event_by_event_name_and_description ***'
GO
CREATE PROCEDURE [dbo].[sp_select_event_by_event_name_and_description] 
(
	@EventName 			nvarchar(50),
	@EventDescription	nvarchar(1000)
)
AS
	BEGIN
		SELECT 
			[EventID],
			[EventName],
			[EventDescription],
			[DateCreated],
			[TotalBudget],
			[LocationID]
		FROM [dbo].[Event]
		WHERE [EventName] = @EventName
			AND [EventDescription] = @EventDescription
			AND [Active] = 1
		ORDER BY [DateCreated] DESC
	END	
GO

/***************************************************************
Jace Pettinger
Created: 2022/02/03

Description:
Stored procedure to update an event from the Event table

**************************************************************
Alaina Gilson
Updated: 2022/02/22

Description: 
Added TotalBudget fields
****************************************************************/

print '' print '*** creating sp_update_event_by_eventID ***'
GO
CREATE PROCEDURE [dbo].[sp_update_event_by_eventID]
(
	@EventID 				[int],
	@OldEventName			[nvarchar](50),
	@OldEventDescription	[nvarchar](1000),
	@OldTotalBudget			[money],
	@OldActive				[bit],
	@NewEventName			[nvarchar](50),
	@NewEventDescription	[nvarchar](1000),
	@NewTotalBudget			[money],
	@NewActive				[bit]
)
AS
	BEGIN
		UPDATE	[Event]
		SET		
			[EventName] = @NewEventName,
			[EventDescription] = @NewEventDescription,
			[TotalBudget] = @NewTotalBudget,
			[Active] = @NewActive
		WHERE 	
			[EventID] = @EventID
		  AND	
			@OldEventName = [EventName]
		  AND
			@OldEventDescription = [EventDescription]
		  AND
			@OldTotalBudget = [TotalBudget]
		  AND
			@OldActive = [Active]
		RETURN @@ROWCOUNT
	END
GO

/***************************************************************
Derrick Nagy
Created: 2022/02/06

Description:
Stored procedure to select active events from the events table from the future and past
**************************************************************
<Jace Pettinger>
Updated: 2022/02/15

Description: 
Adding LocationID to the selected values
**************************************************************
Alaina Gilson
Updated: 2022/02/22

Description: 
Added TotalBudget field
****************************************************************
Derrick Nagy
Updated: 2022/03/24

Description: 
Added Location information
****************************************************************
Vinayak Deshpande
Updated: 2022/04/13

Description: 
modified the city and state parts
****************************************************************
Derrick Nagy
Updated: 2022/04/17

Description: 
Changed join on location and zip tables to left outer join to include events that do not have a location

****************************************************************/
print '' print '*** creating sp_select_active_events_for_past_and_future_event_dates ***'
GO
CREATE PROCEDURE [dbo].[sp_select_active_events_for_past_and_future_event_dates]
AS
	BEGIN
		SELECT 
			[Event].[EventID],
			[EventName],
			[EventDescription],
			[DateCreated],
			[TotalBudget],
			[Location].[LocationID],
			[EventDate].[EventDateID],
			[Location].[UserID],				
			[Location].[LocationName],			
			[Location].[LocationDescription],	
			[Location].[LocationPricingText],	
			[Location].[LocationPhone],		
			[Location].[LocationEmail],			
            [Location].[LocationAddress1],		
            [Location].[LocationAddress2],		
            [ZIP].[City],			
            [ZIP].[States],			
            [Location].[LocationZipCode],		
			[Location].[LocationImagePath],		
			[Location].[LocationActive]
		FROM [dbo].[Event]
			JOIN [dbo].[EventDate] ON [EventDate].[EventID] = [Event].[EventID]
			LEFT OUTER JOIN [dbo].[Location] ON [Location].[LocationID] = [Event].[LocationID]
			LEFT OUTER JOIN [dbo].[ZIP] ON [Location].[LocationZipCode] = [ZIP].[ZIPCode]
		WHERE [Event].[Active] = 1
		ORDER BY [Event].[EventID] ASC
		
	END	
GO

/***************************************************************
Derrick Nagy
Created: 2022/02/06

Description:
Stored procedure to select active upcoming event from the events table
**************************************************************
<Jace Pettinger>
Updated: 2022/02/15

Description: 
Adding LocationID to the selected values
**************************************************************
Alaina Gilson
Updated: 2022/02/22

Description: 
Added TotalBudget field
****************************************************************
Derrick Nagy
Updated: 2022/03/24

Description: 
Added Location information
****************************************************************
Vinayak Deshpande
Updated: 2022/04/13

Description: 
modified the city and state parts
****************************************************************
Derrick Nagy
Updated: 2022/04/17

Description: 
Changed join on location and zip tables to left outer join to include events that do not have a location

****************************************************************/
print '' print '*** creating sp_select_active_events_for_upcoming_dates ***'
GO
CREATE PROCEDURE [dbo].[sp_select_active_events_for_upcoming_dates]
AS
	BEGIN
		SELECT DISTINCT
			[Event].[EventID],
			[EventName],
			[EventDescription],
			[DateCreated],
			[TotalBudget],
			[Event].[LocationID],
			[EventDate].[EventDateID],
			[Location].[UserID],				
			[Location].[LocationName],			
			[Location].[LocationDescription],	
			[Location].[LocationPricingText],	
			[Location].[LocationPhone],		
			[Location].[LocationEmail],			
            [Location].[LocationAddress1],		
            [Location].[LocationAddress2],		
            [ZIP].[City],			
            [ZIP].[States],			
            [Location].[LocationZipCode],		
			[Location].[LocationImagePath],		
			[Location].[LocationActive]
		FROM [dbo].[Event]
			JOIN [dbo].[EventDate] ON [EventDate].[EventID] = [Event].[EventID]
			LEFT OUTER JOIN [dbo].[Location] ON [Location].[LocationID] = [Event].[LocationID]
			LEFT OUTER JOIN [dbo].[ZIP] ON [Location].[LocationZipCode] = [ZIP].[ZIPCode]
		WHERE [Event].[Active] = 1
			AND [EventDateID] >= GETDATE()
		ORDER BY [Event].[EventID] ASC
		
	END	
GO

/***************************************************************
Derrick Nagy
Created: 2022/02/06

Description:
Stored procedure to select active past events from the events 
**************************************************************
<Jace Pettinger>
Updated: 2022/02/15

Description: 
Adding LocationID to the selected values
**************************************************************
Alaina Gilson
Updated: 2022/02/22

Description: 
Added TotalBudget field
****************************************************************
Derrick Nagy
Updated: 2022/03/24

Description: 
Added Location information
****************************************************************
Vinayak Deshpande
Updated: 2022/04/13

Description: 
modified the city and state parts
****************************************************************
Derrick Nagy
Updated: 2022/04/17

Description: 
Changed join on location and zip tables to left outer join to include events that do not have a location

****************************************************************/
print '' print '*** creating sp_select_active_events_for_past_dates ***'
GO
CREATE PROCEDURE [dbo].[sp_select_active_events_for_past_dates]
AS
	BEGIN
		SELECT 
			[Event].[EventID],
			[EventName],
			[EventDescription],
			[DateCreated],
			[TotalBudget],
			[Location].[LocationID],
			[EventDate].[EventDateID],
			[Location].[UserID],				
			[Location].[LocationName],			
			[Location].[LocationDescription],	
			[Location].[LocationPricingText],	
			[Location].[LocationPhone],		
			[Location].[LocationEmail],			
            [Location].[LocationAddress1],		
            [Location].[LocationAddress2],		
            [ZIP].[City],			
            [ZIP].[States],			
            [Location].[LocationZipCode],		
			[Location].[LocationImagePath],		
			[Location].[LocationActive]
		FROM [dbo].[Event]
			JOIN [dbo].[EventDate] ON [EventDate].[EventID] = [Event].[EventID]
			LEFT OUTER JOIN [dbo].[Location] ON [Location].[LocationID] = [Event].[LocationID]
			LEFT OUTER JOIN [dbo].[ZIP] ON [Location].[LocationZipCode] = [ZIP].[ZIPCode]
		WHERE [Event].[Active] = 1
			AND [EventDateID] < GETDATE()
		ORDER BY [Event].[EventID] ASC
		
	END	
GO


/***************************************************************
Derrick Nagy
Created: 2022/02/08

Description:
Stored procedure to select active upcoming events from the events table for a user
**************************************************************
<Jace Pettinger>
Updated: 2022/02/15

Description: 
Adding LocationID to the selected values
**************************************************************
Alaina Gilson
Updated: 2022/02/22

Description: 
Added TotalBudget field
****************************************************************
Derrick Nagy
Updated: 2022/03/24

Description: 
Added Location information
****************************************************************
Vinayak Deshpande
Updated: 2022/04/13

Description: 
modified the city and state parts
****************************************************************
Derrick Nagy
Updated: 2022/04/17

Description: 
Changed join on location and zip tables to left outer join to include events that do not have a location

****************************************************************/
print '' print '*** creating sp_select_active_events_for_upcoming_dates_for_user ***'
GO
CREATE PROCEDURE [dbo].[sp_select_active_events_for_upcoming_dates_for_user]
(
	@UserID 	[int]
)
AS
	BEGIN
		SELECT 
			[UserEvent].[EventID],
			[Event].[EventName],
			[Event].[EventDescription],
			[Event].[DateCreated],
			[Event].[TotalBudget],
			[Event].[LocationID], -- 5
			[EventDate].[EventDateID], -- 6
			[Location].[UserID], -- 7
			[Location].[LocationName], -- 8
			[Location].[LocationDescription], -- 9
			[Location].[LocationPricingText], --10
			[Location].[LocationPhone], -- 11
			[Location].[LocationEmail], --12
            [Location].[LocationAddress1], --13
            [Location].[LocationAddress2], -- 14
            [ZIP].[City], --15
            [ZIP].[States], --16
            [Location].[LocationZipCode], -- 17
			[Location].[LocationImagePath], --18
			[Location].[LocationActive] -- 19
		FROM [dbo].[Event]
			JOIN [dbo].[EventDate] ON [EventDate].[EventID] = [Event].[EventID]
			JOIN [dbo].[UserEvent] ON [UserEvent].[UserID] = @UserID
			LEFT OUTER JOIN [dbo].[Location] ON [Location].[LocationID] = [Event].[LocationID]
			LEFT OUTER JOIN [dbo].[ZIP] ON [Location].[LocationZipCode] = [ZIP].[ZIPCode]
		WHERE [Event].[Active] = 1
			AND [UserEvent].[EventID] = [Event].[EventID]
			AND [EventDateID] >= GETDATE()
			
			
		ORDER BY [UserEvent].[EventID] ASC
		
	END	
GO



/***************************************************************
Derrick Nagy
Created: 2022/02/08

Description:
Stored procedure to select active past events from the events table for a user
**************************************************************
<Jace Pettinger>
Updated: 2022/02/15

Description: 
Adding LocationID to the selected values
**************************************************************
Alaina Gilson
Updated: 2022/02/22

Description: 
Added TotalBudget field
****************************************************************
Derrick Nagy
Updated: 2022/03/24

Description: 
Added Location information
****************************************************************
Vinayak Deshpande
Updated: 2022/04/13

Description: 
modified the city and state parts
****************************************************************
Derrick Nagy
Updated: 2022/04/17

Description: 
Changed join on location and zip tables to left outer join to include events that do not have a location

****************************************************************/
print '' print '*** creating sp_select_active_events_for_past_dates_for_user ***'
GO
CREATE PROCEDURE [dbo].[sp_select_active_events_for_past_dates_for_user]
(
	@UserID 	[int]
)
AS
	BEGIN
		SELECT 
			[UserEvent].[EventID],
			[Event].[EventName],
			[Event].[EventDescription],
			[Event].[DateCreated],
			[Event].[TotalBudget],
			[Event].[LocationID],
			[EventDate].[EventDateID],
			[Location].[UserID],				
			[Location].[LocationName],			
			[Location].[LocationDescription],	
			[Location].[LocationPricingText],	
			[Location].[LocationPhone],		
			[Location].[LocationEmail],			
            [Location].[LocationAddress1],		
            [Location].[LocationAddress2],		
            [ZIP].[City],			
            [ZIP].[States],		
            [Location].[LocationZipCode],		
			[Location].[LocationImagePath],		
			[Location].[LocationActive]
		FROM [dbo].[Event]
			JOIN [dbo].[EventDate] ON [EventDate].[EventID] = [Event].[EventID]
			JOIN [dbo].[UserEvent] ON [UserEvent].[UserID] = @UserID
			LEFT OUTER JOIN [dbo].[Location] ON [Location].[LocationID] = [Event].[LocationID]
			LEFT OUTER JOIN [dbo].[ZIP] ON [Location].[LocationZipCode] = [ZIP].[ZIPCode]
		WHERE [Event].[Active] = 1
			AND [EventDateID] < GETDATE()
			AND [UserEvent].[EventID] = [Event].[EventID]
		ORDER BY [UserEvent].[EventID] ASC
		
	END	
GO


/***************************************************************
Derrick Nagy
Created: 2022/02/08

Description:
Stored procedure to select active past and upcoming events from the events table for a user
**************************************************************
<Jace Pettinger>
Updated: 2022/02/15

Description: 
Adding LocationID to the selected values
**************************************************************
Alaina Gilson
Updated: 2022/02/22

Description: 
Added TotalBudget field
****************************************************************
Derrick Nagy
Updated: 2022/03/24

Description: 
Added Location information
****************************************************************
Vinayak Deshpande
Updated: 2022/04/13

Description: 
modified the city and state parts
****************************************************************
Derrick Nagy
Updated: 2022/04/17

Description: 
Changed join on location and zip tables to left outer join to include events that do not have a location

****************************************************************/
print '' print '*** creating sp_select_active_events_for_past_and_upcoming_dates_for_user ***'
GO
CREATE PROCEDURE [dbo].[sp_select_active_events_for_past_and_upcoming_dates_for_user]
(
	@UserID 	[int]
)
AS
	BEGIN
		SELECT 
			[UserEvent].[EventID],
			[Event].[EventName],
			[Event].[EventDescription],
			[Event].[DateCreated],
			[Event].[TotalBudget],
			[Event].[LocationID],
			[EventDate].[EventDateID],
			[Location].[UserID],				
			[Location].[LocationName],			
			[Location].[LocationDescription],	
			[Location].[LocationPricingText],	
			[Location].[LocationPhone],		
			[Location].[LocationEmail],			
            [Location].[LocationAddress1],		
            [Location].[LocationAddress2],		
            [ZIP].[City],			
            [ZIP].[States],		
            [Location].[LocationZipCode],		
			[Location].[LocationImagePath],		
			[Location].[LocationActive]			
		FROM [dbo].[Event]
			JOIN [dbo].[EventDate] ON [EventDate].[EventID] = [Event].[EventID]
			JOIN [dbo].[UserEvent] ON [UserEvent].[UserID] = @UserID
			LEFT OUTER JOIN [dbo].[Location] ON [Location].[LocationID] = [Event].[LocationID]
			LEFT OUTER JOIN [dbo].[ZIP] ON [Location].[LocationZipCode] = [ZIP].[ZIPCode]
		WHERE [Event].[Active] = 1			
			AND [UserEvent].[EventID] = [Event].[EventID]
		ORDER BY [UserEvent].[EventID] ASC
		
	END	
	
/***************************************************************
Christopher Repko
Created: 2022/02/09

Description:
Stored procedure to update an event's location data
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_update_event_location_by_event_id ***'
GO
CREATE PROCEDURE [dbo].[sp_update_event_location_by_event_id]
(
	@EventID 				[int],
	@OldLocationID			[int],
	@LocationID				[int]
)
AS
	BEGIN
		UPDATE	[Event]
		SET		
			[LocationID] = @LocationID
		WHERE 	
			[EventID] = @EventID
		  AND	
			(
				@OldLocationID = [LocationID] OR
				(
					@OldLocationID IS NULL AND
					[LocationID] IS NULL
				)
			)
		RETURN @@ROWCOUNT
	END
GO

/***************************************************************
Derrick Nagy
Created: 2022/02/17

Description:
Stored procedure to insert an event into the events table that returns the EventID
**************************************************************
Alaina Gilson
Updated: 2022/02/22

Description: 
Added TotalBudget field. Also updated formatting
****************************************************************/
print '' print '*** creating sp_insert_event_return_event_id ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_event_return_event_id]
(
	@EventName			nvarchar(50),
	@EventDescription	nvarchar(1000),
	@TotalBudget		money
)
AS
	BEGIN
		INSERT INTO [dbo].[Event]
		(
			[EventName],				
			[EventDescription],
			[TotalBudget]
		)
		OUTPUT Inserted.EventID
		VALUES
		(
			@EventName, 
			@EventDescription,
			@TotalBudget
		)		

	END	
GO


-- sp_insert_event_with_user_ID_return_event_id

/***************************************************************
Derrick Nagy
Created: 2022/02/18

Description:
Stored procedure to insert an event into the events table that returns the EventID
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_insert_event_with_user_ID_return_event_id ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_event_with_user_ID_return_event_id]
(
	@EventName			nvarchar(50)
	,@EventDescription	nvarchar(1000)
	,@TotalBudget		money
	,@UserID			int
)
AS
	BEGIN -- SP
		BEGIN TRAN
			BEGIN TRY
			
				DECLARE @EventID INT
			
				-- insert into event the record	
				INSERT INTO [dbo].[Event]
				(
					[EventName],
					[EventDescription],
					[TotalBudget]
				)
				OUTPUT Inserted.EventID
				VALUES
				(
					@EventName, 
					@EventDescription,
					@TotalBudget
				)
				
				SET @EventID = SCOPE_IDENTITY()
								
				-- insert into UserEvent				
				INSERT INTO [dbo].[UserEvent]
				(
					[UserID]
					, [RoleID]
					, [EventID]
				)
				VALUES
				(@UserID, 'Event Planner', @EventID)
				--, (@UserID, 'Event Manager', @EventID) -- Event Manager was dropped
				
				COMMIT TRANSACTION
				
			END TRY
			BEGIN CATCH
				ROLLBACK TRANSACTION
			END CATCH
	END	-- SP
GO


/***************************************************************
Derrick Nagy
Created: 2022/02/22

Description:
Stored procedure to select the roles that a user has for an event
**************************************************************

Updated: 

Description: 

****************************************************************/
print '' print '*** creating sp_select_user_roles_for_event ***'
GO
CREATE PROCEDURE [dbo].[sp_select_user_roles_for_event]
(
	@EventID	[int]
	,@UserID 	[int]
)
AS
	BEGIN
		SELECT 
			[UserEvent].[RoleID]
		FROM [dbo].[UserEvent]
		WHERE [UserEvent].[EventID] = @EventID
			AND [UserEvent].[UserID] = @UserID
	END	
GO	
	

/***************************************************************
Derrick Nagy
Created: 2022/02/22

Description:
Stored procedure to select the roles that a user has for an event
**************************************************************

Updated: 

Description: 

****************************************************************/
	print '' print '*** creating sp_select_event_planners_for_event ***'
GO
CREATE PROCEDURE [dbo].[sp_select_event_planners_for_event]
(
	@EventID	[int]
)
AS
	BEGIN
		SELECT 
			[Users].[UserID],
			[GivenName],
			[FamilyName],
			[Email], 
			[UserState],
			[City],
			[Zip],
			[Active]
		FROM Users
		Join 
		UserEvent
		ON [UserEvent].[RoleID] = 'Event Planner' 
			AND  [UserEvent].[EventID] = @EventID
			AND [Users].[UserID] = [UserEvent].[UserID]
	END	
GO	
	
/***************************************************************
Derrick Nagy
Created: 2022/03/26
Description:
Selects all the events for a user that do no have dates
****************************************************************
Derrick Nagy
Updated: 2022/04/17

Description: 
Changed join on location and zip tables to left outer join to include events that do not have a location

****************************************************************/
print '' print '*** creating sp_select_active_events_with_no_dates_for_user ***'
GO
CREATE PROCEDURE [dbo].[sp_select_active_events_with_no_dates_for_user]
(
	@UserID 	[int]
)
AS
	BEGIN
	
		SELECT
			[Event].[EventID],
			[Event].[EventName],
			[Event].[EventDescription],
			[Event].[DateCreated],
			[Event].[TotalBudget],
			[Event].[LocationID],
			[Location].[UserID],				
			[Location].[LocationName],			
			[Location].[LocationDescription],	
			[Location].[LocationPricingText],	
			[Location].[LocationPhone],		
			[Location].[LocationEmail],			
			[Location].[LocationAddress1],		
			[Location].[LocationAddress2],		
			[ZIP].[City],			
			[ZIP].[States],		
			[Location].[LocationZipCode],		
			[Location].[LocationImagePath],		
			[Location].[LocationActive]		
		FROM [Event]
			JOIN [UserEvent] ON [UserEvent].EventID = [Event].EventID
			LEFT OUTER JOIN [dbo].[Location] ON [Location].[LocationID] = [Event].[LocationID]
			LEFT OUTER JOIN [dbo].[ZIP] ON [Location].[LocationZipCode] = [ZIP].[ZIPCode]
		WHERE [UserEvent].UserID = @UserID		
		AND [Event].[Active] = 1
		AND [Event].[EventID] 
			NOT IN (
				SELECT EventID 
				FROM [EventDate]
				WHERE [EventID] = [Event].[EventID]
			)	
		ORDER BY [Event].[EventID] ASC
		
	END	
GO

/***************************************************************
Vinayak Deshpande
Created: 2022/04/01

Description:
selects event by event id
****************************************************************/
print '' print '*** creating sp_select_event_by_event_id ***'
GO
CREATE PROCEDURE [dbo].[sp_select_event_by_event_id]
(
	@EventID [int]
)
AS
	BEGIN
		SELECT
			[EventName],
			[EventDescription],
			[DateCreated],
			[TotalBudget],
			[LocationID]
		FROM [dbo].[Event]
		WHERE [EventID] = @EventID
	END	
GO


/***************************************************************
Derrick Nagy
Created: 2022/04/06
Description:
Selects the active events by the search query
****************************************************************
Vinayak Deshpande
Updated: 2022/04/13

Description: 
modified the city and state parts
****************************************************************/
print '' print '*** creating sp_select_active_events_by_search ***'
GO
CREATE PROCEDURE [dbo].[sp_select_active_events_by_search]
(
	@Search 	nvarchar(50)
)
AS
	BEGIN
		SELECT DISTINCT
			[Event].[EventID],
			[EventName],
			[EventDescription],
			[DateCreated],
			[TotalBudget],
			[Event].[LocationID],
			[EventDate].[EventDateID],
			[Location].[UserID],				
			[Location].[LocationName],			
			[Location].[LocationDescription],	
			[Location].[LocationPricingText],	
			[Location].[LocationPhone],		
			[Location].[LocationEmail],			
			[Location].[LocationAddress1],		
			[Location].[LocationAddress2],		
			[ZIP].[City],			
            [ZIP].[States],		
			[Location].[LocationZipCode],		
			[Location].[LocationImagePath],		
			[Location].[LocationActive]
		FROM [dbo].[Event]
			JOIN [dbo].[EventDate] ON [EventDate].[EventID] = [Event].[EventID]
			JOIN [dbo].[Location] ON [Location].[LocationID] = [Event].[LocationID]
			Join [dbo].[ZIP] ON [Location].[LocationZipCode] = [ZIP].[ZIPCode]
		WHERE [Event].[Active] = 1
			AND [EventDateID] >= GETDATE()
			AND 
				(
				[EventName] LIKE '%'+@Search+'%'
				OR
				[EventDescription] LIKE '%'+@Search+'%'
				OR
				[LocationName] LIKE '%'+@Search+'%'
				OR
				[City] LIKE '%'+@Search+'%'
				OR
				[States] LIKE '%'+@Search+'%'
				)
			
		ORDER BY [Event].[EventID] ASC
		
	END	
GO

/***************************************************************
Kris Howell
Created: 2022/04/28
Description:
Deactivate an event
****************************************************************/
print '' print '*** Creating sp_deactivate_event_by_eventID ***'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_event_by_eventID]
(
	@EventID 		[int]
)
AS
	BEGIN
		UPDATE [Event]
		SET
			[Active] = 0
		WHERE
			[EventID] = @EventID
		RETURN @@ROWCOUNT
	END
GO