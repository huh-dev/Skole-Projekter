namespace MetodeOpgaver
{   
    using System.Text.RegularExpressions;

    internal class LastSeven
    {   

        //MARK: Setup
        public static void Setup()
        {
            Console.Clear();
            bool keepRunning = true;
            do
            {
                Console.WriteLine("Vælg en opgave: \n0) Exit \n1) Opgave 7a \n2) Opgave 7b \n3) Opgave 7c");
                string inputTask7 = Console.ReadLine();
                switch (inputTask7)
                {
                    case "0":
                        keepRunning = false;
                        Program.Main(null);
                        break;
                    case "1":
                        Opgave7a();
                        System.Threading.Thread.Sleep(1000);
                        Console.Clear();
                        break;
                    case "2":
                        Opgave7b();
                        System.Threading.Thread.Sleep(1000);
                        Console.Clear();
                        break;
                    case "3":
                        Opgave7c();
                        System.Threading.Thread.Sleep(1000);
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        System.Threading.Thread.Sleep(1000);
                        Console.Clear();
                        break;
                }
            } while (keepRunning);

        }


        //MARK: Opgave 7a
        private static void Opgave7a()
        {
            Console.Clear();
            bool chooseTempratur = true;

            do
            {
                Console.WriteLine("Indsæt tempraturen: ");
                string tempratur = Console.ReadLine();
                if (!Functions.IsNumber(tempratur))
                {
                    Console.WriteLine("Ugyldigt input");
                }
                
                Console.WriteLine("Vælg den temperatur du skrev ind: \n0) Exit \n1) Celsius \n2) Fahrenheit \n3) Kelvin \n4) Réaumur");
                string temperaturType = Console.ReadLine();

                switch (temperaturType)
                {
                    case "0":
                        chooseTempratur = false;
                        break;
                    case "1":
                    case "2": 
                    case "3":
                    case "4":
                        TemperaturOmregner(tempratur, temperaturType);
                        System.Threading.Thread.Sleep(1000);
                        Console.Clear();    
                        break;
                    default:
                        Console.WriteLine("Ugyldigt input");
                        System.Threading.Thread.Sleep(1000);
                        Console.Clear();
                        break;
                }
            } while (chooseTempratur);

        }

        //MARK: Opgave 7a (Temperatur Omregner)
        private static void TemperaturOmregner(string tempratur, string temperaturType)
        {
            switch (temperaturType)
            {
                case "1": 
                    double celsius = Convert.ToDouble(tempratur);
                    Console.WriteLine($"Fahrenheit: {(celsius * 9/5) + 32}");
                    Console.WriteLine($"Kelvin: {celsius + 273.15}");
                    Console.WriteLine($"Réaumur: {celsius * 4/5}");
                    break;
                case "2":   
                    double fahrenheit = Convert.ToDouble(tempratur);
                    Console.WriteLine($"Celsius: {(fahrenheit - 32) * 5/9}");
                    Console.WriteLine($"Kelvin: {((fahrenheit - 32) * 5/9) + 273.15}");
                    Console.WriteLine($"Réaumur: {(fahrenheit - 32) * 4/9}");
                    break;
                case "3": 
                    double kelvin = Convert.ToDouble(tempratur);
                    double celsiusFromKelvin = kelvin - 273.15;
                    Console.WriteLine($"Celsius: {celsiusFromKelvin}");
                    Console.WriteLine($"Fahrenheit: {(celsiusFromKelvin * 9/5) + 32}");
                    Console.WriteLine($"Réaumur: {celsiusFromKelvin * 4/5}");
                    break;
                case "4": 
                    double reaumur = Convert.ToDouble(tempratur);
                    double celsiusFromReaumur = reaumur * 5/4;
                    Console.WriteLine($"Celsius: {celsiusFromReaumur}");
                    Console.WriteLine($"Fahrenheit: {(celsiusFromReaumur * 9/5) + 32}");
                    Console.WriteLine($"Kelvin: {celsiusFromReaumur + 273.15}");
                    break;
                default:
                    break;
            }
        }

        //MARK: Opgave 7b
        private static void Opgave7b()
        {
            bool keepRunningNumbers = true;

            do
            {
                Console.WriteLine("Indtast et tal:");
                string inputNumber = Console.ReadLine();
                if (!Functions.IsNumber(inputNumber))
                {
                    Console.WriteLine("Ugyldigt input");
                }

                Omregner7b(inputNumber);

                keepRunningNumbers = false;

            } while (keepRunningNumbers);
        }

        //MARK: Opgave 7b (Omregner)
        private static void Omregner7b(string inputNumber)
        {
            int number = int.Parse(inputNumber);
            Console.WriteLine($"Decimal: {number}");
            string hexValue = number.ToString("X");
            Console.WriteLine($"Hexadecimal: {hexValue}");
            string binaryValue = Convert.ToString(number, 2);
            Console.WriteLine($"Binary: {binaryValue}");
            Console.WriteLine("Tryk for at fortsætte");
            Console.ReadLine();
            Console.Clear();
        }

        //MARK: Opgave 7c
        private static void Opgave7c()
        {

            do
            {
                
                Console.WriteLine("Indtast en hvilken som helst datatype: (Skriv 0 for at stoppe)");
                string input = Console.ReadLine();

                if (input == "0")
                {
                    break;
                }


                if (Functions.IsNumber(input))
                {
                    Console.WriteLine("Det er et tal");
                }
                else if (Functions.IsBoolean(input))
                {
                    Console.WriteLine("Det er en bool");
                }
                else if (Functions.IsFloatOrDouble(input))
                {
                    Console.WriteLine("Det er en float eller double");
                }
                else if (Functions.IsString(input))
                {
                    Console.WriteLine("Det er en tekst");
                }

                Console.WriteLine("Tryk for at fortsætte");
                Console.ReadLine();
                Console.Clear();
            } while (true);
        }
    }
}