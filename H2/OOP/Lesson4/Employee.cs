namespace Lesson04;


public class Employee
{
    public string Name { get; set; }
    public string HireDate { get; set; }

    public Employee(string name, string hireDate)
    {
        Name = name;
        HireDate = hireDate;
    }

    // virtual: kaldet går til den afledte klasses override, ikke altid denne return 0.
    // Uden virtual ville Description() altid kalde denne metode og returnere 0 — også for SalariedEmployee og HourlyEmployee.
    public virtual decimal CalculateSalary()
    {
        // At skrive nul her er den simpleste måde at implementere en metode i en base klasse, som skal overrides af subklasserne.
        return 0;
    }

    // Description() kalder CalculateSalary(). Fordi CalculateSalary er virtual, bruges den konkrete
    // types lønberegning — også når de afledte klasser kalder base.Description().
    public virtual string Description()
    {
        return $"Name: {Name}, HireDate: {HireDate} Tjener {CalculateSalary()} kr. om måneden";
    }
}