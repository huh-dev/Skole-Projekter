using MySql.Data;
using MySql.Data.MySqlClient;

namespace Database.Dag1;

public static class ExecuteSQL
{
    public static void Execute(this MySqlConnection connection)
    {
        //Get the sql file
        var sqlFile = File.ReadAllText("db_dag_1.sql");

        //Execute the sql file - this is the important part. This will take the above sql file, and execute it on the database.
        var command = new MySqlCommand(sqlFile, connection);
        command.ExecuteNonQuery();

        Console.WriteLine("Tables inserted");
    }
}