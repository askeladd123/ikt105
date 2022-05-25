# Ask & Kristoffer

## Create a conceptual data model based on the information above.

	
  
## Create a logical data model for a relational database based on (1) the information above, and (2) your conceptual data model. 



## Create a physical data model by transforming the logical data model. In this assignment you shall target the MySql database – SQLite is not allowed. (Deliverance for 1-3: (1) A complete EA model (2) ONE document with the models and the data dictionary included.  



## Implement the physical data model in MySql. (Deliverance the SQL scripts for creating the SQL tables, 
script for populating the database with your data, scripts for all the SQL statements above – in total 3 
SQL scripts.) 
All the data for demonstrating the SQL statements above must be added in the population script. If the 
data for demonstrating the statements above is not present, the query will be deemed “not successful”.  



## Create an application in C#, that connects to the database, and executes all the transactions requirements 
described in the Transaction Requirements above.  

You can find the Microsoft Visual Studio project "CoVidRental" inside the "Visual Studio" folder in our project root folder. 
To build the project you need to install "MySql.Data" library, and change working directory to our project root folder. Then you will need to set up a database, using the SQL-code in the file "database_setup.txt". 

The code need you to specify connection, then it will automatically run the three scipts, outputting data to the console. Note: a INSERT can give a "query failed" message if row already exists. This doesn't mean the code is broken.