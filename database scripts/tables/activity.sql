
/***************************************************************
Emma Pollock
Created: 2022/01/31

Description:
File containing the activity table
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
Activity table
**************************************************************
Vinayak Deshpande
Updated: 2022/03/14

Description: Changed sublocationID field to be nullable.
**************************************************************
Kris Howell
Updated: 2022/03/24

Description: Changed sublocationID field to be non-nullable.
****************************************************************/
print '' print '*** creating Activity table ***'
CREATE TABLE [dbo].[Activity] (
	[ActivityID]			[int] IDENTITY(100000,1)	NOT NULL
	,[ActivityName]			[nvarchar](50)				NOT NULL
	,[ActivityDescription]	[nvarchar](250)				NULL
	,[PublicActivity]		[bit]						NOT NULL DEFAULT 1
	,[StartTime]			[time](0)					NOT NULL
	,[EndTime]				[time](0)					NOT NULL
	,[ActivityImageName]	[nvarchar](25)				NULL
	,[SublocationID]		[int]						NULL
	,[EventDateID]			[date]						NOT NULL
	,[EventID]				[int]						NOT NULL

	CONSTRAINT [pk_ActivityID] PRIMARY KEY([ActivityID])
	,CONSTRAINT [fk_AcitivitySublocation] FOREIGN KEY([SublocationID])
		REFERENCES [dbo].[Sublocation] ([SublocationID]) ON UPDATE CASCADE
	,CONSTRAINT [fk_AcitivityEventDate] FOREIGN KEY([EventDateID], [EventID])
		REFERENCES [dbo].[EventDate] ([EventDateID], [EventID]) ON UPDATE CASCADE
	,CONSTRAINT [fk_ActivityEvent] FOREIGN KEY([EventID])
		REFERENCES [dbo].[Event] ([EventID])
)
GO

/***************************************************************
 Emma Pollock
 Created: 2022/02/02
 
 Description:
 Test records for activities
***************************************************************
 Derrick Nagy
 Updated: 2022/02/10

 Description: 
 Changed the dates and times on sample EventDate records which caused
 a conflict with the foreign key restraints. Changed the sample records.
****************************************************************/
print '' print '*** test records for Activities ***'

GO
INSERT INTO [dbo].[Activity] (		
	[ActivityName]			
	,[ActivityDescription]	
	,[PublicActivity]		
    ,[StartTime]			
    ,[EndTime]					
    ,[SublocationID]		
    ,[EventDateID]			
    ,[EventID]				
)VALUES 
/*
	('Activity 1', 'The first activity', 1, '08:30', '20:30', 100000, '2022-01-29', 100000)
	,('Activity 2', 'The second activity', 1, '10:30', '12:30', 100001, '2022-01-30', 100000)
	,('Activity 3', 'The third activity', 1, '11:00', '18:45', 100000, '2022-01-31', 100000)
	,('Activity 4', 'The fourth activity', 1, '06:15', '10:45', 100002, '2022-04-29', 100001)
	,('Activity 5', 'The fith activity', 1, '12:00', '11:00', 100003, '2022-06-01', 100002)
	,('Activity 6', 'The sixth activity', 1, '12:30', '11:00', 100001, '2022-06-02', 100002)
	
*/
	('Caber Toss', 'Competitors launching 20-foot logs as far as they can.', 1, '08:30', '20:30', 100000, '2022-06-29', 100000)
	,('Tug o'' War', 'Teams competing to tug a rope in opposite directions.', 1, '08:30', '20:30', 100001, '2022-06-30', 100000)
	,('Hammer Throw', 'Competitors throwing a special hammer as far as they can.', 1, '11:00', '18:45', 100000, '2022-06-28', 100000)
	,('Park Cleanup', 'Come and join us to clean up trash in the local park.', 1, '06:15', '10:45', 100002, '2022-04-29', 100001)
	,('Live Concert', '<band name here> live in concert', 1, '10:00', '11:00', 100003, '2022-05-01', 100002)
	,('Catering', 'Caters will be brought in to provide snacks for event participants.', 1, '10:00', '11:00', 100001, '2022-05-02', 100002)
	,('Apple Bobbing', 'Stick your head in a barrel full of apples to try to pull one out with your teeth.', 1, '8:00', '23:15', 100001, '2021-07-24', 100008)
	
GO