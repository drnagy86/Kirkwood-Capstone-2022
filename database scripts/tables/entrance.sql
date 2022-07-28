USE [tadpole_db]
GO

/***************************************************************
 Alaina Gilson
 Created: 2022/02/25
 
 Description: 
 Creating the Entrance table
****************************************************************/

print '' print '*** creating Entrance table ***'
CREATE TABLE [dbo].[Entrance] (
	[EntranceID]		[int]IDENTITY(100000,1)			NOT NULL,
	[LocationID]		[int]							NOT NULL,
	[EntranceName]		[nvarchar](100)					NOT NULL,
	[Description]		[nvarchar](255)					NULL,
	[Active]			[bit] DEFAULT 1					NOT NULL
	CONSTRAINT [fk_LocationID_Entrance] FOREIGN KEY([LocationID])
		REFERENCES [Location]([LocationID])
)
GO

/***************************************************************
 Alaina Gilson
 Created: 2022/02/25
 
 Description: 
 Test records for Entrance table
****************************************************************/

print '' print '*** test records for Entrance table ***'
GO
INSERT INTO [dbo].[Entrance] (
	[LocationID],
	[EntranceName],
	[Description]
)
VALUES 
	(100000, 'Backdoor', 'A place for volunteers and suppliers to enter'),
	(100000, 'Front door', 'For the average attendee')
GO