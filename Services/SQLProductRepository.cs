using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services
{
    public class SQLProductRepository : IProductRepository
    {
        private readonly AppDbContext context;

        public SQLProductRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Product AddProduct(Product newProduct)
        {

            context.Database.ExecuteSqlRaw("spInsertClient {0},{1},{2},{3}",
                                            newProduct.Name,
                                            newProduct.Description,
                                            newProduct.Price,
                                            newProduct.PhotoPath);


            return newProduct;
        }


        public Product Delete(int id)
        {
            Product product = context.Products.Find(id);
            if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
            }
            return product;

        }

        public IEnumerable<Product> GetAllProducts()
        {
            return context.Products
                .FromSqlRaw<Product>("SELECT * FROM Products")
                .ToList();
        }

        public Product GetProduct(int id)
        {
            SqlParameter parameter = new SqlParameter("Id", id);

            return context.Products.FromSqlRaw<Product>("spGetProductById @Id", parameter)
                .ToList()
                .FirstOrDefault();
        }

        public IEnumerable<Product> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return context.Products;
            }

            return context.Products.Where(x => x.Name.Contains(searchTerm));
        }

        public Product Update(Product updatedProduct)
        {
            var product = context.Products.Attach(updatedProduct);
            product.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return updatedProduct;
        }
    }
}
