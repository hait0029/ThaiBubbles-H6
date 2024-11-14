
using ThaiBubbles_h6.Model;

namespace ThaiBubbles_H6.Tests.Controllers
{
    public class ProduktControllerTest
    {
        private readonly ProductController _productController;
        private Mock<IProductRepositories> _productRepository = new();

        public ProduktControllerTest()
        {
            _productController = new ProductController(_productRepository.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenProductsExists()
        {
            // Arrange
            List<Product> products = new();
            
            products.Add(new Product
            { 
                ProductID = 1,
                Name = "TestProduct1",
                Price = 100,
                Description = "TestDescription1",
                CategoryId = 1,
                category = new Category { CategoryID = 1, CategoryName = "TestCategory1" },
                orderlists = new List<ProductList>()
            });

            _productRepository.Setup(x => x.GetAllProducts()).ReturnsAsync(products);

            // Act

            var result = (IStatusCodeActionResult)await _productController.GetProducts();

            // Assert

            Assert.Equal(200, result.StatusCode);
        }
    }
}
