using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OnlineInventory.Application.Interfaces;
using OnlineInventory.Domain.Entities;
using OnlineInventory.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineInventory.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly OnlineInventoryDbContext _dbContext;

        public ProductRepository(OnlineInventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _dbContext.Products.ToList();
        }

        public IEnumerable<Product> GetProductById(int productId)
        {
            return _dbContext.Products.Where(p => p.ProductId == productId).ToList();
        }

        public async Task InsertProductAsync(Product product)
        {
            product.CreatedAt = DateTime.UtcNow;
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();

            LogAuditEntry("Product created", product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            product.UpdatedAt = DateTime.UtcNow;
            _dbContext.Entry(product).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            LogAuditEntry("Product updated", product);
        }

        public async Task DeleteProductByIdAsync(int productId)
        {
            var product = await _dbContext.Products.FindAsync(productId);

            if (product != null)
            {
                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();

                LogAuditEntry("Product deleted", product);
            }
        }

        private void LogAuditEntry(string action, Product product)
        {
            var auditLog = new
            {
                Timestamp = DateTime.UtcNow,
                Action = action,
                ProductId = product.ProductId,
                ProductName = product.ProductName
            };

            var firebaseDatabase = new Firebase.Database.FirebaseClient("https://onlineinventory-56e2f-default-rtdb.firebaseio.com");
            firebaseDatabase.Child("audit_logs").PostAsync(JsonConvert.SerializeObject(auditLog));
        }
    }
}
