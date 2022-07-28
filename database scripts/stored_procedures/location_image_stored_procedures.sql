USE [tadpole_db]
GO

/***************************************************************
Austin Timmerman
Created: 2022/02/04

Description:
File containing the stored procedures for LocationImage
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

/***************************************************************
Austin Timmerman
Created: 2022/02/04

Description:
Stored procedure to select all the images associated with a
location
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_select_location_images ***'
GO
CREATE PROCEDURE [dbo].[sp_select_location_images]
(
    @LocationID     [int]
)
AS
	BEGIN
		SELECT 
			[ImageName]
		FROM [LocationImage]
		WHERE [LocationID] = @LocationID
	END	
GO