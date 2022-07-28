
/***************************************************************
Emma Pollock
Created: 2022/01/31

Description:
File containing the sublocation table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/


USE [tadpole_db]
GO


/***************************************************************
Emma Pollock
Created: 2022/01/31

Description:
Sublocation table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating Sublocation table ***'
GO
CREATE TABLE [dbo].[Sublocation] (
	[SublocationID]				[int] IDENTITY(100000,1)	NOT NULL
	,[LocationID] 				[int]						NOT NULL
	,[SublocationName]			[nvarchar](160)				NOT NULL
	,[SublocationDescription]	[nvarchar](1000)			NULL
	,[Active]					[bit]						NOT NULL DEFAULT 1

	CONSTRAINT [pk_SublocationID] PRIMARY KEY([SublocationID])
	,CONSTRAINT [fk_Sublocation_Location] FOREIGN KEY([LocationID])
		REFERENCES [dbo].[Location] ([LocationID]) ON UPDATE CASCADE
)
GO

/***************************************************************
 Emma Pollock
 Created: 2022/02/02
 
 Description:
 Test records for sublocation
***************************************************************
Vinayak Deshpande
 Updated: 2022/05/04

 Description: Added more details and more sublocations
****************************************************************/
print '' print '*** test records for Sublocations ***'
GO
INSERT INTO [dbo].[Sublocation] (
	[LocationID] 				
	,[SublocationName]			
	,[SublocationDescription]						
	
)VALUES 
	(100000, 'Kids Area', 'A good place for childrens activities.')
	,(100000, 'Teenagers Area', 'An emotionally neutral area for teenagers to faff about.')
	,(100000, 'Adults Area', 'A good place for activities for Adults who have left their dependents in the other areas.')
	,(100000, 'Seniors Party Room', 'Perfect area for Seniors to play bingo.')
	,(100001, 'Test Bus', 'A Mobile Testing Room')
	,(100001, 'Test Room A', 'A Medium Sized room with a full suite of testing equipment.')
	,(100001, 'Test Room B', 'A Small sized room with limited testing equipment.')
	,(100001, 'Test Lounge', 'A fully equiped break room at the testing facility.')
	,(100002, 'Play Structure', 'Standard playground play structure.')
	,(100002, 'Gazebo A', 'A medium sized gazebo.')
	,(100002, 'Grass Field A', 'A large football field sized grassy area.')
	,(100003, 'Ballroom', 'At 7,454 square feet, the Ballroom is our largest venue, and can accommodate 700 theatre style or up to 550 at rounds. It features digital ceiling mounted projectors with four strategically positioned 16 x 10-foot screens.')
	,(100003, 'Tippe Business Event Room', 'This 1,517 square-foot space seats 54 classroom-style or up to 90 at rounds. The ultimate business meeting room, it features leather ergonomic chairs, a floor-to-ceiling pocketed white board, a ceiling-mounted digital projector and screen, a document camera and pushpin surface walls.')
	,(100003, 'Culinary Labs', 'Each Culinary Lab space contains eight two-person commercial Vulcan cooking stations equipped with six-burner gas cooking ranges. The space also contains four each of Vulcan deep fryers, griddles, char broilers and one salamander. There are five flat-screen television monitors, a multi-purpose ingredient station and hundreds of small kitchen necessities. This space is perfect for Culinary Team Building events.')
	,(100003, 'Executive Technology Amphitheatre', 'With 2,470 square feet of space, this venue accommodates 104 for a classroom-style seating. Five flat-screen LCD television screens help the audience see every detail, making it perfect for cooking demonstrations, wine tastings or theatre presentations.')
	,(100003, 'The Hotel Board Room', 'Located in the hotel lobby, this 368-square-foot room includes high-end leather ergonomic seating for 8, a 42-inch flat-screen TV, and whiteboard.')
	,(100003, 'The Hotel Conference Room', 'This 632-square-foot space is situated in the hotel lobby and offers high-end flexible seating for 16, as well as a 60-inch flat-screen TV, and whiteboard.')
	,(100003, 'Pre-Function Area', 'Our Pre-function Area is 4,100 square feet, and is a beautifully appointed space for receptions and gatherings of up to 300 guests.')
	,(100003, 'Breakout Rooms', 'The Hotel at Kirkwood offers six breakout rooms, ranging from 686 – 820 square feet. They provide classroom-style seating for 30 or up to 50 at rounds. Each also includes ceiling-mounted projectors, and screens.')
	,(100004, 'Sidewalk', 'It is a section of sidewalk')
	,(100005, 'Conference Room', 'A medium sized conference room')
	,(100005, 'Lobby', 'The building lobby')
GO