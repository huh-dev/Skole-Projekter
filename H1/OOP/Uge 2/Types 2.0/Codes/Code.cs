namespace Types
{
    public class SearchResultDTO
    {
        public string Text { get; set; }
        public string WordToSearch { get; set; }
        public int Result { get; set; }
    }

    
    public class Code
    {
        public SearchResultDTO SearchWord(string text, string wordToSearch)
        {
            var words = text.Split(new[] { ' ', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
            var count = words.Count(w => w.Contains(wordToSearch, StringComparison.OrdinalIgnoreCase));

            return new SearchResultDTO
            {
                Text = text,
                WordToSearch = wordToSearch,
                Result = count
            };
        }
    }
}
