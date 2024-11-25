using ThaiBubbles_H6.Model;

namespace ThaiBubbles_H6.Tests.Repositories
{
    public class ProductListRepositoriesTests
    {
        private DbContextOptions<DatabaseContext> _options;
        private DatabaseContext _context;
        private ProductListRepositories _repository;

        public ProductListRepositoriesTests()
        {
            // Set up the in-memory database
            _options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "ProductListRepositoryTests")
                .Options;

            _context = new DatabaseContext(_options);
            _repository = new ProductListRepositories(_context);
        }

        [Fact]
        public async Task CreateProductOrderList_ShouldAddProductList_WhenValid()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();  // Clear the database before the test
            var productList = new ProductList { Quantity = 5, OrderId = null, ProductId = null };

            // Act
            var result = await _repository.CreateProductOrderList(productList);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(5, result.Quantity);
            Assert.Single(_context.ProductList);
        }

        [Fact]
        public async Task GetProductOrderList_ShouldReturnAllProductLists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            _context.ProductList.Add(new ProductList { Quantity = 1 });
            _context.ProductList.Add(new ProductList { Quantity = 2 });
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetProductOrderList();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetProductOrderListById_ShouldReturnProductList_WhenExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            var productList = new ProductList { ProductOrderListID = 1, Quantity = 3 };
            _context.ProductList.Add(productList);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetProductOrderListById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Quantity);
        }

        [Fact]
        public async Task UpdateProductOrderList_ShouldModifyProductList_WhenExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            var productList = new ProductList { ProductOrderListID = 1, Quantity = 3 };
            _context.ProductList.Add(productList);
            await _context.SaveChangesAsync();

            var updatedProductList = new ProductList { Quantity = 10 };

            // Act
            var result = await _repository.UpdateProductOrderList(1, updatedProductList);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(10, result.Quantity);
        }

        [Fact]
        public async Task DeleteProductOrderList_ShouldRemoveProductList_WhenExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            var productList = new ProductList { ProductOrderListID = 1, Quantity = 5 };
            _context.ProductList.Add(productList);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.DeleteProductOrderList(1);
            var deletedProductList = await _repository.GetProductOrderListById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Null(deletedProductList);
        }
    }
}
