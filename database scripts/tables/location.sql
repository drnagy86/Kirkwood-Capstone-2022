/***************************************************************
Kris Howell
Created: 2022/02/03

Description:
File containing the location table
****************************************************************/

USE [tadpole_db]
GO

/***************************************************************
Kris Howell
Created: 2022/02/03

Description:
Location table
****************************************************************
Vinayak Deshpande
Updated: 2022/04/13

Description: 
removed the city and state parts
****************************************************************/

print '' print '*** creating Location table ***'
CREATE TABLE [dbo].[Location] (
	[LocationID]			[int] IDENTITY(100000,1)	NOT NULL
	,[UserID]				[int]						NULL
	,[LocationName]			[nvarchar](160)				NOT NULL
	,[LocationDescription]	[nvarchar](3000)			NULL
	,[LocationPricingText]	[nvarchar](3000)			NULL
	,[LocationPhone]		[nvarchar](15)				NULL
	,[LocationEmail]		[nvarchar](250)				NULL
	,[LocationAddress1]		[nvarchar](100)				NOT NULL
	,[LocationAddress2]		[nvarchar](100)				NULL
	,[LocationZipCode]		[nvarchar](100)				NOT NULL
	,[LocationImagePath]	[nvarchar](200)				NULL
	,[LocationActive]		[bit]						NOT NULL DEFAULT 1
	

	CONSTRAINT [pk_LocationID] PRIMARY KEY([LocationID])
	,CONSTRAINT [fk_UserID_Location] FOREIGN KEY([UserID])
		REFERENCES [Users]([UserID])
	,CONSTRAINT [ak_LocationAddress1] UNIQUE([LocationAddress1])
	,CONSTRAINT [fk_ZIPCode_Location] FOREIGN KEY([LocationZipCode]) REFERENCES [ZIP]([ZIPCode])
)
GO

/***************************************************************
Kris Howell
Created: 2022/02/03
 
Description:
Test records for location table
**************************************************************
Derrick Nagy
Updated: 2022/03/02

Description:
Added Lincolnway park and The Hotel at Kirkwood
****************************************************************
Derrick Nagy
Updated: 2022/03/24

Description: 
Added more test locations
****************************************************************
Vinayak Deshpande
Updated: 2022/04/13

Description: 
Removed the city and state parts
****************************************************************/
print '' print '*** test records for Location table ***'
GO
INSERT INTO [dbo].[Location] (
	[UserID]
	,[LocationName]
	,[LocationDescription]
	,[LocationPricingText]
	,[LocationPhone]		
	,[LocationEmail]		
	,[LocationAddress1]		
	,[LocationAddress2]				
	,[LocationZipCode]		
	,[LocationImagePath]
)VALUES 
	-- LocationID 100000
	(100000, "Locations R Us", "I'm a Locations R Us kid.", "5 bucks a night.", "888-888-8888", "locationsrus@locations.com", "123 Location Ave", null, "52404", "http://imagehost.com/locationsrus.png"),
	-- LocationID 100001
	(100000, "Test Y 2", "This is a Testing Facility.", "Contact for Quote", "888-883-8888", "test@locations.com", "123 Test Ave", null, "52404", "http://imagehost.com/testy.png"),
	-- LocationID 100002
	(100001, 'Lincolnway Park', 'Park in Cedar Rapids, Iowa', 'Free', null, NULL, 'J St SW', null, '52404', 'lincolnway-park.jpg'),
	-- LocationID 100003
	(100002, 'The Hotel at Kirkwood', 'Hotel and Conference Center', 'Request prices', '319-848-8700', 'hotel@kirkwood.edu', '7725 Kirkwood Blvd SW', null, '52404', 'the-hotel-at-kirkwood.jpg'),
		-- LocationID 100004
	(100002, 'Downtown Iowa City', 'The pedmall and old capital area downtown Iowa City', 'Request prices', '319-848-1231', 'icity@iowa.gov', ' 201 S Clinton St', null, '52240', NULL),
		-- LocationID 100005
	(100002, 'Mason City', 'City hall area', 'Request prices', '641-421-3600', 'mcity@iowa.gov', '1st St NW', null, '50401', NULL)
GO

