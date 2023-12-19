namespace OnlineInventory.Tests
{
    public class ProductControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public ProductControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAllProducts_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/products");

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetProductById_ReturnsSuccessStatusCode()
        {
            // Arrange
            var productId = 1;
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"/api/products/{productId}");

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task InsertProduct_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var productServiceMock = new Mock<IProductService>();
            var controller = new ProductController(productServiceMock.Object);

            // Act
            var response = await client.PostAsJsonAsync("/api/products", new Product());

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // Verify that the service method was called
            productServiceMock.Verify(service => service.InsertProductAsync(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public async Task UpdateProduct_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var productServiceMock = new Mock<IProductService>();
            var controller = new ProductController(productServiceMock.Object);

            // Act
            var response = await client.PutAsJsonAsync("/api/products", new Product());

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // Verify that the service method was called
            productServiceMock.Verify(service => service.UpdateProductAsync(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public async Task DeleteProductById_ReturnsSuccessStatusCode()
        {
            // Arrange
            var productId = 1;
            var client = _factory.CreateClient();
            var productServiceMock = new Mock<IProductService>();
            var controller = new ProductController(productServiceMock.Object);

            // Act
            var response = await client.DeleteAsync($"/api/products/{productId}");

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // Verify that the service method was called
            productServiceMock.Verify(service => service.DeleteProductByIdAsync(productId), Times.Once);
        }
    }
}
