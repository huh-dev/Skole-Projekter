namespace Lesson04;

public class SalariedEmployee : Employee
{
    public decimal BaseSalary { get; set; }
    public decimal Bonus { get; set; }

    public SalariedEmployee(string name, string hireDate, decimal baseSalary, decimal bonus) : base(name, hireDate)
    {
        BaseSalary = baseSalary;
        Bonus = bonus;
    }

    // override: denne implementation kører, når CalculateSalary() kaldes via en Employee-reference.
    // Uden override (kun new) ville metoden skjule basisklassens, men ikke blive kaldt fra base.Description().
    public override decimal CalculateSalary()
    {
        return BaseSalary + Bonus;
    }

    public override string Description()
    {
        return base.Description() + $" BaseSalary: {BaseSalary}, Bonus: {Bonus}";
    }
}