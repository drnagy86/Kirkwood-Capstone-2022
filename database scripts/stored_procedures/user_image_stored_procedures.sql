/***************************************************************
Austin Timmerman
Created: 2022/03/07

Description:
File containing the stored procedures for User Images

**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

USE [tadpole_db]
GO

/***************************************************************
Austin Timmerman
Created: 2022/03/07

Description:
Stored procedure to select all images for a user by UserID
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_select_userimages_by_userID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_userimages_by_userID](
	@UserID			[int] 
)
AS
	BEGIN
		SELECT 
			[ImageID],		
			[UserImage],	
			[DateCreated]
		FROM [dbo].[UserImage]
		WHERE [UserID] = @UserID
	END	
GO