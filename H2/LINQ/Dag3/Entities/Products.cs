using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LINQ.Dag3.Entities;

[Table("Products")]
public class Products
{

    [Key] public int id { get; set; }

    [Column("name")] public string name { get; set; }

    [Column("price")] public decimal price { get; set; }

    [Column("category")] public string category { get; set; }

    [Column("units_sold")] public int units_sold { get; set; }

    [Column("specifications")] public string specifications { get; set; }
    
    [Column("created_at")] public DateTime created_at { get; set; }

    [Column("updated_at")] public DateTime updated_at { get; set; }

}