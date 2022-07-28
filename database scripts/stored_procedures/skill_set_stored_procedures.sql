/***************************************************************
Austin Timmerman
Created: 2022/03/07

Description:
File containing the stored procedures for Skill Sets

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
Stored procedure to select all skills for a volunteer
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_select_skill_set_by_volunteerID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_skill_set_by_volunteerID](
	@VolunteerID			[int] 
)
AS
	BEGIN
		SELECT 
			[VolunteerSkillSet].[SkillSetID],
			[SkillSet].[SkillSetDescription]
		FROM [dbo].[VolunteerSkillSet] JOIN [SkillSet] ON [VolunteerSkillSet].[SkillSetID] = [SkillSet].[SkillSetID]
		WHERE [VolunteerID] = @VolunteerID
	END	
GO