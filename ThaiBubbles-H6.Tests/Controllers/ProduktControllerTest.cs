using ThaiBubbles_H6.Model; //I don't know where this is from :(

namespace ThaiBubbles_H6.Tests.Controllers
{
    public class ProduktControllerTest
    {
        private readonly ProductController _productController;
        private readonly Mock<IProductRepositories> _productRepository = new();

        public ProduktControllerTest()
        {
            _productController = new ProductController(_productRepository.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenProductsExists()
        {
            // Arrange
            List<Product> products = new()
            {
                new Product
                {
                    ProductID = 1,
                    Name = "TestProduct1",
                    Price = 100,
                    Description = "TestDescription1",
                    CategoryId = 1,
                    category = new Category { CategoryID = 1, CategoryName = "TestCategory1" },
                    orderlists = new List<ProductList>()
                }
            };

            _productRepository.Setup(x => x.GetAllProducts()).ReturnsAsync(products);

            // Act
            var result = (IStatusCodeActionResult)await _productController.GetProducts();

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_NoProductsExists()
        {
            // Arrange
            List<Product> products = new();
            _productRepository.Setup(x => x.GetAllProducts()).ReturnsAsync(products);

            // Act
            var result = (IStatusCodeActionResult)await _productController.GetProducts();

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            _productRepository.Setup(x => x.GetAllProducts()).ThrowsAsync(new Exception("Error"));

            // Act
            var result = (IStatusCodeActionResult)await _productController.GetProducts();

            // Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode200_WhenProductExists()
        {
            // Arrange
            int productId = 1;
            var product = new Product
            {
                ProductID = productId,
                Name = "TestProduct1",
                Price = 100,
                Description = "TestDescription1",
                CategoryId = 1,
                category = new Category { CategoryID = 1, CategoryName = "TestCategory1" }
            };

            _productRepository.Setup(x => x.GetProductById(productId)).ReturnsAsync(product);

            // Act
            var result = (IStatusCodeActionResult)await _productController.GetProductById(productId);

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhenProductDoesNotExist()
        {
            // Arrange
            int productId = 1;
            _productRepository.Setup(x => x.GetProductById(productId)).ReturnsAsync((Product)null);

            // Act
            var result = (IStatusCodeActionResult)await _productController.GetProductById(productId);

            // Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode201_WhenProductIsCreated()
        {
            // Arrange
            var newProduct = new Product
            {
                ProductID = 1,
                Name = "TestProduct1",
                Price = 100,
                Description = "TestDescription1",
                CategoryId = 1
            };

            _productRepository.Setup(x => x.CreateProduct(It.IsAny<Product>())).ReturnsAsync(newProduct);

            // Act
            var result = await _productController.PostProduct(newProduct);

            // Assert
            var statusCodeResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(201, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            var newProduct = new Product
            {
                ProductID = 1,
                Name = "TestProduct1",
                Price = 100,
                Description = "TestDescription1",
                CategoryId = 1
            };

            _productRepository.Setup(x => x.CreateProduct(It.IsAny<Product>())).ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _productController.PostProduct(newProduct);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenProductIsUpdated()
        {
            // Arrange
            int productId = 1;
            var updatedProduct = new Product
            {
                ProductID = productId,
                Name = "UpdatedProduct",
                Price = 200,
                Description = "UpdatedDescription",
                CategoryId = 2
            };

            _productRepository.Setup(x => x.UpdateProduct(It.IsAny<Product>(), productId)).ReturnsAsync(updatedProduct);

            // Act
            var result = (IStatusCodeActionResult)await _productController.PutProduct(productId, updatedProduct);

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int productId = 1;
            var updatedProduct = new Product
            {
                ProductID = productId,
                Name = "UpdatedProduct",
                Price = 200,
                Description = "UpdatedDescription",
                CategoryId = 2
            };

            _productRepository.Setup(x => x.UpdateProduct(It.IsAny<Product>(), productId)).ThrowsAsync(new Exception("Error"));

            // Act
            var result = (IStatusCodeActionResult)await _productController.PutProduct(productId, updatedProduct);

            // Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode200_WhenProductIsDeleted()
        {
            // Arrange
            int productId = 1;
            var product = new Product
            {
                ProductID = productId,
                Name = "ProductToDelete",
                Price = 150,
                Description = "Description"
            };

            _productRepository.Setup(x => x.DeleteProduct(productId)).ReturnsAsync(product);

            // Act
            var result = (IStatusCodeActionResult)await _productController.DeleteProduct(productId);

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode404_WhenProductDoesNotExist()
        {
            // Arrange
            int productId = 1;
            _productRepository.Setup(x => x.DeleteProduct(productId)).ReturnsAsync((Product)null);

            // Act
            var result = (IStatusCodeActionResult)await _productController.DeleteProduct(productId);

            // Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int productId = 1;
            _productRepository.Setup(x => x.DeleteProduct(productId)).ThrowsAsync(new Exception("Error"));

            // Act
            var result = (IStatusCodeActionResult)await _productController.DeleteProduct(productId);

            // Assert
            Assert.Equal(500, result.StatusCode);
        }
    }
}
