USE [tadpole_db]
GO

/***************************************************************
Vinayak Deshpande
Created: 2022/04/13

Description:
File containing the stored procedures for ZIPCodes
****************************************************************/

print '' print '*** creating sp_select_all_zip_codes ***'
GO
Create Procedure [dbo].[sp_select_all_zip_codes]
AS
	Begin
		Select
			[ZIPCode],
			[City],
			[States]
			From [dbo].[ZIP]
	End
GO

/***************************************************************
Vinayak Deshpande
Created: 2022/04/13

Description:
stored procedure to return city and state from zip code
****************************************************************/

print '' print '*** creating sp_select_city_and_states_by_zipcode ***'
GO
Create Procedure [dbo].[sp_select_city_and_states_by_zipcode]
(
	@ZIPCode [nvarchar](100)
)
AS
	Begin
		Select
			[City],
			[States]
			From [dbo].[ZIP]
			WHERE [ZIPCode] = @ZIPCode
	End
GO