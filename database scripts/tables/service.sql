/***************************************************************
Austin Timmerman
Created: 2022/03/02

Description:
File containing the service table
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
Service table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating Service table ***'
CREATE TABLE [dbo].[Service] (
	[ServiceID]				[int] IDENTITY(100000,1)	NOT NULL,
	[SupplierID]			[int]						NOT NULL,
	[ServiceName]			[nvarchar](160)				NOT NULL,
	[Price]					[decimal](10,2)				NOT NULL,
	[Description]			[nvarchar](3000)			NULL,
	[ServiceImageName]		[nvarchar](200)				NULL,

	CONSTRAINT [pk_ServiceID] PRIMARY KEY([ServiceID]),
	CONSTRAINT [fk_Service_SupplierID] FOREIGN KEY([SupplierID])
		REFERENCES [dbo].[Supplier] ([SupplierID]) ON UPDATE CASCADE ON DELETE CASCADE
)
GO

/***************************************************************
 Austin Timmerman
 Created: 2022/03/02
 
 Description:
 Test records for service
***************************************************************
 Derrick Nagy
 Updated: 2022/04/05

 Description: 
 Added test records:
 	('100002', 'Catering', 15.00, "Catering for an event. Price is approximate price per person, minimum 10 people. Choose from menu ahead of time.", null),
	('100002', 'Food cart', 25.00, "Food cart to be available at the location. Price per hour. Food not included.", null),
	('100002', 'Food cart with food included', 200.00, "Food cart to be available at the location. Full price for food for 10 people. Avaliable for one hour.", null)
****************************************************************/
print '' print '*** test records for Service ***'

GO
INSERT INTO [dbo].[Service] (					
    [SupplierID],		
    [ServiceName],		
    [Price],				
    [Description],		
    [ServiceImageName]
)VALUES 
	('100000', 'Burgers', 10.32, "Just the burgers", "burder.jpg"),
	('100000', 'Buns', 100.71, "Only top buns", "buns.jpg"),
	('100000', 'Potatoes', 6.35, "Raw potatoes", "potatoes.jpg"),
	('100001', 'Air Frying', 1.35, "Fry something with less oil", "airfyer.jpg"),
	('100001', 'Grilling Cheese', 3.30, "Large Industrial Cheese Grilling", "grilled-cheese-sandwich-sq.jpg"),
	('100000', 'Lettuce', 11.30, "Frozen Iceburg Lettuce", "lettuce.jpg"),
	('100000', 'Enchiladas', 4.10, "Authentic Frozen Enchiladas", "enchiladas.jpg"),
	('100002', 'Catering', 15.00, "Catering for an event. Price is approximate price per person, minimum 10 people. Choose from menu ahead of time.", "caterer.jpg"),
	('100002', 'Food cart', 25.00, "Food cart to be available at the location. Price per hour. Food not included.", "foodcart.jpg"),
	('100002', 'Food cart with food included', 200.00, "Food cart to be available at the location. Full price for food for 10 people. Avaliable for one hour.", "foodcart.jpg")
	
GO