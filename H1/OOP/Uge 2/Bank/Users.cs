namespace Bank
{
    public class Users
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string fullname
        {
            get
            {
                return $"{firstname} {lastname}";   
            }
        }
    }
}
