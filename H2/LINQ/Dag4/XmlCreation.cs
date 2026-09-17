using System.Xml.Serialization;

namespace LINQ.Dag4;

public static class XmlCreation
{
    public static void CreateXml()
    {

        //Create the XML File if not exists
        if (!File.Exists("products.xml"))
        {
            File.Create("products.xml");
        }

        //Serialize the products to the XML File
        var xml = new XmlSerializer(typeof(Product[]));
        using var stream = File.OpenWrite("products.xml");
        xml.Serialize(stream, PopulateXmlWithProducts());
        Console.WriteLine("XML created successfully");
    }

    public static void WriteXml(Product[] products)
    {
        var xml = new XmlSerializer(typeof(Product[]));
        using var stream = File.OpenWrite("products.xml");
        xml.Serialize(stream, products);
    }

    public static Product[] ReadXml()
    {
        var xml = new XmlSerializer(typeof(Product[]));
        using var stream = File.OpenRead("products.xml");
        var products = xml.Deserialize(stream) as Product[];
        return products;
    }


    public static Product[] PopulateXmlWithProducts()
    {
        var products = new List<Product>
        {
            new Product("Gaming Laptop", 12500m, "Computer"),
            new Product("Office Laptop", 7500m, "Computer"),
            new Product("Gaming Mus", 650m, "Tilbehør"),
            new Product("Keyboard", 1100m, "Tilbehør"),
            new Product("4K Skærm", 4500m, "Skærm"),
            new Product("Gaming Headset", 1500m, "Tilbehør"),
            new Product("27\" Gaming Skærm", 5500m, "Skærm"),
            new Product("USB-C Dock", 1800m, "Tilbehør"),
            new Product("MacBook Air", 9500m, "Computer"),
            new Product("Gaming PC", 15000m, "Computer"),
            new Product("Webkamera", 850m, "Tilbehør"),
            new Product("32\" 4K Skærm", 5500m, "Skærm")
        };
   
        return products.ToArray();
    }
}