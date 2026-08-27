namespace CRUD
{
    internal class Utils
    {   
        // MARK: Get Document Content
        //@return string[]
        public static string[] GetDocumentContent()
        {
            string filePath = "vehicles.txt";
            string[] lines = File.ReadAllLines(filePath);
            return lines;
        }


        //MARK: Is INT valid
        //@param input: string
        //@return bool
        public static bool IsIntValid(string input)
        {
            return int.TryParse(input, out _);
        }
    }
}