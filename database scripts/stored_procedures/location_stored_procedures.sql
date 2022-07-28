USE [tadpole_db]
GO

/***************************************************************
Kris Howell
Created: 2022/02/03

Description:
File containing the stored procedures for locations
****************************************************************/

/***************************************************************
Kris Howell
Created: 2022/02/03

Description:
Stored procedure to select all active locations 
from the locations table
****************************************************************
Vinayak Deshpande
Updated: 2022/04/13

Description: 
modified the city and state parts
****************************************************************/
print '' print '*** creating sp_select_active_locations ***'
GO
CREATE PROCEDURE [dbo].[sp_select_active_locations]
AS
	BEGIN
		SELECT 
			[Location].[LocationID]			
			,[Location].[UserID]				
			,[Location].[LocationName]			
			,[Location].[LocationDescription]	
			,[Location].[LocationPricingText]	
			,[Location].[LocationPhone]		
			,[Location].[LocationEmail]		
			,[Location].[LocationAddress1]		
			,[Location].[LocationAddress2]		
			,[ZIP].[City]			
			,[ZIP].[States]		
			,[Location].[LocationZipCode]		
			,[Location].[LocationImagePath]	
		FROM [dbo].[Location] JOIN [dbo].[ZIP]
			ON [Location].[LocationZipCode] = [ZIP].[ZIPCode]
		WHERE [LocationActive] = 1
	END	
GO

/***************************************************************
Austin Timmerman
Created: 2022/02/02

Description:
Stored procedure to select a location by LocationID
****************************************************************
Vinayak Deshpande
Updated: 2022/04/13

Description: 
modified the city and state parts
****************************************************************/
print '' print '*** creating sp_select_location_by_locationID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_location_by_locationID]
(
	@LocationID		[int]
)
AS
	BEGIN
		SELECT 
			[Location].[UserID]				
			,[Location].[LocationName]			
			,[Location].[LocationDescription]	
			,[Location].[LocationPricingText]	
			,[Location].[LocationPhone]		
			,[Location].[LocationEmail]		
			,[Location].[LocationAddress1]		
			,[Location].[LocationAddress2]		
			,[ZIP].[City]			
			,[ZIP].[States]		
			,[Location].[LocationZipCode]		
			,[Location].[LocationImagePath]	
		FROM [dbo].[Location] JOIN [dbo].[ZIP]
			ON [Location].[LocationZipCode] = [ZIP].[ZIPCode] 
		WHERE [LocationID] = @LocationID AND [LocationActive] = 1
	END	
GO


/***************************************************************
Logan Baccam
Created: 2022/01/26

Description:
Stored procedure to retrieve Locations by locationID
****************************************************************
Vinayak Deshpande
Updated: 2022/04/13

Description: 
removed the city and state parts
****************************************************************/
print '' print '*** creating sp_select_location_by_name_and_address ***'
GO
CREATE PROCEDURE [dbo].[sp_select_location_by_name_and_address]
(
	@LocationName			[nvarchar](160),
	@LocationAddress1		[nvarchar](100)
	
)
AS
	BEGIN
		SELECT 
			[LocationID],
			[LocationName],
			[LocationDescription],
			[LocationPricingText],
			[LocationPhone],
			[LocationEmail],
			[LocationAddress1],
			[LocationActive]
		FROM
			[Location]
		WHERE @LocationName = LocationName
		AND @LocationAddress1 = LocationAddress1
		AND LocationActive = 1
	END	
GO

/***************************************************************
Logan Baccam
Created: 2022/01/22

Description:
Stored procedure to create a new Location record
**************************************************************
****************************************************************
Vinayak Deshpande
Updated: 2022/04/13

Description: 
removed the city and state parts
****************************************************************/
print '' print '*** creating sp_insert_location_by_name_address_city_state_zip ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_location_by_name_address_city_state_zip]
(
	 @LocationName			[nvarchar](160)	
	,@LocationAddress1		[nvarchar](100)	
	,@LocationCity			[nvarchar](100)
    ,@LocationState			[nvarchar](100)
	,@LocationZipCode		[nvarchar](100)
	
)
AS
	BEGIN
		INSERT INTO [dbo].[Location]
		(
		[LocationName]						
		,[LocationAddress1]	
		,[LocationZipCode]
		)
		VALUES
		(@LocationName, @LocationAddress1, @LocationZipCode)		
	END	
GO

/***************************************************************
Jace Pettinger
Created: 2022/02/22

Description:
Stored procedure to deactivate a location in the Location table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_deactivate_location_by_locationID ***'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_location_by_locationID]
(
	@LocationID 				[int]
)
AS
	BEGIN
		UPDATE	[Location]
		SET	
			[LocationActive] = 0
		WHERE
			[LocationID] = @LocationID
		RETURN @@ROWCOUNT
	END
GO

/***************************************************************
Jace Pettinger
Created: 2022/03/04

Description:
Stored procedure to deactivate a location in the Location table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_update_location_bio_by_locationID ***'
GO
CREATE PROCEDURE [dbo].[sp_update_location_bio_by_locationID]
(
	@LocationID 				[int],
	@OldDescription				[nvarchar](3000)	= NULL,
	@OldPhone					[nvarchar](15)		= NULL,
	@OldEmail					[nvarchar](250)		= NULL,
	@OldAddress1				[nvarchar](100),
	@OldAddress2				[nvarchar](100)		= NULL,
	@OldPricing					[nvarchar](3000)	= NULL,
	@NewDescription				[nvarchar](3000)	= NULL,
	@NewPhone					[nvarchar](15)		= NULL,
	@NewEmail					[nvarchar](250)		= NULL,
	@NewAddress1				[nvarchar](100),
	@NewAddress2				[nvarchar](100)		= NULL,
	@NewPricing					[nvarchar](3000)	= NULL
)
AS
	BEGIN
		UPDATE	[dbo].[Location]
		SET	
			[LocationDescription] = @NewDescription,
			[LocationPhone] = @NewPhone,
			[LocationEmail] = @NewEmail,
			[LocationAddress1] = @NewAddress1,
			[LocationAddress2] = @NewAddress2,
			[LocationPricingText] = @NewPricing
		WHERE
			[LocationID] = @LocationID
		RETURN @@ROWCOUNT
	END
GO

/**************************************************************
Logan Baccam
created 2022/04/03
Description:
Stored procedure to retrieve tags by locationID
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** sp_select_tags_by_locationID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_tags_by_locationID](
	@LocationID [int]
)
AS
	BEGIN
		select 
			[SupplierTag].[TagID]
		FROM [dbo].[SupplierTag] JOIN [dbo].[Supplier] ON [Supplier].[SupplierID] = [SupplierTag].[SupplierID]
								 JOIN [dbo].[Location] ON [Location].[UserID] = [Supplier].[UserID]
		WHERE
			[Location].[LocationID] = @LocationID
			RETURN @@ROWCOUNT
	END
GO	
