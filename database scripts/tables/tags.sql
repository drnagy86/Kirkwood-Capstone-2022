/***************************************************************
Christopher Repko
Created: 2022/02/11

Description:
File to contain Tag and Tag-join tables
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description:
<Decription> 
****************************************************************/
	
USE [tadpole_db]
GO

/***************************************************************
 Christopher Repko
 Created: 2022/02/11
 
 Description:
 Create Tag table
***************************************************************
 <Updater Name>
 Updated: yyyy/mm/dd

 Description: 
****************************************************************/
print '' print '*** creating Tag table ***'
CREATE TABLE [dbo].[Tag] (
	[TagID]			[nvarchar](50)
	
	CONSTRAINT [pk_TagID] PRIMARY KEY([TagID])
)
GO

/***************************************************************
 Christopher Repko
 Created: 2022/02/11
 
 Description:
 Create Tag table sample data
***************************************************************
 <Updater Name>
 Updated: yyyy/mm/dd

 Description: 
****************************************************************/
print '' print '*** creating Tag table sample data ***'
INSERT INTO [dbo].[Tag] (
	[TagID]
	) VALUES 
	("Food"),
	("Fast Food"),
	("Delivered")
GO
	
/***************************************************************
 Christopher Repko
 Created: 2022/02/11
 
 Description:
 Create SupplierTag table
***************************************************************
 Christopher Repko
 Updated: 2022/02/17

 Description: Added dependencies that were missed previously.
***************************************************************
 Kris Howell
 Updated: 2022/02/17

 Description: Corrected fk_SupplierTagTagID to create foreign key TagID.
****************************************************************/
print '' print '*** creating SupplierTag table ***'
CREATE TABLE [dbo].[SupplierTag] (
	[TagID]			[nvarchar](50),
	[SupplierID]	[int]
	CONSTRAINT [pk_SupplierTagID] PRIMARY KEY([TagID], [SupplierID])
	,CONSTRAINT [fk_SupplierTagSupplierID] FOREIGN KEY([SupplierID])
		REFERENCES [Supplier]([SupplierID])
	,CONSTRAINT [fk_SupplierTagTagID] FOREIGN KEY([TagID])
		REFERENCES [Tag]([TagID])
)
GO

/***************************************************************
 Christopher Repko
 Created: 2022/02/11
 
 Description:
 Create SupplierTag table sample data
***************************************************************
 <Updater Name>
 Updated: yyyy/mm/dd

 Description: 
****************************************************************/
print '' print '*** creating SupplierTag table sample data ***'
INSERT INTO [dbo].[SupplierTag] (
	[TagID],
	[SupplierID]
	) VALUES 
	("Food", 100000),
	("Fast Food", 100000),
	("Delivered", 100000)
GO