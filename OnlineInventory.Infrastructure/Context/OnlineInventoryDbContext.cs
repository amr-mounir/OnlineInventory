using Microsoft.EntityFrameworkCore;
using OnlineInventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace OnlineInventory.Infrastructure.Context
{
    public class OnlineInventoryDbContext : DbContext
    {
        public OnlineInventoryDbContext(DbContextOptions<OnlineInventoryDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
