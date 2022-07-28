/***************************************************************
Emma Pollock
Created: 2022/02/02

Description:
File containing the stored procedures for Activities

**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

USE [tadpole_db]
GO

/***************************************************************
Emma Pollock
Created: 2022/02/02

Description:
Stored procedure to select all activities for an event
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_select_activities_by_eventID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_activities_by_eventID](
	@EventID			[int] 
)
AS
	BEGIN
		SELECT 
			[ActivityID]			
			,[ActivityName]			
			,[ActivityDescription]	
			,[PublicActivity]		
			,[StartTime]			
			,[EndTime]				
			,[ActivityImageName]	
			,[Activity].[SublocationID]
			,[EventDateID]				
		FROM [dbo].[Activity]
		WHERE [EventID] = @EventID
	END	
GO

/***************************************************************
Emma Pollock
Created: 2022/02/05

Description:
Stored procedure to insert a new activity record
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print'*** creating sp_insert_activity ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_activity](
	@ActivityName			[nvarchar](50)
	,@ActivityDescription	[nvarchar](250)
	,@PublicActivity		[bit]
	,@StartTime				[time]
	,@EndTime				[time]
	,@ActivityImageName		[nvarchar](25)
	,@SublocationID			[int]
	,@EventDateID			[date]
	,@EventID				[int]
)
AS
	BEGIN
		INSERT INTO [dbo].[Activity]
		(
			[ActivityName]			
			,[ActivityDescription]	
			,[PublicActivity]		
			,[StartTime]			
			,[EndTime]				
			,[ActivityImageName]	
			,[SublocationID]		
			,[EventDateID]	
			,[EventID]
		)
		VALUES
		(
			@ActivityName
			,@ActivityDescription
			,@PublicActivity
			,@StartTime
			,@EndTime
			,@ActivityImageName
			,@SublocationID
			,@EventDateID
			,@EventID
		)		
	END	
GO

/***************************************************************
Emma Pollock
Created: 2022/02/05

Description:
Stored procedure to select all activities for a specific date
	of an event
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_select_activities_by_eventID_and_event_dateID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_activities_by_eventID_and_event_dateID](
	@EventID			[int] 
	,@EventDateID		[date]
)
AS
	BEGIN
		SELECT 
			[ActivityID]			
			,[ActivityName]			
			,[ActivityDescription]	
			,[PublicActivity]		
			,[StartTime]			
			,[EndTime]				
			,[ActivityImageName]	
			,[SublocationID]								
		FROM [dbo].[Activity]
		WHERE [EventID] = @EventID
			AND [EventDateID] = @EventDateID
	END	
GO


/***************************************************************
Logan Baccam
Created: 2022/02/13

Description:
Stored procedure to select all activities for past and future dates
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_select_activities_for_past_and_upcoming_dates ***'
GO
CREATE PROCEDURE [dbo].[sp_select_activities_for_past_and_upcoming_dates]
AS
	BEGIN
		SELECT 		
			 
			[Activity].[ActivityID],
			[Activity].[ActivityName],
			[Activity].[ActivityDescription],
			[Activity].[StartTime],
			[Activity].[EndTime],
			[Activity].[ActivityImageName],
			[Sublocation].[SublocationID],
			[Sublocation].[SublocationName],
			[Activity].[EventID],
			[Activity].[EventDateID],
			[Event].[EventName]


			FROM Activity
			JOIN Sublocation ON Sublocation.SublocationID = Activity.SublocationID
			JOIN [Event] ON [Event].EventID = [Activity].[EventID]
			
			
			
			AND [Activity].[PublicActivity] = 1
			ORDER BY [Activity].[EventDateID] DESC
		
	END	
GO
/***************************************************************
Logan Baccam
Created: 2022/02/13

Description:
Stored procedure to select all event activities from the activities table for a user
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
**************************************************************** */
print '' print '*** creating sp_select_all_activities_for_user ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_activities_for_user]
(
	@UserID 	[int]
)
AS
	BEGIN
		SELECT 
			[UserActivity].ActivityID
			,[Activity].[ActivityName]
			,[Activity].[ActivityDescription]
			,[Activity].[StartTime]
			,[Activity].[EndTime]
			,[Activity].[ActivityImageName]
			,[Sublocation].[SublocationID]
			,[Sublocation].[SublocationName]
			,[Event].[EventID]
			,[Activity].[EventDateID]
			,[Event].[EventName]
			,[Activity].[PublicActivity]

	FROM Activity
	JOIN [UserActivity] ON [UserActivity].[ActivityID] =[Activity].[ActivityID]
	JOIN [Event] ON [Event].[EventID] = [Activity].[EventID] 
	JOIN [Sublocation] ON [Sublocation].[SublocationID] = [Activity].[SublocationID] 
	
	AND [UserActivity].[UserID] = @UserID
	ORDER BY [Activity].[EventDateID] DESC	
	END	
GO

/***************************************************************
Austin Timmerman
Created: 2022/02/24

Description:
Stored procedure to select all activities by sublocationID
>>>>>>> origin/main
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_select_activities_by_sublocationID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_activities_by_sublocationID](
	@SublocationID		[int] 
)
AS
	BEGIN
		SELECT 
			[ActivityID]			
			,[ActivityName]		
			,[PublicActivity]		
			,[StartTime]			
			,[EndTime]				
			,[EventDateID]
		FROM [dbo].[Activity]
		WHERE [SublocationID] = @SublocationID
	END	
GO

/***************************************************************
Logan Baccam
Created: 2022/02/24

Description:
Stored procedure to select all activities in viewmodel for viewing an events activities
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_select_activities_by_eventID_for_activityvm ***'
GO
CREATE PROCEDURE [dbo].[sp_select_activities_by_eventID_for_activityvm](
	@EventID			[int] 
)
AS
	BEGIN
		SELECT 
			[ActivityID]			
			,[ActivityName]			
			,[ActivityDescription]	
			,[PublicActivity]		
			,[StartTime]			
			,[EndTime]				
			,[ActivityImageName]	
			,[Activity].[SublocationID]
			,[EventDateID]		
			,[Sublocation].[SublocationName]
		FROM [dbo].[Activity]
		JOIN [Sublocation] ON [Sublocation].[SublocationID] = [Activity].[SublocationID]
		WHERE [EventID] = @EventID
	END	
GO

/***************************************************************
Kris Howell
Created: 2022/02/24

Description:
Select all activities occuring on a given date that a
given supplier is associated with
****************************************************************/
print '' print '*** creating sp_select_activities_by_supplierID_and_date ***'
GO
CREATE PROCEDURE [dbo].[sp_select_activities_by_supplierID_and_date]
(
    @SupplierID     [int]
	,@ActivityDate	[date]
)
AS
	BEGIN
		SELECT
			[SupplierAttendance].[ActivityID]
			,[ActivityName]
			,[ActivityDescription]
			,[PublicActivity]
			,[StartTime]
			,[EndTime]
			,[ActivityImageName]
			,[SublocationID]
			,[EventDateID]
			,[EventID]
		FROM [dbo].[Activity]
		JOIN [dbo].[SupplierAttendance]
			ON [Activity].[ActivityID] = [SupplierAttendance].[ActivityID]
		WHERE [SupplierAttendance].[SupplierID] = @SupplierID
			AND [Activity].[EventDateID] = @ActivityDate
	END
GO

/***************************************************************
Vinayak Deshpande
Created: 2022/03/14

Description:
Stored PROCEDURE that allows the user to remove an activity from
a sublocation
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_update_activity_sublocation_by_activity_id ***'
GO
CREATE PROCEDURE [dbo].[sp_update_activity_sublocation_by_activity_id]
(
	@ActivityID 		[int],
	@OldSublocationID	[int],
	@SublocationID	[int]
)
AS
	BEGIN
		UPDATE [dbo].[Activity]
		SET		
			[SublocationID] = @SublocationID
		WHERE 	
			[ActivityID] = @ActivityID
		  AND	
			(
				@OldSublocationID = [SublocationID] OR
				(
					@OldSublocationID IS NULL AND
					[SublocationID] IS NULL
				)
			)
		RETURN @@ROWCOUNT
	END
GO

/***************************************************************
Austin Timmerman
Created: 2022/04/06

Description:
Select all activities that a given supplier is associated with
****************************************************************/
print '' print '*** creating sp_select_activities_by_supplierID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_activities_by_supplierID]
(
    @SupplierID     [int]
)
AS
	BEGIN
		SELECT
			[SupplierAttendance].[ActivityID]
			,[ActivityName]
			,[ActivityDescription]
			,[PublicActivity]
			,[StartTime]
			,[EndTime]
			,[ActivityImageName]
			,[SublocationID]
			,[EventDateID]
			,[EventID]
		FROM [dbo].[Activity]
		JOIN [dbo].[SupplierAttendance]
			ON [Activity].[ActivityID] = [SupplierAttendance].[ActivityID]
		WHERE [SupplierAttendance].[SupplierID] = @SupplierID
	END
GO

/***************************************************************
Mike Cahow
Created: 2022/04/08

Description:
Stored PROCEDURE that allows selects the activity by its ID
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** Creating sp_select_activityVM_by_activityID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_activityVM_by_activityID]
(
	@ActivityID		[int]
)
AS
	BEGIN
		
			SELECT	[ActivityID]			
					,[ActivityName]			
					,[ActivityDescription]	
					,[PublicActivity]		
					,[StartTime]			
					,[EndTime]				
					,[ActivityImageName]	
					,[Activity].[SublocationID]
					,[EventDateID]		
					,[Sublocation].[SublocationName]
					,[Event].[EventID]
			FROM	[dbo].[Activity]
			JOIN	[Sublocation] ON [Sublocation].[SublocationID] = [Activity].[SublocationID]
			JOIN	[Event] ON [Event].[EventID] = [Activity].[EventID]
			WHERE	[ActivityID] = @ActivityID
			
	END
GO			