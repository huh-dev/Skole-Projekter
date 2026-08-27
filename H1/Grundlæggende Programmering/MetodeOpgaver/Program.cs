namespace MetodeOpgaver
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.Clear();
            bool keepRunning = true;
            do
            {
                Console.WriteLine("Vælg nogle opgaver: \n0) Exit\n1) 1-6 \n2) 7");
                string inputs = Console.ReadLine();
                switch (inputs)
                {
                    case "0":
                        keepRunning = false;
                        break;
                    case "1":
                        bool keepRunningSeven = true;
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Opgaverne her fremviser alle div. opgaver fra 1-6 i metode dokumentationen \nØnsker du stadig at se opgaverne? \n1) Ja \n2) Nej");
                            string inputSeven = Console.ReadLine();
                            switch (inputSeven)
                            {
                                case "1":
                                    Console.Clear();
                                    FirstSix.Setup();
                                    break;
                                case "2":
                                    keepRunningSeven = false;
                                    Console.Clear();
                                    break;
                                default:
                                    Console.WriteLine("Invalid input");
                                    System.Threading.Thread.Sleep(1000);
                                    Console.Clear();
                                    break;
                            }
                        } while (keepRunningSeven);
                        break;
                    case "2":
                        LastSeven.Setup();
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        System.Threading.Thread.Sleep(1000);
                        Console.Clear();
                        break;
                }
            } while (keepRunning);

            Console.ReadKey();
        }
    }
}

