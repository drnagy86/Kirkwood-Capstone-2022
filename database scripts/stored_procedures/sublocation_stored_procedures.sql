/***************************************************************
Emma Pollock
Created: 2022/02/03

Description:
File containing the stored procedures for Sublocations

**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

USE [tadpole_db]
GO

/***************************************************************
Logan Baccam
Created: 2022/03/29

Description:
Stored procedure to insert a new Sublocation by locationID
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_insert_sublocation_by_locationID ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_sublocation_by_locationID](
	@LocationID				[int],
	@SublocationName		[nvarchar](160),
	@SublocationDescription	[nvarchar](1000)
)
AS
	BEGIN
		INSERT INTO [dbo].[Sublocation](
			[LocationID],
			[SublocationName],
			[SublocationDescription]
		)VALUES(
			@LocationID,
			@SublocationName,
			@SublocationDescription
		)
	END	
GO

/***************************************************************
Emma Pollock
Created: 2022/02/02

Description:
Stored procedure to select a specific Sublocation
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_select_sublocation_by_sublocationID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_sublocation_by_sublocationID](
	@SublocationID			[int] 
)
AS
	BEGIN
		SELECT 			
			[SublocationName]			
			,[SublocationDescription]	
			,[Active]	
			,[LocationID]			
		FROM [dbo].[Sublocation]
		WHERE [SublocationID] = @SublocationID
	END	
GO


/***************************************************************
Christopher Repko
Created: 2022/02/24

Description:
Stored procedure to select a list of sublocations linked to a location
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_select_sublocations_by_locationID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_sublocations_by_locationID](
	@LocationID			[int] 
)
AS
	BEGIN
		SELECT 			
			[SublocationName]			
			,[SublocationDescription]	
			,[Active]	
			,[SublocationID]			
		FROM [dbo].[Sublocation]
		WHERE [LocationID] = @LocationID AND
			[Active] = 1
	END	
GO

/***************************************************************
Christopher Repko
Created: 2022/03/03

Description:
Stored procedure to update a sublocation record
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_update_sublocation ***'
GO
CREATE PROCEDURE [dbo].[sp_update_sublocation](
	@SublocationID				[int],
	@OldLocationID				[int],
	@OldSublocationName			[nvarchar](160),
	@OldSublocationDescription	[nvarchar](1000),
	@NewLocationID				[int],
	@NewSublocationName			[nvarchar](160),
	@NewSublocationDescription	[nvarchar](1000)
	
)
AS
	BEGIN
		UPDATE [dbo].[Sublocation]
			SET [SublocationName] = @NewSublocationName,
				[SublocationDescription] = @NewSublocationDescription,
				[LocationID] = @NewLocationID
		WHERE [LocationID] = @OldLocationID AND
			[SublocationName] = @OldSublocationName AND
			[SublocationDescription] = @OldSublocationDescription
	END	
GO

/***************************************************************
Christopher Repko
Created: 2022/03/03

Description:
Stored procedure to update a sublocation record
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_deactivate_sublocation ***'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_sublocation](
	@SublocationID				[int]
)
AS
	BEGIN
		UPDATE [dbo].[Sublocation]
			SET [Active] = 0
		WHERE [SublocationID] = @SublocationID
	END	
GO