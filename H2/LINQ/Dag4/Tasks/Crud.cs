namespace LINQ.Dag4.Tasks;

public static class Crud
{
    //CREATE A NEW PRODUCT
    public static Product[] CreateProduct(this Product[] products, Product product)
    {
        return products.Append(product).ToArray();
    }

    //UPDATE AN EXISTING PRODUCTS PRICE
    public static Product[] UpdateProductPrice(this Product[] products, Product product, decimal price)
    {
        return products.Select(p => p.name == product.name ? new Product(p.name, price, p.category) : p).ToArray();
    }

    //DELETE A PRODUCT
    public static Product[] DeleteProduct(this Product[] products, Product product)
    {
        return products.Where(p => p.name != product.name).ToArray();
    }
}