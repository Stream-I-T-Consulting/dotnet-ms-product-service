
using System.Collections.Generic;

namespace ProductService.Model
{
  public interface IDataRepository
  {
    List<Product> AddProduct(Product Product);
    List<Product> GetProducts();
    Product PutProduct(Product Product);
    Product GetProductById(Int64 id);
  }
}
