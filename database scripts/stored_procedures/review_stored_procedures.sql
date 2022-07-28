USE [tadpole_db]
GO

/***************************************************************
Austin Timmerman
Created: 2022/02/04

Description:
File containing the stored procedures for Review
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

/***************************************************************
Austin Timmerman
Created: 2022/02/04

Description:
Stored procedure to select all the reviews associated with a
location
**************************************************************
Emma Pollock
Updated: 2022/04/22

Description: 
Added UserID
****************************************************************/
print '' print '*** creating sp_select_location_reviews ***'
GO
CREATE PROCEDURE [dbo].[sp_select_location_reviews]
(
    @LocationID     [int]
)
AS
	BEGIN
		SELECT 
			[LocationReview].[ReviewID],
			[Review].[UserID],
            CONCAT([GivenName], ' ', [FamilyName]),
            [ReviewType],
            [Rating],
            [Review],
            [Review].[DateCreated],
            [Review].[Active]
		FROM [LocationReview]
        JOIN [Review] ON [LocationReview].[ReviewID] = [Review].[ReviewID]
		JOIN [Users] ON [Users].[UserID] = [Review].[UserID]
        WHERE [LocationID] = @LocationID
	END	
GO

/***************************************************************
Emma Pollock
Created: 2022/04/27

Description:
Stored procedure to insert a location review into the 
	LocationReview table.
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_insert_location_review ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_location_review]
(
	@ReviewID 		[int]
	,@LocationID 	[int]
)
AS
	BEGIN				
		INSERT INTO [dbo].[LocationReview]
		(
			[ReviewID]
			,[LocationID]
		)
		VALUES
		(
			@ReviewID
			,@LocationID
		)
	END
GO

/***************************************************************
Christopher Repko
Created: 2022/02/11

Description:
Stored procedure to select all the reviews associated with a supplier
**************************************************************
Emma Pollock
Updated: 2022/04/22

Description: 
Added UserID
****************************************************************/
print '' print '*** creating sp_select_supplier_reviews ***'
GO
CREATE PROCEDURE [dbo].[sp_select_supplier_reviews]
(
    @SupplierID     [int]
)
AS
	BEGIN
		SELECT 
			[SupplierReview].[ReviewID],
			[Review].[UserID],
            CONCAT([GivenName], ' ', [FamilyName]),
            [ReviewType],
            [Rating],
            [Review],
            [Review].[DateCreated],
            [Review].[Active]
		FROM [SupplierReview]
        JOIN [Review] ON [SupplierReview].[ReviewID] = [Review].[ReviewID]
		JOIN [Users] ON [Users].[UserID] = [Review].[UserID]
        WHERE [SupplierID] = @SupplierID
	END	
GO

/***************************************************************
Emma Pollock
Created: 2022/04/22

Description:
Stored procedure to insert a supplier review into the 
	SupplierReview table.
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_insert_supplier_review ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_supplier_review]
(
	@ReviewID 		int
	,@SupplierID 	int
)
AS
	BEGIN				
		INSERT INTO [dbo].[SupplierReview]
		(
			[ReviewID]
			,[SupplierID]
		)
		VALUES
		(
			@ReviewID
			,@SupplierID
		)
	END
GO


/***************************************************************
Austin Timmerman
Created: 2022/01/26

Description:
Stored procedure to select all reviews for the volunteers
**************************************************************
Austin Timmerman
Updated: 2022/03/10

Description: 
Updated to match the current review
****************************************************************/
print '' print '*** creating sp_select_all_volunteer_reviews ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_volunteer_reviews]
AS
	BEGIN
		SELECT 
			[VolunteerReview].[VolunteerID],
			AVG([Review].[Rating])
		FROM [VolunteerReview] JOIN [Review] ON [Review].[ReviewID] = [VolunteerReview].[ReviewID]
		GROUP BY [VolunteerID]
	END	
GO


/***************************************************************
Austin Timmerman
Created: 2022/03/10

Description:
Stored procedure to select all reviews for a volunteer by volunteerID
**************************************************************
Emma Pollock
Updated: 2022/04/22

Description: 
Added UserID
****************************************************************/
print '' print '*** creating sp_select_volunteer_reviews_by_volunteerID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_volunteer_reviews_by_volunteerID]
(
	@VolunteerID		[int]
)
AS
	BEGIN
		SELECT 
			[VolunteerReview].[ReviewID],
			[Review].[UserID],
			[Review].[Rating],
			CONCAT([GivenName], " ", [FamilyName]) AS "FullName",
			[Review].[ReviewType],		
			[Review].[Review],
			[Review].[DateCreated]
		FROM [VolunteerReview] 
		JOIN [Review] ON [Review].[ReviewID] = [VolunteerReview].[ReviewID]
		JOIN [Users] ON [Users].[UserID] = [Review].[UserID]
		WHERE [Review].[Active] = 1
			   AND [VolunteerID] = @VolunteerID 
	END	
GO

/***************************************************************
Emma Pollock
Created: 2022/04/28

Description:
Stored procedure to insert a volunteer review into the 
	VolunteerReview table.
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_insert_volunteer_review ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_volunteer_review]
(
	@ReviewID 		[int]
	,@VolunteerID 	[int]
)
AS
	BEGIN				
		INSERT INTO [dbo].[VolunteerReview]
		(
			[ReviewID]
			,[VolunteerID]
		)
		VALUES
		(
			@ReviewID
			,@VolunteerID
		)
	END
GO

/***************************************************************
Emma Pollock
Created: 2022/04/22

Description:
Stored procedure to insert a review into the review table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_insert_review ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_review]
(
	@UserID				[int]
	,@ReviewType		[nvarchar](20)
	,@Rating			[int]
	,@Review			[nvarchar](3000)
	,@DateCreated		[DateTime]
)
AS
	BEGIN
		INSERT INTO [dbo].[Review]
		(		
			[UserID]
			,[ReviewType]
			,[Rating]	
			,[Review]		
			,[DateCreated]
		)
		VALUES
		(
			@UserID			
			,@ReviewType		
			,@Rating			
			,@Review			
			,@DateCreated		
		)
	END
GO

/***************************************************************
Emma Pollock
Created: 2022/04/27

Description:
Stored procedure to retrieve the id of a specific review
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** createing sp_select_review_id_by_review ***'
GO

CREATE PROCEDURE [dbo].[sp_select_review_id_by_review]
(
	@UserID				[int]
	,@ReviewType		[nvarchar](20)
	,@Rating			[int]
	,@Review			[nvarchar](3000)
	,@DateCreated		[DateTime]
)
AS
	BEGIN
			SELECT TOP 1 ReviewID 
			FROM Review 
			WHERE UserID = @UserID
				AND ReviewType = @ReviewType
				AND Rating = @Rating
				AND Review = @Review
				AND DateCreated = @DateCreated
	END				
GO					