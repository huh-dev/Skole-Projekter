namespace MetodeOpgaver
{
    internal class Functions
    {

        //MARK: Number
        public static bool IsNumber(string input)
        {
            try
            {
                int.Parse(input);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        //MARK: String
        public static bool IsString(string input)
        {
            return input.GetType() == typeof(string);
        }

        //MARK: Boolean
        public static bool IsBoolean(string input)
        {
            return input == "true" || input == "false";
        }

        //MARK: Float or Double
        public static bool IsFloatOrDouble(string input)
        {
            return float.TryParse(input, out _) || double.TryParse(input, out _);
        }
    }
}