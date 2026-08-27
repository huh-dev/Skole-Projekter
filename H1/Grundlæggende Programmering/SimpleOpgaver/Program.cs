
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        bool beginnerTasks = false;
        bool intermediateTasks = false;
        bool advancedTasks = false;

        Console.WriteLine("Vælg Opgave \n0) Exit \n1) Beginner Tasks \n2) Intermediate Tasks \n3) Advanced Tasks");
        string taskValue = Console.ReadLine();

        switch (taskValue)
        {
            case "0":
                break;
            case "1":
                beginnerTasks = true;
                break;
            case "2":
                intermediateTasks = true;
                break;
            case "3":
                advancedTasks = true;
                break;
        }

        //MARK: Beginner Tasks
        if (beginnerTasks)
        {
            do
            {
            Console.WriteLine("Vælg Opgave \n0) Exit \n1) Hello World \n2) Variable \n3) Calculation \n4) Alder \n5) Farenheit til Celsius \n6) Lommeregner \n7) Metode \n8) For Loop \n9) Foreach Loop, \n10) Multidimensional Array, \n11) Konstanter \n12) Email Validering \n15) Rækkefølge \n16) BMI \n17) String Manipulation \n18) Gæt Tal \n19) Grundig af Versionsstyring \n20) Login System");
            string menuValue = Console.ReadLine() ?? string.Empty;

            switch (menuValue)
            {
                case "0":
                    beginnerTasks = false;
                    break;
                case "1":
                    //MARK: Hello World
                    Console.WriteLine("Hej Verden");
                    System.Threading.Thread.Sleep(1000);
                    Console.Clear();
                    break;
                case "2":
                    //MARK: Variable
                    string variable = "Eksempel";
                    Console.WriteLine(variable);

                    System.Threading.Thread.Sleep(1000);
                    Console.Clear();
                    break;
                case "3":
                    //MARK: Calculation
                    Console.WriteLine("Vælg første tal");
                    if (!int.TryParse(Console.ReadLine(), out int numberOne))
                    {
                        Console.WriteLine("Fejl: Du skal indtaste et gyldigt tal.");
                        System.Threading.Thread.Sleep(1000);
                        Console.Clear();
                        continue;
                    }

                    Console.WriteLine("Vælg andet tal"); 
                    if (!int.TryParse(Console.ReadLine(), out int numberTwo))
                    {
                        Console.WriteLine("Fejl: Du skal indtaste et gyldigt tal.");
                        System.Threading.Thread.Sleep(1000);
                        Console.Clear();
                        continue;
                    }

                    Console.WriteLine("Plus {0} ", (numberOne + numberTwo));
                    Console.WriteLine("Minus {0}", (numberOne - numberTwo));    
                    Console.WriteLine("Division {0}", (numberOne / numberTwo));
                    Console.WriteLine("Multiplikation {0}", (numberOne * numberTwo));

                    System.Threading.Thread.Sleep(5000);
                    Console.Clear();
                    break;
                case "4":
                    //MARK: Alder
                    Console.WriteLine("Indtast din alder");
                    int age = int.Parse(Console.ReadLine());

                    if (age < 18)
                    {
                        Console.WriteLine("Du er ikke myndig");
                    }
                    else
                    {
                        Console.WriteLine("Du er myndig");
                    }
                    System.Threading.Thread.Sleep(5000);
                    Console.Clear();
                    break;
                case "5":
                    //MARK: Farenheit til Celsius
                    Console.WriteLine("Indtast Farenheit");  
                    int farenheit = int.Parse(Console.ReadLine());
                    Console.WriteLine("Celsius {0}", (farenheit - 32) * 5 / 9);
                    System.Threading.Thread.Sleep(5000);
                    Console.Clear();
                    break;
                case "6":
                    //MARK: Lommeregner
                    Console.WriteLine("Vælg en Operator\n1) Plus\n2) Minus\n3) Multiplikation\n4) Division");
                    string operatorChoice = Console.ReadLine();
                    Console.WriteLine("Indtast første tal");
                    int num1 = int.Parse(Console.ReadLine());
                    Console.WriteLine("Indtast andet tal"); 
                    int num2 = int.Parse(Console.ReadLine());
                    
                    switch (operatorChoice)
                    {
                        case "1":
                            Console.WriteLine($"Resultat {num1 + num2}");
                            break;
                        case "2":
                            Console.WriteLine($"Resultat {num1 - num2}");
                            break;
                        case "3":
                            Console.WriteLine($"Resultat {num1 * num2}");
                            break;
                        case "4":
                            Console.WriteLine($"Resultat {num1 / num2}");
                            break;
                        default:
                            Console.WriteLine("Fejl: Du skal indtaste en gyldig operator");
                            break;
                    }
                    System.Threading.Thread.Sleep(3000);
                    Console.Clear();
                    break;

                case "7":
                    //MARK: Metode
                    void metode()
                    {
                        Console.WriteLine("Hej Verden");
                    }
                    metode();
                    break;
                case "8":
                    //MARK: For Loop
                    for (int i = 1; i < 11; i++)
                    {
                        Console.WriteLine(i);
                    }
                    System.Threading.Thread.Sleep(3000);
                    Console.Clear();
                    break;
                case "9":
                    //MARK: Foreach Loop
                    string[] names = {"æble", "banan", "citron"};
                    foreach (string name in names)
                    {
                        Console.WriteLine(name);
                    }
                    System.Threading.Thread.Sleep(3000);
                    Console.Clear();
                    break;
                case "10":
                    //MARK: Multidimensional array - 2x2 matrix
                    int[,] numbers = new int[2, 2] {
                        {1, 2},
                        {3, 4}
                    };
                    
                    for (int i = 0; i < numbers.GetLength(0); i++)
                    {
                        for (int j = 0; j < numbers.GetLength(1); j++)
                        {
                            Console.Write(numbers[i, j] + " ");
                        }
                        Console.WriteLine(); 
                    }
                    System.Threading.Thread.Sleep(3000);
                    Console.Clear();
                    break;
                case "11":
                    //MARK: Konstanter
                    const int moms = 25;
                    Console.WriteLine("Indtast pris");
                    int pris = int.Parse(Console.ReadLine());
                    Console.WriteLine("Pris med moms {0}", pris + (pris * moms / 100));
                    System.Threading.Thread.Sleep(3000);
                    Console.Clear();
                    break;
                case "12":
                    //MARK: Email Validering
                    Console.WriteLine("Indtast en email");
                    string email = Console.ReadLine();
                    if (email.Contains("@") && email.Contains("."))
                    {
                        Console.WriteLine("Emailen er gyldig");
                    }
                    else
                    {
                        Console.WriteLine("Emailen er ikke gyldig");
                    }
                    System.Threading.Thread.Sleep(3000);
                    Console.Clear();
                    break;

                case "15":
                    //MARK: Rækkefølge
                    int[] numberss = new int[3];

                    for (int i = 0; i < 3; i++)
                    {
                        Console.WriteLine($"Indtast tal {i+1}:");
                        numberss[i] = int.Parse(Console.ReadLine());
                    }
                    Array.Sort(numberss);

                    Console.WriteLine("Tallene i stigende rækkefølge:");
                    foreach (int num in numberss)
                    {
                        Console.WriteLine(num);
                    }

                    System.Threading.Thread.Sleep(3000);
                    Console.Clear();
                    break;
                case "16":
                    //MARK: BMI
                    Console.WriteLine("Indtast vægt");
                    double vægt = double.Parse(Console.ReadLine());
                    Console.WriteLine("Indtast højde");
                    double højde = double.Parse(Console.ReadLine());
                    double højdeIMeter = højde / 100.0; 
                    double bmi = vægt / (højdeIMeter * højdeIMeter);
                    Console.WriteLine("BMI {0:F2}", bmi);
                    System.Threading.Thread.Sleep(3000);
                    Console.Clear();
                    break;
                case "17":
                    //MARK: String Manipulation
                    Console.WriteLine("Indtast dit navn:");
                    string navn = Console.ReadLine();
                    Console.WriteLine("Indtast din alder:");
                    int alder = int.Parse(Console.ReadLine());
                    
                    string formateret = $"Hej {navn}! Du er {alder} år gammel.";
                    Console.WriteLine(formateret);
                    Console.WriteLine("Dit navn med store bogstaver: {0}", navn.ToUpper());
                    Console.WriteLine("Dit navn med små bogstaver: {0}", navn.ToLower());
                    Console.WriteLine($"Dit navn er {navn.Length} bogstaver langt");
                    
                    System.Threading.Thread.Sleep(5000);
                    Console.Clear();
                    break;
                case "18":
                    //MARK: Gæt Tal
                    Random random = new Random();
                    int hemmeligtTal = random.Next(1, 11);
                    int gæt;
                    bool harGættetRigtigt = false;

                    Console.WriteLine("Gæt et tal mellem 1 og 10:");

                    do
                    {
                        if (!int.TryParse(Console.ReadLine(), out gæt))
                        {
                            Console.WriteLine("Indtast venligst et gyldigt tal.");
                            continue;
                        }

                        if (gæt == hemmeligtTal)
                        {
                            Console.WriteLine("Tillykke! Du gættede rigtigt!");
                            harGættetRigtigt = true;
                        }

                    } while (!harGættetRigtigt);

                    System.Threading.Thread.Sleep(3000);
                    Console.Clear();
                    break;

                case "19":
                    //MARK: Grundig af Versionsstyring
                    Console.WriteLine("Indtast en versionstyring");
                    Console.WriteLine("Indsending til GitHub");
                    Console.WriteLine("git add .");
                    Console.WriteLine("git commit -m \"commit message\"");
                    Console.WriteLine("git pull");
                    Console.WriteLine("git push");
                    System.Threading.Thread.Sleep(3000);
                    Console.Clear();
                    break;
                case "20":
                    //MARK: Login System
                    Console.WriteLine("Indtast dit brugernavn");
                    string brugernavn = Console.ReadLine();
                    Console.WriteLine("Indtast dit password");
                    string password = Console.ReadLine();
                    if (brugernavn == "admin" && password == "1234")
                    {
                        Console.WriteLine("Du er logget ind");
                    }
                    else
                    {
                        Console.WriteLine("Fejl: Forkert brugernavn eller password");
                    }
                    System.Threading.Thread.Sleep(3000);
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("Fejltast");
                    break;
            }

            } while (beginnerTasks);
        }


        //MARK: Intermediate Tasks
        if (intermediateTasks)
        {
            do
            {
            Console.WriteLine("Vælg Opgave \n0) Exit \n1) Avanceret Lommeregner \n2) List Sortering \n3) Palindrome Check \n4) Valutakonvertering \n5) Gæt Tal (tilkobling) \n6) Pagination \n7) Brugerdefinierede fejlmeddelelser \n8) Matematiske ligninger \n9) Bankkonto Simulator \n10) Opretning af Diagrammer for et Program, \n11) Logik-baseret Login \n12) Analysering af tekstfil \n13) Versionering af Programændringer \n14) Geometriske beregninger \n15) Udvidet Månedsbudget \n16) Filter tal rækkefølge \n17) Sortér kontakter alfabetisk \n18) Email validering med regex \n19) Fibonaci Sekvens \n20) Opgavekommentarer og Dokumentation");
            string menuValue = Console.ReadLine() ?? string.Empty;

            switch (menuValue)
            {
                case "0":
                    intermediateTasks = false;
                    break;
                case "1":
                    //MARK: Avanceret Lommeregner
                    Console.WriteLine("Indtast første tal");
                    int num1 = int.Parse(Console.ReadLine());
                    Console.WriteLine("Indtast andet tal");
                    int num2 = int.Parse(Console.ReadLine());
                    Console.WriteLine("Vælg en operator \n1) Plus \n2) Minus \n3) Multiplikation \n4) Division \n5) Exponentiering \n6) Modulus");
                    string operatorChoice = Console.ReadLine();

                    switch (operatorChoice)
                    {
                        case "1":
                            Console.WriteLine($"Resultat {num1 + num2}");
                            break;
                        case "2":
                            Console.WriteLine($"Resultat {num1 - num2}");
                            break;
                        case "3":
                            Console.WriteLine($"Resultat {num1 * num2}");
                            break;
                        case "4":
                            Console.WriteLine($"Resultat {num1 / num2}");
                            break;
                        case "5":
                            Console.WriteLine($"Resultat {Math.Pow(num1, num2)}");
                            break;
                        case "6":
                            Console.WriteLine($"Resultat {num1 % num2}");
                            break;
                        default:
                            Console.WriteLine("Fejl: Du skal indtaste en gyldig operator");
                            break;
                    }
                    System.Threading.Thread.Sleep(3000);
                    Console.Clear();
                    break;
                case "2":
                    //MARK: List Sortering
                    List<int> numbersListIntermediate = new List<int> { 5, 2, 8, 1, 3 };
                    numbersListIntermediate.Sort();
                    foreach (int number in numbersListIntermediate)
                    {
                        Console.WriteLine(number);
                    }
                    System.Threading.Thread.Sleep(3000);
                    Console.Clear();
                    break;
                case "3":
                    //MARK: Palindrome Check
                    Console.WriteLine("Indtast et ord");
                    string word = Console.ReadLine();
                    if (word == word.Reverse().ToString())
                    {
                        Console.WriteLine("Det er et palindrom");
                    }
                    else
                    {
                        Console.WriteLine("Det er ikke et palindrom");
                    }
                    break;
                case "4":
                    //MARK: Valutakonvertering
                    double euro = 7.46;
                    Console.WriteLine("Indtast et tal i DKK");
                    double dkk = double.Parse(Console.ReadLine());
                    Console.WriteLine($"Resultat {dkk * euro}");
                    System.Threading.Thread.Sleep(3000);
                    Console.Clear();
                    break;
                case "5":
                    //MARK: Gæt Tal (tilkobling)
                    Random random = new Random();
                    int hemmeligtTal = random.Next(1, 11);
                    int gæt;
                    bool harGættetRigtigt = false;

                    Console.WriteLine("Gæt et tal mellem 1 og 10:");

                    do
                    {
                        if (!int.TryParse(Console.ReadLine(), out gæt))
                        {
                            Console.WriteLine("Indtast venligst et gyldigt tal.");
                            continue;
                        }

                        if (gæt < 1 || gæt > 10)
                        {
                            Console.WriteLine("Tallet skal være mellem 1 og 10.");
                            continue;
                        }

                        if (gæt < hemmeligtTal)
                        {
                            Console.WriteLine("Højere! Prøv igen:");
                        }
                        else if (gæt > hemmeligtTal)
                        {
                            Console.WriteLine("Lavere! Prøv igen:");
                        }
                        else
                        {
                            Console.WriteLine("Tillykke! Du gættede rigtigt!");
                            harGættetRigtigt = true;
                        }

                    } while (!harGættetRigtigt)


                    System.Threading.Thread.Sleep(3000);
                    Console.Clear();
                    break;
                case "6":
                    //MARK: Pagination
                    string[] contacts = { "John Doe", "Jane Smith", "Bob Wilson", "Alice Brown", "Mike Johnson", 
                                        "Sarah Davis", "Tom Miller", "Emma White", "James Taylor", "Lucy Anderson" };
                    int pageSize = 3;
                    int currentPage = 1;
                    int totalPages = (int)Math.Ceiling((double)contacts.Length / pageSize);

                    bool viewing = true;
                    while (viewing)
                    {
                        Console.Clear();
                        Console.WriteLine($"Side {currentPage} af {totalPages}\n");

                        for (int i = 0; i < pageSize; i++)
                        {
                            int kontaktNummer = (currentPage - 1) * pageSize + i;
                            if (kontaktNummer < contacts.Length)
                            {
                                Console.WriteLine($"{kontaktNummer + 1}. {contacts[kontaktNummer]}");
                            }
                        }

                        Console.WriteLine("\nHvad vil du gøre?");
                        Console.WriteLine("N - Se næste side");
                        Console.WriteLine("P - Gå tilbage");
                        Console.WriteLine("Q - Afslut");

                        var valg = Console.ReadKey().Key;

                        if (valg == ConsoleKey.N && currentPage < totalPages)
                        {
                            currentPage++;
                        }
                        else if (valg == ConsoleKey.P && currentPage > 1) 
                        {
                            currentPage--;
                        }
                        else if (valg == ConsoleKey.Q)
                        {
                            viewing = false;
                        }
                    }

                    Console.Clear();
                    break;

                case "7":
                    //MARK: Brugerdefinierede fejlmeddelelser
                    try
                    {
                        Console.WriteLine("Indtast et tal");
                        string input = Console.ReadLine();
                        
                        if (string.IsNullOrEmpty(input))
                        {
                            throw new ArgumentException("Du skal indtaste et tal!");
                        }
                        
                        if (!int.TryParse(input, out int tal))
                        {
                            throw new FormatException("Fejl: Du skal indtaste et gyldigt tal!");
                        }
                        
                        if (tal < 0)
                        {
                            throw new ArgumentOutOfRangeException("tal", "Fejl: Tallet skal være større end eller lig med 0!");
                        }
                        
                        Console.WriteLine($"Success: Du indtastede tallet {tal}");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Der opstod en uventet fejl: {ex.Message}");
                    }
                    finally 
                    {
                        Console.WriteLine("\nTryk på en tast for at fortsætte...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    break;
                case "8":
                    //MARK: Matematiske ligninger
                    Console.WriteLine("Løs ligningen ax^2 + bx + c = 0");

                    Console.WriteLine("Indtast a");
                    if (!double.TryParse(Console.ReadLine(), out double a) || a == 0)
                    {
                        Console.WriteLine("Fejl: a skal være et tal forskelligt fra 0");
                        System.Threading.Thread.Sleep(3000);
                        Console.Clear();
                        break;
                    }

                    Console.WriteLine("Indtast b");
                    if (!double.TryParse(Console.ReadLine(), out double b))
                    {
                        Console.WriteLine("Fejl: b skal være et tal");
                        System.Threading.Thread.Sleep(3000);
                        Console.Clear();
                        break;
                    }

                    Console.WriteLine("Indtast c"); 
                    if (!double.TryParse(Console.ReadLine(), out double c))
                    {
                        Console.WriteLine("Fejl: c skal være et tal");
                        System.Threading.Thread.Sleep(3000);
                        Console.Clear();
                        break;
                    }

                    double diskriminant = Math.Pow(b, 2) - (4 * a * c);

                    if (diskriminant > 0)
                    {
                        double x1 = (-b + Math.Sqrt(diskriminant)) / (2 * a);
                        double x2 = (-b - Math.Sqrt(diskriminant)) / (2 * a);
                        Console.WriteLine($"Ligningen har to løsninger:");
                        Console.WriteLine($"x1 = {x1:F2}");
                        Console.WriteLine($"x2 = {x2:F2}");
                    }
                    else if (diskriminant == 0)
                    {
                        double x = -b / (2 * a);
                        Console.WriteLine($"Ligningen har én løsning:");
                        Console.WriteLine($"x = {x:F2}");
                    }
                    else
                    {
                        Console.WriteLine("Ligningen har ingen reelle løsninger");
                    }
                    System.Threading.Thread.Sleep(3000);
                    Console.Clear();
                    break;

                case "9":
                    //MARK: Bankkonto Simulator
                    int saldo = 1000;
                    bool fortsæt = true;
                    
                    do 
                    {
                        Console.WriteLine("Ønsker du at indsætte eller hæve penge? (I/H)");
                        string valg = Console.ReadLine();
                        if (valg == "I" || valg == "i")
                        {
                            Console.WriteLine("Indtast beløb");
                            int beløb = int.Parse(Console.ReadLine());
                            saldo += beløb;
                            Console.WriteLine($"Saldo: {saldo}");
                            Console.WriteLine("Vil du fortsætte? (J/N)");
                            string svar = Console.ReadLine();
                            if (svar == "N" || svar == "n")
                            {
                                fortsæt = false;
                            }
                        }
                        else if (valg == "H" || valg == "h")
                        {
                            Console.WriteLine("Indtast beløb");
                            int beløb = int.Parse(Console.ReadLine());
                            if (beløb > saldo)
                            {
                                Console.WriteLine("Fejl: Du har ikke penge nok til at hæve dette beløb");
                                System.Threading.Thread.Sleep(3000);
                                Console.Clear();
                                continue;
                            }
                            saldo -= beløb;
                            Console.WriteLine($"Saldo: {saldo}");
                            Console.WriteLine("Vil du fortsætte? (J/N)");
                            string svar = Console.ReadLine();
                            if (svar == "N" || svar == "n")
                            {
                                fortsæt = false;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Fejl: Du skal indtaste I eller H");
                            System.Threading.Thread.Sleep(3000);
                            Console.Clear();
                            break;
                        }
                    } while (fortsæt);

                    break;
                case "10":
                    //MARK: Opretning af Diagrammer for et Program (Afventer)
                    System.Threading.Thread.Sleep(3000);
                    Console.Clear();
                    break;
                case "11":
                    //MARK: Logik-baseret Login

                    int attempts = 0;
                    Console.WriteLine("Indtast dit brugernavn");
                    string brugernavn = Console.ReadLine();
                    Console.WriteLine("Indtast dit password");
                    string password = Console.ReadLine();
                    if (brugernavn == "admin" && password == "1234")
                    {
                        Console.WriteLine("Du er logget ind");
                        System.Threading.Thread.Sleep(3000);
                        Console.Clear();
                        break;
                    } else {
                        Console.WriteLine("Fejl: Forkert brugernavn eller password");
                        attempts++;
                    }

                    if (attempts == 3)
                    {
                        Console.WriteLine("Du har forsøgt 3 gange. Programmet vil nu afsluttes.");
                        System.Threading.Thread.Sleep(3000);
                        Console.Clear();
                        break;
                    }

                    System.Threading.Thread.Sleep(3000);
                    Console.Clear();
                    break;
                case "12":
                    //MARK: Analysering af tekstfiler
                    Console.WriteLine("Indtast navnet på en tekstfil");
                    string filnavn = Console.ReadLine();

                    try {
                        string tekst = File.ReadAllText(filnavn);
                        Console.WriteLine($"Filen indeholder {tekst.Length} tegn");
                    } catch (Exception ex) {
                        Console.WriteLine($"Fejl: {ex.Message}");
                    }
                    System.Threading.Thread.Sleep(3000);
                    Console.Clear();
                    break;
                case "13":
                    //MARK: Versionering af programændringer
                    Console.WriteLine("Versionering af programændringer");
                    Console.WriteLine("\nEksempel på versioneringskommentarer:");
                    Console.WriteLine("feat: tilføjet ny funktionalitet");
                    Console.WriteLine("fix: rettet fejl i beregning");
                    Console.WriteLine("docs: opdateret dokumentation");
                    Console.WriteLine("style: formatering af kode");
                    Console.WriteLine("refactor: omstrukturering af kode");
                    Console.WriteLine("test: tilføjet tests");
                    System.Threading.Thread.Sleep(3000);
                    Console.Clear();
                    break;
                case "14":
                    //MARK: Geometriske beregninger

                    Console.WriteLine("Hvilken figur vil du beregne arealet af? \n0) Exit \n1) Kvadrat \n2) Rektangel \n3) Trekant \n4) Cirkel");
                    string figur = Console.ReadLine();
                    bool continueGeometriskeBeregninger = true;

                    while (continueGeometriskeBeregninger)
                    {
                        switch (figur)
                        {
                        case "0":
                            continueGeometriskeBeregninger = false;
                            break;
                        case "1":
                            Console.WriteLine("Indtast sidelængden");
                            double sidelængde = double.Parse(Console.ReadLine());
                            Console.WriteLine($"Arealet af kvadratet er {sidelængde * sidelængde}");
                            Console.WriteLine($"Omkredsen af kvadratet er {4 * sidelængde}");
                            break;
                        case "2":
                            Console.WriteLine("Indtast længden");
                            double længde = double.Parse(Console.ReadLine());
                            Console.WriteLine("Indtast bredden");
                            double bredde = double.Parse(Console.ReadLine());
                            Console.WriteLine($"Arealet af rektanglet er {længde * bredde}");
                            Console.WriteLine($"Omkredsen af rektanglet er {2 * (længde + bredde)}");
                            break;
                        case "3":
                            Console.WriteLine("Indtast grundlinjen");
                            double grundlinje = double.Parse(Console.ReadLine());
                            Console.WriteLine("Indtast højden");
                            double højde = double.Parse(Console.ReadLine());
                            Console.WriteLine($"Arealet af trekanten er {0.5 * grundlinje * højde}");
                            Console.WriteLine($"Omkredsen af trekanten er {grundlinje + højde + Math.Sqrt(Math.Pow(grundlinje, 2) + Math.Pow(højde, 2))}");
                            break;
                        case "4":
                            Console.WriteLine("Indtast radiusen");
                            double radius = double.Parse(Console.ReadLine());
                            Console.WriteLine($"Arealet af cirklen er {Math.PI * Math.Pow(radius, 2)}");
                            Console.WriteLine($"Omkredsen af cirklen er {2 * Math.PI * radius}");
                            

                            break;
                        default:
                            Console.WriteLine("Fejl: Du skal indtaste en gyldig figur");
                            break;
                        }
                    }
                    
                    System.Threading.Thread.Sleep(3000);
                    Console.Clear();
                    break;
                case "15":
                    //MARK: Udvidet månedsbudget
                    string[] months = {"Januar", "Februar", "Marts", "April", "Maj", "Juni", "Juli", "August", "September", "Oktober", "November", "December"};
                    int[] budget = {2000, 2200, 2100, 2300, 2400, 2500, 2600, 2700, 2800, 2900, 3000, 3100};

                    for (int i = 0; i < months.Length; i++)
                    {
                        Console.WriteLine($"{months[i]}: {budget[i]}");
                    }

                    Console.WriteLine("Gennemsnitligt budget: {0}", budget.Average());

                    System.Threading.Thread.Sleep(3000);
                    Console.Clear();
                    break;
                case "16":
                    //MARK: Filter tal rækkefølge

                    Console.WriteLine("Indtast dine tal med en mellemrum");
                    string talFilterRækkefølge = Console.ReadLine();
                    string[] talArrayFilterRækkefølge = talFilterRækkefølge.Split(' ');

                    Console.WriteLine("Dette er dine tal i nedadgående rækkefølge");
                    Array.Sort(talArrayFilterRækkefølge);
                    Array.Reverse(talArrayFilterRækkefølge);
                    foreach (string tal in talArrayFilterRækkefølge)
                    {
                        Console.WriteLine(tal);
                    }

                    System.Threading.Thread.Sleep(3000);
                    Console.Clear();
                    break;
                case "17":
                    //MARK: Sortér kontakter alfabetisk
                    string[] kontakter = {"John Doe", "Jane Smith", "Alice Johnson", "Bob Brown", "Charlie Davis"};
                    Array.Sort(kontakter);
                    foreach (string kontakt in kontakter)
                    {
                        Console.WriteLine(kontakt);
                    }
                    System.Threading.Thread.Sleep(3000);
                    Console.Clear();
                    break;
                case "18":
                    //MARK: Email validering med regex
                    Console.WriteLine("Indtast din email");
                    string email = Console.ReadLine();
                    Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                    Match match = regex.Match(email);
                    if (match.Success)
                    {
                        Console.WriteLine("Emailen er gyldig");
                    }
                    else
                    {
                        Console.WriteLine("Emailen er ikke gyldig");
                    }
                    System.Threading.Thread.Sleep(3000);
                    Console.Clear();
                    break;
                case "19":
                    //MARK: Fibonaci Sekvens 
                    Console.WriteLine("Indtast antal tal i Fibonaci rækken");
                    int antal = int.Parse(Console.ReadLine());
                    int[] fibonaci = new int[antal];
                    fibonaci[0] = 0;
                    fibonaci[1] = 1;
                    for (int i = 2; i < antal; i++)
                    {
                        fibonaci[i] = fibonaci[i - 1] + fibonaci[i - 2];
                    }
                    foreach (int tal in fibonaci)
                    {
                        Console.WriteLine(tal);
                    }
                    System.Threading.Thread.Sleep(3000);
                    Console.Clear();
                    break;
                case "20":
                    //MARK: Opgavekommentarer og Dokumentation
                    Console.WriteLine("Opgavekommentarer og Dokumentation");
                    Console.WriteLine("Opgavekommentarer er kommentarer, der beskriver, hvordan en opgave er løst");
                    Console.WriteLine("Dokumentation er en beskrivelse af, hvordan en opgave er løst");
                    System.Threading.Thread.Sleep(3000);
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("Fejl: Du skal indtaste en gyldig opgave");
                    System.Threading.Thread.Sleep(3000);
                    // Console.Clear();
                    break;
            }
            } while (intermediateTasks);
        }


        //MARK: Advanced Tasks


        Console.ReadKey();
    }
}