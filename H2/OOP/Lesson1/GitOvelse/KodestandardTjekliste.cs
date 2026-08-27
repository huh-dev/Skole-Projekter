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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class productCalculator
{
    const int maxQuantity = 100;
    static List<string> items = new List<string>();

    static void Main(string[] args)
    {

        //Update so users can add more items after if needed
        bool addMoreItems = false;
        int iQuantity = 0;
        do
        {
            Console.WriteLine("Indtast et varenavn:");
            string strItemName = Console.ReadLine();
            Console.WriteLine("Indtast antal varer:");
            string strQuantity = Console.ReadLine();
            items.Add($"{strItemName} - {strQuantity}");
            iQuantity += Convert.ToInt32(strQuantity);

            Console.WriteLine("Vil du tilføje flere varer? (j/n)");
            string strAddMoreItems = Console.ReadLine() ?? string.Empty;
            addMoreItems = strAddMoreItems == "j" ? true : false;
        } while (addMoreItems);

    

        Console.WriteLine("Indtast pris pr. vare:");
        string strPrice = Console.ReadLine();
            decimal dPrice = Convert.ToDecimal(strPrice);

        // Sætter x til antal gange pris
        decimal x = iQuantity * dPrice;

        if (x > 500)
        {
        decimal y = CalculateDiscount(x);
        decimal z = x - y;
            Console.WriteLine("Rabat: " + Math.Round(y, 2));
            Console.WriteLine("Items: " + string.Join(", ", items));
            Console.WriteLine("Total: " + Math.Round(z, 2));
        }
        else {
            Console.WriteLine("Total: " + Math.Round(x, 2));
            Console.WriteLine("Items: " + string.Join(", ", items));
        }

        var message = calculate_Status(iQuantity);
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

    static string calculate_Status(int Quantity)
    {
        if (Quantity > 50)
        {
            return "Stor ordre";
        }
        return "Almindelig ordre";
    }
}

class customer
{
    public string name { get; set; }
    public string Phone_Number { get; set; }
}
