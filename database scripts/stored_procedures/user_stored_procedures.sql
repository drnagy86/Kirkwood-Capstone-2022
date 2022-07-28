USE [tadpole_db]
GO

/***************************************************************
Ramiro Pena
Created: 2022/01/24

Description:
File containing the stored procedures for users
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

/***************************************************************
/ Ramiro Pena
/ Created: 2022/01/24
/ 
/ Description: Creating Store Procedure to Insert a User I/E Register
/
/
/ <Updater Name>
/ Updated: yyyy/mm/dd
/
/ Description: 
****************************************************************/

print '' print '*** creating sp_insert_User ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_User]
(
	@GivenName			[nvarchar](50),			
	@FamilyName			[nvarchar](50),			
	@Email				[nvarchar](250),
	@UserState 			[char](2), 				
	@City				[nvarchar](75), 			
	@Zip				[int]
)
AS
	BEGIN
		INSERT INTO [dbo].[Users]
			([GivenName], [FamilyName], [Email], [UserState], [City], [Zip])
		VALUES
			(@GivenName, @FamilyName, @Email, @UserState, @City,
			@Zip)
	END
GO

/***************************************************************
/ Ramiro Pena
/ Created: 2022/01/24
/ 
/ Description: Creating Stored Procedure for authentication Login
/
/
/ <Updater Name>
/ Updated: yyyy/mm/dd
/
/ Description: 
****************************************************************/

/* stored procedures for login */
print '' print '*** creating sp_authenticate_user ***'
GO
CREATE PROCEDURE [dbo].[sp_authenticate_user]
(
	@Email 				[nvarchar](100),
	@PasswordHash		[nvarchar](100)
)
AS
	BEGIN
		SELECT COUNT([UserID]) AS 'Authenticated'
		FROM 	[Users]
		WHERE 	@Email = [Email]
		  AND	@PasswordHash = [PasswordHash]
		  AND	Active = 1
	END
GO

/***************************************************************
/ Ramiro Pena
/ Created: 2022/01/24
/ 
/ Description: Creating Stored Procedure to Select a User
/
/
/ <Updater Name>
/ Updated: yyyy/mm/dd
/
/ Description: 
****************************************************************/

print '' print '*** creating sp_select_user_by_email ***'
GO
CREATE PROCEDURE [dbo].[sp_select_user_by_email]
(
	@Email 				[nvarchar](100)
)
AS
	BEGIN
		SELECT 	[UserID], [GivenName], [FamilyName], [Email], [UserState],
			[City], [Zip], [Active]
		FROM 	[Users]
		WHERE 	@Email = [Email]
	END
GO

/***************************************************************
/ Ramiro Pena
/ Created: 2022/01/24
/ 
/ Description: Creating Store Procedure to Update a user's password from defualt.
/
/
/ <Updater Name>
/ Updated: yyyy/mm/dd
/
/ Description: 
****************************************************************/

print '' print '*** creating sp_update_passwordHash ***'
GO
CREATE PROCEDURE [dbo].[sp_update_passwordHash]
(
	@Email 				[nvarchar](100),
	@OldPasswordHash	[nvarchar](100),
	@NewPasswordHash	[nvarchar](100)
)
AS
	BEGIN
		UPDATE	[Users]
		SET		[PasswordHash] = @NewPasswordHash
		WHERE 	@Email = [Email]
		  AND	@OldPasswordHash = [PasswordHash]
		RETURN @@ROWCOUNT
	END
GO


/***************************************************************
Derrick Nagy
Created: 2022/03/08

Description:
Stored procedure to select the roles that a user has from the event users table
**************************************************************

Updated: 

Description: 

****************************************************************/
print '' print '*** creating sp_select_user_roles_from_event_users_table ***'
GO
CREATE PROCEDURE [dbo].[sp_select_user_roles_from_event_users_table]
(
	@UserID 	[int]
)
AS
	BEGIN
		SELECT 
			[UserEvent].[RoleID]
		FROM [dbo].[UserEvent]
		WHERE [UserEvent].[UserID] = @UserID
	END	
GO

/***************************************************************
Christopher Repko
Created: 2022/03/24

Description:
Stored procedure to select all roles in the roles table
**************************************************************

Updated: 

Description: 

****************************************************************/
print '' print '*** creating sp_select_all_roles ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_roles]
AS
	BEGIN
		SELECT 
			[Role].[RoleID]
		FROM [dbo].[Role]
	END	
GO

/***************************************************************
Christopher Repko
Created: 2022/03/24

Description: 
Stored procedure to insert a user role
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_insert_user_role ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_user_role]
(
	@UserID				[int],
	@RoleID				[nvarchar](50)
)
AS
	BEGIN
		INSERT INTO [dbo].[UserRole]
			([UserID], [RoleID])
		VALUES
			(@UserID, @RoleID)
	END
GO

/***************************************************************
Christopher Repko
Created: 2022/03/24

Description: 
Stored procedure to delete a user role
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_delete_user_role ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_user_role]
(
	@UserID				[int],
	@RoleID				[nvarchar](50)
)
AS
	BEGIN
		DELETE FROM 
			[dbo].[UserRole]
		WHERE
			@UserID = [UserID] AND
			@RoleID = [RoleID]
	END
GO

/***************************************************************
Christopher Repko
Created: 2022/04/27

Description: 
Stored procedure to retrieve a user by userID.
****************************************************************/

print '' print '*** creating sp_select_user_by_userID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_user_by_userID]
(
	@UserID				[int]
)
AS
	BEGIN
		SELECT 	[UserID], [GivenName], [FamilyName], [Email], [UserState],
			[City], [Zip], [Active]
		FROM 	[Users]
		WHERE 	@UserID = [UserID]
	END
GO

/***************************************************************
Christopher Repko
Created: 2022/04/27

Description:
Stored procedure to select the roles that a user has in the UserRole table.
****************************************************************/
print '' print '*** creating sp_select_user_roles ***'
GO
CREATE PROCEDURE [dbo].[sp_select_user_roles]
(
	@UserID 	[int]
)
AS
	BEGIN
		SELECT 
			[UserRole].[RoleID]
		FROM [dbo].[UserRole]
		WHERE [UserRole].[UserID] = @UserID
	END	
GO