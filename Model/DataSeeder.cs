using System.Collections.Generic;
using System.Linq;

namespace ProductService.Model
{
  public class DataSeeder
  {
    private readonly ProductDbContext ProductDbContext;

    public DataSeeder(ProductDbContext ProductDbContext)
    {
      this.ProductDbContext = ProductDbContext;
    }

    public void Seed()
    {
      if (!ProductDbContext.Product.Any())
      {
        var Products = new List<Product>()
                {
                        new Product()
                        {
                            Name = "Apple",
                            Price = 100,
                            ProductId = 1
                        },
                        new Product()
                        {
                            Name = "Banana",
                            Price = 200,
                            ProductId = 2
                        },
                        new Product()
                        {
                            Name = "Carrot",
                            Price = 300,
                            ProductId = 3
                        }
                };

        ProductDbContext.Product.AddRange(Products);
        ProductDbContext.SaveChanges();
      }
    }
  }
}
