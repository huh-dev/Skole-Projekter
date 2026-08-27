// KodestandardTjekliste.cs
//
// ØVELSESFIL — Modul 3, Lektion 5.
// Denne fil "virker" (den kompilerer og giver korrekte resultater), men
// overtræder Microsofts C#-kodestandard på en lang række punkter.
//
// DIN OPGAVE: Find og noter, hvilke KATEGORIER af fejl du kan se i filen
// (ikke bare hvert enkelt sted — men hvilken type problem det er), og ret
// derefter filen, så den fuldt ud overholder kodestandarden fra materiale.md.
//
// Underviserens facitliste findes i KodestandardTjekliste-FACIT.md i denne
// mappe — kig IKKE i den, før du selv har lavet øvelsen færdig.

class ProductCalculator
{
    private static List<(string ItemName, decimal PricePerItem, int Quantity)> items = new List<(string, decimal, int)>();

    static void Main(string[] args)
    {

        //Update so users can add more items after if needed
        bool addMoreItems = false;
        int iQuantity = 0;
        decimal dPrice = 0;
        do
        {
            Console.WriteLine("Indtast et varenavn:");
            string strItemName = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Indtast antal varer:");
            string strQuantity = Console.ReadLine() ?? string.Empty;
            int itemQuantity = Convert.ToInt32(strQuantity);
            iQuantity += itemQuantity;
            Console.WriteLine("Pris pr. vare:");
            string strPrice = Console.ReadLine() ?? string.Empty;
            dPrice = Convert.ToDecimal(strPrice);
            items.Add((strItemName, dPrice, itemQuantity));

            Console.WriteLine("Vil du tilføje flere varer? (j/n)");
            string strAddMoreItems = Console.ReadLine() ?? string.Empty;
            addMoreItems = strAddMoreItems == "j" ? true : false;
        } while (addMoreItems);

        // Sætter x til antal gange pris
        decimal x = items.Sum(item => item.PricePerItem * item.Quantity);

        if (x > 500)
        {
        decimal y = CalculateDiscount(x);
        decimal z = x - y;
        Console.WriteLine("Rabat: " + Math.Round(y, 2));
        Console.WriteLine("Varer:");
        foreach (var item in items)
        {
            Console.WriteLine($"- {item.ItemName}: {item.Quantity} stk. á {item.PricePerItem:C}");
        }
        Console.WriteLine("Total: " + Math.Round(z, 2));
        }
        else
        {
            Console.WriteLine("Total: " + Math.Round(x, 2));
            Console.WriteLine("Varer:");
            foreach (var item in items)
            {
                Console.WriteLine($"- {item.ItemName}: {item.Quantity} stk. á {item.PricePerItem:C}");
            }
        }
   

        string message = CalculateStatus(iQuantity);
        Console.WriteLine(message);
    }

    static decimal CalculateDiscount(decimal total)
    {
        if (total > 500)
        {
            return total * 0.15m;
        }
        return 0;
    }

    static string CalculateStatus(int Quantity)
    {
        if (Quantity > 50)
        {
            return "Stor ordre";
        }
        return "Almindelig ordre";
    }
}

class Customer
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
}
