/***************************************************************
Austin Timmerman
Created: 2022/03/02

Description:
File containing the stored procedures for Service

**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

USE [tadpole_db]
GO

/***************************************************************
Austin Timmerman
Created: 2022/03/02

Description:
Stored procedure to select all services for a supplier
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_select_services_by_supplierID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_services_by_supplierID](
	@SupplierID			[int] 
)
AS
	BEGIN
		SELECT 
			[ServiceID],					
			[ServiceName],		
			[Price],				
			[Description],		
			[ServiceImageName]
		FROM [dbo].[Service]
		WHERE [SupplierID] = @SupplierID
	END	
GO

/***************************************************************
Christopher Repko
Created: 2022/04/28

Description:
Stored procedure to insert a service
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_insert_service ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_service](
	@SupplierID			[int],
	@ServiceName			[nvarchar](160),	
	@Price					[decimal](10,2),
	@Description			[nvarchar](3000),
	@ServiceImageName		[nvarchar](200)
)
AS
		BEGIN
		INSERT INTO [dbo].[Service]
		([SupplierID], [ServiceName], [Price], [Description], [ServiceImageName])
		VALUES
		(@SupplierID, @ServiceName, @Price, @Description, @ServiceImageName)
	END	
GO

/***************************************************************
Christopher Repko
Created: 2022/04/28

Description:
Stored procedure to update a service
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_update_service ***'
GO
CREATE PROCEDURE [dbo].[sp_update_service](
	@ServiceID					[int],
	@OldSupplierID				[int],
	@OldServiceName			[nvarchar](160),	
	@OldPrice					[decimal](10,2),
	@OldDescription			[nvarchar](3000),
	@OldServiceImageName		[nvarchar](200),
	@NewSupplierID				[int],
	@NewServiceName			[nvarchar](160),	
	@NewPrice					[decimal](10,2),
	@NewDescription			[nvarchar](3000),
	@NewServiceImageName		[nvarchar](200)
)
AS
	BEGIN
		UPDATE [dbo].[Service]
			SET [SupplierID] = @NewSupplierID,
				[ServiceName] = @NewServiceName,
				[Price] = @NewPrice,
				[Description] = @NewDescription,
				[ServiceImageName] = @NewServiceImageName
			WHERE[SupplierID] = @OldSupplierID and
				[ServiceName] = @OldServiceName and
				[Price] = @OldPrice and
				([Description] = @OldDescription or [Description] is null and @OldDescription is null) and
				([ServiceImageName] = @OldServiceImageName or [ServiceImageName] is null and @OldServiceImageName is null) and
				[ServiceID] = @ServiceID
	END	
GO

/***************************************************************
Christopher Repko
Created: 2022/03/02

Description:
Stored procedure to select a service
****************************************************************/

print '' print '*** creating sp_select_service_by_ServiceID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_service_by_ServiceID](
	@ServiceID			[int] 
)
AS
	BEGIN
		SELECT 
			[SupplierID],					
			[ServiceName],		
			[Price],				
			[Description],		
			[ServiceImageName]
		FROM [dbo].[Service]
		WHERE [ServiceID] = @ServiceID
	END	
GO

/***************************************************************
Christopher Repko
Created: 2022/03/02

Description:
Stored procedure to delete a service
****************************************************************/

print '' print '*** creating sp_delete_service ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_service](
	@ServiceID			[int] 
)
AS
	BEGIN
		DELETE FROM [dbo].[Service]
		WHERE [ServiceID] = @ServiceID
	END	
GO

