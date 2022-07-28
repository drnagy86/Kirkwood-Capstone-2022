/***************************************************************
Austin Timmerman
Created: 2022/02/09

Description:
File containing the availability table
**************************************************************
Kris Howell
Updated: 2022/03/03

Description: 
Restructuring for weekly availability + exceptions
****************************************************************/


USE [tadpole_db]
GO


/***************************************************************
Austin Timmerman
Created: 2022/01/22

Description:
Availability table
**************************************************************
Kris Howell
Updated: 2022/03/03

Description: 
Restructuring for weekly availability
NOTE FOR INPUT VALIDATION FOR INSERT -- 
	Must validate that at least one day is selected
****************************************************************/

print '' print '*** creating Availability table ***'
CREATE TABLE [dbo].[Availability] (
	[AvailabilityID]		[int]IDENTITY(100000,1)		NOT NULL,
	[TimeStart]				[time]						NOT NULL,
	[TimeEnd]				[time]						NOT NULL,
	[Sunday]				[bit] 						NOT NULL,
	[Monday]				[bit] 						NOT NULL,
	[Tuesday]				[bit] 						NOT NULL,
	[Wednesday]				[bit] 						NOT NULL,
	[Thursday]				[bit] 						NOT NULL,
	[Friday]				[bit] 						NOT NULL,
	[Saturday]				[bit] 						NOT NULL,

	CONSTRAINT [pk_Availability_AvailabilityID] PRIMARY KEY([AvailabilityID])
)
GO

/***************************************************************
 Austin Timmerman
 Created: 2022/02/09
 
 Description:
 Test records for availability dates
***************************************************************
 Kris Howell
 Updated: 2022/03/03

 Description: 
 Updating for new weekly availability structure
***************************************************************
 Derrick Nagy
 Updated: 2022/04/5

 Description: 
Added test record ('10:30', '23:59', 1, 0, 1, 1, 1, 1, 1)
***************************************************************
 Derrick Nagy
 Updated: 2022/04/14

 Description: 
 merge conflict between. Both added
 	('10:30', '23:59', 1, 0, 1, 1, 1, 1, 1),
	('7:30', '22:00', 0, 0, 1, 0, 1, 1, 0)

****************************************************************/
print '' print '*** test records for weekly Availability ***'
GO
INSERT INTO [dbo].[Availability] (
	[TimeStart],
	[TimeEnd],
	[Sunday],
	[Monday],
	[Tuesday],
	[Wednesday],
	[Thursday],
	[Friday],
	[Saturday]
)VALUES 
	('11:30', '21:00', 0, 1, 1, 1, 1, 0, 0),
	('11:30', '23:00', 1, 0, 0, 0, 0, 1, 1),
	('08:30', '11:30', 1, 0, 0, 0, 0, 0, 1),
    ('9:30', '19:00', 0, 1, 1, 1, 1, 1, 0),
	--Id 100004 - 10:30am to midnight everyday except monday
	('10:30', '23:59', 1, 0, 1, 1, 1, 1, 1),
	-- Merge conflict here.
	('7:30', '22:00', 0, 0, 1, 0, 1, 1, 0)
GO

/***************************************************************
Kris Howell
Created: 2022/03/03

Description:
AvailabilityException table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating AvailabilityException table ***'
CREATE TABLE [dbo].[AvailabilityException] (
	[ExceptionID]			[int]IDENTITY(100000,1)		NOT NULL,
	[ExceptionDate]			[date]						NOT NULL,
	[TimeStart]				[time]						NULL,
	[TimeEnd]				[time]						NULL,

	CONSTRAINT [pk_AvailabilityException_ExceptionID] PRIMARY KEY([ExceptionID])
)
GO

/***************************************************************
 Kris Howell
 Created: 2022/03/03
 
 Description:
 Test records for availability exception dates
***************************************************************
 Derrick Nagy
 Updated: 2022/04/14
 
 Description:
 	Merge conflict for additional records
	('2022-05-18', NULL, NULL),
	('2022-05-13', '11:00', '13:00')
	('2022-03-02', null, null),
	-- merge conflict
	('2022-03-03', '22:00', '23:00'),
	('2022-04-02', '22:00', '23:00'),
	('2022-06-29', '7:00', '23:00')
****************************************************************/
print '' print '*** test records for Availability Exceptions ***'
GO
INSERT INTO [dbo].[AvailabilityException] (
	[ExceptionDate],
	[TimeStart],
	[TimeEnd]
)VALUES 
	('2022-03-04', '8:45', '13:00'),
	('2022-03-03', null, null),
	('2022-03-02', '06:30', '8:30'),
	('2022-03-02', '22:00', '23:00'),
	-- merge conflict start 2022-04-14
	('2022-05-18', NULL, NULL),
	('2022-05-13', '11:00', '13:00'),
	('2022-03-02', null, null),
	-- merge conflict 2022-04-14
	('2022-03-03', '22:00', '23:00'),
	('2022-04-02', '22:00', '23:00'),
	('2022-06-29', '7:00', '23:00')
	-- merge conflict end 2022-04-14
GO


/***************************************************************
Austin Timmerman
Created: 2022/01/22

Description:
LocationAvailability table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating LocationAvailability table ***'
CREATE TABLE [dbo].[LocationAvailability] (
	[LocationID]			[int]						NOT NULL,
	[AvailabilityID]		[int] 						NOT NULL,

	CONSTRAINT [fk_LocationAvailability_LocationID] FOREIGN KEY([LocationID])
		REFERENCES [dbo].[Location]([LocationID])
		ON DELETE CASCADE,
	CONSTRAINT [fk_LocationAvailability_AvailabilityID] FOREIGN KEY([AvailabilityID])
		REFERENCES [dbo].[Availability]([AvailabilityID])
		ON DELETE CASCADE
)
GO

/***************************************************************
 Austin Timmerman
 Created: 2022/02/09
 
 Description:
 Test records for locationavailability dates
***************************************************************
 Kris Howell
 Updated: 2022/03/03

 Description: 
 Update test records to new availability structure
****************************************************************/
print '' print '*** test records for LocationAvailability ***'
GO
INSERT INTO [dbo].[LocationAvailability] (	
	[LocationID],
	[AvailabilityID]
)VALUES 
	(100000, 100003),
	(100003, 100003)
GO

/***************************************************************
Kris Howell
Created: 2022/03/03

Description:
LocationAvailabilityException table
****************************************************************/

print '' print '*** creating LocationAvailabilityException table ***'
CREATE TABLE [dbo].[LocationAvailabilityException] (
	[LocationID]			[int]						NOT NULL,
	[ExceptionID]			[int] 						NOT NULL,

	CONSTRAINT [fk_LocationAvailabilityException_LocationID] FOREIGN KEY([LocationID])
		REFERENCES [dbo].[Location]([LocationID])
		ON DELETE CASCADE,
	CONSTRAINT [fk_LocationAvailabilityException_ExceptionID] FOREIGN KEY([ExceptionID])
		REFERENCES [dbo].[AvailabilityException]([ExceptionID])
		ON DELETE CASCADE
)
GO

/***************************************************************
 Kris Howell
 Created: 2022/03/03
 
 Description:
 Test records for LocationAvailabilityException dates
****************************************************************/
print '' print '*** test records for LocationAvailabilityException ***'
GO
INSERT INTO [dbo].[LocationAvailabilityException] (	
	[LocationID],
	[ExceptionID]
)VALUES 
	(100000, 100000),
	(100003, 100000)
GO

/***************************************************************
Kris Howell
Created: 2022/03/03

Description:
SupplierAvailability table
****************************************************************/

print '' print '*** creating SupplierAvailability table ***'
CREATE TABLE [dbo].[SupplierAvailability] (
	[SupplierID]			[int]						NOT NULL,
	[AvailabilityID]		[int] 						NOT NULL,

	CONSTRAINT [fk_SupplierAvailability_SupplierID] FOREIGN KEY([SupplierID])
		REFERENCES [dbo].[Supplier]([SupplierID])
		ON DELETE CASCADE,
	CONSTRAINT [fk_SupplierAvailability_AvailabilityID] FOREIGN KEY([AvailabilityID])
		REFERENCES [dbo].[Availability]([AvailabilityID])
		ON DELETE CASCADE
)
GO

/***************************************************************
 Kris Howell
 Created: 2022/03/03
 
 Description:
 Test records for SupplierAvailability
****************************************************************/
print '' print '*** test records for SupplierAvailability ***'
GO
INSERT INTO [dbo].[SupplierAvailability] (	
	[SupplierID],
	[AvailabilityID]
)VALUES 
	(100000, 100000),
	(100000, 100001),
	(100000, 100002),
	(100002, 100004)
	
	
GO

/***************************************************************
Kris Howell
Created: 2022/03/03

Description:
SupplierAvailabilityException table
****************************************************************/

print '' print '*** creating SupplierAvailabilityException table ***'
CREATE TABLE [dbo].[SupplierAvailabilityException] (
	[SupplierID]			[int]						NOT NULL,
	[ExceptionID]			[int] 						NOT NULL,

	CONSTRAINT [fk_SupplierAvailabilityException_SupplierID] FOREIGN KEY([SupplierID])
		REFERENCES [dbo].[Supplier]([SupplierID])
		ON DELETE CASCADE,
	CONSTRAINT [fk_SupplierAvailabilityException_ExceptionID] FOREIGN KEY([ExceptionID])
		REFERENCES [dbo].[AvailabilityException]([ExceptionID])
		ON DELETE CASCADE
)
GO

/***************************************************************
 Kris Howell
 Created: 2022/03/03
 
 Description:
 Test records for SupplierAvailabilityException dates
****************************************************************/
print '' print '*** test records for SupplierAvailabilityException ***'
GO
INSERT INTO [dbo].[SupplierAvailabilityException] (	
	[SupplierID],
	[ExceptionID]
)VALUES 
	(100000, 100000),
	(100000, 100001),
	(100000, 100002),
	(100000, 100003),
	(100002, 100004),
	(100002, 100005)	
GO

-------------------------------------------------------------------------------------------

/***************************************************************
Austin Timmerman
Created: 2022/03/29

Description:
VolunteerAvailability table
****************************************************************/

print '' print '*** creating VolunteerAvailability table ***'
CREATE TABLE [dbo].[VolunteerAvailability] (
	[VolunteerID]			[int]						NOT NULL,
	[AvailabilityID]		[int] 						NOT NULL,

	CONSTRAINT [fk_VolunteerAvailability_VolunteerID] FOREIGN KEY([VolunteerID])
		REFERENCES [dbo].[Volunteer]([VolunteerID])
		ON DELETE CASCADE,
	CONSTRAINT [fk_VolunteerAvailability_AvailabilityID] FOREIGN KEY([AvailabilityID])
		REFERENCES [dbo].[Availability]([AvailabilityID])
		ON DELETE CASCADE
)
GO

/***************************************************************
Austin Timmerman
 Created: 2022/03/29
 
 Description:
 Test records for VolunteerAvailability
****************************************************************/
print '' print '*** test records for VolunteerAvailability ***'
GO
INSERT INTO [dbo].[VolunteerAvailability] (	
	[VolunteerID],
	[AvailabilityID]
)VALUES 
	(100000, 100004)
GO

/***************************************************************
Kris Howell
Created: 2022/03/03

Description:
VolunteerAvailabilityException table
****************************************************************/

print '' print '*** creating VolunteerAvailabilityException table ***'
CREATE TABLE [dbo].[VolunteerAvailabilityException] (
	[VolunteerID]			[int]						NOT NULL,
	[ExceptionID]			[int] 						NOT NULL,

	CONSTRAINT [fk_VolunteerAvailabilityException_SupplierID] FOREIGN KEY([VolunteerID])
		REFERENCES [dbo].[Volunteer]([VolunteerID])
		ON DELETE CASCADE,
	CONSTRAINT [fk_VolunteerAvailabilityException_ExceptionID] FOREIGN KEY([ExceptionID])
		REFERENCES [dbo].[AvailabilityException]([ExceptionID])
		ON DELETE CASCADE
)
GO

/***************************************************************
 Austin Timmerman
 Created: 2022/03/29
 
 Description:
 Test records for VolunteerAvailabilityException dates
****************************************************************/
print '' print '*** test records for VolunteerAvailabilityException ***'
GO
INSERT INTO [dbo].[VolunteerAvailabilityException] (	
	[VolunteerID],
	[ExceptionID]
)VALUES 
	(100000, 100004),
	(100000, 100005),
	(100000, 100006),
	(100000, 100007)
GO