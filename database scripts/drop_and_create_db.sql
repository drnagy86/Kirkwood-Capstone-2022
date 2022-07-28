/*
Check whether the databse exists, and if so, drop it.
*/

IF EXISTS(
	SELECT 1 FROM master.dbo.sysdatabases
	WHERE name = 'tadpole_db'
)

BEGIN 
	DROP DATABASE tadpole_db
	print '' print '*** dropping database tadpole_db ***'
END
GO

print '' print '*** creating database tadpole_db ***'

GO
CREATE DATABASE tadpole_db
GO

print '' print '*** using database tadpole_db ***'


