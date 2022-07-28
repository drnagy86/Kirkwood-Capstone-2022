USE [tadpole_db]
GO

/***************************************************************
/ Ramiro Pena
/ Created: 2022/01/24
/ 
/ Description: Creating Users Table
***************************************************************
/ Updates:
/ Derrick Nagy
/ Updated: 2022/02/07
/
/ Description: 
/ Added the test records for "Finn" and "River"
***************************************************************
/ Updates:
/ Derrick Nagy
/ Updated: 2022/04/05
/
/ Description: 
/ Added the test records for Mark Paterno
/
/ Christopher Repko
/ Updated: 2022/02/07
/
/ Description: 
/ Changed default password

Vinayak Deshpande
 Updated: 2022/04/08

 Description: Added user test data
****************************************************************/
print '' print '*** creating Users Table ***'

GO
CREATE TABLE [dbo].[Users](
	[UserID]			[int] IDENTITY(100000,1)	NOT NULL,
	[GivenName]			[nvarchar](50)				NOT NULL,
	[FamilyName]		[nvarchar](50)				NOT NULL,
	[Email]				[nvarchar](250)				NOT NULL,
	[PasswordHash]		[nvarchar](100)				NOT NULL DEFAULT 
		'b03ddf3ca2e714a6548e7495e2a03f5e824eaac9837cd7f159c67b90fb4b7342',
	[UserState] 		[char](2) 					NULL,
	[City]				[nvarchar](75) 				NULL,
	[Zip]				[int]						NULL,
	[UserPhoto]			[nvarchar](200)				NULL,
	[UserDescription]	[nvarchar](3000)			NULL,
	[Active]			[bit]						NOT NULL DEFAULT 1,
	[DateCreated]		[DateTime]					NOT NULL DEFAULT GETDATE(),
	CONSTRAINT [pk_UserID] PRIMARY KEY([UserID] ASC),
	CONSTRAINT [ak_Email] UNIQUE([Email] ASC)
)
GO

print '' print '*** adding sample user records ***'
GO
INSERT INTO [dbo].[Users]
		([GivenName], [FamilyName], [Email], [UserState], [City], [Zip], [UserPhoto], [UserDescription])
	VALUES
		('Joanne', 'Smith', 'joanne@company.com', 'IA', 'Cedar Rapids', 52402, NULL, "A jack of all trades, Joanne keeps herself busy throughout the week with all of the roles she fulfills. Whatever you need, Joanne is sure to get the job done.")
		,('Finn', 'Human', 'finn@company.com', 'IA', 'Coralville', 52241, NULL, NULL)
		,('River', 'Blueberry Rainbow', 'river@company.com', 'IA', 'Boone', 50036, NULL, NULL)

		-- merge conflict
		,('Mark', 'Paterno', 'marcosgrilledcheese@fake.com', 'IA', 'Iowa City', 52245, NULL, NULL)
		
		-- merge conflict
		,('Nicholas', 'Hart', 'nicholas@company.com', 'IA', 'Cedar Rapids', 52402, Null, Null)
		,('Alan', 'Graham', 'alan@company.com', 'IA', 'Coralville', 52241, NULL, NULL)
		,('Sarah', 'Greene', 'sarah@company.com', 'IA', 'Coralville', 52241, NULL, NULL)
		,('Madeleine', 'Fraser', 'madeleine@company.com', 'IA', 'Cedar Rapids', 52402, NULL, NULL)
		,('Elizabeth', 'Blake', 'elizabeth@company.com', 'IA', 'Coralville', 52241, NULL, NULL)
		,('Sally', 'King', 'sally@company.com', 'IA', 'Cedar Rapids', 52402, NULL, NULL)
		,('Adrian', 'Skinner', 'adrian@company.com', 'IA', 'Coralville', 52241, NULL, NULL)
		,('Lauren', 'Bower', 'lauren@company.com', 'IA', 'Cedar Rapids', 52402, NULL, NULL)
		,('Joe', 'Arnold', 'joe@company.com', 'IA', 'Coralville', 52241, NULL, NULL)
		,('Irene', 'Rutherford', 'irene@company.com', 'IA', 'Cedar Rapids', 52402, NULL, NULL)
		,('Sean', 'Morrison', 'sean@company.com', 'IA', 'Coralville', 52241, NULL, NULL)
GO
