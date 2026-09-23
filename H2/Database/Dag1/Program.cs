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

    //Simple connection to the database without the use of EF Core, this will come in later projects.
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