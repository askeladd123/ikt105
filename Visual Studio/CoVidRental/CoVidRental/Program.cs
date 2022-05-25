using System;
using MySql.Data.MySqlClient;

Console.WriteLine(
    "Requirements: this program expects a running database,\n" +
    "\tthat must be created by the \"database_setup.txt\" file. \n");

Console.WriteLine("Loading scripts...");

// lese scipts
Dictionary<string, string> scripts = new Dictionary<string, string>();
try
{
    scripts.Add("sparringer_populate.txt", System.IO.File.ReadAllText("sparringer_populate.txt"));
    scripts.Add("sparringer_branch.txt", System.IO.File.ReadAllText("sparringer_branch.txt"));
    scripts.Add("sparringer_business.txt", System.IO.File.ReadAllText("sparringer_business.txt"));
    Console.WriteLine("Scipts loaded. \n");
}
catch (Exception e)
{
    Console.WriteLine("Couldn't load scipts: {0}", e.Message);
    Console.WriteLine("Press a key to quit. ");
    Console.ReadLine();
    Environment.Exit(-1);
}

Console.WriteLine("Enter connection data, example: \"server=localhost;userid=root;password=lol123;database=mydb\"\n" +
    "\tenter \"d\" for default data: ");
string connection_data = Console.ReadLine();

// koble til server
if (connection_data == "d")
{
    connection_data = @"server=localhost;userid=root;password=bolle;database=mydb";
}
MySqlConnection connection = new MySqlConnection(connection_data);

try
{
    connection = new MySqlConnection(connection_data);
    connection.Open();
    Console.WriteLine("Connection successful. \n");
}
catch (MySqlException e)
{ 
    Console.WriteLine("Connection failed: {0}", e.Message);
    Console.WriteLine("Press a key to quit. ");
    Console.ReadLine();
    Environment.Exit(-1);
}

foreach (KeyValuePair<string, string> pair in scripts)
{
    Console.WriteLine("Executing script {0}", pair.Key);
    string[] queries = pair.Value.Split(';', StringSplitOptions.RemoveEmptyEntries);

    // kjør alle spørriger
    foreach (string query in queries)
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

    Console.WriteLine("Executed script {0}", pair.Key);
}

connection.Close();

Console.WriteLine("All scripts executed. Press enter to quit. ");
Console.ReadLine();
