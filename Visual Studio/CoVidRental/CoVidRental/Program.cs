using System;
using MySql.Data.MySqlClient;

Console.WriteLine("Enter connection data, enter \"d\" for default data: ");
string connection_data = Console.ReadLine();

// koble til server
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
Console.WriteLine("Connection successful. \n");

// lese fil
string[] file = { "" };
try
{
    file = System.IO.File.ReadAllText("lol.txt").Split(';', StringSplitOptions.RemoveEmptyEntries);
}
catch
{
    Console.WriteLine("Failed to read file. ");
    Console.WriteLine("Press enter to quit. ");
    Console.ReadLine();
    Environment.Exit(-1);
}

// kjør alle spørriger
foreach (string query in file)
{
    if (string.IsNullOrWhiteSpace(query))
    {
        continue;
    }
    Console.WriteLine(query);
    MySqlCommand command = new MySqlCommand(query, connection);

    if (query.IndexOf("select", StringComparison.CurrentCultureIgnoreCase) != -1)
    {
        // alle SELECT spørringer
        MySqlDataReader data_reader = command.ExecuteReader();

        Console.WriteLine("Query successful. Data recieved: ");
        while (data_reader.Read())
        {
            Console.Write(" * ");
            int i = 0;
            for (; i < data_reader.FieldCount - 1; i++)
            {
                Console.Write(data_reader[i].ToString() + ", ");
            }
            Console.Write(data_reader[i].ToString() + "\n");
        }
        Console.WriteLine("");

        data_reader.Close();
    }
    else
    {
        // alle andre spørringer
        try
        {
            command.ExecuteNonQuery();
            Console.WriteLine("Query successful. \n");
        }
        catch (MySqlException e)
        {
            Console.WriteLine("Query failed: {0} \n", e.Message);
        }
    }
}

connection.Close();

Console.WriteLine("Press enter to quit. ");
Console.ReadLine();
