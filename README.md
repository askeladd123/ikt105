# Ask & Kristoffer

In this folder you can find different files for "Eksamenh_h22". 

## Models

You need Enterprise Architect to open "models.qea", where you can find two different models. One top level model, and one more detailed.

## Data Dictionary

You can find information about the entities and attributes in "data_dictionary.docx". Open with Microsoft Word.

## Make database

We used MySQL to generate a script to create the required database. It's called "database_setup.txt", and you can copy paste into MySQL's server interface to get it running.

## Console application in C#

The application reads and runs these three SQL scripts, "queries_populate.txt", "queries_branch.txt" and "queries_business.txt".

You can find the Microsoft Visual Studio project "CoVidRental" inside the "Visual Studio" folder in our project root folder. 
To build the project you need to install "MySql.Data" library, and change working directory to our project root folder. Then you will need to set up a database, using the SQL-code in the file "database_setup.txt". 

The code need you to specify connection, then it will automatically run the three scipts, outputting data to the console. Note: a INSERT can give a "query failed" message if row already exists. This doesn't mean the code is broken!
