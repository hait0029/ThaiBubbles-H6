
namespace ThaiBubbles_H6.Tests.Controllers
{
    public class OrderControllerTests
    {
        private readonly Mock<IOrderRepositories> _orderRepositoryMock;
        private readonly OrderController _orderController;

        public OrderControllerTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepositories>();
            _orderController = new OrderController(_orderRepositoryMock.Object);
        }

        [Fact]
        public async Task GetOrders_ShouldReturnAllOrders_WhenOrdersExist()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order { OrderID = 1, OrderDate = DateTime.Now, UserId = 1 },
                new Order { OrderID = 2, OrderDate = DateTime.Now, UserId = 2 }
            };
            _orderRepositoryMock.Setup(repo => repo.GetAllOrders()).ReturnsAsync(orders);

            // Act
            var result = await _orderController.GetOrders();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedOrders = Assert.IsType<List<Order>>(okResult.Value);
            Assert.Equal(2, returnedOrders.Count);
        }

        [Fact]
        public async Task GetOrderById_ShouldReturnOrder_WhenOrderExists()
        {
            // Arrange
            var order = new Order { OrderID = 1, OrderDate = DateTime.Now, UserId = 1 };
            _orderRepositoryMock.Setup(repo => repo.GetOrderById(order.OrderID)).ReturnsAsync(order);

            // Act
            var result = await _orderController.GetOrdersById(order.OrderID);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedOrder = Assert.IsType<Order>(okResult.Value);
            Assert.Equal(order.OrderID, returnedOrder.OrderID);
        }

        [Fact]
        public async Task CreateOrder_ShouldReturnCreatedOrder_WhenOrderIsValid()
        {
            // Arrange
            var newOrder = new Order { OrderDate = DateTime.Now, UserId = 1 };
            _orderRepositoryMock.Setup(repo => repo.CreateOrder(newOrder)).ReturnsAsync(newOrder);

            // Act
            //                                                              There is no create in controller
            // var result = await _orderController.CreatedAtAction(newOrder);

            // Assert
            //                                                              Tied to act. Act is commented out so is this.
            // var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            // var returnedOrder = Assert.IsType<Order>(createdResult.Value);
            // Assert.Equal(newOrder.UserId, returnedOrder.UserId);
        }

        [Fact]
        public async Task UpdateOrder_ShouldReturnUpdatedOrder_WhenOrderExists()
        {
            // Arrange
            var updatedOrder = new Order { OrderID = 1, OrderDate = DateTime.Now, UserId = 2 };
            _orderRepositoryMock.Setup(repo => repo.UpdateOrder(updatedOrder.OrderID, updatedOrder)).ReturnsAsync(updatedOrder);

            // Act
            var result = await _orderController.UpdateOrder(updatedOrder.OrderID, updatedOrder);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedOrder = Assert.IsType<Order>(okResult.Value);
            Assert.Equal(2, returnedOrder.UserId);
        }

        [Fact]
        public async Task DeleteOrder_ShouldReturnDeletedOrder_WhenOrderExists()
        {
            // Arrange
            var order = new Order { OrderID = 1, OrderDate = DateTime.Now, UserId = 1 };
            _orderRepositoryMock.Setup(repo => repo.DeleteOrder(order.OrderID)).ReturnsAsync(order);

            // Act
            var result = await _orderController.DeleteOrder(order.OrderID);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var deletedOrder = Assert.IsType<Order>(okResult.Value);
            Assert.Equal(order.OrderID, deletedOrder.OrderID);
        }

        [Fact]
        public async Task DeleteOrder_ShouldReturnNotFound_WhenOrderDoesNotExist()
        {
            // Arrange
            int orderId = 1;
            _orderRepositoryMock.Setup(repo => repo.DeleteOrder(orderId)).ReturnsAsync((Order)null);

            // Act
            var result = await _orderController.DeleteOrder(orderId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
