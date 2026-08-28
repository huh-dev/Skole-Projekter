namespace Lesson04;

class Program
{
    static void Main(string[] args)
    {

        List<Employee> employees = new List<Employee>();
        employees.Add(new Employee("John Doe", "2021-01-01"));
        employees.Add(new SalariedEmployee("Jane Doe", "2021-01-01", 1000, 100));
        employees.Add(new HourlyEmployee("Jim Beam", "2021-01-01", 100, 10));
        employees.Add(new SalariedEmployee("John Smith", "2021-01-01", 1500, 200));
        employees.Add(new HourlyEmployee("John Doe", "2021-01-01", 150, 15));
        employees.Add(new Director("Jane Doe", "2021-01-01", 1000, 100, 10000));



        // Polymorfi: listen er List<Employee>, så løkken kalder Description() via en basisklasse-reference.
        // Runtime vælger den konkrete klasses override (Employee / SalariedEmployee / HourlyEmployee).
        // Lønnen i teksten kommer fra CalculateSalary(), som også er virtual — derfor 0, 1100, 1000, 1700, 2250.
        //
        // Uden virtual/override (kun en ny metode med samme navn i underklasserne) ville output være:
        // - Jane Doe: "Tjener 0 kr." i stedet for 1100
        // - Jim Beam: "Tjener 0 kr." i stedet for 1000
        // - John Smith: "Tjener 0 kr." i stedet for 1700
        // - John Doe (timeløn): "Tjener 0 kr." i stedet for 2250
        // Extra-felter (BaseSalary, Bonus, HourlyRate, HoursWorked) ville stadig vises, fordi Description() er virtual.
        foreach (Employee employee in employees)
        {
            Console.WriteLine(employee.Description());
        }

        Console.WriteLine();
        Console.WriteLine("=== Salary Calculator ===");

        // Compileren vælger hvilken overload af CalculateBonus, der skal kaldes ud fra
        // argumenternes antal og type, når den gennemgår koden – altså under kompilering (compile time).
        // Så det bestemmes FØR programmet kører, ikke under kørslen.
        // Fx:
        // - CalculateBonus(1000) matcher metoden med én decimal-parameter.
        // - CalculateBonus(1000, 0.05m) matcher overloaden med to decimal-parametre.
        // - CalculateBonus(1000, 0.05m, 10) matcher overloaden med to decimal og én int.

        // Nej, override bruges kun, når man arver fra en baseklasse og vil ændre (overskrive) en virtuel/abstrakt metode.
        // Her handler det om flere metoder med samme navn, men forskelligt parameterliste i samme klasse (overloading).
        // Overriding kræver at metoderne har PRÆCIS samme signatur, kun implementeringen ændres i subklasser –
        // derfor ville override ikke kunne bruges her.

        SalaryCalculator salaryCalculator = new SalaryCalculator();
        Console.WriteLine($"Standard bonus for 1000 kr.: {salaryCalculator.CalculateBonus(1000)} kr.");
        Console.WriteLine($"Bonus for 1000 kr. med 5%: {salaryCalculator.CalculateBonus(1000, 0.05m)} kr.");
        Console.WriteLine($"Bonus for 1000 kr. med 5% og 10 år anciennitet: {salaryCalculator.CalculateBonus(1000, 0.05m, 10)} kr.");


        HourlyEmployee hourlyEmployee = new HourlyEmployee("John Doe", "2021-01-01", 150, 15);
        hourlyEmployee.RegisterOvertime(10);
        Console.WriteLine(hourlyEmployee.Description());
    }
}