using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface IProductRepository
    {
        IEnumerable<Product> Search(string searchTerm);
        IEnumerable<Product> GetAllProducts();
        Product GetProduct(int id);
        Product Update(Product updatedProduct);
        Product AddProduct(Product newProduct);
        Product Delete(int id);

    }
}
