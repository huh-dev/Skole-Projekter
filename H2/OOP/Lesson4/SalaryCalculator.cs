namespace Lesson04;

public class SalaryCalculator
{

    public decimal CalculateBonus(decimal baseSalary)
    {
        // fx 5% af grundlønnen som standardbonus
        return baseSalary * 0.05m;
    }

    public decimal CalculateBonus(decimal baseSalary, decimal percentage)
    {
        // bonus udregnet med en angivet procentsats
        return baseSalary * percentage;
    }

    public decimal CalculateBonus(decimal baseSalary, decimal percentage, int yearsOfSeniority)
    {
        // fx: procent-bonus + 1% ekstra pr. anciennitetsår
        return baseSalary * percentage + yearsOfSeniority * 0.01m;
    }
}