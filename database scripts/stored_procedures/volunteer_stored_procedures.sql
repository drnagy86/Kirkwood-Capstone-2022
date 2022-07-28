USE [tadpole_db]
GO

/***************************************************************
Austin Timmerman
Created: 2022/01/26

Description:
File containing the stored procedures for volunteers
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

/***************************************************************
Austin Timmerman
Created: 2022/01/26

Description:
Stored procedure to select all the volunteers
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_select_all_volunteers ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_volunteers]
AS
	BEGIN
		SELECT 
			[Volunteer].[UserID],
			[Volunteer].[VolunteerID],
			[Users].[GivenName],
			[Users].[FamilyName],			
			[Users].[UserState],		
			[Users].[City],			
			[Users].[Zip],
			[VolunteerType].[RoleID],
			[Users].[Email],
			[Users].[UserDescription]
		FROM [Volunteer] 
		JOIN [Users] ON [Volunteer].[UserID] = [Users].[UserID]
		JOIN [VolunteerType] ON [Volunteer].[VolunteerID] = [VolunteerType].[VolunteerID]
	END	
GO



/***************************************************************
Austin Timmerman
Created: 2022/01/26

Description:
Stored procedure to select all reviews for the volunteers
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
/*
print '' print '*** creating sp_select_all_volunteer_reviews ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_volunteer_reviews]
AS
	BEGIN
		SELECT 
			[VolunteerReview].[VolunteerID],
			AVG([VolunteerReview].[Rating])
		FROM [VolunteerReview] 
		GROUP BY [VolunteerID]
	END	
GO
*/

/***************************************************************
Emma Pollock
Created: 2022/04/07

Description:
Stored procedure to select a volunteer by their user ID
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_select_volunteer_by_userID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_volunteer_by_userID](
	@UserID [int]
)
AS
	BEGIN
		SELECT 
			[Volunteer].[UserID],
			[Volunteer].[VolunteerID],
			[Users].[GivenName],
			[Users].[FamilyName],			
			[Users].[UserState],		
			[Users].[City],			
			[Users].[Zip],
			[VolunteerType].[RoleID],
			[Users].[Email],
			[Users].[UserDescription]
		FROM [Volunteer] 
		JOIN [Users] ON [Volunteer].[UserID] = [Users].[UserID]
		JOIN [VolunteerType] ON [Volunteer].[VolunteerID] = [VolunteerType].[VolunteerID]
		WHERE [Volunteer].[UserID] = @UserID
	END	
GO


/***************************************************************
Derrick Nagy
Created: 2022/04/28

Description:
Stored procedure that creates a new volunteer record for a user,
sets approval to false, creates a new availability record,
and associates it with the volunteer id for the user
****************************************************************/
print '' print '*** creating sp_insert_volunteer_application ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_volunteer_application](
	@UserID 		[int]
	,@TimeStart		[time]
	,@TimeEnd		[time]
	,@Sunday		[bit]
	,@Monday		[bit]
	,@Tuesday		[bit]
	,@Wednesday		[bit]
	,@Thursday		[bit]
	,@Friday		[bit]
	,@Saturday		[bit]
)
AS
	BEGIN -- SP
		BEGIN TRAN
			BEGIN TRY
			
				DECLARE @VolunteerID 	[INT]
				DECLARE @AvailabilityID [INT]
			
				INSERT INTO [dbo].[Volunteer] (
					[UserID],
					[Approved]
				)VALUES
					(@UserID, 0 )
				
				-- get last inserted id
				SET @VolunteerID = SCOPE_IDENTITY()
				
				-- insert into availability
				INSERT INTO [dbo].[Availability] (
					[TimeStart],
					[TimeEnd],
					[Sunday],
					[Monday],
					[Tuesday],
					[Wednesday],
					[Thursday],
					[Friday],
					[Saturday]
				)VALUES
					(@TimeStart, @TimeEnd, @Sunday, @Monday, @Tuesday, @Wednesday, @Thursday, @Friday, @Saturday)
					

				-- get last inserted id
				SET @AvailabilityID = SCOPE_IDENTITY()
					
				INSERT INTO [dbo].[VolunteerAvailability] (	
					[VolunteerID],
					[AvailabilityID]
				)VALUES 
					(@VolunteerID, @AvailabilityID)
					
				
				-- insert into volunteer type table default "Open Volunteer"
				INSERT INTO [dbo].[VolunteerType] (
					[VolunteerID],
					[RoleID]
				)VALUES 
					(@VolunteerID, "Open Volunteer")
					
				COMMIT TRANSACTION
			
			END TRY
			BEGIN CATCH
				ROLLBACK TRANSACTION
			END CATCH
	END	-- SP
GO
