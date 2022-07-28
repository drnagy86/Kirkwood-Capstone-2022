/***************************************************************
Christopher Repko
Created: 2022/02/11

Description:
File containing the SupplierImage table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/


USE [tadpole_db]
GO


/***************************************************************
Christopher Repko
Created: 2022/02/11

Description:
SupplierImage table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating SupplierImage table ***'
CREATE TABLE [dbo].[SupplierImage] (
	[SupplierID]		[int]                    	NOT NULL,
	[ImageName]			[nvarchar](200) 			NOT NULL,

	CONSTRAINT [fk_SupplierImage_SupplierID] FOREIGN KEY([SupplierID])
        REFERENCES [Supplier]([SupplierID])
)
GO


/***************************************************************
Christopher Repko
Created: 2022/02/11
 
 Description:
 Test records for SupplierImage table
***************************************************************
 <Updater Name>
 Updated: yyyy/mm/dd

 Description: 
****************************************************************/
print '' print '*** test records for SupplierImage table ***'
GO
INSERT INTO [dbo].[SupplierImage] (					
    [SupplierID],
    [ImageName]			
)VALUES 
	(100000, "7263a839-3428-49f2-b26f-875d3811ef85.jpg"),
    (100000, "lincolnway-park.jpg"),
	(100002, "grilled-cheese-sandwich-sq.jpg")
GO
