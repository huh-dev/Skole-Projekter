namespace Lesson04;

public class Director : SalariedEmployee
{
    public decimal CompanyCarAllowance { get; set; }

    public Director(string name, string hireDate, decimal baseSalary, decimal bonus, decimal companyCarAllowance) : base(name, hireDate, baseSalary, bonus)
    {
        CompanyCarAllowance = companyCarAllowance;
    }

    public override decimal CalculateSalary()
    {
        return base.CalculateSalary() + CompanyCarAllowance;
    }

    public override string Description()
    {
        return base.Description() + $" CompanyCarAllowance: {CompanyCarAllowance}";
    }
}