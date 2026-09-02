namespace Lesson06;

public class Kunde
{
    public string Navn { get; set; }
    public string Kørekortnummer { get; set; }
    public int Telefonnummer { get; set; }

    public Kunde(string navn, string kørekortnummer, int telefonnummer)
    {
        Navn = navn;
        Kørekortnummer = kørekortnummer;
        Telefonnummer = telefonnummer;
    }
}