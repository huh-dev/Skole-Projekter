namespace H2.Database.Dag2;

public class Logs
{
    public static void Save(DateTime date, string action, string recordId, string table)
    {
        //Get the logs file
        var logsFile = Path.Combine(Directory.GetCurrentDirectory(), "logs.txt");

        //Append the log to the file
        File.AppendAllText(logsFile, $"{date} - {action} - {recordId} - {table}\n");
    }
}