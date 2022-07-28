/***************************************************************
Austin Timmerman
Created: 2022/03/07

Description:
File containing the SkillSet and VolunteerSkillSet table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/


USE [tadpole_db]
GO


/***************************************************************
Austin Timmerman
Created: 2022/03/07

Description:
SkillSet table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating SkillSet table ***'
CREATE TABLE [dbo].[SkillSet] (
	[SkillSetID]			[nvarchar](50)                    	NOT NULL,
	[SkillSetDescription]	[nvarchar](200) 					NOT NULL,

	CONSTRAINT [pk_SkillSetID] PRIMARY KEY([SkillSetID])
)
GO


/***************************************************************
Austin Timmerman
Created: 2022/03/07
 
 Description:
 Test records for SkillSet table
***************************************************************
 <Updater Name>
 Updated: yyyy/mm/dd

 Description: 
****************************************************************/
print '' print '*** test records for SkillSet table ***'
GO
INSERT INTO [dbo].[SkillSet] (					
    [SkillSetID],
    [SkillSetDescription]			
)VALUES 
	("Chef", "A certified chef who has earned their chef degree."),
    ("Software Developer", "A developer of software able to provide any sort of assistance for software development."),
	("Certified Driver", "A driver with their license checked and accepted who is able to drive to and from places.")
GO


/***************************************************************
Austin Timmerman
Created: 2022/03/07

Description:
VolunteerSkillSet table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating VolunteerSkillSet table ***'
CREATE TABLE [dbo].[VolunteerSkillSet] (
	[VolunteerID]			[int]              			     	NOT NULL,
	[SkillSetID]			[nvarchar](50) 						NOT NULL,

	CONSTRAINT [fk_VolunteerSkillSet_VolunteerID] FOREIGN KEY([VolunteerID]) 
		REFERENCES [Volunteer]([VolunteerID]),
	CONSTRAINT [fk_VolunteerSkillSet_SkillSetID] FOREIGN KEY([SkillSetID])
		REFERENCES [SkillSet]([SkillSetID])
)
GO


/***************************************************************
Austin Timmerman
Created: 2022/03/07
 
 Description:
 Test records for VolunteerSkillSet table
***************************************************************
 <Updater Name>
 Updated: yyyy/mm/dd

 Description: 
****************************************************************/
print '' print '*** test records for VolunteerSkillSet table ***'
GO
INSERT INTO [dbo].[VolunteerSkillSet] (					
    [VolunteerID],
	[SkillSetID]			
)VALUES 
	(100000, "Chef"),
	(100000, "Software Developer")
GO