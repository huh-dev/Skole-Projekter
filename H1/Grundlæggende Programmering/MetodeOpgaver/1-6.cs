namespace MetodeOpgaver
{
    internal class FirstSix
    {
        public static void Setup()
        {
            bool keepRunning = true;
            do 
            {
                Console.Clear();
                Console.WriteLine("Vælg nogle opgaver: \n0) Exit \n1) Hello World \n2) Bruger Inputs \n3) Regning af tre tal \n4) Navn og alder gruppe inddelig \n5) Opdeling af komma-separeret streng \n6) Gæt et tal");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "0":
                        keepRunning = false;
                        Program.Main(null);
                        break;
                    case "1":
                        Console.WriteLine(HelloWorld());
                        System.Threading.Thread.Sleep(1000);
                        Console.Clear();
                        break;
                    case "2":
                        Console.WriteLine("Indtast et input i string: ");
                        string userInput = Console.ReadLine();
                        brugerInput(userInput);
                        System.Threading.Thread.Sleep(1000);
                        Console.Clear();  
                        break;
                    case "3":
                        Console.WriteLine("Indtast tal1: ");
                        int tal1 = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Indtast tal2: ");
                        int tal2 = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Indtast tal3: ");
                        int tal3 = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Indtast operator: 1) Plus 2) Minus 3) Gange 4) Divider");
                        string operatorChoice = Console.ReadLine();
                        Console.WriteLine(Regning(tal1, tal2, tal3, operatorChoice));
                        System.Threading.Thread.Sleep(1000);
                        Console.Clear();
                        break;
                    case "4":
                        Console.WriteLine("Indtast alder: ");
                        int alder = Convert.ToInt32(Console.ReadLine());

                        GruppeInddelig(alder);

                        System.Threading.Thread.Sleep(1000);
                        Console.Clear();
                        break;
                    case "5":
                        Console.WriteLine("Indtast en komma-separeret streng: ");
                        string kommaSepareret = Console.ReadLine();
                        Opdeling(kommaSepareret);
                        System.Threading.Thread.Sleep(1000);
                        Console.Clear();
                        break;
                    case "6":
                        Random random = new Random();
                        int hemmeligtTal = random.Next(1, 25);
                        bool keepRunningGæt = true;
                        do
                        {
                            bool gæt = GætTal(hemmeligtTal);

                            if (gæt)
                            {
                                keepRunningGæt = false;
                                System.Threading.Thread.Sleep(1000);
                                Console.Clear();
                            }
                        } while (keepRunningGæt);
                        

                        System.Threading.Thread.Sleep(1000);
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine("Ugyldigt valg");
                        break;
                }
            } while (keepRunning);
        }



        //MARK: Hello World (OPG. 1)
        private static string HelloWorld()
        {
            return "Hello, World!";
        }
        
        //MARK: Bruger Input (OPG. 2)
        private static void brugerInput(string input)
        {
            Console.WriteLine(input);
        }

        //MARK: Regning af tre tal (OPG. 3)
        private static int Regning(int tal1, int tal2, int tal3, string operatorChoice)
        {
            switch (operatorChoice)
            {
                case "1":
                    return tal1 + tal2 + tal3;
                case "2":
                    return tal1 - tal2 - tal3;
                case "3":
                    return tal1 * tal2 * tal3;
                case "4":
                    return (tal1 + tal2) / tal3;
                default:
                    return 0;
            }
        }

        //MARK: Navn og alder gruppe inddelig (OPG. 4)
        private static void GruppeInddelig(int alder)
        {
            if (alder <= 1)
            {
                Console.WriteLine("Du er nyfødt");
            }
            else if (alder >= 2 && alder <= 3)
            {
                Console.WriteLine("Du er i dagpleje eller vuggestue");
            }
            else if (alder >= 4 && alder <= 5)
            {
                Console.WriteLine("Du er i børnehave");
            }
            else if (alder >= 6 && alder <= 18)
            {
                Console.WriteLine("Du er i skole.");
            }

        }

        //MARK: Opdeling af komma-separeret streng (OPG. 5)
        private static void Opdeling(string input)
        {
            string[] opdeling = input.Split(',');
            foreach (string element in opdeling)
            {
                Console.WriteLine(element);
            }
        }

        //MARK: Gæt et tal (OPG. 6)
        private static bool GætTal(int hemmeligtTal)
        {

            Console.WriteLine("Gæt et tal mellem 1 og 25: ");
            int gæt = Convert.ToInt32(Console.ReadLine());
            
            do
            {
                if (gæt == hemmeligtTal)
                {
                    Console.WriteLine("Du har gættet det hemmelige tal!");
                    return true;
                } else if (gæt < hemmeligtTal)
                {
                    Console.WriteLine("Du har gættet for lavt!");
                    return false;
                } else if (gæt > hemmeligtTal)
                {
                    Console.WriteLine("Du har gættet for højt!");
                    return false;
                }
            } while (gæt != hemmeligtTal);

            return false;



        }


    }
}