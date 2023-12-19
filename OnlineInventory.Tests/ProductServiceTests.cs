namespace OnlineInventory.Application.Tests
{
    public class ProductServiceTests
    {
        [Fact]
        public void GetAllProducts_ReturnsProducts()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(repo => repo.GetAllProducts())
                .Returns(new List<Product> { new Product(), new Product() });

            var productService = new ProductService(mockRepository.Object);

            // Act
            var result = productService.GetAllProducts();

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IEnumerable<Product>>();
            result.Should().HaveCount(2);
        }

        [Fact]
        public void GetProductById_ReturnsProduct()
        {
            // Arrange
            var productId = 1;
            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(repo => repo.GetProductById(productId))
                .Returns(new List<Product> { new Product { ProductId = productId } });

            var productService = new ProductService(mockRepository.Object);

            // Act
            var result = productService.GetProductById(productId);

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IEnumerable<Product>>();
            result.Should().HaveCount(1);
            result.Should().Contain(p => p.ProductId == productId);
        }

        [Fact]
        public async Task InsertProductAsync_ShouldAddProductToRepository()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var productService = new ProductService(mockRepository.Object);
            var product = new Product { ProductName = "TestProduct", ProductPrice = 10.99m, ProductQuantity = 5 };

            // Act
            await productService.InsertProductAsync(product);

            // Assert
            mockRepository.Verify(repo => repo.InsertProductAsync(product), Times.Once);
        }

        [Fact]
        public async Task UpdateProductAsync_ShouldUpdateProductInRepository()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var productService = new ProductService(mockRepository.Object);
            var product = new Product { ProductId = 1, ProductName = "UpdatedProduct", ProductPrice = 15.99m, ProductQuantity = 10 };

            // Act
            await productService.UpdateProductAsync(product);

            // Assert
            mockRepository.Verify(repo => repo.UpdateProductAsync(product), Times.Once);
        }

        [Fact]
        public async Task DeleteProductByIdAsync_ShouldDeleteProductFromRepository()
        {
            // Arrange
            var productId = 1;
            var mockRepository = new Mock<IProductRepository>();
            var productService = new ProductService(mockRepository.Object);

            // Act
            await productService.DeleteProductByIdAsync(productId);

            // Assert
            mockRepository.Verify(repo => repo.DeleteProductByIdAsync(productId), Times.Once);
        }
    }
}