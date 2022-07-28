/***************************************************************
Emma Pollock
Created: 2022/02/03

Description:
File containing the stored procedures for Activity Results

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
Stored procedure to select the activity results for a specific
	activity
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_select_activity_results_by_activityID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_activity_results_by_activityID](
	@ActivityID			[int] 
)
AS
	BEGIN
		SELECT 			
			[ActivityResultRank],
			[ActivityResultName]
		FROM [dbo].[ActivityResult]
		WHERE [ActivityID] = @ActivityID
	END	
GO