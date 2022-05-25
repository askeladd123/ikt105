using System;
using MySql.Data.MySqlClient;

Console.WriteLine(
    "Requirements: this program expects a running database,\n" +
    "\tthat must be created by the \"database_setup.txt\" file. \n");

Console.WriteLine("Loading scripts:");

// lese scipts
Dictionary<string, string> scripts = new Dictionary<string, string>();
try
{
    scripts.Add("queries_populate.txt", System.IO.File.ReadAllText("queries_populate.txt"));
    scripts.Add("queries_branch.txt", System.IO.File.ReadAllText("queries_branch.txt"));
    scripts.Add("queries_business.txt", System.IO.File.ReadAllText("queries_business.txt"));
    Console.WriteLine("\tscipts loaded. \n");
}
catch (Exception e)
{
    Console.WriteLine("couldn't load scipts: {0}", e.Message);
    Console.WriteLine("Press a key to quit. ");
    Console.ReadLine();
    Environment.Exit(-1);
}

Console.WriteLine("Enter connection data, example: \"server=localhost;userid=root;password=lol123;database=mydb\"\n" +
    "enter \"d\" for default data: ");
string connection_data = Console.ReadLine();

// koble til server
if (connection_data == "d")
{
    connection_data = "server=localhost;userid=root;password=bolle;database=mydb";
}
MySqlConnection connection;

try
{
    connection = new MySqlConnection(connection_data);
    connection.Open();
    Console.WriteLine("\tconnection successful. \n");
}
catch (Exception e)
{ 
    Console.WriteLine("connection failed: {0}", e.Message);
    Console.WriteLine("Press a key to quit. ");
    Console.ReadLine();
    Environment.Exit(-1);
}

//connection = new MySqlConnection(connection_data);

Console.WriteLine(" - - - - - - - - - - - -\n");

foreach (KeyValuePair<string, string> pair in scripts)
{
    Console.WriteLine("Executing script {0}\n", pair.Key);
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

            Console.WriteLine("\tQuery successful. Data recieved: ");
            if (!data_reader.HasRows)
            {
                Console.WriteLine("\t(no rows in data)\n");
            }
            while (data_reader.Read())
            {
                Console.Write("\t* ");
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
                Console.WriteLine("\tQuery successful. \n");
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Query failed: {0} \n", e.Message);
            }
        }
    }

    Console.WriteLine("Executed script {0}\n\n - - - - - - - - - - - -\n", pair.Key);
}

connection.Close();

Console.WriteLine("All scripts executed. Press enter to quit. ");
Console.ReadLine();
