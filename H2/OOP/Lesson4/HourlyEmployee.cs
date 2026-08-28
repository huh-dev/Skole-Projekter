namespace Lesson04;

public class HourlyEmployee : Employee
{
    public decimal HourlyRate { get; set; }
    public double HoursWorked { get; set; }

    public HourlyEmployee(string name, string hireDate, decimal hourlyRate, double hoursWorked) : base(name, hireDate)
    {
        HourlyRate = hourlyRate;
        HoursWorked = hoursWorked;
    }

    // override: denne implementation kører, når CalculateSalary() kaldes via en Employee-reference.
    // Uden override (kun new) ville metoden skjule basisklassens, men ikke blive kaldt fra base.Description().
    public override decimal CalculateSalary()
    {
        return HourlyRate * (decimal)HoursWorked;
    }

    public override string Description()
    {
        return base.Description() + $" HourlyRate: {HourlyRate}, HoursWorked: {HoursWorked}";
    }

    // Når compileren gennemgår koden, vil den kun kigge på hvad der er definieret i Employee-klassen. Og eftersom at denne metode ikke er der, vil den ikke kunne finde den.
    // For så at kunne kalde denne metode, så skal vi "overtale" compileren til at behandle metoden som en objekt som en HourlyEmployee. Dette kaldes casting
    public void RegisterOvertime(double extraHours)
    {
        HoursWorked += extraHours;
        Console.WriteLine($"Overtime registered: {extraHours} hours");
    }

}