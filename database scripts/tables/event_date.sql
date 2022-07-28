
/***************************************************************
Derrick Nagy
Created: 2022/01/22

Description:
File containing the event table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/


USE [tadpole_db]
GO


/***************************************************************
Derrick Nagy
Created: 2022/01/29

Description:
Event Date table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating Event Date ***'
CREATE TABLE [dbo].[EventDate] (
	[EventDateID]		[Date]						NOT NULL 
	,[EventID]			[int] 						NOT NULL
	,[StartTime]		[Time](0)					NOT NULL 
	,[EndTime]			[Time](0)					NOT NULL 
	,[Active]			[bit]						NOT NULL DEFAULT 1

	CONSTRAINT [pk_EventDateID_EventID] PRIMARY KEY([EventDateID],[EventID])
	, CONSTRAINT [fk_EventDate_EventID] FOREIGN KEY([EventID])
		REFERENCES [dbo].[Event]([EventID])
)
GO

/***************************************************************
 Derrick Nagy
 Created: 2022/01/29
 
 Description:
 Test records for event dates
***************************************************************
 Derrick Nagy
 Updated: 2202/02/06

 Description: 
 Added more dates for testing past dates
****************************************************************/
print '' print '*** test records for Event Dates ***'
GO
INSERT INTO [dbo].[EventDate] (
	[EventDateID]
	,[EventID]
	,[StartTime]
	,[EndTime]
)VALUES 
	('2022-06-29', 100000, '08:30', '20:30')	
	,('2022-06-30', 100000, '08:30', '20:30')	
	,('2022-06-28', 100000, '08:30', '20:30')	
	,('2022-04-29', 100001, '06:15', '10:45')
	,('2022-05-01', 100002, '10:00', '11:00')
	,('2022-05-02', 100002, '10:00', '11:00')
	,('2022-07-11', 100003, '8:00', '11:00')
	,('2022-06-01', 100004, '11:00', '23:15')
	,('2022-06-02', 100004, '11:00', '23:15')
	,('2022-06-03', 100004, '11:00', '23:15')
	,('2022-08-15', 100005, '9:30', '19:30')
	,('2021-07-24', 100008 ,'8:00', '23:15')
GO
