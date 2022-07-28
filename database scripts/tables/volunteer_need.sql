/***************************************************************
 Vinayak Deshpande
 2022/02/04
 Description:
 Holds EventID, TaskID, Number of total needed volunteers, and 
 number of currently assigned volunteers
 Dependencies:
 Needs Event Table and Task Table

***************************************************************/

USE [tadpole_db]
GO

/***************************************************************
 Vinayak Deshpande
 2022/02/04
 Description
 Volunteer Need table
***************************************************************/

print '' print '*** creating VolunteerNeed table ***'
CREATE TABLE [dbo].[VolunteerNeed] (
	[TaskID]				[int] 						NOT NULL UNIQUE,
	[NumTotalVolunteers]	[int]						NOT NULL DEFAULT 0,
	[NumCurrentVolunteers]	[int]						NOT NULL DEFAULT 0
	
	CONSTRAINT [fk_VolNeed_TaskID] FOREIGN KEY([TaskID]) REFERENCES [dbo].[Task]([TaskID]) ON DELETE CASCADE
)
GO

print '' print '*** creating test data for VolunteerNeed table ***'
GO
INSERT INTO [dbo].[VolunteerNeed]
(
	[TaskID],
	[NumTotalVolunteers],
	[NumCurrentVolunteers]
) 
	Values 
	(100000, 1, 0),
	(100001, 1, 1),
	(100002, 5, 4)

GO
