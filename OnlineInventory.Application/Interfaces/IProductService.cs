using OnlineInventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineInventory.Application.Interfaces
{
    public interface IProductService
    {
        
            IEnumerable<Product> GetAllProducts();
            IEnumerable<Product> GetProductById(int productId);
            Task InsertProductAsync(Product product);
            Task UpdateProductAsync(Product product);
            Task DeleteProductByIdAsync(int productId);
    }
}
