using MyCollectionExamples;

MutablePerson mutablePerson = new MutablePerson() { firstName = "John", lastName = "Doe" };
List<MutablePerson> mutablePeople = new List<MutablePerson>();

ImmutablePerson immutablePerson = new ImmutablePerson() { firstName = "John", lastName = "Doe" };
List<ImmutablePerson> immutablePeople = new List<ImmutablePerson>();

EquatablePerson equatablePerson = new EquatablePerson() { firstName = "John", lastName = "Doe" };
EquatablePerson equatablePerson2 = new EquatablePerson() { firstName = "John", lastName = "Does" };
List<EquatablePerson> equatablePeople = new List<EquatablePerson>();

CompareablePerson compareablePerson1 = new CompareablePerson() { firstName = "John", lastName = "Doe" };
CompareablePerson compareablePerson2 = new CompareablePerson() { firstName = "Jane", lastName = "Doe" };
CompareablePerson compareablePerson3 = new CompareablePerson() { firstName = "John", lastName = "Smith" };
List<CompareablePerson> compareablePeople = new List<CompareablePerson>();

mutablePeople.Add(mutablePerson);
immutablePeople.Add(immutablePerson);
compareablePeople.Add(compareablePerson1);
compareablePeople.Add(compareablePerson2);
compareablePeople.Add(compareablePerson3);

equatablePeople.Add(equatablePerson);
equatablePeople.Add(equatablePerson2);

compareablePeople.Sort();

Console.WriteLine(equatablePerson.Equals(equatablePerson2));

// Print sorted list
foreach (var person in compareablePeople)
{
    Console.WriteLine($"{person.firstName} {person.lastName}");
}
























