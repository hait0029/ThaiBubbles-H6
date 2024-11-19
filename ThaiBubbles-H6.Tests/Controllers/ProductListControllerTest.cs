using ThaiBubbles_H6.Model;

namespace ThaiBubbles_H6.Tests.Controllers
{
    public class ProductListControllerTests
    {
        private readonly Mock<IProductListRepositories> _repositoryMock;
        private readonly ProductListController _controller;

        public ProductListControllerTests()
        {
            _repositoryMock = new Mock<IProductListRepositories>();
            _controller = new ProductListController(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetProductLists_ShouldReturnOk_WhenProductListsExist()
        {
            // Arrange
            var productLists = new List<ProductList>
            {
                new ProductList { ProductOrderListID = 1, Quantity = 10 },
                new ProductList { ProductOrderListID = 2, Quantity = 20 }
            };
            _repositoryMock.Setup(repo => repo.GetProductOrderList()).ReturnsAsync(productLists);

            // Act
            var result = await _controller.GetProductLists();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProductLists = Assert.IsType<List<ProductList>>(okResult.Value);
            Assert.Equal(2, returnedProductLists.Count);
        }

        [Fact]
        public async Task GetProductstById_ShouldReturnOk_WhenProductListExists()
        {
            // Arrange
            var productList = new ProductList { ProductOrderListID = 1, Quantity = 15 };
            _repositoryMock.Setup(repo => repo.GetProductOrderListById(1)).ReturnsAsync(productList);

            // Act
            var result = await _controller.GetProductstById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProductList = Assert.IsType<ProductList>(okResult.Value);
            Assert.Equal(15, returnedProductList.Quantity);
        }

        [Fact]
        public async Task PutProductList_ShouldReturnNotFound_WhenProductListDoesNotExist()
        {
            // Arrange
            var updatedProductList = new ProductList { Quantity = 50 };
            _repositoryMock.Setup(repo => repo.UpdateProductOrderList(1, updatedProductList)).ReturnsAsync((ProductList)null);

            // Act
            var result = await _controller.PutProductList(1, updatedProductList);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task PostProductList_ShouldReturnCreated_WhenProductListIsValid()
        {
            // Arrange
            var newProductList = new ProductList { Quantity = 20 };
            _repositoryMock.Setup(repo => repo.CreateProductOrderList(newProductList)).ReturnsAsync(newProductList);

            // Act
            var result = await _controller.PostProductList(newProductList);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnedProductList = Assert.IsType<ProductList>(createdResult.Value);
            Assert.Equal(20, returnedProductList.Quantity);
        }

        [Fact]
        public async Task DeleteProductList_ShouldReturnOk_WhenProductListExists()
        {
            // Arrange
            var productList = new ProductList { ProductOrderListID = 1, Quantity = 30 };
            _repositoryMock.Setup(repo => repo.DeleteProductOrderList(1)).ReturnsAsync(productList);

            // Act
            var result = await _controller.DeleteProductList(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProductList = Assert.IsType<ProductList>(okResult.Value);
            Assert.Equal(30, returnedProductList.Quantity);
        }

        [Fact]
        public async Task DeleteProductList_ShouldReturnNotFound_WhenProductListDoesNotExist()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.DeleteProductOrderList(1)).ReturnsAsync((ProductList)null);

            // Act
            var result = await _controller.DeleteProductList(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
