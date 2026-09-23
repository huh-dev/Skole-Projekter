namespace H2.Database.Dag3;
using Input;

public static class Program
{
    public static void Main(string[] args)
    {

        var context = Database.Connect();

        Inserts.InsertAuthor(context);


    }
}