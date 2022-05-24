using System;
using MySql.Data.MySqlClient;

Console.WriteLine("Enter connection data (enter \"d\" for default data): ");
string connection_data = Console.ReadLine();

// prove a koble til
if (connection_data == "d")
{
    connection_data = @"server=localhost;userid=root;password=bolle;database=mydb"; //denne må endres på
}
MySqlConnection connection = new MySqlConnection(connection_data);

try
{
    connection = new MySqlConnection(connection_data);
    connection.Open();
}
catch
{ 
    Console.WriteLine("Connection failed. ");
    Console.WriteLine("Press a key to quit. ");
    Console.ReadLine();
    Environment.Exit(-1); 
}
Console.WriteLine("Connection successful. ");

string query = "select * from employee";
var command = new MySqlCommand(query, connection);
MySqlDataReader data_reader = command.ExecuteReader();

while(data_reader.Read())
{
    Console.WriteLine("");
    Console.WriteLine(data_reader.GetString(0));
}



connection.Close();

Console.WriteLine("Press a key to quit. ");
Console.ReadLine();

