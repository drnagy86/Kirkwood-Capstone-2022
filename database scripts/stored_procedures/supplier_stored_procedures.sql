USE [tadpole_db]
GO

/***************************************************************
Kris Howell
Created: 2022/01/27

Description:
File containing the stored procedures for suppliers
****************************************************************/


/**************************************************************
Logan Baccam
created 2022/04/20
Description:
Stored procedure to insert a new supplier
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_insert_supplier ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_supplier]
(
	@UserID					[int]
	,@SupplierName			[nvarchar](160)	
	,@SupplierDescription	[nvarchar](3000)	
	,@SupplierPhone			[nvarchar](15)	
	,@SupplierEmail			[nvarchar](250)	
	,@SupplierAddress1		[nvarchar](100)	
	,@SupplierCity			[nvarchar](100)
    ,@SupplierState			[nvarchar](100)
	,@SupplierZipCode		[nvarchar](100)
	
)
AS
	BEGIN
		INSERT INTO [dbo].[Supplier]
		(
		[UserID]
		,[SupplierName]		
		,[SupplierDescription]
		,[SupplierPhone]		
		,[SupplierEmail]	
		,[SupplierAddress1]	
		,[SupplierCity]		
		,[SupplierState]		
		,[SupplierZipCode]
		)
		VALUES
		(
		@UserID
		,@SupplierName		
		,@SupplierDescription
		,@SupplierPhone		
		,@SupplierEmail		
		,@SupplierAddress1	
		,@SupplierCity		
		,@SupplierState		
		,@SupplierZipCode
		)		
	END
	GO

/***************************************************************
Kris Howell
Created: 2022/01/27

Description:
Stored procedure to select all active suppliers 
from the suppliers table
****************************************************************
Kris Howell
Updated: 2022/02/18

Description:
Add city, state, and zip code to select
****************************************************************/
print '' print '*** creating sp_select_active_suppliers ***'
GO
CREATE PROCEDURE [dbo].[sp_select_active_suppliers]
AS
	BEGIN
		SELECT 
			[SupplierID]
			,[UserID]
			,[SupplierName]
			,[SupplierDescription]
			,[SupplierPhone]
			,[SupplierEmail]
			,[SupplierTypeID]
			,[SupplierAddress1]
			,[SupplierAddress2]
			,[SupplierCity]
			,[SupplierState]
			,[SupplierZipCode]
		FROM [dbo].[Supplier]
		WHERE [Active] = 1	AND
			[Approved] = 1
	END	
GO

/***************************************************************
Christopher Repko
Created: 2022/02/11

Description:
Stored procedure to select a supplier's tags 
from the suppliers table
****************************************************************/
print '' print '*** creating sp_select_supplier_tags ***'
GO
CREATE PROCEDURE [dbo].[sp_select_supplier_tags]
(
    @SupplierID     [int]
)
AS
	BEGIN
		SELECT 
			[TagID]
		FROM [dbo].[SupplierTag]
		WHERE [SupplierID] = @SupplierID	
	END	
GO

/***************************************************************
Christopher Repko
Created: 2022/02/11

Description:
Stored procedure to select a supplier's images 
from the suppliers table
****************************************************************/
print '' print '*** creating sp_select_supplier_images ***'
GO
CREATE PROCEDURE [dbo].[sp_select_supplier_images]
(
    @SupplierID     [int]
)
AS
	BEGIN
		SELECT 
			[ImageName]
		FROM [dbo].[SupplierImage]
		WHERE [SupplierID] = @SupplierID	
	END	
GO



/***************************************************************
Logan Baccam
Created: 2022/04/03

Description:
stored procedure to retrieve a supplier by id

****************************************************************
Christopher Repko
Updated: 2022/04/27

Description
Added missing UserID field to table.
****************************************************************/
print '' print '*** creating sp_select_supplier_by_supplierID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_supplier_by_supplierID]
(
    @SupplierID     [int]
)
AS
	BEGIN
		SELECT 
			[SupplierID],
			[UserID],
			[SupplierName],
			[SupplierDescription],
			[SupplierPhone],
			[SupplierEmail],
			[SupplierTypeID],
			[SupplierAddress1],
			[SupplierAddress2],
			[SupplierCity],
			[SupplierState],
			[SupplierZipCode],
			[Approved]
		FROM [dbo].[Supplier]
		WHERE [SupplierID] = @SupplierID	
	END	
GO

/***************************************************************
Christopher Repko
Created: 2022/04/22

Description:
Stored procedure to select all unapproved suppliers 
from the suppliers table
****************************************************************/
print '' print '*** creating sp_select_unapproved_suppliers ***'
GO
CREATE PROCEDURE [dbo].[sp_select_unapproved_suppliers]
AS
	BEGIN
		SELECT 
			[SupplierID]
			,[UserID]
			,[SupplierName]
			,[SupplierDescription]
			,[SupplierPhone]
			,[SupplierEmail]
			,[SupplierTypeID]
			,[SupplierAddress1]
			,[SupplierAddress2]
			,[SupplierCity]
			,[SupplierState]
			,[SupplierZipCode]
		FROM [dbo].[Supplier]
		WHERE [Approved] is null
	END	
GO

/***************************************************************
Christopher Repko
Created: 2022/04/22

Description:
Stored procedure to approve a supplier 
from the suppliers table
****************************************************************/
print '' print '*** creating sp_approve_supplier ***'
GO
CREATE PROCEDURE [dbo].[sp_approve_supplier]
(
	@SupplierID 			[int]
)
AS
	BEGIN
		UPDATE [dbo].[Supplier]
		SET		
			[Approved] = 1
		WHERE 	
			[SupplierID] = @SupplierID
		RETURN @@ROWCOUNT
	END	
GO

/***************************************************************
Christopher Repko
Created: 2022/04/22

Description:
Stored procedure to disapprove a supplier 
from the suppliers table
****************************************************************/
print '' print '*** creating sp_disapprove_supplier ***'
GO
CREATE PROCEDURE [dbo].[sp_disapprove_supplier]
(
	@SupplierID 			[int]
)
AS
	BEGIN
		UPDATE [dbo].[Supplier]
		SET		
			[Approved] = 0
		WHERE 	
			[SupplierID] = @SupplierID
		RETURN @@ROWCOUNT
	END	
GO

/***************************************************************
Christopher Repko
Created: 2022/04/22

Description:
Stored procedure to disapprove a supplier 
from the suppliers table
****************************************************************/
print '' print '*** creating sp_requeue_supplier_application ***'
GO
CREATE PROCEDURE [dbo].[sp_requeue_supplier_application]
(
	@SupplierID 			[int]
)
AS
	BEGIN
		UPDATE [dbo].[Supplier]
		SET		
			[Approved] = null
		WHERE 	
			[SupplierID] = @SupplierID
		RETURN @@ROWCOUNT
	END	
GO

/***************************************************************
Christopher Repko
Created: 2022/04/29

Description:
Stored procedure to select all suppliers belonging to a single user.
****************************************************************/
print '' print '*** creating sp_select_suppliers_by_userID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_suppliers_by_userID](
	@UserID [int]
)
AS
	BEGIN
		SELECT 
			[SupplierID]
			,[UserID]
			,[SupplierName]
			,[SupplierDescription]
			,[SupplierPhone]
			,[SupplierEmail]
			,[SupplierTypeID]
			,[SupplierAddress1]
			,[SupplierAddress2]
			,[SupplierCity]
			,[SupplierState]
			,[SupplierZipCode]
			,[Approved]
		FROM [dbo].[Supplier]
		WHERE [UserID] = @UserID	
	END	
GO
/***************************************************************
Christopher Repko
Created: 2022/04/29

Description:
Stored procedure to update a supplier
from the suppliers table
****************************************************************/
print '' print '*** creating sp_update_supplier ***'
GO
CREATE PROCEDURE [dbo].[sp_update_supplier]
(
	@SupplierID 				[int],
	@OldSupplierName			[nvarchar](160),
	@OldSupplierDescription		[nvarchar](3000),
	@OldSupplierPhone			[nvarchar](15),
	@OldSupplierEmail			[nvarchar](250),
	@OldSupplierAddress1		[nvarchar](100),
	@OldSupplierAddress2		[nvarchar](100),
	@OldSupplierCity			[nvarchar](100),
	@OldSupplierState			[nvarchar](100),
	@OldSupplierZipCode			[nvarchar](100),
	@NewSupplierName			[nvarchar](160),
	@NewSupplierDescription		[nvarchar](3000),
	@NewSupplierPhone			[nvarchar](15),
	@NewSupplierEmail			[nvarchar](250),
	@NewSupplierAddress1		[nvarchar](100),
	@NewSupplierAddress2		[nvarchar](100),
	@NewSupplierCity			[nvarchar](100),
	@NewSupplierState			[nvarchar](100),
	@NewSupplierZipCode			[nvarchar](100)	
	
)
AS
	BEGIN
		UPDATE [dbo].[Supplier]
		SET		
			[SupplierName] = @NewSupplierName,
			[SupplierDescription] = @NewSupplierDescription,
			[SupplierPhone] = @NewSupplierPhone,
			[SupplierEmail] = @NewSupplierEmail,
			[SupplierAddress1] = @NewSupplierAddress1,
			[SupplierAddress2] = @NewSupplierAddress2,
			[SupplierCity] = @NewSupplierCity,
			[SupplierState] = @NewSupplierState,
			[SupplierZipCode] = @NewSupplierZipCode
		WHERE 	
			[SupplierName] = @OldSupplierName and
			([SupplierDescription] = @OldSupplierDescription or [SupplierDescription] is null and @OldSupplierDescription is null) and
			[SupplierPhone] = @OldSupplierPhone and
			[SupplierEmail] = @OldSupplierEmail and
			[SupplierAddress1] = @OldSupplierAddress1 and
			([SupplierAddress2] = @OldSupplierAddress2 or [SupplierAddress2] is null and @OldSupplierAddress2 is null) and
			[SupplierCity] = @OldSupplierCity and
			[SupplierState] = @OldSupplierState and
			[SupplierZipCode] = @OldSupplierZipCode
		RETURN @@ROWCOUNT
	END	
GO