USE [tadpole_db]
GO
/***************************************************************
Vinayak Deshpande
Created: 2022/02/04

Description:
The Volunteer Needs stored procedures
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

/*
 needed stored procedures
	request volunteers
	update needs
	delete needs
	
*/
print '' print '*** creating sp_select_volunteer_need_by_taskID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_volunteer_need_by_taskID]
(
	@TaskID int
)
AS
	BEGIN
		SELECT [NumTotalVolunteers],
				[NumCurrentVolunteers]
		FROM [dbo].[VolunteerNeed]
		WHERE [VolunteerNeed].[TaskID] = @TaskID
	END
GO

print '' print '*** creating sp_create_volunteer_need ***'
GO

CREATE PROCEDURE [dbo].[sp_insert_new_volunteer_need]
(
	@TaskID 			int,
	@NumTotalVolunteers int
)
AS
	BEGIN
		INSERT INTO [dbo].[VolunteerNeed]
		(
			[TaskID],
			[NumTotalVolunteers]
		)
		VALUES
		(		
			@TaskID,
			@NumTotalVolunteers
		)
	END
GO

print '' print '*** creating sp_update_volunteer_need ***'
GO

CREATE PROCEDURE [dbo].[sp_update_volunteer_need]
(
	@TaskID 			int,
	@NumTotalVolunteers int
)
AS
	BEGIN
		UPDATE [dbo].[VolunteerNeed]
		SET [VolunteerNeed].[NumTotalVolunteers] = @NumTotalVolunteers
		WHERE [VolunteerNeed].[TaskID] = @TaskID
	END
GO

print '' print '*** creating sp_delete_volunteer_need ***'
GO

CREATE PROCEDURE [dbo].[sp_delete_volunteer_need]
(
	@TaskID 			int
)
AS
	BEGIN
		Delete From [dbo].[VolunteerNeed]
		WHERE [VolunteerNeed].[TaskID] = @TaskID
	END
GO

print '' print '*** creating sp_add_curr_volunteers ***'
GO

Create Procedure [dbo].[sp_add_curr_volunteers]
(
	@TaskID int
)
AS
	BEGIN
		UPDATE [dbo].[VolunteerNeed]
		SET NumCurrentVolunteers = NumCurrentVolunteers + 1
		WHERE [VolunteerNeed].[TaskID] = @TaskID
	END
GO

print '' print '*** creating sp_subtract_curr_volunteers ***'
GO

Create Procedure [dbo].[sp_subtract_curr_volunteers]
(
	@TaskID int
)
AS
	BEGIN
		UPDATE [dbo].[VolunteerNeed]
		SET NumCurrentVolunteers = NumCurrentVolunteers - 1
		WHERE [VolunteerNeed].[TaskID] = @TaskID
	END
GO

