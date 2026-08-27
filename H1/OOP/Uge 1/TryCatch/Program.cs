namespace TryCatch
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Indtast et tal: ");
            string input = Console.ReadLine();
            TestTryCatch(input);
        }

        static void TestTryCatch(string input)
        {
            try
            {
                int tal = int.Parse(input);
                Console.WriteLine($"Du indtastede tallet: {tal}");
                Console.WriteLine("Indtast et andet tal: ");
                string input2 = Console.ReadLine();
                Division(tal, input2);
            }
            catch (FormatException)
            {
                Console.WriteLine("Indtastet tal er ikke et tal");
            }
            finally
            {
                Console.WriteLine("Programmet er afsluttet");
            }
        }

        static void Division(int input1, string input2)
        {
            try
            {
                int tal = int.Parse(input2);
                Console.WriteLine($"Du indtastede tallet: {tal}");
                int result = input1 / tal;
                Console.WriteLine($"Resultatet af divisionen er: {result}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Indtastet tal er ikke et tal");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Du kan ikke dividere med 0");
            }
            finally
            {
                Console.WriteLine("Programmet er afsluttet");
            }
        }
    }
}
