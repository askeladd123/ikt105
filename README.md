# Ask & Kristoffer

In this folder you can find different files for "Eksamenh_h22". 

## Conceptual data model in EA

You need Enterprise Architect to open "models_conceptual&physical.qea", where you can find the conceptual model. Use this file together with "data_dictionary.docx".

## Logical data model in EA

The logical model is also in "models_conceptual&physical.qea", and again, use the data dictionary.

## Physical data model in EA  

abc

## Physical data model in MySql

We used MySQL to generate a script to create the required database. It's called "database_setup.txt", and you can copy paste into MySQL's server interface to get it running.

## Console application in C#

The application reads and runs three scripts, while giving feedback to the console.

You can find the Microsoft Visual Studio project "CoVidRental" inside the "Visual Studio" folder in our project root folder. 
To build the project you need to install "MySql.Data" library, and change working directory to our project root folder. Then you will need to set up a database, using the SQL-code in the file "database_setup.txt". 

The code need you to specify connection, then it will automatically run the three scipts, outputting data to the console. Note: a INSERT can give a "query failed" message if row already exists. This doesn't mean the code is broken!
