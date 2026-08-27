namespace MyCollectionExamples
{
    public class MutablePerson
    {
        public string firstName { get; set; }
        public string lastName { get; set; }

    }

    public record ImmutablePerson
    {
        public string firstName { get; init; }
        public string lastName { get; init; }
    }

    public class EquatablePerson : IEquatable<EquatablePerson>              
    {
        public string firstName { get; init; }
        public string lastName { get; init; }

        public bool Equals(EquatablePerson other)
        {
            if (other == null) return false;
            Console.WriteLine($"Comparing {this.firstName} {this.lastName} with {other.firstName} {other.lastName}");
            return this.firstName == other.firstName && this.lastName == other.lastName;
        }
    }

    public class CompareablePerson : IComparable<CompareablePerson>
    {
        public string firstName { get; init; }
        public string lastName { get; init; }

        public int CompareTo(CompareablePerson other)
        {
            if (other == null) return 0;
            return string.Compare(this.firstName, other.firstName);
        }
        
        
        }

}
