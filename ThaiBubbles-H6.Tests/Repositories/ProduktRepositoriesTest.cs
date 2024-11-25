
namespace ThaiBubbles_H6.Tests.Repositories
{
    public class ProductRepositoryTests
    {
        private DbContextOptions<DatabaseContext> _options;
        private DatabaseContext _context;
        private ProductRepositories _productRepository;

        public ProductRepositoryTests()
        {
            // Set up the in-memory database
            _options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "ProductRepository")
                .Options;

            _context = new DatabaseContext(_options);
            _productRepository = new ProductRepositories(_context);
        }

        [Fact]
        public async Task GetAll_ShouldReturnListOfProducts_WhenProductsExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            _context.Product.Add(new Product { ProductID = 1, Name = "Tea", Price = 50, Description = "Bubble Tea", CategoryId = 1 });
            _context.Product.Add(new Product { ProductID = 2, Name = "Smoothie", Price = 75, Description = "Fruit Smoothie", CategoryId = 2 });
            await _context.SaveChangesAsync();

            // Act
            var result = await _productRepository.GetAllProducts();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetAll_ShouldReturnEmptyList_WhenNoProductsExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _productRepository.GetAllProducts();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Product>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetById_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int productId = 1;
            _context.Product.Add(new Product { ProductID = productId, Name = "Tea", Price = 50, Description = "Bubble Tea", CategoryId = 1 });
            await _context.SaveChangesAsync();

            // Act
            var result = await _productRepository.GetProductById(productId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(productId, result.ProductID);
        }

        [Fact]
        public async Task GetById_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _productRepository.GetProductById(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Create_ShouldAddNewIdToProduct_WhenSavingToDatabase()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int expectedNewId = 1;
            Product product = new() { ProductID = expectedNewId, Name = "Tea", Price = 50, Description = "Bubble Tea", CategoryId = 1 };

            // Act
            var result = await _productRepository.CreateProduct(product);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(expectedNewId, result.ProductID);
        }

        [Fact]
        public async Task Create_ShouldFailToAddProduct_WhenProductIdAlreadyExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            Product product = new() { ProductID = 1, Name = "Tea", Price = 50, Description = "Bubble Tea", CategoryId = 1 };
            await _productRepository.CreateProduct(product);

            // Act
            async Task action() => await _productRepository.CreateProduct(product);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async Task Update_ShouldChangeValuesOnProduct_WhenProductExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int productId = 1;
            _context.Product.Add(new Product { ProductID = productId, Name = "Tea", Price = 50, Description = "Bubble Tea", CategoryId = 1 });
            await _context.SaveChangesAsync();
            Product updatedProduct = new() { ProductID = productId, Name = "Updated Tea", Price = 60, Description = "Updated Bubble Tea", CategoryId = 1 };

            // Act
            var result = await _productRepository.UpdateProduct(updatedProduct, productId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(updatedProduct.Name, result.Name);
            Assert.Equal(updatedProduct.Price, result.Price);
        }

        [Fact]
        public async Task Update_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int productId = 1;
            Product updateProduct = new() { ProductID = productId, Name = "Updated Tea", Price = 60, Description = "Updated Bubble Tea", CategoryId = 1 };

            // Act
            var result = await _productRepository.UpdateProduct(updateProduct, productId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnDeletedProduct_WhenProductIsDeleted()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int productId = 1;
            Product product = new() { ProductID = productId, Name = "Tea", Price = 50, Description = "Bubble Tea", CategoryId = 1 };
            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            // Act
            var result = await _productRepository.DeleteProduct(productId);
            var deletedProduct = await _productRepository.GetProductById(productId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(productId, result.ProductID);
            Assert.Null(deletedProduct);
        }

        [Fact]
        public async Task Delete_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _productRepository.DeleteProduct(1);

            // Assert
            Assert.Null(result);
        }
    }
}
