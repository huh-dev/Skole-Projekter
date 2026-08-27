namespace Types
{

    public enum TypeMsg
    {
        Type,
        Types,
    }
    public struct Code
    {
        private readonly string message;
        public int count { get; set; } = 0;

        public Code(string message)
        {
            this.message = message;
            CheckType();
        }

        private void CheckType()
        {
            string[] words = message.Split(new[] { ' ', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
            string[] enumValues = Enum.GetNames(typeof(TypeMsg));
            count = words.Count(w => enumValues.Contains(w, StringComparer.OrdinalIgnoreCase));
        }
    }
}
