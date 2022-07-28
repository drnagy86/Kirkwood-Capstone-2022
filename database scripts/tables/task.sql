USE [tadpole_db]
GO

/***************************************************************
/ Mike Cahow
/ Created: 2022/01/22
/ 
/ Description: Creating the Priority table
/
***************************************************************
/ Mike Cahow
/ Updated: 2022/01/23
/
/ Description: Creating Sample Priority Records
****************************************************************/


print '' print '*** creating Priority table ***'
CREATE TABLE [dbo].[Priority] (
	[PriorityID]		[int] IDENTITY(1,1)				NOT NULL,
	[Description]		[nvarchar](10)					NOT NULL,
	CONSTRAINT [pk_PriorityID] PRIMARY KEY ([PriorityID])
)
GO

print '' print '*** test records for Priority table ***'
INSERT INTO [dbo].[Priority]
			([Description])
		VALUES
			('High'),
			('Medium'),
			('Low')
GO

/***************************************************************
/ Mike Cahow
/ Created: 2022/01/22
/ 
/ Description:
/ Creating Table for Task, user story 1023_Task List Create
****************************************************************
/ Emma Pollock
/ Updated: 2022/03/09
/
/ Description: 
/ Removed TaskAssignmentID field so that a Task can have multiple
/ 	TaskAssignments
****************************************************************/


print '' print '*** creating Task table ***'
CREATE TABLE [dbo].[Task] (
	[TaskID]			[int] IDENTITY(100000, 1)		NOT NULL,
	[Name]				[nvarchar](50)					NOT NULL,
	[Description]		[nvarchar](255)					NULL,
	[DueDate]			[datetime]						NULL
									DEFAULT GETDATE(),
	[Priority]			[int]							NOT NULL,
	[CompletionDate]	[datetime]						NULL,
	[ProofID]			[int]							NULL,
	[IsDone]			[bit]							NOT NULL
									DEFAULT 0,
	[EventID]			[int]							NOT NULL,
	[Active]			[bit]							NOT NULL
									DEFAULT 1,
	CONSTRAINT [fk_Priority_PriorityID] FOREIGN KEY ([Priority])
		REFERENCES [dbo].[Priority] ([PriorityID]),
	CONSTRAINT [fk_Event_EventID] FOREIGN KEY ([EventID])
		REFERENCES [dbo].[Event] ([EventID]),
	CONSTRAINT [pk_TaskID] PRIMARY KEY([TaskID])
)
GO

/***************************************************************
/ Mike Cahow
/ Created: 2022/01/31
/ 
/ Description: Creating Task Table records
/
***************************************************************
/ <Updater Name>
/ Updated: yyyy/mm/dd
/
/ Description: 
****************************************************************/

print '' print '*** creating sample Task records ***'
GO
INSERT INTO [dbo].[Task]
		([Name], [Description], [Priority], [EventID])
	VALUES
		('Mop', 'Mop up a spilled drink in the bathroom', 2, 100000),
		('Sweep', 'Sweep up some broken glass by staging area', 1, 100000),
		('Wipe Tables', 'Wipe down tables after lunch ends', 3, 100000)
GO

/***************************************************************
/ Mike Cahow
/ Created: 2022/01/22
/ 
/ Description: Creating the TaskAssignment table
/
***************************************************************
/ Emma Pollock
/ Updated: 2022/03/09
/
/ Description: 
/ Changed RoleID from int to nvarchar and added foreign key 
/ 	references for UserID and RoleID
****************************************************************/


print '' print '*** creating TaskAssignment Table ***'
CREATE TABLE [dbo].[TaskAssignment] (
	[TaskAssignmentID]	[int] IDENTITY(100000, 1)		NOT NULL,
	[DateAssigned]		[datetime]						NOT NULL
									DEFAULT GETDATE(),
	[TaskID]			[int]							NOT NULL,
	[UserID]			[int]							NULL,
	[RoleID]			[nvarchar](50)					NULL,
	CONSTRAINT [fk_Task_TaskID] FOREIGN KEY ([TaskID])
		REFERENCES [dbo].[Task] ([TaskID]) ON DELETE CASCADE,
	CONSTRAINT [fk_User_UserID] FOREIGN KEY ([UserID])
		REFERENCES [dbo].[Users] ([UserID]),
	CONSTRAINT [fk_Role_RoleID] FOREIGN KEY ([RoleID])
		REFERENCES [dbo].[Role] ([RoleID]),
	CONSTRAINT [pk_TaskAssignmentID] PRIMARY KEY ([TaskAssignmentID])
)
GO

/***************************************************************
/ Emma Pollock
/ Created: 2022/03/09
/ 
/ Description: Creating TaskAssignment Table records
/
***************************************************************
/ <Updater Name>
/ Updated: yyyy/mm/dd
/
/ Description: 
****************************************************************/

print '' print '*** creating sample TaskAssignment records ***'
GO
INSERT INTO [dbo].[TaskAssignment]
		([DateAssigned], [TaskID], [UserID], [RoleID])
	VALUES
		('2022-01-30', 100000, 100000, 'Event Planner')
GO

/***************************************************************
/ Mike Cahow
/ Created: 2022/01/22
/ 
/ Description: Creating the TaskSortTag table
/
***************************************************************
/ <Updater Name>
/ Updated: yyyy/mm/dd
/
/ Description: 
****************************************************************/

print '' print '*** creating TaskSortTag table ***'
CREATE TABLE [dbo].[TaskSortTag] (
	[TaskSortTagID]		[int] IDENTITY(100000, 1)		NOT NULL,
	[Task]				[int]							NOT NULL,
	[Name]				[nvarchar](250)					NOT NULL,
	CONSTRAINT [fk_Task_Task] FOREIGN KEY ([Task])
		REFERENCES [dbo].[Task] ([TaskID]),
	CONSTRAINT [pk_TaskSortTagID] PRIMARY KEY ([TaskSortTagID])
)
GO


