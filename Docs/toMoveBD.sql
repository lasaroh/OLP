/* To move the database from its current location to a new location

1 - Rigth-click the DB in Object Explorer select "Tasks" and then "Detach"

2 - Open a new query and xecute the following command to get the current location of the DB files
*/

--SELECT name, physical_name
--FROM sys.master_files
--WHERE database_id = DB_ID('OLP')

/*
3 - Stop the SQL Server service from the "Services Manager" -> in this case: SQL Server (OLP_DB)

4 - Copy the database files (usually have an .mdf and .ldf extension) from the current location to the new location

5 - Go back to SSMS and execute the folowwing command to move the database files to the new location:
*/

--ALTER DATABASE OLP 
--MODIFY FILE (NAME = OLP, FILENAME = 'D:\Proyectos\OLP\DB\OLP.mdf');
--ALTER DATABASE OLP 
--MODIFY FILE (NAME = OLP_log, FILENAME = 'D:\Proyectos\OLP\DB\OLP_log.ldf');

/*
6 - Start the SQL Server service

7 - Reconnect the DB in SSMS by right-clickcing the "Database" node in the Object Explorer and selecting "Connect Database"

Once you've completed these steps, the OLP database should be available in the new location you specified
*/