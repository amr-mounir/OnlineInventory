namespace OnlineInventory.Infrastructure.Tests
{
    public class ProductRepositoryTests
    {
        [Fact]
        public async Task GetAllProducts_ReturnsProductsFromDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<OnlineInventoryDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Database_GetAllProducts")
                .Options;

            using (var context = new OnlineInventoryDbContext(options))
            {
                context.Products.AddRange(new Product(), new Product());
                context.SaveChanges();
            }

            using (var context = new OnlineInventoryDbContext(options))
            {
                var repository = new ProductRepository(context);

                // Act
                var result = repository.GetAllProducts();

                // Assert
                result.Should().NotBeNull().And.BeAssignableTo<IEnumerable<Product>>();
                result.Should().HaveCount(2);
            }
        }

        [Fact]
        public async Task GetProductById_ReturnsProductFromDatabase()
        {
            // Arrange
            var productId = 1;
            var options = new DbContextOptionsBuilder<OnlineInventoryDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Database_GetProductById")
                .Options;

            using (var context = new OnlineInventoryDbContext(options))
            {
                context.Products.Add(new Product { ProductId = productId });
                context.SaveChanges();
            }

            using (var context = new OnlineInventoryDbContext(options))
            {
                var repository = new ProductRepository(context);

                // Act
                var result = repository.GetProductById(productId);

                // Assert
                result.Should().NotBeNull().And.BeAssignableTo<IEnumerable<Product>>();
                result.Should().HaveCount(1);
                result.First().ProductId.Should().Be(productId);
            }
        }

        [Fact]
        public async Task InsertProductAsync_ShouldAddProductToDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<OnlineInventoryDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Database_InsertProductAsync")
                .Options;

            using (var context = new OnlineInventoryDbContext(options))
            {
                var repository = new ProductRepository(context);
                var product = new Product { ProductName = "TestProduct", ProductPrice = 10.99m, ProductQuantity = 5 };

                // Act
                await repository.InsertProductAsync(product);
            }

            // Assert
            using (var context = new OnlineInventoryDbContext(options))
            {
                context.Products.Should().HaveCount(1);
                context.Products.Should().Contain(p => p.ProductName == "TestProduct");
            }
        }

        [Fact]
        public async Task UpdateProductAsync_ShouldUpdateProductInDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<OnlineInventoryDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Database_UpdateProductAsync")
                .Options;

            using (var context = new OnlineInventoryDbContext(options))
            {
                context.Products.Add(new Product { ProductId = 1, ProductName = "OriginalProduct", ProductPrice = 10.99m, ProductQuantity = 5 });
                context.SaveChanges();
            }

            using (var context = new OnlineInventoryDbContext(options))
            {
                var repository = new ProductRepository(context);
                var product = new Product { ProductId = 1, ProductName = "UpdatedProduct", ProductPrice = 15.99m, ProductQuantity = 10 };

                // Act
                await repository.UpdateProductAsync(product);
            }

            // Assert
            using (var context = new OnlineInventoryDbContext(options))
            {
                var updatedProduct = context.Products.Find(1);
                updatedProduct.ProductName.Should().Be("UpdatedProduct");
            }
        }

        [Fact]
        public async Task DeleteProductByIdAsync_ShouldDeleteProductFromDatabase()
        {
            // Arrange
            var productId = 1;
            var options = new DbContextOptionsBuilder<OnlineInventoryDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Database_DeleteProductByIdAsync")
                .Options;

            using (var context = new OnlineInventoryDbContext(options))
            {
                context.Products.Add(new Product { ProductId = productId, ProductName = "TestProduct", ProductPrice = 10.99m, ProductQuantity = 5 });
                context.SaveChanges();
            }

            using (var context = new OnlineInventoryDbContext(options))
            {
                var repository = new ProductRepository(context);

                // Act
                await repository.DeleteProductByIdAsync(productId);
            }

            // Assert
            using (var context = new OnlineInventoryDbContext(options))
            {
                context.Products.Should().HaveCount(0);
            }
        }
    }
}