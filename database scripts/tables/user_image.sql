/***************************************************************
Austin Timmerman
Created: 2022/03/05

Description:
File containing the UserImage table
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
UserImage table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating UserImage table ***'
CREATE TABLE [dbo].[UserImage] (
	[ImageID]			[int] IDENTITY(100000,1)  	NOT NULL,
	[UserID]			[int]						NOT NULL,
	[UserImage]			[nvarchar](200) 			NOT NULL,
	[DateCreated]		[Datetime]					NOT NULL,

	CONSTRAINT [fk_UserImage_UserID] FOREIGN KEY([UserID])
        REFERENCES [Users]([UserID])
)
GO


/***************************************************************
Austin Timmerman
Created: 2022/03/07
 
 Description:
 Test records for UserImage table
***************************************************************
 <Updater Name>
 Updated: yyyy/mm/dd

 Description: 
****************************************************************/
print '' print '*** test records for UserImage table ***'
GO
INSERT INTO [dbo].[UserImage] (					
    [UserID],	
    [UserImage],	
    [DateCreated]
)VALUES 
	/* Image is license free; https://unsplash.com/photos/3pVPHn0QN8g */
	(100000, "f515b7f6-a7f5-4bd5-b489-75d78e6a185f.jpg", GETDATE())
GO
