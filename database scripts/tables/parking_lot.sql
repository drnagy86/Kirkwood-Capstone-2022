/***************************************************************
<Your Name>
Created: yyyy/mm/dd

Description:
<Decription>
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description:
<Decription> 
****************************************************************/
	
USE [tadpole_db]
GO

/***************************************************************
Derrick Nagy
Created: 2022/03/02

Description:
Table for parking lot records
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description:
<Decription> 
****************************************************************/
print '' print '*** creating ParkingLot table ***'
CREATE TABLE [dbo].[ParkingLot] (
	[ParkingLotID]		[int] IDENTITY(100000,1)	NOT NULL,
	[LocationID]		[int]						NOT NULL,
	[Name]				[nvarchar](160)				NOT NULL,
	[Description]		[nvarchar](3000)			NULL,
	[ImageName]			[nvarchar](200)				NULL,
	[Active]			[bit]						NOT NULL DEFAULT 1
	CONSTRAINT [fk_LocationID_ParkingLot] FOREIGN KEY([LocationID])
		REFERENCES [Location]([LocationID])
	CONSTRAINT [pk_ParkingLotID] PRIMARY KEY([ParkingLotID])
)
GO


/***************************************************************
Derrick Nagy
Created: 2022/03/02

Description:
Insert sample records into ParkingLot table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description:
<Decription> 
****************************************************************/
print '' print '*** test records for the ParkingLot table ***'
GO
INSERT INTO [dbo].[ParkingLot] (	
	[LocationID]		
	,[Name]				
	,[Description]		
	,[ImageName]
)
VALUES 
	-- "Parking Lot" by jgrimm is marked with CC BY-NC-ND 2.0. To view the terms, visit https://creativecommons.org/licenses/by-nd-nc/2.0/jp/?ref=openverse
	-- https://live.staticflickr.com/170/431697830_a680ecbc13_b.jpg
	(100000, 'LRU Parking Lot A', 'Parking for the north entrance', 'test-parkinglot-1.jpg')
	
	-- "Parking Lot" by pennuja is marked with CC BY 2.0. To view the terms, visit https://creativecommons.org/licenses/by/2.0/?ref=openverse
	-- https://live.staticflickr.com/6124/5948174831_02f4291285_b.jpg
	,(100000, 'LRU Parking Lot B', 'Parking for the south entrance', 'test-parkinglot-2.jpg')
	
	-- "The Parking Lot" by aldenjewell is marked with CC BY 2.0. To view the terms, visit https://creativecommons.org/licenses/by/2.0/?ref=openverse
	-- https://live.staticflickr.com/7281/16598201447_397e2a30b4_b.jpg
	,(100001, 'Testy Parking', 'Street parking', 'test-parkinglot-3.jpg')
	
	,(100002, 'Lincolnway Park Parking', 'Street parking', 'lincolnway-park-street-parking.jpg')
	,(100003, 'The Hotel at Kirkwood', 'Parking lot map', 'the-hotel-at-kirkwood-parking.jpg')
	
GO