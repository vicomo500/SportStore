using SportStore.Domain.Abstract;
using SportStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private EFDbContext dbContext = new EFDbContext();

        public IEnumerable<Product> Products
        {
            get { return dbContext.Products; }
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                dbContext.Products.Add(product);
            }
            else
            {
                Product dbEntry = dbContext.Products.Find(product.ProductID);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                    dbEntry.ImageData = product.ImageData;
                    dbEntry.ImageMimeType = product.ImageMimeType;
                }
            }
            dbContext.SaveChanges();
        }

        public Product DeleteProduct(int productID)
        {
            Product dbEntry = dbContext.Products.Find(productID);
            if (dbEntry != null)
            {
                dbContext.Products.Remove(dbEntry);
                dbContext.SaveChanges();
            }
            return dbEntry;
        }
    }
}
