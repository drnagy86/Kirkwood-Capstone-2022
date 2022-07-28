/***************************************************************
Kris Howell
Created: 2022/01/27

Description:
File containing the supplier table
****************************************************************/

USE [tadpole_db]
GO

/***************************************************************
Kris Howell
Created: 2022/01/27

Description:
SupplierTypeID table
****************************************************************/

print '' print '*** creating SupplierType table ***'
CREATE TABLE [dbo].[SupplierType] (
	[SupplierTypeID]		[nvarchar](10)				NOT NULL
	,[Description]			[nvarchar](500)				NOT NULL

	CONSTRAINT [pk_SupplierTypeID] PRIMARY KEY([SupplierTypeID])
)
GO

/***************************************************************
Kris Howell
Created: 2022/01/27
 
Description:
Test records for supplier type table
***************************************************************
 Derrick Nagy
 Updated: 2022/04/05

 Description: 
 Added test records:
	("Catering", "Supplier that will offer catering services")
****************************************************************/
print '' print '*** test records for SupplierType table ***'
GO
INSERT INTO [dbo].[SupplierType] (
	[SupplierTypeID]
	,[Description]
)VALUES 
	("Vendor", "Supplier looking to set up at an event and sell their goods/services"),
	("Catering", "Supplier that will offer catering services")
	
GO



/***************************************************************
Kris Howell
Created: 2022/01/27

Description:
Supplier table
****************************************************************
Kris Howell
Updated: 2022/02/18

Description:
Add City, State, and Zip.  
Address1 and Address2 pair must be unique.
****************************************************************/

print '' print '*** creating Supplier table ***'
CREATE TABLE [dbo].[Supplier] (
	[SupplierID]			[int] IDENTITY(100000,1)	NOT NULL
	,[UserID]				[int]						NOT NULL
	,[SupplierName]			[nvarchar](160)				NOT NULL
	,[SupplierDescription]	[nvarchar](3000)			NULL
	,[SupplierPhone]		[nvarchar](15)				NOT NULL
	,[SupplierEmail]		[nvarchar](250)				NOT NULL
	,[SupplierTypeID]		[nvarchar](10)				NULL
	,[SupplierAddress1]		[nvarchar](100)				NOT NULL
	,[SupplierAddress2]		[nvarchar](100)				NULL
	,[SupplierCity]			[nvarchar](100)				NOT NULL
	,[SupplierState]		[nvarchar](100)				NOT NULL
	,[SupplierZipCode]		[nvarchar](100)				NOT NULL
	,[Active]				[bit]						NOT NULL DEFAULT 1
	,[Approved]				[bit]						NULL
	

	CONSTRAINT [pk_SupplierID] PRIMARY KEY([SupplierID])
	,CONSTRAINT [fk_SupplierTypeID] FOREIGN KEY([SupplierTypeID])
		REFERENCES [SupplierType]([SupplierTypeID])
	,CONSTRAINT [fk_UserID] FOREIGN KEY([UserID])
		REFERENCES [Users]([UserID])
	,CONSTRAINT [ak_SupplierAddress] UNIQUE([SupplierAddress1],[SupplierAddress2])
)
GO

/***************************************************************
Kris Howell
Created: 2022/01/27
 
Description:
Test records for supplier table
****************************************************************
Kris Howell
Updated: 2022/02/18

Description:
Added City, State, and ZipCode fields
Added new test supplier
****************************************************************
Derrick 
Updated: 2022/05/01

Description:
Changed approval on Marco's Grilled Cheese to approved
****************************************************************/
print '' print '*** test records for Supplier table ***'
GO
INSERT INTO [dbo].[Supplier] (
	[UserID]
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
)VALUES 
	(100000, "McSupplier", "I'm liking it.", "999-999-9999", "mcsupplier@suppliers.com", "Vendor", "123 McSupplier Lane", null, "Cedar Rapids", "Iowa", "52404", 1)
	,(100000, "Supplier King", "Supply it your way.", "888-888-8888", "supplierking@suppliers.com", "Vendor", "456 Supplier King Blvd", null, "Iowa City", "Iowa", "52240", NULL)
	,(100003, "Marco's Grilled Cheese", "Iowa City's Finest Food carts, and now also a restaurant", "123-456-7894", "marcosgrilledcheese@gmail.com", "Catering", "17 N Linn St", null, "Iowa City", "Iowa", "52245", 1)
	
	
GO

/***************************************************************
Kris Howell
Created: 2022/02/24

Description:
SupplierAttendance table
****************************************************************/

print '' print '*** creating SupplierAttendance table ***'
CREATE TABLE [dbo].[SupplierAttendance] (
	[SupplierID]			[int]						NOT NULL
	,[ActivityID]			[int]						NOT NULL

	CONSTRAINT [pk_SupplierAttendance] PRIMARY KEY([SupplierID], [ActivityID])
	,CONSTRAINT [fk_Activity] FOREIGN KEY([ActivityID])
		REFERENCES [Activity]([ActivityID])
)
GO

/***************************************************************
Kris Howell
Created: 2022/02/24
 
Description:
Test records for supplier attendance table
****************************************************************/
print '' print '*** test records for SupplierAttendance table ***'
GO
INSERT INTO [dbo].[SupplierAttendance] (
	[SupplierID]
	,[ActivityID]
)VALUES 
	(100000, 100000)
	,(100000, 100001)
	,(100000, 100002)
	,(100000, 100003)
	,(100000, 100004)
	,(100000, 100005)
GO