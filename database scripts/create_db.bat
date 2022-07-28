rem server is localhost
ECHO off

sqlcmd -S localhost -E -i drop_and_create_db.sql
sqlcmd -S localhost -E -i tables/user.sql
sqlcmd -S localhost -E -i tables/zip.sql
sqlcmd -S localhost -E -i tables/location.sql
sqlcmd -S localhost -E -i tables/event.sql
sqlcmd -S localhost -E -i tables/event_date.sql
sqlcmd -S localhost -E -i tables/role.sql
sqlcmd -S localhost -E -i tables/volunteer.sql
sqlcmd -S localhost -E -i tables/task.sql
sqlcmd -S localhost -E -i tables/sublocation.sql
sqlcmd -S localhost -E -i tables/activity.sql
sqlcmd -S localhost -E -i tables/activity_result.sql
sqlcmd -S localhost -E -i tables/volunteer_request.sql
sqlcmd -S localhost -E -i tables/supplier.sql
sqlcmd -S localhost -E -i tables/user_role.sql
sqlcmd -S localhost -E -i tables/user_event.sql
sqlcmd -S localhost -E -i tables/user_activity.sql
sqlcmd -S localhost -E -i tables/location_image.sql
sqlcmd -S localhost -E -i tables/review.sql
sqlcmd -S localhost -E -i tables/availability.sql
sqlcmd -S localhost -E -i tables/tags.sql
sqlcmd -S localhost -E -i tables/supplier_image.sql
sqlcmd -S localhost -E -i tables/service.sql
sqlcmd -S localhost -E -i tables/entrance.sql
sqlcmd -S localhost -E -i tables/user_image.sql
sqlcmd -S localhost -E -i tables/skill_set.sql
:: added 2022-03-02
sqlcmd -S localhost -E -i tables/volunteer_need.sql
sqlcmd -S localhost -E -i tables/parking_lot.sql
sqlcmd -S localhost -E -i stored_procedures/event_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/event_date_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/user_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/supplier_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/task_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/location_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/volunteer_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/activity_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/sublocation_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/activity_result_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/volunteer_request_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/location_image_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/review_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/availability_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/service_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/entrance_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/skill_set_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/user_image_stored_procedures.sql
:: added 2022-03-02
sqlcmd -S localhost -E -i stored_procedures/parking_lot_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/volunteer_need_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/zipcode_stored_procedures.sql

rem list depenecies after this line:
rem task.sql requires event.sql, user.sql, and role.sql
rem activity.sql requires event.sql, event_date.sql, and sublocation.sql 
rem activity_result.sql requires activity.sql
rem supplier.sql requires user.sql
rem location.sql requires user.sql
rem event.sql requires location.sql
REM tables/event_date.sql depends on tables/event.sql
REM tables/user_role depends on tables/user.sql, tables/role.sql
REM tables/user_event depends on tables/user.sql, tables/event.sql, and tables/role.sql
REM tables/user_activity depends on tables/user.sql, tables/activity.sql, and tables/role.sql
REM tables/volunteers.sql depends on tables/role.sql
REM tables/volunteer_request depends on tables/task.sql, tables/volunteer.sql
rem supplier_image.sql requires supplier.sql
rem tags.sql requires supplier.sql
rem review.sql requires supplier.sql and location.sql
rem entrance.sql requires location.sql
rem tables/parking_lot.sql requires location.sql
rem tables/volunteer_need requires task.sql 	

REM PROPOSED CHANGED FOR TRACKING DEPENDENCES
:: ************************
:: FILES WHICH REQUIRE: event.sql
::	task.sql
::	event_date.sql
::  user_event.sql
::
:: ************************
:: FILES WHICH REQUIRE: zip.sql
::	location.sql
:: ************************
:: FILES WHICH REQUIRE:  user.sql
:: 	supplier.sql
::  location.sql
::  user_role.sql
::  user_event.sql
::  user_activity.sql
:: 	task.sql
::  user_image.sql
:: ************************
:: FILES WHICH REQUIRE:  role.sql
::  user_role.sql
::  user_event.sql
::  volunteers.sql
::  user_activity.sql
:: 	task.sql
:: ************************
:: FILES WHICH REQUIRE:  location.sql
::  availability.sql
::  tables/parking_lot.sql
::  entrance.sql
::  event.sql
:: ************************
:: FILES WHICH REQUIRE:  supplier.sql
::  service.sql
:: ************************
:: FILES WHICH REQUIRE:  volunteer.sql
::  skill_set.sql
:: ************************
:: FILES WHICH REQUIRE:  activity.sql
::  supplier.sql

ECHO .
ECHO if no errors appear DB was created
PAUSE