
/***************************************************************
Emma Pollock
Created: 2022/02/02

Description:
File containing the ActivityResult table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/


USE [tadpole_db]
GO


/***************************************************************
Emma Pollock
Created: 2022/02/02

Description:
ActivityResult table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating ActivityResult table ***'
CREATE TABLE [dbo].[ActivityResult] (
	[ActivityResultRank]		[int]			NOT NULL
	,[ActivityID]				[int]			NOT NULL
	,[ActivityResultName]		[nvarchar](50)	NULL

	CONSTRAINT [pk_ActivityResultRank_ActivityID] PRIMARY KEY([ActivityResultRank],[ActivityID])
	,CONSTRAINT [fk_ActivityResult_ActivityID] FOREIGN KEY([ActivityID])
		REFERENCES [dbo].[Activity] ([ActivityID])
)
GO

/***************************************************************
 Emma Pollock
 Created: 2022/02/03
 
 Description:
 Test records for activity result
***************************************************************
 <Updater Name>
 Updated: yyyy/mm/dd

 Description: 
****************************************************************/
print '' print '*** test records for ActivityResult ***'
GO
INSERT INTO [dbo].[ActivityResult] (
	[ActivityResultRank]
	,[ActivityID]
	,[ActivityResultName]
)VALUES 
	(1, 100000, 'John Smith'),
	(2, 100000, 'Jane Doe'),
	(3, 100000, 'Bill Williams'),
	(1, 100001, 'Jill'),
	(2, 100001, 'Mario'),
	(3, 100001, 'Toni'),
	(4, 100001, 'Greg'),
	(5, 100001, 'Jared'),
	(1, 100002, 'Team 4')
GO