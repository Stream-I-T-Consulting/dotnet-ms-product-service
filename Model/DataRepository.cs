using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Model
{
  public class DataRepository : IDataRepository
  {
    private readonly ProductDbContext db;

    public DataRepository(ProductDbContext db)
    {
      this.db = db;
    }

    public List<Product> GetProducts() => db.Product.ToList();

    public Product PutProduct(Product Product)
    {
      db.Product.Update(Product);
      db.SaveChanges();
      return db.Product.Where(x => x.ProductId == Product.ProductId).First();
    }

    public List<Product> AddProduct(Product Product)
    {
      db.Product.Add(Product);
      db.SaveChanges();
      return db.Product.ToList();
    }

    public Product GetProductById(Int64 Id)
    {
      return db.Product.Where(x => x.ProductId == Id).First();
    }

  }
}