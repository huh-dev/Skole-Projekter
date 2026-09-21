using System.Runtime.Serialization;
using MySql.Data;
using MySql.Data.MySqlClient;


namespace Database.Dag1;

class Program
{
    static void Main(string[] args)
    {

        var connection = ConnectToDatabase();

        connection.Execute();
    }

    public static MySqlConnection ConnectToDatabase()
    {
        var connectionString = "server=localhost;database=db_dag_1;user=root;password=";
        var connection = new MySqlConnection(connectionString);
        try
        {
            connection.Open();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return connection;
    }
}